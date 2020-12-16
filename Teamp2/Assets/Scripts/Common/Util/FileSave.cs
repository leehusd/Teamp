using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileSave  
{
    public delegate void DelegateReadFunc(BinaryReader br);
    public delegate void DelegateWriteFunc(BinaryWriter bw);

    public DelegateReadFunc m_ReadFunc = null;
    public DelegateWriteFunc m_WriteFunc = null;

    public bool SaveFile( string sPathName, DelegateWriteFunc func)
    {
        if( m_WriteFunc == null )
            m_WriteFunc = new DelegateWriteFunc(func);

        FileStream fs =  new FileStream(sPathName, FileMode.Create, FileAccess.Write);
        BinaryWriter bw = new BinaryWriter(fs);

        if (m_WriteFunc != null)
            m_WriteFunc(bw);

        bw.Close();
        fs.Close();

        return true;
    }

    public bool LoadFile(string sPathName, DelegateReadFunc func)
    {
        if( m_ReadFunc == null )
            m_ReadFunc = new DelegateReadFunc(func);

        FileStream fs = new FileStream(sPathName, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);

        if (m_ReadFunc != null)
            m_ReadFunc(br);

        br.Close();
        fs.Close();
        return true;
    }
}




//public bool TestLoadFile()
//{

//    FileStream fs = new FileStream("D:/data.bin", FileMode.Create, FileAccess.Write);
//    BinaryWriter bw = new BinaryWriter(fs);

//    int i = 100;
//    float f = 123.34f;
//    double d = 456789.1234;
//    string str = "cafe.naver.com";

//    bw.Write(i);
//    bw.Write(f);
//    bw.Write(d);
//    bw.Write(str);

//    bw.Close();
//    fs.Close();

//    return true;
//}

//public bool TestSaveFile()
//{

//    FileStream fs = new FileStream("D:/data.bin", FileMode.Open, FileAccess.Read);
//    BinaryReader br = new BinaryReader(fs);

//    int i = br.ReadInt32();
//    float f = br.ReadSingle();
//    double d = br.ReadDouble();
//    string str = br.ReadString();

//    Debug.Log(i + " " + f + " " + d + " " + str);

//    br.Close();
//    fs.Close();
//    return true;
//}