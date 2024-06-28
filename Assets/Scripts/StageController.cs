using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    static GameObject[] Stages;
    static int stageNum;
    public static int stageID, maxStageID;

    // Start is called before the first frame update
    void Start()
    {
        stageNum = transform.childCount;
        Stages = new GameObject[stageNum];
        stageID = PlayerPrefs.GetInt("StageID", 0);
        maxStageID = PlayerPrefs.GetInt("MaxStageID", 0);

        for (int i = 0; i < stageNum; i++)
        {
            Stages[i] = transform.GetChild(i).gameObject;
        }
    }

    public static void LoadStage()
    {
        Stages[stageID].SetActive(true);
        Stages[stageID].GetComponent<RuleController>().SetLamp();
        PlayerPrefs.SetInt("StageID", stageID);
    }

    public static void UnloadStage()
    {
        Stages[stageID].SetActive(false);
        SymbolController.ResetAll();
    }

    public static bool MoveStage(StageMove move)
    {
        if (!Movable(move)) return false;

        UnloadStage();

        stageID += (int)move;

        LoadStage();
        LampController.MoveInLamp(move);

        return true;
    }

    public static bool Movable(StageMove move)
    {
        return stageID + (int)move >= 0 && stageID + (int)move <= stageNum - 1;
    }

    public static void StageClear()
    {
        if(maxStageID == stageID && Movable(StageMove.Next))
        {
            maxStageID += 1;
            PlayerPrefs.SetInt("MaxStageID", maxStageID);
        }
    }

    public enum StageMove
    {
        Prev = -1, None = 0, Next = 1
    }
}