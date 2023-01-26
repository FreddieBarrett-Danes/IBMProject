using BT;

public class DRecentChase : BT_Node
{
    private readonly BotInfo bot;

    public DRecentChase(BotInfo pInput)
    {
        bot = pInput;
    }

    public override NodeState Evaluate()
    {
        if (!bot.bRecentlyChase)
        {
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}