using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] StageController.StageMove move;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    Color ableClick = new Color(1, 32.0f / 255, 32.0f / 255, 1);
    Color disableClick = new Color(1, 32.0f / 255, 32.0f / 255, 64.0f / 255);


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(StageController.Movable(move))
        {
            spriteRenderer.enabled = true;
            if(StageController.stageID + (int)move <= StageController.maxStageID)
            {
                spriteRenderer.color = ableClick;
                boxCollider.enabled = true;
            }
            else
            {
                spriteRenderer.color = disableClick;
                boxCollider.enabled = false;
            }
        }
        else
        {
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }
}
