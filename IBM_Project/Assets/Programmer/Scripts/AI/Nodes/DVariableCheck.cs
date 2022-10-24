using BT;

public class DBoolChecks : BT_Node
{
    private readonly bool check;

    public DBoolChecks(bool input)
    {
        check = input;
    }

    public override NodeState Evaluate()
    {
        switch (check)
        {
            case true:
                state = NodeState.SUCCESS;
                return state;
            case false:
                state = NodeState.FAILURE;
                return state;
        }
    }
}