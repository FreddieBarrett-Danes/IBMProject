using BT;

public class DRemainingBots : BT_Node
{
    private readonly BotInfo bot;

    public DRemainingBots(BotInfo pinput)
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