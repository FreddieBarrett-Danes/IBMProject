using BT;

public class DVariableThreshold : BT_Node
{
    private readonly BotInfo bot;

    public DVariableThreshold(BotInfo pinput)
    {
        bot = pinput;
    }

    public override NodeState Evaluate()
    {
        switch (bot.remainingBots < bot.botCount / 2)
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