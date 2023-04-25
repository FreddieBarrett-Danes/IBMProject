using BT;
using UnityEngine;

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
        bot.bViewCone.GetComponent<Light>().color = Color.yellow;
        state = NodeState.FAILURE;
        return state;
    }
}