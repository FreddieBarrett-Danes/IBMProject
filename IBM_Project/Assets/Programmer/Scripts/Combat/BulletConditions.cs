using UnityEngine;

public class BulletConditions : MonoBehaviour
{
    private GameObject[] enemies;
    private GameController gC;
    public AudioSource breakbox;

    public AudioSource PlayerShoot;
    public AudioSource EnemyShoot;
    public AudioSource WallHit;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("EnemyScript");
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (gameObject.CompareTag("Player"))
        {
            EnemyShoot.enabled = false;
            PlayerShoot.enabled = true;
        }
        else
        {
            PlayerShoot.enabled = false;
            EnemyShoot.enabled = true;
        }
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
                    WallHit.enabled = true;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<SphereCollider>().enabled = false;

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
           if (other.gameObject.CompareTag("Player"))
           {
                //reduce level timer
                gC.playerHit = true;
                Destroy(gameObject);
           }
           else if (other.gameObject.CompareTag("Wall"))
           {
                WallHit.enabled = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<SphereCollider>().enabled = false;
           }
            else if (other.gameObject.CompareTag("BreakableBox"))
            {

                

                Destroy(gameObject);
            }
        }
        
 
        
        /*else
        {
            Destroy(this.gameObject);
        }*/
    }
}


