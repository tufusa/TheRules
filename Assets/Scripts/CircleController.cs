using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CircleController : EventTrigger
{
    static PointerController pointerController;
    public int index { get; private set; } = -1;

    // Start is called before the first frame update
    void Start()
    {
        pointerController = GameObject.FindGameObjectWithTag("Pointer").GetComponent<PointerController>();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (Input.GetMouseButtonUp(0))
        {
            pointerController.ClickSymbol(index);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        pointerController.EnterSymbol(index);
    }

    public void SetIndex(int index)
    {
        if (this.index == -1) this.index = index;
    }
}
