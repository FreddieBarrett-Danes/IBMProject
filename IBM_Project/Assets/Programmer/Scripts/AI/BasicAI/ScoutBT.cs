using System.Collections.Generic;
using BT;
using UnityEngine.AI;

public class ScoutBT : BT_Tree
{
    protected override BT_Node SetupTree()
    {
        BT_Node root = new BT_Sequence(new List<BT_Node>
        {
            new BT_Decorator(new DSuspiciousStateCheck(GetComponent<BotInfo>()), 
                new DViewChange(GetComponent<BotInfo>())),
            new TPatrol(GetComponent<NavMeshAgent>(), GetComponent<BotInfo>()),
        });

        return root;
    }
}