using BT;
using UnityEngine;

public class DHuntedState : BT_Node
{
    private readonly BotInfo bot;

    public DHuntedState(BotInfo pinput)
    {
        bot = pinput;
    }

    public override NodeState Evaluate()
    {
        switch (bot.bGameControl.PlayerStatus)
        {
            case GameController.Status.SAFE:
                state = NodeState.FAILURE;
                return state;
            case GameController.Status.ALERTED:
                state = NodeState.FAILURE;
                return state;
            case GameController.Status.HUNTED:
                bot.bViewCone.GetComponent<Light>().color = Color.red;
                bot.bViewRadius = bot.bSusViewRadius;
                bot.bInnerViewRadius = bot.bSusInnerViewRadius;
                state = NodeState.SUCCESS;
                return state;
            default:
                state = NodeState.FAILURE;
                return state;
        }
    }
}