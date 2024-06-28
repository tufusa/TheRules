using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionsController : MonoBehaviour
{
    static IntersectionController[] Intersections;
    static int intersectionTemp;
    public static int intersectionCount { get; private set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        Intersections = new IntersectionController[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            Intersections[i] = transform.GetChild(i).gameObject.GetComponent<IntersectionController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        intersectionTemp = 0;
        foreach (IntersectionController intersection in Intersections)
        {
            if (intersection.isIntersection) intersectionTemp++;
        }
        intersectionCount = intersectionTemp;
    }
}