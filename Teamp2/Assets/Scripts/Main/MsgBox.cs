using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgBox : MonoBehaviour
{
    public Button m_btnOk = null;
    public Button m_btnCancel = null;

    Action<bool> m_funcOK = null;       // 델리게이트


    // Start is called before the first frame update
    void Start()
    {
        m_btnOk.onClick.AddListener(OnClicked_OK);
        m_btnCancel.onClick.AddListener(OnClicked_Cancel);
    }

    public bool IsShow()
    {
        return gameObject.activeSelf;
    }

    public void OpenUI(Action<bool> func)
    {
        Show(true);
        m_funcOK += func;
    }

    public void CloseUI()
    {
        Time.timeScale = 0;
        Show(false);
    }
    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

  
    public void OnClicked_OK()
    {
        if( m_funcOK != null)
            m_funcOK(true);
        
        Show(false);
    }

    public void OnClicked_Cancel()
    {
        if (m_funcOK != null)
            m_funcOK(false);

        Show(false);
    }




}
