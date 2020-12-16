using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectMgr : MonoBehaviour
{
    //public List<CItemObj> m_Items = null;     // 아이템 리스트, 타입별로 보관
    public List<FXSerialize> m_FxList = null;   // 이펙트 리스트

    //주의: 하이러키뷰에서 ItemPos 노드가 items노드의 자식에 위치해 있기때문에 
    //      LocalPosition 기준으로 움직인다.
    public Transform m_MinPos = null;
    public Transform m_MaxPos = null;

    private float m_ItemKeepTime = 2.0f;
    private float m_ItemAppearDelay = 5.0f;

    private bool m_bCreateItem = false;         // 아이템오브젝트 생성 여부

    public void Initialize(float fKeepTime, float fItemAppearDelay)
    {
        if (m_bCreateItem)
            return;

        m_ItemKeepTime = fKeepTime;
        m_ItemAppearDelay = fItemAppearDelay;

        m_bCreateItem = true;
        StartCoroutine("EnumFunc_ItemCreate");
    }

    IEnumerator EnumFunc_ItemCreate()
    {

        float fKeepTime = m_ItemKeepTime;
        float fDelay = m_ItemAppearDelay;

        while (m_bCreateItem)
        {
            yield return new WaitForSeconds(fDelay - fKeepTime);

            if (!m_bCreateItem)
                break;

            int nAssId = 0;
            MakeRandomItemObjectID(ref nAssId); 
            CItemObj kItem = CreateItemObj(nAssId);
            kItem.Initialize(nAssId, MakeRamdomPos());

            //Debug.LogFormat("ItemPos  y = {0}", kItem.transform.localPosition.y);

            yield return new WaitForSeconds(fKeepTime);
            if( kItem != null && kItem.gameObject != null)
                Destroy(kItem.gameObject);

            fDelay = MakeRandomDelay(m_ItemAppearDelay);
        }

        yield return null;
    }

    // 아이템 오브젝트 ID 랜덤으로 생성하기
    void MakeRandomItemObjectID(ref int rAssId)
    {
        int nItemCount = AssetMgr.Inst.GetAsstItemCount();
        int id = Random.Range(1, nItemCount + 1);
        AssetItem kAssItem = AssetMgr.Inst.GetAssetItem(id);

        rAssId = id;
        //return kAssItem.m_nType;
    }

    public CItemObj CreateItemObj(int nAssId)
    {
        AssetItem kAssItem = AssetMgr.Inst.GetAssetItem(nAssId);
        GameObject goPrefab = Resources.Load(kAssItem.m_sPrefabName) as GameObject;
        GameObject go = Instantiate(goPrefab, this.transform);
        return go.GetComponent<CItemObj>();
    }


    float MakeRandomDelay(float fDelay)
    {
        int nValue = Random.Range(-2, 2);
        fDelay += nValue;
        return fDelay;
    }


    // 아이템위치를  랜덤 위치로 만들기.
    public Vector3 MakeRamdomPos()
    {
        Vector3 vMax = m_MaxPos.position;
        Vector3 vMin = m_MinPos.position;

        float x = Random.Range(vMin.x, vMax.x);
        float z = Random.Range(vMin.z, vMax.z);

        return new Vector3(x, 0, z);
    }


    public void SetIsCreateItem(bool bCreate)
    {
        m_bCreateItem = bCreate;
    }

    public void ActionEffect(int id)
    {
        if (id > 0 && id <= m_FxList.Count)
        {
            m_FxList[id - 1].Play();
        }
    }

    public void HideEffect(int id)
    {
        if (id > 0 && id <= m_FxList.Count)
        {
            m_FxList[id - 1].Show(false);
        }
    }

    // 삭제는 유니티 인스펙터 창에서 자동 삭제 처리
    public FXSerialize CreateEffect(string fxName)
    {
        GameObject goPrefab = Resources.Load(fxName) as GameObject;
        if (goPrefab == null)
            return null;

        GameObject go = Instantiate(goPrefab, this.transform);
        return go.GetComponent<FXSerialize>();
    }

}