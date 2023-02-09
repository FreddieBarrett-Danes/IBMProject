using UnityEngine;

public class BulletConditions : MonoBehaviour
{
    private GameObject[] enemies;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    private void OnTriggerEnter(Collider other)
    {
       if(gameObject.CompareTag("Player"))
       {
           foreach(GameObject enemy in enemies)
           {
               if (other.gameObject == enemy)
               {

                   Destroy(other.gameObject);
                   Destroy(gameObject);
               }
               else if (other.gameObject.tag == "Wall")
               {
                   Destroy(gameObject);
               }
           }
            

       }
       else if (gameObject.CompareTag("Enemy"))
       {
           if (other.gameObject.tag == "Player")
           {
                Destroy(other.gameObject);
                Destroy(gameObject);
           }
           else if (other.gameObject.tag == "Wall")
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


