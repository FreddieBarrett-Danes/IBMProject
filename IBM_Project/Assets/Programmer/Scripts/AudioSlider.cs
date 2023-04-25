using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [Range(0f, 1f), SerializeField]
    private float volume, lastFrame;
    public GameObject prefabToSpawn;

    void OnGUI()
    {
        volume = GetComponent<Slider>().value;
        AudioListener.volume = volume/10;

        if(volume != lastFrame)
        {
            GameObject audio = Instantiate(prefabToSpawn, transform.position, transform.rotation);
            Destroy(audio, 3f);
        }

        lastFrame = volume;
    }
}
