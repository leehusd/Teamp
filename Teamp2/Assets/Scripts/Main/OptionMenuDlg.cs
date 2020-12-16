using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenuDlg : MonoBehaviour
{
    [SerializeField] Toggle m_toggleBgm = null;
    [SerializeField] Toggle m_toggleSFX = null;
    [SerializeField] Text m_txtBgm = null;
    [SerializeField] Text m_txtSFX = null;
    [SerializeField] Button m_btnOK = null;
    [SerializeField] Button m_btnCancel = null;

    AudioSource m_BGM = null;
    // Start is called before the first frame update
    void Start()
    {
        m_btnOK.onClick.AddListener(OnClicked_OK);
        m_btnCancel.onClick.AddListener(OnClicked_Cancel);
        
        m_toggleBgm.onValueChanged.AddListener((bool bOn) =>
        {
            PrintBgmText(bOn);
            //OnValueChanged_Bgm(bOn);
        });
        m_toggleSFX.onValueChanged.AddListener((bool bOn) =>
        {
            OnValueChanged_SFX(bOn);
        });
    }

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    
    public void OpenUI(AudioSource kBgm)
    {
        Show(true);

        m_BGM = kBgm;
        Initialize();
    }

    public void Initialize()
    {
        LocalSave.Inst().Load();
        
        m_toggleBgm.isOn = LocalSave.Inst().IsUseBgm;
        PrintBgmText(m_toggleBgm.isOn);

        m_toggleSFX.isOn = LocalSave.Inst().IsUseSFX;
        PrintSFXText(m_toggleSFX.isOn);
    }

    public void CloseUI()
    {
        Show(false);
    }

    public void OnClicked_OK()
    {
        LocalSave.Inst().IsUseBgm = m_toggleBgm.isOn;
        LocalSave.Inst().IsUseSFX = m_toggleSFX.isOn;

        LocalSave.Inst().Save();

        if (LocalSave.Inst().IsUseBgm)
            m_BGM.Play();
        else
            m_BGM.Stop();

        CloseUI();
    }

    public void OnClicked_Cancel()
    {
        CloseUI();
    }

    public void OnValueChanged_Bgm(bool bOn)
    {
        PrintBgmText(bOn);
    }

    public void OnValueChanged_SFX(bool bOn)
    {
        PrintSFXText(bOn);
    }


    public void PrintBgmText(bool bOn)
    {
        string sSound = "BGM ON";
        if (!bOn)
            sSound = "BGM OFF";

        m_txtBgm.text = sSound;
    }
    public void PrintSFXText(bool bOn)
    {
        string sSound = "SFX ON";
        if (!bOn)
            sSound = "SFX OFF";

        m_txtSFX.text = sSound;
    }

}
