using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector3 m_vDir = Vector3.zero;    // 총알 이동 방향
    //private Vector3 m_vTargetPos = Vector3.zero;
    float m_Speed = 1.0f;

    void Start()
    {
        Destroy(gameObject, 50.0f);
    }


    public void Initialize(Transform target, float fSpeed)
    {
        m_Speed = fSpeed;
        RotationToTarget(target.position);
        m_vDir = target.position - transform.position;
        m_vDir.Normalize();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(m_vDir);
    }

    void RotationToTarget(Vector3 vTargetPos)
    {
        transform.LookAt(vTargetPos);
    }


    public void Move(Vector3 vDir)
    {
        transform.Translate(vDir * m_Speed, Space.World);
    }
    
}
