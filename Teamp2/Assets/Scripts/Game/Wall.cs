using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject, 0.001f);
        }

        // 벽에 다면 정지
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody kRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            kRigidbody.velocity = Vector3.zero;
        }
    }

}
