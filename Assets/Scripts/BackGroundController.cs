using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    public readonly Color[] Colors = { Color.black, Color.white };
    Color color = Color.black;
    int colorID;
    bool switching = false;

    // Start is called before the first frame update
    void Awake()
    {
        colorID = PlayerPrefs.GetInt("ColorID", 0);
        RenderSettings.ambientLight = Colors[colorID];
    }

    void Update()
    {
        if(switching)
        {
            RenderSettings.ambientLight += color;
            if(Mathf.Abs(RenderSettings.ambientLight.r - Colors[colorID].r) <= 0.05f)
            {
                RenderSettings.ambientLight = Colors[colorID];
                switching = false;
            }
        }
    }

    public void SwitchColor()
    {
        if (!switching)
        {
            colorID = (colorID + 1) % 2;
            switching = true;
            color = (Colors[colorID] - RenderSettings.ambientLight) * Time.deltaTime / 2;
            PlayerPrefs.SetInt("ColorID", colorID);
        }
    }

    public void ForceSwith()
    {
        RenderSettings.ambientLight = Colors[colorID];
        switching = false;
    }
}
