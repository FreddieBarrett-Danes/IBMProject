using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : Ability
{   
    public float bulletSpeed;

    public override void Execute()
    {
        GameObject bullet = Resources.Load<GameObject>("Bullet");
        GameObject tempBullet = Instantiate(bullet, host.transform.position, host.transform.rotation);
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
        Destroy(tempBullet, 5f);
    }
}
