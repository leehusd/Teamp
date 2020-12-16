using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepTimeBarUI : MonoBehaviour
{

    public Text m_txtTime = null;
    float m_maxTime = 0;
    bool m_bAction = false;
    public Image m_bar = null;


    void Start()
    {

    }
    //m_HudUI.PrintDurationTime();
    public void Initialize()
    {
        AssetStage kAssStage = GameMgr.Inst.GetCurAssetStage();
        m_maxTime = kAssStage.m_StageKeepTime;

        SetActionBar(true);
        m_txtTime.text = ((int)m_maxTime).ToString();
    }

    public void SetActionBar(bool bAction)
    {
        m_bAction = bAction;
    }

    void Update()
    {
        if (m_bAction)
        {
            OnUpdateTime();
        }
    }

    public void ClearKeepTime()
    {
        SetActionBar(false);
        m_bar.fillAmount = 1.0f;
        m_txtTime.text = ((int)m_maxTime).ToString();
    }
    public void OnUpdateTime()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        float fTime = m_maxTime - kGameInfo.m_fDurationTime;
        if (fTime <= 0)
        {
            m_bAction = false;
            GameMgr.Inst.gameScene.SetResultState(true);

        }

        m_bar.fillAmount = fTime / m_maxTime;
        m_txtTime.text = ((int)fTime).ToString();

        //ActorInfo kActor = kGameInfo.m_ActorInfo;
        //int curHP = kActor.m_HP;
        //int maxHP = kActor.CalculateMaxHP();

        //m_bar.fillAmount = (float)curHP / maxHP;
        //m_txtHP.text = kActor.m_HP.ToString();
    }

    
}
