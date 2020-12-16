using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorInfo : MonoBehaviour
{

    public int m_HP = 0;
    public int m_MaxHP = 0;
    public int m_ExtraHP = 0;

    
    void Start()
    {
        
    }
    public void Initialize(int nMaxHp)
    {
        m_MaxHP = nMaxHp;
        //m_ExtraHP = CalculateAddHP();
        m_HP = nMaxHp + m_ExtraHP;
    }

    public void AddDamage(int nDamage)
    {
        m_HP -= nDamage;
        if (m_HP <= 0)
            m_HP = 0;
    }

    public bool IsDie()
    {
        return m_HP == 0;
    }


    //public int CalculateAddHP()
    //{
    //    SaveInfo kSaveInfo = GameMgr.Inst.m_SaveInfo;
    //    //return (int)(kSaveInfo.m_AccumulateScore * 0.001f * Config.DMIN_ADD_HP);
    //}

    public int CalculateMaxHP()
    {
        return m_MaxHP + m_ExtraHP;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
