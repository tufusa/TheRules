using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool cleared;
    GameObject Stage;
    GameObject Title;
    GameObject BackGround;
    LogoController logoController;
    AudioSource clearAudio;

    void Awake()
    {
        Screen.fullScreen = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        Stage = GameObject.FindGameObjectWithTag("Stage");
        Title = GameObject.FindGameObjectWithTag("Title");
        BackGround = GameObject.FindGameObjectWithTag("BackGround");
        logoController = GameObject.FindGameObjectWithTag("LogoController").GetComponent<LogoController>();
        clearAudio = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioSource>();

        Stage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!PointerController.drawing && cleared)
        {
            LineController.BurstParticle();
            StageController.StageClear();
            cleared = false;
            clearAudio.Play();
        }

        if(Input.GetKey(KeyCode.Escape) && Stage.activeSelf)
        {
            ReturnToTitle();
        }
    }

    public void GameStart()
    {
        BackGround.GetComponent<BackGroundController>().ForceSwith();
        Title.SetActive(false);
        Stage.SetActive(true);
        StageController.LoadStage();
    }

    public void ReturnToTitle()
    {
        StageController.UnloadStage();
        Stage.SetActive(false);
        Title.SetActive(true);
        logoController.Start();
    }

    public void Exit()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            PlayerPrefs.DeleteAll();
        }
        Application.Quit();
    }

    void MoveStage(StageController.StageMove move)
    {
        StageController.MoveStage(move);
        PointerController.drawing = false;
        cleared = false;
    }

    public void MoveStageInt(int move)
    {
        MoveStage((StageController.StageMove)move);
    }
}