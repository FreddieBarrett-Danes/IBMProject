using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameController gC;
    private GameObject player;

    private void Start()
    {
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (gC.mC.inMaze)
        {
            Vector3 temp = new Vector3(20.0f, gameObject.transform.position.y, 20.0f);
            gameObject.transform.position = temp;
            gameObject.GetComponent<Camera>().orthographicSize = 25.0f;
        }
        else if(gC.mC.inDoor)
        {

        }
        else
        {
            if (!player) return;
            Vector3 temp = new Vector3(player.transform.position.x, gameObject.transform.position.y,
                player.transform.position.z);
            gameObject.transform.position = temp;
            gameObject.GetComponent<Camera>().orthographicSize = 5.0f;
        }

    }
}
