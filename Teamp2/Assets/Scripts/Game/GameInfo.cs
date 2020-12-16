using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public int m_nStage = 1;
    public bool m_isSuccess = false;
    public float m_fDurationTime = 0;


    public AssetStage m_AssStage = null;

    public ActorInfo m_ActorInfo = new ActorInfo();

    public List<ItemInfo> m_listItemInfo = new List<ItemInfo>();


    
    public float stageKeepTime { get { return m_AssStage.m_StageKeepTime; } }
    //스테이지 유지 시간
    public float BulletDamage { get { return m_AssStage.m_BulletAttack; } }
    //총알 데미지
    public float turretCount { get { return m_AssStage.m_TurretCount; } }
    //터렛 갯수
    public float itemAppearDelay { get { return m_AssStage.m_ItemAppearDelay; } }
    //아이템 생성 주기
    public float itemKeepTime { get { return m_AssStage.m_ItemKeepTime; } }
    //아이템 유지 시간
    public float turretFireDelay { get { return m_AssStage.m_FireDelayTime; } }
    //터렛 발사 지연 시간
    public float bulletSpeed { get { return m_AssStage.m_BulletSpeed; } }
    //총알 속도


    public void Initialize()
    {
        SaveInfo kSaveInfo = GameMgr.Inst.m_SaveInfo;
        m_nStage = kSaveInfo.m_LastStage;

        Initialize_Stage(m_nStage);
        Initialize_Item();
    }

    public void Initialize_Stage(int nStage)
    {
        m_AssStage = AssetMgr.Inst.GetAssetStage(nStage);
        m_ActorInfo.Initialize(m_AssStage.m_PlayerHP);
        m_fDurationTime = 0;
    }

    public void Initialize_Item()
    {
        for(int i = 0; i < AssetMgr.Inst.m_AssItems.Count; i++)
        {
            AssetItem kAss = AssetMgr.Inst.m_AssItems[i];
            ItemInfo kInfo = new ItemInfo();

            kInfo.Initialize(kAss.m_nType, kAss.m_fValue);
            m_listItemInfo.Add(kInfo);
        }
    }

    public void AddDamage()
    {
        m_ActorInfo.AddDamage((int)this.BulletDamage);

    }

    public bool IsPlayerDie()
    {
        return m_ActorInfo.IsDie();
    }

    public void OnUpdate(float fElaspedTime)
    {
        m_fDurationTime += fElaspedTime;
    }

    public bool IsSuccess()
    {
        return m_isSuccess;
    }

    public ItemInfo ActionItem(int nItemId)
    {
        ItemInfo kInfo = GetItemInfo(nItemId);
        if (kInfo.m_ItemType == (int)ItemInfo.Type.eHealing)
        {
            m_ActorInfo.m_HP += (int)kInfo.m_ItemValue;
        }
        if (kInfo.m_ItemType == (int)ItemInfo.Type.eExplose)
        {
            //return kInfo;
        }
        return kInfo;
    }

    public ItemInfo GetItemInfo(int id)
    {
        if (id > 0 && id <= m_listItemInfo.Count)
        {
            return m_listItemInfo[id - 1];
        }
        return null;
    }

    public int CalculateScore()
    {
        int curHP = m_ActorInfo.m_HP;
        int maxHP = m_ActorInfo.CalculateMaxHP();
        int nScore=(int)((float)curHP/maxHP)*Config.DMAX_SCORE;
        if (nScore < Config.DMIN_SCORE)
            nScore = Config.DMIN_SCORE;
        return nScore;
    }
}
