using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : Ability
{   
    public float bulletSpeed;

    public override void Execute()
    {
        if (gameObject.CompareTag("Player"))
        {
            GameObject bullet = Resources.Load<GameObject>("Bullet");
            GameObject tempBullet = Instantiate(bullet, host.transform.position, host.transform.rotation);

            Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
            tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
            tempBullet.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
            tempBullet.tag = host.tag;
            //Debug.Log(tempBullet.tag + " Bullet Shot");
            Destroy(tempBullet, 5f);
        }
        else
        {
            GameObject bullet = Resources.Load<GameObject>("EnemyBullet");
            GameObject tempBullet = Instantiate(bullet, host.transform.position, host.transform.rotation);

            Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
            tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
            tempBullet.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
            tempBullet.tag = host.tag;
            //Debug.Log(tempBullet.tag + " Bullet Shot");
            Destroy(tempBullet, 5f);
        }
    }
}
