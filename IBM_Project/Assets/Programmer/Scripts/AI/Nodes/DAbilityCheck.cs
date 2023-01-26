using UnityEngine;
using BT;

public class DAbilityCheck : BT_Node
{
    private readonly BotInfo botInfo;
    private readonly string ability;

    public DAbilityCheck(BotInfo pinput, string pAbility)
    {
        botInfo = pinput;
        ability = pAbility;
    }

    public override NodeState Evaluate()
    {
        foreach (Component t in botInfo.bAbilitiesList)
        {
            if (t.GetType().ToString() == ability)
            {
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}