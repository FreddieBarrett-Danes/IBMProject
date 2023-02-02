using TMPro;
using UnityEngine;

public class PlayerBehind : MonoBehaviour
{
    private GameObject player;
    private bool playerClose = false;
    private PlayerController pC;

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pC = player.GetComponent<PlayerController>();
    }
    void Update()
    {
        if(playerClose)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = player.transform.position - transform.position;
            if (Vector3.Dot(forward, toOther) < 0)
            {
                pC.isBehindEnemy = true;
                //this needs to c;heck if there are more than one enemy
                pC.enemyControlled = this.gameObject;
            }
            else
            {
                pC.isBehindEnemy = false;
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            playerClose = true;
            player.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            //Debug.Log("exiting collider");
            playerClose = false;
            player.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
