using UnityEngine;

public class PlayerBehind : MonoBehaviour
{
    private GameObject player;

    private PlayerController pC;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pC = player.GetComponent<PlayerController>();
    }
    void Update()
    {

    }


    private void OnTriggerStay(Collider other)
    {

        if (other == player.GetComponent<CapsuleCollider>())
        {
            if (other != null)
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
    }

    private void OnTriggerExit(Collider other)
    {
        if(other == player.GetComponent<CapsuleCollider>())
        {
            pC.isBehindEnemy = false;
        }
    }
}
