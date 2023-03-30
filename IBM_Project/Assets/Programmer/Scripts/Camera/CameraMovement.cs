using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameController gC;
    private GameObject player;

    private void Start()
    {
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        //Debug.Log(gC.name);
        if (gC.mC.inMaze)
        {
            Vector3 temp = new Vector3(20.0f, gameObject.transform.position.y, 20.0f);
            gameObject.transform.position = temp;
            gameObject.GetComponent<Camera>().orthographicSize = 25.0f;
        }
        else if(gC.mC.inDoor)
        {
            Vector3 temp = new Vector3(0.5f, 1.5f, 25.0f);
            gameObject.transform.position = temp;

            Quaternion rotTemp = Quaternion.Euler(180.0f, 0.0f, 0.0f);
            gameObject.transform.rotation = rotTemp;
            gameObject.GetComponent<Camera>().orthographicSize = 8.0f;
        }
        else
        {
            if (!player) return;
            Vector3 temp = new Vector3(player.transform.position.x, gameObject.transform.position.y,
                player.transform.position.z);
            gameObject.transform.position = temp;
            Quaternion rotTemp = Quaternion.Euler(90.0f, 0.0f, 0.0f);
            gameObject.transform.rotation = rotTemp;
            gameObject.GetComponent<Camera>().orthographicSize = 5.0f;
        }

    }
}
