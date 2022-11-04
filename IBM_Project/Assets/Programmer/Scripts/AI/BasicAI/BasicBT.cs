using System.Collections.Generic;
using UnityEngine.AI;
using BT;

public class BasicBT : BT_Tree
{
    protected override BT_Node SetupTree()
    {
        BT_Node root = new BT_Selector(new List<BT_Node>
        {
            new BT_Sequence(new List<BT_Node>
            {
                new TDetectPlayer(GetComponent<NavMeshAgent>(), transform, GetComponent<BotInfo>()),
                new TPathToPlayer(GetComponent<NavMeshAgent>(), GetComponent<BotInfo>()),
                new BT_Decorator(new DBoolCheckInverted(GetComponent<BotInfo>())),
                new TSuspicious(GetComponent<NavMeshAgent>(), GetComponent<BotInfo>()),
            }),
            new TPatrol(GetComponent<NavMeshAgent>(), GetComponent<BotInfo>())
            //new TWander(GetComponent<NavMeshAgent>(), transform, GetComponent<BotInfo>())
        });

        return root;
    }
}
