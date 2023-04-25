using UnityEngine;

public class BulletConditions : MonoBehaviour
{
    private GameObject[] enemies;
    private GameController gC;
    public AudioSource breakBox;

    public AudioSource playerShoot;
    public AudioSource enemyShoot;
    public AudioSource wallHit;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("EnemyScript");
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (gameObject.CompareTag("Player"))
        {
            enemyShoot.enabled = false;
            playerShoot.enabled = true;
        }
        else
        {
            playerShoot.enabled = false;
            enemyShoot.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Player"))
        {
            foreach (GameObject enemy in enemies)
            {
                if (other.gameObject == enemy)
                {

                    other.GetComponent<BotInfo>().bIsDead = true;
                    Destroy(gameObject);
                }
                else if (other.gameObject.CompareTag("Wall"))
                {
                    wallHit.enabled = true;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<SphereCollider>().enabled = false;

                }
                else if (other.gameObject.CompareTag("BreakableBox"))
                {

                    Destroy(other.gameObject);
                    breakBox.enabled = true;


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
                wallHit.enabled = true;
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


