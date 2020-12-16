using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{

    public Image m_bar = null;
 
    [SerializeField] Text m_txtHP = null;

    private bool m_bAction = false;

    void Start()
    {
        
    }

    public void Initialize()
    {
        m_bAction = true;
        PrintBar();
    }

    public void SetIsActionBar(bool bAction)
    {
        m_bAction = bAction;
    }

    public void PrintBar()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        ActorInfo kActor = kGameInfo.m_ActorInfo;
        int curHP = kActor.m_HP;
        int maxHP = kActor.CalculateMaxHP();

        m_bar.fillAmount = (float)curHP / maxHP;
        m_txtHP.text = kActor.m_HP.ToString();
    }

    void Update()
    {
        if (m_bAction)
            PrintBar();
    }
    //public float max_health = 100f;
    //public float min_health = 0f;

    //void Start()
    //{
    //    min_health = max_health;
    //    //while(==)//총알을 맞을때 깍인다
    //    //InvokeRepeating("reduce_HP", 3.0f,1.0f);


    //}

    //void reduce_HP()
    //{
    //    min_health -= 5f;
    //    float minn_health = min_health / max_health;
    //    setHealth(minn_health);
    //}

    //void setHealth(float myHealth)
    //{
    //    m_bar.fillAmount = myHealth;
    //}
}