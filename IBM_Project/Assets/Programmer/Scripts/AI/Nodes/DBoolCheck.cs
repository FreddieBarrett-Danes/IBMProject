using BT;
using UnityEngine;

public class DBoolCheck : BT_Node
{
    private readonly BotInfo bot;

    public DBoolCheck(BotInfo pinput)
    {
        bot = pinput;
    }

    public override NodeState Evaluate()
    {
        switch (bot.playerInView)
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