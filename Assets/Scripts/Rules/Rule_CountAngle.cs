using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule_CountAngle : Rule
{
    [SerializeField] AngleType angleType;
    [SerializeField] int angleNum;
    [SerializeField] CountType countType;
    List<List<int>> Paths = new List<List<int>>();
    int symbolCount, nowAngleNum;

    // Start is called before the first frame update
    void Start()
    {
        symbolCount = SymbolController.symbolCount;

        for (int i = 0; i < symbolCount; i++) Paths.Add(new List<int>());
    }

    public override bool IsAchieved()
    {
        nowAngleNum = 0;

        for(int i = 0; i < symbolCount; i++)
        {
            Paths[i].Clear();

            if (SymbolController.GetPaths[i].Count < 2) continue;

            foreach (int index in SymbolController.GetPaths[i])
            {
                Paths[i].Add((index - i + symbolCount) % symbolCount);
            }

            Paths[i].Sort();

            for(int j = 0; j < Paths[i].Count - 1; j++)
            {
                if (Paths[i][j + 1] - Paths[i][j] == (int)angleType / 30) nowAngleNum++;
            }
        }
 
        switch (countType)
        {
            case CountType.Less:
                isAchieved = nowAngleNum < angleNum;
                break;
            case CountType.Equal:
                isAchieved = nowAngleNum == angleNum;
                break;
            case CountType.Greater:
                isAchieved = nowAngleNum > angleNum;
                break;
        }

        isAchieved |= inverse && SymbolController.GetHistory.Count < 2;

        return base.IsAchieved();
    }
}
