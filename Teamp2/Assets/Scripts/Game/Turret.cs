using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    public const float DBASE_BULLET_SPEED = 0.05f;
    // Start is called before the first frame update
    [SerializeField] Transform m_FireStartPos = null;
    [SerializeField] GameObject m_prefabBullet = null;
    [SerializeField] Transform m_BulletParent = null;
    [SerializeField] Transform m_Body = null;
    [SerializeField] FXSerialize m_FireFX = null;


    [SerializeField] Transform m_Target = null;
    [SerializeField] float m_FireDelay = 1.0f;      // 발사 지연시간 ( 예) 1초마다 사격)
    [SerializeField] float m_BulletSpeed = 1;       // 총알 속도

    CSound m_kAudio = null;
    bool m_isFire = false;


    private void Awake()
    {
        m_kAudio = GetComponent<CSound>();
    }

    public void Initialize(Transform target)
    {
        Show(true);

        //GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;

        m_FireDelay = 3.0f;//kGameInfo.turretFireDelay;
        m_BulletSpeed = 1.0f;// kGameInfo.bulletSpeed;

        SetIsFire(true);
        m_Target = target;
        CreateBulletObject();
    }

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    public bool IsShow()
    {
        return gameObject.activeSelf;
    }

    public void CreateBulletObject()
    {
        int nValue = Random.Range(1, 50);
        float fDelay = m_FireDelay + (float)nValue * 0.01f;
        StartCoroutine("IEnumFunc_CreateBullet", fDelay);
    }

    IEnumerator IEnumFunc_CreateBullet(float fDelay)
    {
        while (m_isFire)
        {
            CreateBullet(m_Target, m_BulletParent);

            yield return new WaitForSeconds(fDelay);
        }
        yield return null;
    }

    public void SetIsFire( bool bFire )
    {
        m_isFire = bFire;
    }

    public void CreateBullet(Transform target, Transform trParent)
    {
        GameObject go = Instantiate(m_prefabBullet, trParent);
        go.transform.position = this.m_FireStartPos.position;
        go.transform.rotation = Quaternion.Euler(Vector3.zero);

        Bullet kBullet = go.GetComponent<Bullet>();
        kBullet.Initialize(target, DBASE_BULLET_SPEED * m_BulletSpeed);
       
        if( m_kAudio != null )
            m_kAudio.Play();

        if (m_FireFX != null)
            m_FireFX.Play();
    }

    // Update is called once per frame
    void Update()
    {
       RotationToTarget();
    }

    public void RotationToTarget()
    {
        //m_Body.transform.LookAt(m_Target.position);
        Vector3 vDir = m_Target.position - transform.position;
        vDir.Normalize();
        Quaternion kRot = Quaternion.LookRotation(vDir);

    }


    public void RestartFire()
    {
        SetIsFire(true);
        CreateBulletObject();
    }

    public void StopFire()
    {
        SetIsFire(false);
    }


}
