using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] GameObject linePrefabTemp;
    static GameObject linePrefab;
    const float lineWidth = 0.05f;
    const float particleLifeTime = 2;
    const int particleCountMultiplier = 150;
    static GameObject newLine;
    static GameObject lineParent;
    static List<GameObject> Lines = new List<GameObject>();
    public static readonly ReadOnlyCollection<GameObject> GetLines = Lines.AsReadOnly();
    public static int lineNum { get; private set; }
    public static bool particlePlaying { get; private set; } = false;
    

    void Awake()
    {
        linePrefab = linePrefabTemp;
    }
    void Start()
    {
        lineParent = gameObject;
    }

    void Update()
    {
        if(particlePlaying && !Lines[Lines.Count - 1].GetComponent<ParticleSystem>().isPlaying)
        {
            ClearLine();
        }
    }

    public static void MakeLine()
    {
        newLine = Instantiate(linePrefab);
        Lines.Add(newLine);
        newLine.transform.parent = lineParent.transform;
        lineNum++;
    }

    public static void UpdateLine(Vector2 startPos, Vector2 endPos)
    {
        Transform transform = Lines[lineNum - 1].GetComponent<Transform>();
        Vector2 diff = endPos - startPos;
        float rotz = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        transform.position = (startPos + endPos) / 2;
        transform.localScale = new Vector3(diff.magnitude, lineWidth, 0);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, rotz));
    }

    public static void DeleteLine()
    {
        if(lineNum > 0)
        {
            Destroy(Lines[lineNum - 1]);
            Lines.RemoveAt(lineNum - 1);
            lineNum--;
        }
    }

    public static void BurstParticle()
    {
        particlePlaying = true;
        ParticleSystem particleSystem;
        ParticleSystem.MainModule particleMain;
        foreach(GameObject line in Lines)
        {
            line.GetComponent<BoxCollider2D>().enabled = false;
            particleSystem = line.GetComponent<ParticleSystem>();
            particleMain = particleSystem.main;
            particleMain.startLifetime = particleLifeTime;
            particleSystem.emission.SetBurst(0, new ParticleSystem.Burst(0f, line.transform.localScale.x * particleCountMultiplier));
            particleSystem.Play();
            line.GetComponent<SpriteRenderer>().enabled = false;
        }
        SymbolController.ClearNode();
    }

    public static void ClearLine()
    {
        for (int time = lineNum; time > 0; time--)
        {
            SymbolController.DeleteLine();
        }
        particlePlaying = false;
    }
}