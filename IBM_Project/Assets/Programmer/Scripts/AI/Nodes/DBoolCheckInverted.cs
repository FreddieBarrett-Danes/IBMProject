using BT;

public class DBoolCheckInverted : BT_Node
{
    private readonly BotInfo bot;

    public DBoolCheckInverted(BotInfo pinput)
    {
        bot = pinput;
    }

    public override NodeState Evaluate()
    {
        switch (bot.playerInView)
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