using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletConditions : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
       if(this.gameObject.tag == "Player")
       {
            
            if (other.gameObject == GameObject.FindGameObjectWithTag("Enemy"))
            {
                Debug.Log("playe bullet");
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
            else if (other.gameObject == GameObject.FindGameObjectWithTag("Wall"))
            {
                Destroy(this.gameObject);
            }
       }
       else if (this.gameObject.tag == "Enemy")
       {
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


