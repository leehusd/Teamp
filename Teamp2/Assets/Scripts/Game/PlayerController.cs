using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 m_vOffsetPos;

    public delegate void DelegateFunc(Collision kCollision);


    [SerializeField] FXParticle m_fxExplose = null;
    [SerializeField] float m_Speed = 0.0f;      // 이동속도
    private Rigidbody m_Rigidbody = null;       // 리지드 바디

    public DelegateFunc OnCallber_CollsionEnter;   // 충돌 되었을때 콜백
    //private bool m_isDie = false;

    private float m_LastCollisionTime = 0;      // 마지막으로 충돌한시간...
    private bool m_bAction = false;             // 입력키 사용가능여부    

    
    // Start is called before the first frame update
    void Start()
    {        
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
    }

    public void Initialize(DelegateFunc func)
    {
        m_fxExplose.Show(false);
        gameObject.SetActive(true);
        transform.localPosition = m_vOffsetPos;
        OnCallber_CollsionEnter = new DelegateFunc(func);
        SetIsAction(true);
    }


    // Update is called once per frame
    void Update()
    {
        if(m_bAction)
            Update_Input();
    }

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
        if( bShow )
        {
            transform.localPosition = m_vOffsetPos;
            m_fxExplose.Show(false);
        }
    }

    private void Update_Input()
    {
        // 수평축 과 수직축의 입력값을 감지하여 저장
        float xDelta = Input.GetAxis("Horizontal");
        float zDelta = Input.GetAxis("Vertical");

        // 리지드바디에 할당할 새로운 속도 계산
        //float xSpeed = xDelta * Time.deltaTime;
        //float zSpeed = zDelta * m_Speed * Time.deltaTime;

        // 리지드바디의 속도에 할당
        //m_Rigidbody.velocity = transform.forward * zSpeed;

        Vector3 rot = new Vector3(xDelta, 0 , zDelta).normalized;

        transform.position += rot * m_Speed * Time.deltaTime;

        transform.LookAt(transform.position + rot);
        

        //Vector3 moveVec = rot.normalized * 0.5f;
        //transform.Rotate(rot, Space.Self);
        //Vector3 Rot = transform.eulerAngles;
        //Rot.x = 0;
        //Rot.z = 0;
        //transform.eulerAngles = Rot;


    }

    public void SetIsAction(bool bAction)
    {
        m_bAction = bAction;
        if (!m_bAction)
            m_Rigidbody.velocity = Vector3.zero;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Bullet")
    //    {
            
    //        //Debug.Log("총알이 비행기에 맞음.");
    //        //Destroy(other.gameObject, 0.1f);
    //        //if (OnCallber_TriggerEnter != null)
    //       //     OnCallber_TriggerEnter(this, other);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" )
        {
            Destroy(collision.gameObject, 0.2f);
        }

        if ( collision.gameObject.tag == "Item")
        {
            //collision.gameObject.SetActive(false);  // ItemObjMgr에서 삭제함.
            Destroy(collision.gameObject, 0.2f);
        }

        if (OnCallber_CollsionEnter != null)
            OnCallber_CollsionEnter(collision);
    }
   
    public void HideObject()
    {
        m_fxExplose.Show(false);
        Show(false);

    }

    public void PlayExploseFX()
    {
        if (Time.time - m_LastCollisionTime < 1.0f)
            return;

        m_fxExplose.Play();
        m_LastCollisionTime = Time.time;
    }

    public void StopMove()
    {
        m_Rigidbody.velocity = Vector3.zero;
    }

}
