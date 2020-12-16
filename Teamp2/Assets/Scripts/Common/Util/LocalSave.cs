using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSave 
{
    //싱글톤 클래스 ---------------------------------------------
    private static LocalSave _Instance = null;
    public static LocalSave Inst() 
    {
        if (_Instance == null)
            _Instance = new LocalSave();

        return _Instance;
    }
    //------------------------------------------------------------

    // 멤버 변수
    public bool IsUseBgm { get; set; }
    public bool IsUseSFX { get; set; }


    private LocalSave()
    {
        IsUseBgm = true;
        IsUseSFX = true;
    }

    // 멤버 함수
    public void Load()
    {
        int value = PlayerPrefs.GetInt("bgm", 1);
        IsUseBgm = (value == 1);

        int value2 = PlayerPrefs.GetInt("sfx", 1);
        IsUseSFX = (value2 == 1);

    }


    public void Save()
    {
        int value = IsUseBgm ? 1 : 0;
        PlayerPrefs.SetInt("bgm", value);

        int value2 = IsUseSFX ? 1 : 0;
        PlayerPrefs.SetInt("sfx", value2);
    }


}
