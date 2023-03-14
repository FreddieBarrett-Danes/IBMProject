using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpriteFollow : MonoBehaviour
{
    private void Update()
    {
        if (FindObjectOfType(typeof(PlayerController)).GameObject())
        {
            transform.position = FindObjectOfType(typeof(PlayerController)).GameObject().transform.position;
        }
        else
        {
            Destroy(this);
        }
    }
}
