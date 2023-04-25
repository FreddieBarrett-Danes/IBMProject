using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehind : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private bool playerClose = false;
    [SerializeField]
    private PlayerController pC;

    //public TextMeshProUGUI text1;
    //public TextMeshProUGUI text2;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerController)).GameObject();
        pC = player.GetComponent<PlayerController>();
    }
    void Update()
    {
        if(playerClose)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = player.transform.position - transform.position;
            if (Vector3.Dot(forward, toOther) < 0 && GetComponent<BotInfo>().bIsDead == false)
            {
                pC.isBehindEnemy = true;
                //this needs to check if there are more than one enemy -- this is probably sorted
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
        if (other.gameObject == player)
        {
            playerClose = true;
            //player.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            //Debug.Log("exiting collider");
            playerClose = false;
            pC.isBehindEnemy = false;
            //player.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
