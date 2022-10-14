using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : Ability
{
    public Ability ability;
    
    public float bulletSpeed;
    public GameObject bullet;
    public override void Execute()
    {
        GameObject tempBullet = Instantiate(bullet, host.transform.position, host.transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(host.transform.forward * bulletSpeed);
        Destroy(tempBullet, 5f);
    }
}
