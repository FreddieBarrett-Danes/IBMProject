using BT;

public class DBoolCheckInverted : BT_Node
{
    private readonly BotInfo bot;

    public DBoolCheckInverted(BotInfo pInput)
    {
        bot = pInput;
    }

    public override NodeState Evaluate()
    {
        switch (bot.bPlayerInView)
        {
            case true:
                state = NodeState.FAILURE;
                return state;
            case false:
                state = NodeState.SUCCESS;
                return state;
        }
    }
}