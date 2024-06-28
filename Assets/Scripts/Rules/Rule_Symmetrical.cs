using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule_Symmetrical : Rule
{
    [SerializeField] SymmetricalType symmetricalType;

    public override bool IsAchieved()
    {
        isAchieved = true;

        for (int idx1 = 0; idx1 < SymbolController.symbolCount; idx1++)
        {
            foreach (int idx2 in SymbolController.GetPaths[idx1])
            {

                if (!SymbolController.GetMap[Swap(idx1, symmetricalType)][Swap(idx2, symmetricalType)])
                {
                    isAchieved = false;
                    break;
                }
            }
            if (!isAchieved) break;
        }

        if (SymbolController.GetHistory.Count < 2) isAchieved = inverse;

        return base.IsAchieved();
    }

    static int Swap(int index, SymmetricalType mode)
    {
        if (mode == SymmetricalType.Horizontal) return (6 - index) % 6;
        if (mode == SymmetricalType.Vertical) return (9 - index) % 6;
        if (mode == SymmetricalType.Point) return (3 + index) % 6;
        return -1;
    }
}