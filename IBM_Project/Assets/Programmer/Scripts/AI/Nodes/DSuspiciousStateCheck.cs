using System;
using BT;

public class DSuspiciousStateCheck : BT_Node
{
    private readonly BotInfo bot;

    public DSuspiciousStateCheck(BotInfo pInput)
    {
        bot = pInput;
    }

    public override NodeState Evaluate()
    {
        switch (bot.bGameControl.PlayerStatus)
        {
            case GameController.Status.HUNTED:
                state = NodeState.SUCCESS;
                return state;
            case GameController.Status.ALERTED:
                state = NodeState.SUCCESS;
                return state;
            case GameController.Status.SAFE:
                state = NodeState.FAILURE;
                return state;
            default:
                state = NodeState.FAILURE;
                return state;
        }
    }
}