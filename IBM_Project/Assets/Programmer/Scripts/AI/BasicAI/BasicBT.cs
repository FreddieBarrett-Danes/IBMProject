using System.Collections.Generic;
using BT;
using UnityEngine.AI;

public class BasicBT : BT_Tree
{
    protected override BT_Node SetupTree()
    {
        BT_Node root = new BT_Selector(new List<BT_Node>
        {
            new BT_Decorator(new DHuntedState(GetComponent<BotInfo>()), 
                new BT_Sequence(new List<BT_Node>
                { 
                    new TPathToPlayer(GetComponent<NavMeshAgent>(),GetComponent<BotInfo>()),
                    new TRangedAttack(GetComponent<NavMeshAgent>(), GetComponent<BotInfo>()) 
                })),
            new BT_Decorator(new DRemainingBots(GetComponent<BotInfo>()), new TSuspicious(GetComponent<NavMeshAgent>(),GetComponent<BotInfo>(),GetComponent<Perception>())),
            new BT_Sequence(new List<BT_Node>
            {
                new TDetectPlayer(GetComponent<BotInfo>(), GetComponent<Perception>()),
                new TPathToPlayer(GetComponent<NavMeshAgent>(),GetComponent<BotInfo>()),
                new TRangedAttack(GetComponent<NavMeshAgent>(), GetComponent<BotInfo>())
            }),
            new BT_Decorator(new DTimeSense(GetComponent<BotInfo>(), GetComponent<Perception>()), new TSuspicious(GetComponent<NavMeshAgent>(),GetComponent<BotInfo>(),GetComponent<Perception>())),
            new BT_Decorator(new DRecentChase(GetComponent<BotInfo>()), new TPatrol(GetComponent<NavMeshAgent>(),GetComponent<BotInfo>()))
        });

        return root;
    }
}
