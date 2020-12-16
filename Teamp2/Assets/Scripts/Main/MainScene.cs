using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainScene : MonoBehaviour
{
    [SerializeField] Button m_btnStart = null;
    [SerializeField] Button m_btnOption = null;

    [SerializeField] OptionMenuDlg m_OptionDlg = null;
    [SerializeField] MsgBox m_MsgBox = null;

    AudioSource m_AudioBgm = null;


    void Awake()
    {
        if( !AssetMgr.Inst.IsInstalled )
            AssetMgr.Inst.Initialize();

        LocalSave.Inst().Load();
        m_AudioBgm = GetComponent<AudioSource>();
        
    }

    void Start()
    {
        m_btnStart.onClick.AddListener(OnClicked_Start);
        m_btnOption.onClick.AddListener(OnClicked_Option);
    }



    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Escape))
        {
            m_MsgBox.OpenUI(OnClicked_GameExit);
        }
    }


    public void OnClicked_Start()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClicked_Option()
    {
        m_OptionDlg.OpenUI(m_AudioBgm);
    }


    void OnApplicationQuit()
    {
        Debug.Log("[MainScene] App Quit ......");
        try
        {
            //GameMgr.Inst.SaveFile();
            LocalSave.Inst().Save();
        }
        catch (System.Exception e)
        {
            Debug.Log("[_Error_Quit] " + e.ToString());
        }

    }

    void OnClicked_GameExit(bool bOK)
    {
        if (bOK)
        {
            Application.Quit();
        }
    }

}
