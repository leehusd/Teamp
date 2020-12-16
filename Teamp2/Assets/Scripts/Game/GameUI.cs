using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] List<Turret> m_Turret;
    [SerializeField] PlayerController m_Player;
    [SerializeField] Transform m_BulletParent;

    public ItemObjectMgr m_ItemObjMgr;

    void Start()
    {

    }

    void Update()
    {

    }
    public void ShowPlayer(bool bShow)
    {
        m_Player.gameObject.SetActive(bShow);
    }
    public void Initialize()
    {
        // 플레이어 초기화
        m_Player.Initialize(OnCallback_PlayerCollisionEnter);

        Initialize_Turrets();
        Initialize_Items();
    }

    public void Initialize_Turrets()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;

        // 각 터렛 초기화
        for (int i = 0; i < m_Turret.Count; i++)
        {
            Turret kTurret = m_Turret[i];
            if (i < kGameInfo.turretCount)
            {
                kTurret.Initialize(m_Player.transform);
            }
            else
            {
                kTurret.Show(false);
            }
        }
    }

    public void Initialize_Items()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;

        float fKeepTime = kGameInfo.m_AssStage.m_ItemKeepTime;
        float fDelay = kGameInfo.itemAppearDelay;

        m_ItemObjMgr.Initialize(fKeepTime, fDelay);
    }

    //public void Initialize()
    //{
    //    GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
    //    for (int i = 0; i < m_Turret.Count; i++)
    //    {
    //        if (i < kGameInfo.turretCount)
    //        {
    //            m_Turret[i].Initialize(m_Player.transform);
    //        }
    //        else
    //        {
    //            m_Turret[i].Show(false);
    //        }
    //    }
    //    m_Player.Initialize(OnCallback_PlayerCollisionEnter);
    //    m_ItemObjMgr.Initialize(kGameInfo.itemKeepTime, kGameInfo.itemAppearDelay);
    //}

    //public void SetResult()
    //{
    //    for (int i = 0; i < m_Turret.Count; i++)
    //    {
    //        m_Turret[i].StopFire();
    //    }
    //  //  m_Player.Initialize(OnCallback_PlayerCollisionEnter);
    //    m_Player.SetIsAction(false);
    //    m_Player.gameObject.SetActive(false);
    //}

    public void OnCallback_PlayerCollisionEnter(Collision kCollision)
    {
        if (kCollision.gameObject.tag == "Item")
        {
            //CItemObj kItem = kCollision.gameObject.GetComponent<CItemObj>();
            //GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
            //ItemInfo kItemInfo = kGameInfo.ActionItem(kItem.m_ID);

            //if (kItemInfo.m_ItemType == (int)ItemInfo.Type.eHealing)
            //{
            //    m_ItemObjMgr.ActionEffect(kItemInfo.m_ItemType);
            //}
            //if (kItemInfo.m_ItemType == (int)ItemInfo.Type.eExplose)
            //{
            //    TurretFire(false);
            //    DestroyAllBullet();
            //    Invoke("TurretRestartFire", (float)kItemInfo.m_ItemValue);

            //    m_ItemObjMgr.ActionEffect(kItemInfo.m_ItemType);
            //}
            Collision_Item(kCollision);
        }
        if (kCollision.gameObject.tag == "Bullet")
        {
            Collision_Bullet();
        }
    }

    public void Collision_Item(Collision kCollision)
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        CItemObj kItem = kCollision.gameObject.GetComponent<CItemObj>();
        ItemInfo kItemInfo = kGameInfo.ActionItem(kItem.m_ID);

        if (kItemInfo.m_ItemType == (int)ItemInfo.Type.eExplose)
        {
            TurretFire(false);
            DestroyAllBullet();
            ActionExploseEffect();
            //총알 발사까지의 지연시간 후 다시 발사하기
            Invoke("CBI_RestartTurretFire", (float)kItemInfo.m_ItemValue);
        }
        if (kItemInfo.m_ItemType == (int)ItemInfo.Type.eHealing)
        {
            ActionHealingEffect();
        }
    }

    public void Collision_Bullet()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        if (kGameInfo.IsPlayerDie())
        {
            m_Player.StopMove();
            return;
        }
        kGameInfo.AddDamage();
        m_Player.PlayExploseFX();

        if (kGameInfo.IsPlayerDie())
        {
            // 플레이어 죽음 처리
            // 폭파효과 출력을 위한 시간 지연.
            Invoke("CBI_CollisionDie", 0.1f);
        }
    }


    void CBI_CollisionDie()
    {
        // 배틀 상태를 ResultState 로 변경
        GameScene kGameScene = transform.GetComponentInParent<GameScene>();
        kGameScene.SetResultState(false);
    }

    void CBI_RestartTurretFire()
    {
        if (GameMgr.Inst.IsGameStage())
        {
            TurretFire(true);
        }
    }

    public void ActionExploseEffect()
    {
        m_ItemObjMgr.ActionEffect((int)ItemInfo.Type.eExplose);
    }

    // 힐링 이펙트 Play
    public void ActionHealingEffect()
    {
        m_ItemObjMgr.ActionEffect((int)ItemInfo.Type.eHealing);
    }

    public void SetGameStateExit()
    {
        TurretFire(false);
        m_Player.SetIsAction(false);
        m_ItemObjMgr.SetIsCreateItem(false);
    }

    public void SetResultStateEnter()
    {
        ShowPlayer(false);


    }

    //public void SetResultEnter()
    //{
    //    for (int i = 0; i < m_Turret.Count; i++)
    //    {
    //        m_Turret[i].SetIsFire(false);
    //    }
    //    m_Player.gameObject.SetActive(false);

    //}

  

    //public void TurretRestartFire()
    //{
    //    TurretFire(true);
    //}

    public void TurretFire(bool bStart)
    {
        for (int i = 0; i < m_Turret.Count; i++)
        {
            if (m_Turret[i].IsShow())
            {
                if (bStart)
                    m_Turret[i].RestartFire();
                else
                    m_Turret[i].StopFire();
            }
        }
        //GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        //if (bFire)
        //{
        //    for (int i = 0; i < kGameInfo.turretCount; i++)
        //    {
        //        m_Turret[i].RestartFire();
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < kGameInfo.turretCount; i++)
        //    {
        //        m_Turret[i].StopFire();
        //    }
        //}
    }

    public void DestroyAllBullet()
    {
        Bullet[] kBullet = m_BulletParent.GetComponentsInChildren<Bullet>();
        for (int i = 0; i < kBullet.Length; i++)
        {
            Destroy(kBullet[i].gameObject);
        }
    }


}
