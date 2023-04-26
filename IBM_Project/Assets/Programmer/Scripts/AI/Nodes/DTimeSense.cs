using System;
using BT;

public class DTimeSense : BT_Node
{
    private readonly BotInfo bot;
    private readonly Perception percep;

    public DTimeSense(BotInfo pInput, Perception pPerception)
    {
        bot = pInput;
        percep = pPerception;
    }

    public override NodeState Evaluate()
    {
        if (bot.bComputer.GetComponent<ComputerInteraction>().mazeFailed)
        {
            bot.bGameControl.playerStatus = GameController.Status.ALERTED;
            state = NodeState.SUCCESS;
            return state;
        }
        if (!bot.bRecentlyChase)
        {
            if (bot.bDetectionTimer >= bot.bDetectionTimer / 2)
            {
                DateTime now = DateTime.Now;
                if (percep.sensedRecord.Length != 0)
                {
                    if (!bot.bPlayerInView &&
                        percep.sensedRecord[0].timeLastSensed > now.Subtract(new TimeSpan(0, 0, bot.bSearchTime)))
                    {
                        bot.bGameControl.playerStatus = GameController.Status.ALERTED;
                        state = NodeState.SUCCESS;
                        return state;
                    }

                    state = NodeState.FAILURE;
                    return state;
                }
                state = NodeState.FAILURE;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}