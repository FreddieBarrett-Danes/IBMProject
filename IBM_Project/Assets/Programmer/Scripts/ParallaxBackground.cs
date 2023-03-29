using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxBackground : MonoBehaviour
{
    public float speed;
    public int amountOfImages; // The amount of images that are needed
    public float imageWidth;

    public Canvas canvas;

    public GameObject level1;
    public List<GameObject> close;
    //public GameObject closeContents;

    public Texture image;
    public GameObject imagePrefab;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        //imageWidth = image.GetComponent<RectTransform>().sizeDelta.x;
        imageWidth = image.width;
        amountOfImages = Mathf.CeilToInt(canvas.pixelRect.width/imageWidth) + 1;
        //Debug.Log(canvas.pixelRect.width);
        //Debug.Log(imageWidth / canvas.pixelRect.width);

        for (int i = 0; i < amountOfImages; i++)
        {
            GameObject temp = Instantiate(imagePrefab, new Vector3((-canvas.pixelRect.width / 2) + (i * imageWidth),0,0), Quaternion.identity, level1.transform);
            //Debug.Log(amountOfImages);
            close.Add(temp.GameObject());
        }

        //Texture ahhh = Instantiate(image, canvas.gameObject.GetComponent<Transform>());

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < close.Count; i++)
        {
            float additional = 0f;

            if (close[i].GetComponent<RectTransform>().localPosition.x < -imageWidth)
                additional = (amountOfImages * imageWidth);

            close[i].transform.position = new Vector3(close[i].transform.position.x - (speed * Time.deltaTime), canvas.transform.position.y, close[i].transform.position.z);
            close[i].transform.position = new Vector3(close[i].transform.position.x + additional, close[i].transform.position.y, close[i].transform.position.z);
        }
        
    }
}
