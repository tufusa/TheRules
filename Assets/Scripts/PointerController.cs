using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    [System.NonSerialized] public static bool drawing = false;
    static Vector3 mousePos;
    static Vector3 worldPos;
    static Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (LineController.particlePlaying) return;

        mousePos = Input.mousePosition;
        worldPos = cam.ScreenToWorldPoint(mousePos);
        worldPos.z = 0.0f;

        if(drawing)
        {
            SymbolController.DrawLine(worldPos);
        }

        if(Input.GetMouseButtonDown(0) && !drawing && SymbolController.ContinueLine(worldPos))
        {
            drawing = true;
        }

        if (Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(0))
        {
            if(drawing)
            {
                drawing = false;
                SymbolController.CancelLine();
            }
            else
            {
                SymbolController.DeleteLine();
            }
        }
    }

    public void ClickSymbol(int index)
    {
        if (LineController.particlePlaying) return;
        if(drawing)
        {
            EnterSymbol(index);
            return;
        }
        SymbolController.StartLine(index);
        drawing = true;
    }

    public void EnterSymbol(int index)
    {
        if (!drawing || LineController.particlePlaying) return;
        SymbolController.Connect(index);
    }
}