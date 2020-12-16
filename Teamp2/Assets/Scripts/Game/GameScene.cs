using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{

    public HudUI m_HudUI;
    public MsgBox m_MsgBoxUI;
    public GameUI m_GameUI;

    //public MsgBox m_MsgBoxUI;

    [HideInInspector] public BattleFSM m_BattleFSM = new BattleFSM();

    void Awake()
    {
        if (!AssetMgr.Inst.IsInstalled)
            AssetMgr.Inst.Initialize();

        GameMgr.Inst.Initialize();
        LocalSave.Inst().Load();

        GameMgr.Inst.LoadFile();
    }

    void Start()
    {
        GameMgr.Inst.SetGameScene(this);
        m_BattleFSM.Initialize(OnCallback_ReadyEnter,
                               OnCallback_WaveEnter,
                               OnCallback_GameEnter,
                               OnCallback_ResultEnter);
        m_BattleFSM.SetCallback_GameStateOnExit(OnCallback_GameOnExit);

        m_BattleFSM.SetReadyState();
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_MsgBoxUI.IsShow())
            {
                m_MsgBoxUI.CloseUI();
                return;
            }

            //Time.timeScale = 0;
            m_MsgBoxUI.OpenUI(CBMsg_GameExit);
        }

        if (m_BattleFSM != null)
        {
            m_BattleFSM.OnUpdate();

            if (m_BattleFSM.IsGameState())
            {
                GameMgr.Inst.OnUpdate(Time.deltaTime);
            }
        }
    }

    public void StartGame()
    {
        GameMgr.Inst.ReStart();
        m_HudUI.Initialize();
        m_GameUI.Initialize();

       
    }

    public void ResetGame()
    {
        m_BattleFSM.SetReadyState();
    }

    public void Initialize()
    {
        
    }


    void OnCallback_ReadyEnter()
    {
        //GameMgr.Inst.InitStart();

        m_GameUI.ShowPlayer(true);
        m_HudUI.SetReadyState();
        //m_HudUI.StartReadyCount();// SetReadyState()안에 StartReadyCount()가 있움

        Invoke("CallbackInvoke_Start", 4.5f);


    }
    public void SetResultState(bool bSuccess)
    {
        GameMgr.Inst.m_GameInfo.m_isSuccess = bSuccess;
        m_BattleFSM.SetResultState();
        

    }

    public void SetReadyState()
    {
        m_BattleFSM.SetReadyState();
    }

    void CallbackInvoke_Start()
    {
        m_BattleFSM.SetGameState();
    }

    void OnCallback_WaveEnter()
    {

    }

    void OnCallback_GameEnter()
    {
        StartGame();
        //


    }

    void OnCallback_ResultEnter()
    {
        m_HudUI.SetResultStateEnter();
        m_GameUI.SetResultStateEnter();
    }

    public void StartReadyCount()
    {
        //ShowTestCount(true);
        StartCoroutine("EnumFunc_ReadyCount", 1.0f);
    }

    void OnCallback_GameOnExit()
    {
        m_GameUI.SetGameStateExit();
    }

    private void OnApplicationQuit()
    {
        Debug.Log("[GameScene] App Quit");
        try
        {

            GameMgr.Inst.SaveFile();
            LocalSave.Inst().Save();
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }

    }
    public void ExitGame()
    {
        m_BattleFSM.SetNoneState();
        SceneManager.LoadScene("MainScene");
    }
    public void CBMsg_GameExit(bool bOk)
    {
        if (bOk)
        {
            ExitGame();
        }
    }
}
