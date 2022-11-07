using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletConditions : MonoBehaviour
{
    private GameObject[] enemies;
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    private void OnTriggerEnter(Collider other)
    {
       if(this.gameObject.tag == "Player")
       {
            Debug.Log("player shot");
            foreach(GameObject enemy in enemies)
            {
                if (other.gameObject == enemy)
                {

                    Destroy(other.gameObject);
                    Destroy(this.gameObject);
                }
                else if (other.gameObject == GameObject.FindGameObjectWithTag("Wall"))
                {
                    Destroy(this.gameObject);
                }
            }
            

       }
       else if (this.gameObject.tag == "Enemy")
       {
            Debug.Log("enemy shot");
            if (other.gameObject == GameObject.FindGameObjectWithTag("Player"))
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
            else if (other.gameObject == GameObject.FindGameObjectWithTag("Wall"))
            {
                Destroy(this.gameObject);
            }
       }
        
        /*else
        {
            Destroy(this.gameObject);
        }*/
    }
}


