using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleController : MonoBehaviour
{
    Rule[] Rules;
    int ruleNum;
    int achieveNum;
    int latestHistoryCount = -1;

    void Awake()
    {
        Rules = GetComponents<Rule>();
        ruleNum = Rules.Length;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetLamp();
    }

    // Update is called once per frame
    void Update()
    {
        if (LineController.particlePlaying) return;

        
        if (latestHistoryCount != SymbolController.GetHistory.Count)
        {
            Invoke("ClearCheck", 0.05f);
            latestHistoryCount = SymbolController.GetHistory.Count;
        }
    }

    void ClearCheck()
    {
        achieveNum = 0;

        for(int i = 0; i < ruleNum; i++)
        {
            if (Rules[i].IsAchieved())
            { 
                achieveNum++;
                LampController.TurnLamp(i, true);
            }
            else
            {
                LampController.TurnLamp(i, false);
            }
        }

        if (achieveNum == ruleNum) GameController.cleared = true;
        else GameController.cleared = false;
    }

    public void SetLamp()
    {
        LampController.SetLamp(ruleNum);
    }
}