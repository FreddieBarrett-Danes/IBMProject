using System.Collections.Generic;
using BT;
using UnityEngine.AI;

public class ScoutBT : BT_Tree
{
    protected override BT_Node SetupTree()
    {
        BT_Node root = new BT_Selector(new List<BT_Node>
        {
            new BT_Decorator(new TDeadCheck(GetComponent<BotInfo>()),new BT_Selector(new List<BT_Node>
            {
                new BT_Decorator(new DHuntedState(GetComponent<BotInfo>()), new THuntPatrol(GetComponent<NavMeshAgent>(), GetComponent<BotInfo>())),
                new TDetectPlayer(GetComponent<BotInfo>(), GetComponent<Perception>()),
                new BT_Decorator(new DTimeSense(GetComponent<BotInfo>(), GetComponent<Perception>()), new TScoutSus(GetComponent<NavMeshAgent>(),GetComponent<BotInfo>(),GetComponent<Perception>())),
                new TPatrol(GetComponent<NavMeshAgent>(), GetComponent<BotInfo>()),
            }))
        });

        return root;
    }
}