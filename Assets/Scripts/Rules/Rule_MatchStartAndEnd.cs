using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule_MatchStartAndEnd : Rule
{
    public override bool IsAchieved()
    {
        var symbolHistory = SymbolController.GetHistory;

        isAchieved = symbolHistory.Count > 1 && symbolHistory[0] == symbolHistory[symbolHistory.Count - 1] || (inverse && symbolHistory.Count < 2);

        return base.IsAchieved();
    }
}
