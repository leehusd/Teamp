using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDialogUI : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Show(bool bShow=true)
    {
        gameObject.SetActive(bShow);
    } 

    public virtual void OpenUI() {
        Show();
    }
    public virtual void CloseUI()
    {
        Show(false);
    }

}
