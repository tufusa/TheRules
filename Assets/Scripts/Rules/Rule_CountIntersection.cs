using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule_CountIntersection : Rule
{
    [SerializeField] int intersectionNum;
    [SerializeField] CountType countType;

    public override bool IsAchieved()
    {
        switch (countType)
        {
            case CountType.Less:
                isAchieved = IntersectionsController.intersectionCount < intersectionNum;
                break;
            case CountType.Equal:
                isAchieved = IntersectionsController.intersectionCount == intersectionNum;
                break;
            case CountType.Greater:
                isAchieved = IntersectionsController.intersectionCount > intersectionNum;
                break;
        }

        return base.IsAchieved();
    }
}
