using UnityEngine;
using UnityEditor;

public class GameMgr
{
    static GameMgr instance = null;

    public static GameMgr Inst
    {
        get
        {
            if (instance == null)
                instance = new GameMgr();

            return instance;

        }
    }

    public GameInfo m_GameInfo = new GameInfo();
    public SaveInfo m_SaveInfo = new SaveInfo();

    private GameScene m_GameScene = null;

    public bool IsInstalled { get; set; }
    public void SetGameScene(GameScene scene)
    {
        m_GameScene = scene;
    }

    public void Initialize()
    {
        IsInstalled = true;
        Application.runInBackground = true;
    }

    public void ReStart()
    {
        m_GameInfo.Initialize();
    }

    

    public GameScene gameScene { get { return m_GameScene; } }
    public void LoadFile()
    {
        m_SaveInfo.LoadFile();
    }
    public void SaveFile()
    {
        m_SaveInfo.SaveFile();
    }

    public void InitStart()
    {
        m_GameInfo.Initialize();
    }

    public void OnUpdate(float delta)
    {
        m_GameInfo.OnUpdate(delta);//게임씬에 게임스테이트
    }
    public AssetStage GetCurAssetStage()
    {
        return AssetMgr.Inst.GetAssetStage(m_GameInfo.m_nStage);
    }
    public bool IsGameStage()
    {
        if (m_GameScene != null)
            return m_GameScene.m_BattleFSM.IsGameState();

        return false;
    }
}


//public class GameMgr : Singleton<GameMgr>
//{


//    public bool m_bGameStart = false;
//    public int m_Score = 0;

//    public GameScene m_Gamescene = null;
//    public GameInfo m_GameInfo = new GameInfo();

//    public void SetGameScene(GameScene scene)
//    {
//        m_Gamescene = scene;
//    }

//    public void Initialize()
//    {

//    }

//    public void OnUpdate(float delta)
//    {
//        m_GameInfo.OnUpdate(delta);
//    }

//    public void LoadFile()
//    {

//    }
//}