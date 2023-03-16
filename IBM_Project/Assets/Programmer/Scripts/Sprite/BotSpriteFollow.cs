using Unity.VisualScripting;
using UnityEngine;

public class BotSpriteFollow : MonoBehaviour
{
    public GameObject ToFollow;
    private void Update()
    {
        if(ToFollow)
            transform.position = ToFollow.transform.position;
    }
}
