using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;

public class SymbolController : MonoBehaviour
{
    static GameObject[] Symbols;
    static List<List<int>> Paths = new List<List<int>>(); // 隣接リスト
    static List<List<bool>> Map = new List<List<bool>>(); // 隣接行列 ReadOnlyCollectionがジャグ配列には使えないのでList
    static List<int> History = new List<int>();
    public static readonly ReadOnlyCollection<int> GetHistory = History.AsReadOnly();
    public static ReadOnlyCollection<ReadOnlyCollection<int>> GetPaths { get; private set; }
    public static ReadOnlyCollection<ReadOnlyCollection<bool>> GetMap { get; private set; }
    static List<Vector2> Positions = new List<Vector2>();
    public static int symbolCount { get; private set; }

    void Awake()
    {
        symbolCount = transform.childCount;
        Symbols = new GameObject[symbolCount];
        for (int i = 0; i < symbolCount; i++) Map.Add(new List<bool>(new bool[symbolCount]));
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < symbolCount; i++)
        {
            Symbols[i] = transform.GetChild(i).gameObject;
            Symbols[i].GetComponent<CircleController>().SetIndex(i);
            Paths.Add(new List<int>());
            Positions.Add(Symbols[i].transform.position);
        }

        GetPaths = Paths.ConvertAll(list => list.AsReadOnly()).AsReadOnly();
        GetMap = Map.ConvertAll(list => list.AsReadOnly()).AsReadOnly();
    }

    public static bool Connect(int endIdx)
    {
        if (History.Count == 0) return false;

        int startIdx = History[History.Count - 1];

        if (Map[startIdx][endIdx] || startIdx == endIdx) return true;

        Paths[startIdx].Add(endIdx);
        Paths[endIdx].Add(startIdx);

        Map[startIdx][endIdx] = true;
        Map[endIdx][startIdx] = true;

        LineController.UpdateLine(Positions[startIdx], Positions[endIdx]);
        LineController.GetLines[LineController.lineNum - 1].GetComponent<BoxCollider2D>().enabled = true;
        LineController.MakeLine();

        SwitchNode(endIdx, true);
        History.Add(endIdx);

        return true;
    }

    public static void StartLine(int startIdx)
    {
        LineController.MakeLine();

        SwitchNode(startIdx, true);
        History.Add(startIdx);
    }

    public static void DrawLine(Vector2 pointerPos)
    {
        LineController.UpdateLine(Positions[History[History.Count - 1]], pointerPos);
    }

    public static void CancelLine()
    {
        LineController.DeleteLine();
        SwitchNode(History[History.Count - 1], false);
        if (History.Count == 1) History.RemoveAt(0);
    }

    public static void DeleteLine()
    {
        LineController.DeleteLine();

        if(History.Count > 0)
        {
            int idx1 = History[History.Count - 1];

            if (History.Count > 1)
            {
                int idx2 = History[History.Count - 2];

                Paths[idx1].Remove(idx2);
                Paths[idx2].Remove(idx1);
                 
                Map[idx1][idx2] = false;
                Map[idx2][idx1] = false;

                if (History.Count == 2)
                {
                    SwitchNode(idx2, false);
                    History.RemoveAt(History.Count - 1);
                }
            }
            SwitchNode(idx1, false);
            History.RemoveAt(History.Count - 1);
        }
    }

    public static bool ContinueLine(Vector2 pointerPos)
    {
        if (History.Count != 0)
        {
            LineController.MakeLine();
            DrawLine(pointerPos);
            return true;
        }
        return false;
    }

    static void SwitchNode(int index, bool active)
    {
        if (!active && Paths[index].Count != 0) return;
        Symbols[index].transform.GetChild(0).gameObject.SetActive(active);
    }

    public static void ClearNode()
    {
        foreach(GameObject symbol in Symbols)
        {
            symbol.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    static int Swap(int index, Rule.SymmetricalType mode)
    {
        if (mode == Rule.SymmetricalType.Horizontal) return (6 - index) % 6;
        if (mode == Rule.SymmetricalType.Vertical) return (9 - index) % 6;
        if (mode == Rule.SymmetricalType.Point) return (3 + index) % 6;
        return -1;
    }

    public static bool IsSymmetrical(Rule.SymmetricalType mode)
    {
        if (History.Count < 2) return false;

        for (int idx1 = 0; idx1 < symbolCount; idx1++)
        {
            foreach (int idx2 in Paths[idx1])
            {
                if (!Map[Swap(idx1, mode)][Swap(idx2, mode)])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static void ResetAll()
    {
        for (int i = 0; i < symbolCount; i++)
        {
            for (int j = 0; j < symbolCount; j++) Map[i][j] = false;
        }
        History.Clear();
        foreach (List<int> paths in Paths) paths.Clear();
        LineController.ClearLine();
        ClearNode();
    }
}