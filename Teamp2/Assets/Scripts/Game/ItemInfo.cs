using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo
{

    public enum Type
    {
        eHealing=1,
        eExplose=2,
    }
    
    public int m_ItemType = 0;
    public float m_ItemValue = 0;


    public void Initialize(int nType,float value)
    {
        m_ItemType = nType;
        m_ItemValue = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Initialize()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
