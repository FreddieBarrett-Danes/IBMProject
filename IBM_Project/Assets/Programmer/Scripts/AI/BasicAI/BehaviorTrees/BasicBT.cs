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
                new DetectPlayer(GetComponent<NavMeshAgent>(), transform),
                new PathToPlayer(GetComponent<NavMeshAgent>()),
            }),
            new TWander(GetComponent<NavMeshAgent>(), transform),
        });

        return root;
    }
}
