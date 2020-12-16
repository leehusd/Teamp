using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    [SerializeField] Text m_txtCount = null;
    [SerializeField] HPBarUI m_HPbar = null;
    [SerializeField] KeepTimeBarUI m_KeepTimebar = null;
    [SerializeField] ResultFailDlg m_ResultFailDlg = null;
    [SerializeField] ResultSuccessDlg m_ResultSuccessDlg = null;
    [SerializeField] Text m_txtTime = null;

    void Start()
    {
        ShowTextCount(false);
        StartReadyCount();
        Initialize();
    }


    public void Initialize()
    {
        m_HPbar.Initialize();
        m_KeepTimebar.Initialize();

        ShowTextTime(true);
        PrintDurationTime();

    }

    public void OpenResultFailedUI()
    {
        m_ResultFailDlg.OpenUI();
    }
    public void OpenResultSuccessUI()
    {
        m_ResultSuccessDlg.OpenUI();
    }
    public void OpenMenuUI()
    {
        //m_MenuDlg.OpenUI();
    }


    public void ShowTextTime(bool bShow)
    {
        if (m_txtTime != null)
            m_txtTime.gameObject.SetActive(bShow);
    }


    public void PrintDurationTime()
    {
        if (m_txtTime != null)
        {
            GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
            int nMinute = (int)(kGameInfo.m_fDurationTime / 60);
            int nSecond = (int)(kGameInfo.m_fDurationTime % 60);

            string sTime = string.Format("{0:00}:{1:00}", nMinute, nSecond);


            m_txtTime.text = sTime;
        }
    }

    public void ShowTextCount(bool bShow)
    {
        m_txtCount.gameObject.SetActive(bShow);
    }

    public void StartReadyCount()
    {
        ShowTextCount(true);
        StartCoroutine("EnumFunc_ReadyCount", 1.0f);
    }


    IEnumerator EnumFunc_ReadyCount(float fDelay)
    {
        int nCount = 3;
        while (nCount >= 0)
        {
            if (nCount == 0)
            {
                m_txtCount.text = "Start!";
            }
            else
            {
                m_txtCount.text = nCount.ToString();
            }
            --nCount;

            yield return new WaitForSeconds(fDelay);
        }

        this.ShowTextCount(false);

        yield return null;
    }

    public void SetReadyState()
    {
        m_HPbar.Initialize();
        m_KeepTimebar.Initialize();
        PrintStage();
        StartReadyCount();
    }

    public void SetGameStateExit()
    {
        m_HPbar.SetIsActionBar(false);
        m_KeepTimebar.SetActionBar(false);
    }
    public void SetResultStateEnter()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        if (kGameInfo.IsSuccess())
            OpenResultSuccessUI();
        else
            OpenResultFailedUI();
        ShowTextTime(false);
        
    }

    public void PrintHPBar()
    {
        m_HPbar.PrintBar();
    }
    //public void SetResultEnter()
    //{
    //    ShowTextTime(false);
    //    OpenResultUI();
    //}
    // Update is called once per frame

    public void PrintStage()
    {

    }

    void Update()
    {
        PrintDurationTime();
        
    }
}

