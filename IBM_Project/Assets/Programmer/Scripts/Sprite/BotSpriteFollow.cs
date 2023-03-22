using UnityEngine;

public class BotSpriteFollow : MonoBehaviour
{
    private void Update()
    {
        transform.position = transform.root.GetChild(0).transform.position;
    }
}
