using UnityEngine;

public class BotSpriteFollow : MonoBehaviour
{
    private void Update()
    {
        transform.position = transform.parent.GetChild(0).transform.position;
    }
}
