using UnityEngine;
using System.Collections.Generic;

public class ImageScrolling : MonoBehaviour
{
    public GameObject imagePrefab;
    public float scrollSpeed;
    public float scaleFactor = 1f;

    private int numImages;
    private float imageWidth;
    private float imageHeight;
    private float canvasWidth;
    private float canvasHeight;
    private List<Transform> images = new List<Transform>();

    void Start()
    {
        // Step 1: Spawn the image prefab and get its dimensions
        GameObject tempImage = Instantiate(imagePrefab);
        imageWidth = tempImage.GetComponent<RectTransform>().rect.width;
        imageHeight = tempImage.GetComponent<RectTransform>().rect.height;
        Destroy(tempImage);

        // Step 2: Get the canvas dimensions
        canvasWidth = GetComponent<RectTransform>().rect.width;
        canvasHeight = GetComponent<RectTransform>().rect.height;

        // Step 3: Determine the number of images needed to fill the screen horizontally and vertically
        numImages = Mathf.CeilToInt(canvasWidth / imageWidth);
        int numVerticalImages = Mathf.CeilToInt(canvasHeight / imageHeight);

        // Step 4: Calculate the scale factor needed to fill the screen both horizontally and vertically
        float scaleX = canvasWidth / (numImages * imageWidth);
        float scaleY = canvasHeight / (numVerticalImages * imageHeight);
        float scale = Mathf.Min(scaleX, scaleY) * scaleFactor;

        // Step 5: Spawn the images, resize them, and lay them out side by side
        float imageScaledWidth = imageWidth * scale;
        float imageScaledHeight = imageHeight * scale;
        Vector2 spawnPos = new Vector2(-numImages * imageScaledWidth / 2f, numVerticalImages * imageScaledHeight / 2f);
        for (int i = 0; i < numImages + 5; i++)
        {
            for (int j = 0; j < numVerticalImages + 5; j++)
            {
                GameObject image = Instantiate(imagePrefab, transform);
                image.transform.localScale = new Vector3(scale , scale , 1f);
                image.transform.localPosition = spawnPos + new Vector2(i * imageScaledWidth, -j * imageScaledHeight);
                images.Add(image.transform);
            }
        }
    }

    void Update()
    {
        // Step 6: Scroll all images to the right at the specified speed
        foreach (Transform image in images)
        {
            image.localPosition += new Vector3(scrollSpeed * Time.deltaTime, 0f, 0f);

            // Step 7: If an image goes fully off screen, move it to the back of the queue
            if (image.localPosition.x > imageWidth * (numImages + 0.5f) * image.transform.localScale.x)
            {
                image.localPosition -= new Vector3(imageWidth * (numImages + 5) * image.transform.localScale.x, 0f, 0f);
            }
        }
    }
}
