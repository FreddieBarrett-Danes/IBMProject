using BT;
using UnityEngine.AI;

public class ScoutBT : BT_Tree
{
    protected override BT_Node SetupTree()
    {
        BT_Node root =
            new BT_Decorator(new DRecentChase(GetComponent<BotInfo>()),
                new TPatrol(GetComponent<NavMeshAgent>(), GetComponent<BotInfo>()));

        return root;
    }
}