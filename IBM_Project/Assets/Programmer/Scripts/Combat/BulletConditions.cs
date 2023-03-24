using UnityEngine;

public class BulletConditions : MonoBehaviour
{
    private GameObject[] enemies;
    private AudioSource breakbox;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("EnemyScript");
        breakbox = gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
       if(gameObject.CompareTag("Player"))
       {
           foreach(GameObject enemy in enemies)
           {
               if (other.gameObject == enemy)
               {

                   other.GetComponent<BotInfo>().bIsDead = true;
                   Destroy(gameObject);
               }
               else if (other.gameObject.CompareTag("Wall"))
               {
                    Destroy(gameObject);

               }
               else if(other.gameObject.CompareTag("BreakableBox"))
               {

                    Destroy(other.gameObject);
                    breakbox.enabled = true;

                    
                    //Destroy(gameObject);
               }
            }
            

       }
       else if (gameObject.CompareTag("Enemy"))
       {
           if (other.gameObject.tag == "Player")
           {
                //reduce level timer
                Destroy(other.gameObject);
                Destroy(gameObject);
           }
           else if (other.gameObject.CompareTag("Wall"))
           {
                Debug.Log("wall hit");
                Destroy(gameObject);
           }
       }
        
        /*else
        {
            Destroy(this.gameObject);
        }*/
    }
}


