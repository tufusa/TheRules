using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule_CountLine : Rule
{
    [SerializeField] int lineNum;
    [SerializeField] CountType countType;

    public override bool IsAchieved()
    {
        int nowLineNum = LineController.lineNum;

        if (PointerController.drawing) nowLineNum--;

        switch (countType)
        {
            case CountType.Less:
                isAchieved = nowLineNum < lineNum;
                break;
            case CountType.Equal:
                isAchieved = nowLineNum == lineNum;
                break;
            case CountType.Greater:
                isAchieved = nowLineNum > lineNum;
                break;
        }

        return base.IsAchieved();
    }
}