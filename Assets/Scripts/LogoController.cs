using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoController : MonoBehaviour
{
    Vector3 vec = Vector3.zero;
    Color color1;
    Color color2;
    [SerializeField] GameObject[] Icons;
    // Start is called before the first frame update
    public void Start()
    {
        color1 = new Color(1, 0, 0, 0);
        color2 = new Color(1, 0, 0, 0);
        transform.position = Vector3.zero;
        GetComponent<SpriteRenderer>().color = color1;
        foreach(GameObject icon in Icons)
        {
            icon.SetActive(false);
            icon.GetComponent<SpriteRenderer>().color = color2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (color1.a < 1)
        {
            GetComponent<SpriteRenderer>().color = color1;
            color1.a += 0.5f * Time.deltaTime;
        }
        else if (transform.position.y < 0.5) 
        {
            vec.y = 1.0f * Time.deltaTime;
            transform.Translate(vec);
        }
        else if (color2.a < 1)
        {
            foreach(GameObject icon in Icons)
            {
                icon.SetActive(true);
                icon.GetComponent<SpriteRenderer>().color = color2;
            }
            color2.a += 1.0f * Time.deltaTime;
        }
    }
}
