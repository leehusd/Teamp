using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform m_Target = null;
    //[SerializeField] Transform m_Body = null;
    [SerializeField] float EnemySpeed = 1.0f;

    public void Initialize()
    {

    }
    void Start()
    {
        
    }
    public void RotationToTarget()
    {

        this.transform.LookAt(m_Target.transform);
    }

    void Update()
    {

        Vector3 Dir= m_Target.position - GetComponent<Transform>().position;

        GetComponent<Transform>().position += Dir * EnemySpeed * Time.deltaTime;
        RotationToTarget();
    }
}
