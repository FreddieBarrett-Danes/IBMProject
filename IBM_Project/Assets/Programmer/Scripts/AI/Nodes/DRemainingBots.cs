using BT;

public class DRemainingBots : BT_Node
{
    private readonly BotInfo bot;

    public DRemainingBots(BotInfo pInput)
    {
        bot = pInput;
    }

    public override NodeState Evaluate()
    {
        switch (bot.bBotCount != 1 && bot.bRemainingBots <= bot.bBotCount / 2)
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