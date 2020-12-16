using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;

public class ResultSuccessDlg : CDialogUI
{
    [SerializeField] Button m_btnRestart = null;
    [SerializeField] Button m_btnExit = null;
    [SerializeField] Button m_btnNext = null;
    [SerializeField] Text m_txtStage = null;
    [SerializeField] Text m_txtScore = null;
    [SerializeField] Text m_txtTotalScore= null;

    [SerializeField] FXSerialize m_FX = null;
    void Start()
    {
        m_btnRestart.onClick.AddListener(OnClicked_Restart);
        m_btnExit.onClick.AddListener(OnClicked_ExitGame);
        m_btnNext.onClick.AddListener(OnClicked_NextStage);
    }

    public override void OpenUI()
    {
        
        
        base.OpenUI();
        Initialize();

    }

    public void Initialize()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        SaveInfo kSaveInfo = GameMgr.Inst.m_SaveInfo;

        int nStage = kGameInfo.m_nStage;
        int nScore = kGameInfo.CalculateScore();
        kSaveInfo.SetStageScore(nStage, nScore);

        //long nTotalScore = (long)kSaveInfo.m_AccumulateScore;
        m_txtStage.text = string.Format("STAGE{0}", nStage);

        m_txtScore.text = string.Format("SCORE:{0:#,##0}", nScore);
        //m_txtTotalScore.text = string.Format("Total:{0:#,##0}", nTotalScore);

        m_FX.Play();
    }

    public override void CloseUI()
    {
        base.CloseUI();
    }
    public void OnClicked_Restart()
    {
        GameMgr.Inst.gameScene.ResetGame();

        m_FX.Stop();
        Show(false);
    }

    public void OnClicked_NextStage()
    {
        GameMgr.Inst.m_SaveInfo.m_LastStage++;
        GameMgr.Inst.SaveFile();
        GameMgr.Inst.gameScene.ResetGame();


        m_FX.Stop();
        Show(false);

    }

    public void OnClicked_ExitGame()
    {

        GameMgr.Inst.gameScene.ExitGame();

    }
    void Update()
    {
        
    }
}
