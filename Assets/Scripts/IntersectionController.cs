using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionController : MonoBehaviour
{
    Collider2D pointCollider;
    Collider2D[] overlapResult;
    static ContactFilter2D filter = new ContactFilter2D();
    public bool isIntersection { get; private set; } = false;
    SpriteRenderer spriteRenderer;
    MeshRenderer meshRenderer;
    int overlapCount;

    // Start is called before the first frame update
    void Start()
    {
        pointCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        meshRenderer = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        overlapResult = new Collider2D[3];
        filter.maxDepth = 0.5f;
        filter.useDepth = true;
    }

    // Update is called once per frame
    void Update()
    {
        overlapCount = pointCollider.OverlapCollider(filter, overlapResult);
        if (overlapCount > 1) isIntersection = true;
        else isIntersection = false;
        spriteRenderer.enabled = isIntersection;
        meshRenderer.enabled = isIntersection;
    }
}