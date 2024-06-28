using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule : MonoBehaviour
{
    protected bool isAchieved;
    [SerializeField] protected bool inverse = false;

    public virtual bool IsAchieved() 
    {
        if (inverse) return !isAchieved;
        else return isAchieved;
    }

    protected enum CountType
    {
        Less, Equal, Greater
    }

    public enum SymmetricalType
    {
        Horizontal, Vertical, Point
    }

    protected enum AngleType
    {
        Thirty = 30, Sixty = 60, Ninety = 90, OneTwenty = 120
    }
}
