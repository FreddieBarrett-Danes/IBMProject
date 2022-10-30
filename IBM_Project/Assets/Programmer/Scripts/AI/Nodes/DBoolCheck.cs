using BT;
using UnityEngine;

public class DBoolCheck : BT_Node
{
    private readonly bool check;

    public DBoolCheck(bool input)
    {
        check = input;
    }

    public override NodeState Evaluate()
    {
        Debug.Log(check);
        switch (check)
        {
            case true:
                state = NodeState.SUCCESS;
                Debug.Log("Check Success");
                return state;
            case false:
                state = NodeState.FAILURE;
                Debug.Log("Check Failure");
                return state;
        }
    }
}