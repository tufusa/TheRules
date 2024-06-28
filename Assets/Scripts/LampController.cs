using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{
    [SerializeField] GameObject lampPrefabTemp;
    static Color lampColor = new Color(1, 0, 0);
    static GameObject lampPrefeb;
    static List<GameObject> Lamps = new List<GameObject>();
    static float lampDistance = 1;
    static float onAlpha = 1, offAlpha = 64.0f / 255.0f;
    static Color onColor, offColor;
    static Transform parentTransform;
    static bool isMovedIn;
    static StageController.StageMove nowMove = StageController.StageMove.None;

    void Awake()
    {
        parentTransform = transform;
        lampPrefeb = lampPrefabTemp;
    }

    // Start is called before the first frame update
    void Start()
    {
        onColor = new Color(lampColor[0], lampColor[1], lampColor[2], onAlpha);
        offColor = new Color(lampColor[0], lampColor[1], lampColor[2], offAlpha);
    }

    void Update()
    {
        if(nowMove != StageController.StageMove.None)
        {
            if (!isMovedIn)
            {
                parentTransform.position = new Vector3((int)nowMove * (9 + (Lamps.Count - 1) / 2.0f * lampDistance + 1), 0, 0);
                isMovedIn = true;
            }

            parentTransform.Translate(-32 * Time.deltaTime * (int)nowMove, 0, 0);

            if (nowMove == StageController.StageMove.Next ? parentTransform.position.x < 0 : parentTransform.position.x > 0)
            {
                parentTransform.position = Vector3.zero;
                isMovedIn = false;
                nowMove = StageController.StageMove.None;
            }
        }
    }

    public static void SetLamp(int ruleNum)
    {
        foreach (GameObject lamp in Lamps) Destroy(lamp);
        Lamps.Clear();
        
        Vector3 pos = new Vector3(lampDistance * (1 - ruleNum) / 2.0f, 4, 0);
        GameObject newLamp;

        for(int i = 0; i < ruleNum; i++)
        {
            newLamp = Instantiate(lampPrefeb, parentTransform);
            newLamp.transform.localPosition = pos;
            pos.x += lampDistance;
            Lamps.Add(newLamp);
        }
    }

    public static void TurnLamp(int index, bool lampSwitch)
    {
        if (lampSwitch) Lamps[index].GetComponent<SpriteRenderer>().color = onColor;
        else Lamps[index].GetComponent<SpriteRenderer>().color = offColor;
    }

    public static void MoveInLamp(StageController.StageMove move)
    {
        nowMove = move;
        isMovedIn = false;
    }
}