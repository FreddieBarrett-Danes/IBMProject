using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Ability
{
    public Transform tempAtkPoint;
    
    public float attackRange = 0.1f;


    public override void Execute(LayerMask targetLayer)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(tempAtkPoint.position, attackRange, targetLayer);

        foreach(Collider enemy in hitEnemies)
        {
            Debug.Log("Enemy hit");
            
        }
    }
}
