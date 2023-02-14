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
        state = NodeState.FAILURE;
        return state;
    }
}