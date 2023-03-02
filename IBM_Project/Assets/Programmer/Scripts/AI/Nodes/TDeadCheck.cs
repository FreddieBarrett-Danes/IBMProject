using BT;

public class TDeadCheck : BT_Node
{
    private readonly BotInfo botInfo;

    public TDeadCheck(BotInfo pBotInfo)
    {
        botInfo = pBotInfo;
    }

    public override NodeState Evaluate()
    {
        if (botInfo.bIsDead)
        {
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }

}
