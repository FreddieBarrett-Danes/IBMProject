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
                new TDetectPlayer(GetComponent<NavMeshAgent>(), transform),
                new TPathToPlayer(GetComponent<NavMeshAgent>()),
            }),
            new TWander(GetComponent<NavMeshAgent>(), transform),
        });

        return root;
    }
}