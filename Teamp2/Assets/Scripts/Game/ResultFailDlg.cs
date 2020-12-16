using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultFailDlg : MonoBehaviour
{
  

    [SerializeField] Button m_btnReStart = null;
    [SerializeField] Button m_btnQuit = null;
    [SerializeField] Text m_txtTime = null;
    [SerializeField] FXSerialize m_FX = null;

    void Start()
    {
        m_btnReStart.onClick.AddListener(OnClicked_ReStart);
        m_btnQuit.onClick.AddListener(OnClicked_Exit);
    }
    public void OnClicked_ReStart()
    {
        if (m_FX.IsPlayFX())
            return;

        GameScene kGameScene = GameMgr.Inst.gameScene;
        kGameScene.ResetGame();
        m_FX.Stop();
        CloseUI();


        //GameMgr.Inst.gameScene.m_BattleFSM.SetReadyState();

    }
    public void OnClicked_Exit()
    {

        if (m_FX.IsPlayFX())
            return;

        GameScene kGameScene = GameMgr.Inst.gameScene;
        kGameScene.ExitGame();
        m_FX.Stop();
        CloseUI();
        //GameMgr.Inst.gameScene.m_BattleFSM.SetNoneState();
        //GameMgr.Inst.gameScene.m_HudUI.OpenMenuUI();
    }

    public void PrintDurationTime()
    {
        if (m_txtTime != null)
        {
            GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
            int nMinute = (int)(kGameInfo.m_fDurationTime / 60);
            int nSecond = (int)(kGameInfo.m_fDurationTime % 60);

            string sTime = string.Format("Time {0:00}:{1:00}", nMinute, nSecond);

            m_txtTime.text = sTime;
        }
    }
    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }
    public void OpenUI()
    {
        Show(true);

        SaveInfo kSaveInfo = GameMgr.Inst.m_SaveInfo;
        //kSaveInfo.AddAccumulateScore(Config.DMIN_SCORE);

        PrintDurationTime();
        m_FX.Play();
    }
    public void CloseUI()
    {
        Show(false);
    }

    void Update()
    {
        PrintDurationTime();
    }
}
