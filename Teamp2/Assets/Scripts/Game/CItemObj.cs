using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemObj : MonoBehaviour
{
    public int m_ID = 0;  // = AssetItlem.m_id

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    public void Initialize(int nAssId, Vector3 vLocalPos)
    {
        m_ID = nAssId;
        transform.localPosition = vLocalPos;
        Show(true);
    }

}
