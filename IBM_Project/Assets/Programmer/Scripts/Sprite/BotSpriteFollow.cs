using Unity.VisualScripting;
using UnityEngine;

public class BotSpriteFollow : MonoBehaviour
{
    public GameObject ToFollow;
    private void Update()
    {
        transform.position = ToFollow.transform.position;
    }
}
