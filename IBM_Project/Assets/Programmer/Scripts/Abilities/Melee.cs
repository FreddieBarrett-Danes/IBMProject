using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Ability
{
    public Transform tempAtkPoint;
    
    public float attackRange = 0.1f;
    public LayerMask enemyLayer;


    public override void Execute()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(tempAtkPoint.position, attackRange, enemyLayer);

        foreach(Collider enemy in hitEnemies)
        {
            Debug.Log("Enemy hit");
            
        }
    }
}
