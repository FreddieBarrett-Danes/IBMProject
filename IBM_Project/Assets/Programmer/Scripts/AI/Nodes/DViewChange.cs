using System;
using BT;

public class DViewChange : BT_Node
{
    private readonly BotInfo bot;

    public DViewChange(BotInfo pInput)
    {
        bot = pInput;
    }

    public override NodeState Evaluate()
    {
        bot.bViewRadius = bot.bSusViewRadius;
        bot.bInnerViewRadius = bot.bSusInnerViewRadius;
        state = NodeState.SUCCESS;
        return state;
    }
}