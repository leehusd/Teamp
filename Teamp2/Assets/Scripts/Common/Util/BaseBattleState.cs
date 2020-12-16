using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/**
 *
 *  * 게임 장면 관리 상태 패턴
 * ( state 패턴 + delegate 패턴 )
 * 
 */
public class BaseBattleState
{
    public class IState
    {
        public delegate void DelegateFunc();
        public DelegateFunc OnCallbackEnter = null;
        public DelegateFunc OnCallbackExit = null;
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }

    // 대기상태 ----------------------------------------
    public class CReadyState : IState
    {
        public override void OnEnter()
        {
            Debug.Log("Enter ReadyState!!");
            if (OnCallbackEnter != null)
                OnCallbackEnter();
        }
        public override void OnExit()
        {
            Debug.Log("Exit WaveState!!");
            if (OnCallbackExit != null)
                OnCallbackExit();
        }
    }

    // wave 상태  ----------------------------------------
    public class CWavestate : IState
    {
        public override void OnEnter(){
            Debug.Log("Enter WaveState!!");
            if (OnCallbackEnter != null)
                OnCallbackEnter();
        }
        public override void OnExit(){
            Debug.Log("Exit WaveState!!");
            if (OnCallbackExit != null)
                OnCallbackExit();
        }
    }

    // 게임진행 상태 ----------------------------------------
    public class CGameState : IState
    {
        public override void OnEnter(){
            Debug.Log("Enter GameState!!");
            if (OnCallbackEnter != null)
                OnCallbackEnter();
            
        }
        public override void OnExit(){
            Debug.Log("Exit GameState!!");
            if (OnCallbackExit != null)
                OnCallbackExit();
        }
    }

    // 결과 상태    ----------------------------------------
    public class CResultState : IState
    {
        public override void OnEnter(){
            Debug.Log("Enter ResultState!!");
            if (OnCallbackEnter != null)
                OnCallbackEnter();
        }
        public override void OnExit(){
            Debug.Log("Exit ResultState!!");
            if (OnCallbackExit != null)
                OnCallbackExit();
        }

    }

    // 잠시 멈춤 상태 ----------------------------------------
    public class CPauseState : IState{
        public override void OnEnter(){
            Debug.Log("Enter PauseState!!");
            if (OnCallbackEnter != null)
                OnCallbackEnter();
        }
        public override void OnExit(){
            Debug.Log("Exit PauseState!!");
            if (OnCallbackExit != null)
                OnCallbackExit();
        }

    }
    //------------------------------------------------------------------

    protected CReadyState m_Ready = new CReadyState();
    protected CWavestate m_Wave = new CWavestate();
    protected CGameState m_Game = new CGameState();
    protected CResultState m_Result = new CResultState();
    protected CPauseState m_Pause = new CPauseState();

    protected IState m_kCurState = null;

    //------------------------------------------------------------
    public void Initialize(IState.DelegateFunc onReady, 
                           IState.DelegateFunc onGame, 
                           IState.DelegateFunc onResult,
                           IState.DelegateFunc onWave = null,
                           IState.DelegateFunc onPause = null)
    {
        SetCallbackEnter_Ready(onReady);
        SetCallbackEnter_Game(onGame);
        SetCallbackEnter_Result(onResult);
        SetCallbackEnter_Wave(onWave);
        SetCallbackEnter_Pause(onPause);
    }
    //------------------------------------------------------------
    public void SetState(IState kState)
    {
        if (m_kCurState != null)
            m_kCurState.OnExit();

        if (m_kCurState != kState){
            m_kCurState = kState;
            m_kCurState.OnEnter();
        }
    }

    //------------------------------------------------------------
    public void SetReadyState() { SetState(m_Ready); }
    public void SetGameState() { SetState(m_Game); }
    public void SetWaveState() { SetState(m_Wave); }
    public void SetResultState() { SetState(m_Result); }
    public void SetPauseState() { SetState(m_Pause); }

    //------------------------------------------------------------
    public bool IsReayState(){
        return m_kCurState == m_Ready ? true : false;
    }
    public bool IsGameState(){
        return m_kCurState == m_Game ? true : false;
    }
    public bool IsWaveState(){
        return m_kCurState == m_Wave ? true : false;
    }
    public bool IsPaueState(){
        return m_kCurState == m_Pause ? true : false;
    }
    public bool IsResultState(){
        return m_kCurState == m_Result ? true : false;
    }
    //------------------------------------------------------------
    public void SetCallbackEnter_Ready(IState.DelegateFunc func)
    {
        if( func != null)
            m_Ready.OnCallbackEnter = new IState.DelegateFunc(func);
    }
    public void SetCallbackExit_Ready(IState.DelegateFunc func)
    {
        if (func != null)
            m_Ready.OnCallbackExit = new IState.DelegateFunc(func);
    }
    //------------------------------------------------------------
    public void SetCallbackEnter_Game(IState.DelegateFunc func)
    {
        if (func != null)
            m_Game.OnCallbackEnter = new IState.DelegateFunc(func);
    }
    public void SetCallbackExit_Game(IState.DelegateFunc func)
    {
        if (func != null)
            m_Game.OnCallbackExit = new IState.DelegateFunc(func);
    }
    //------------------------------------------------------------
    public void SetCallbackEnter_Wave(IState.DelegateFunc func)
    {
        if (func != null)
            m_Wave.OnCallbackEnter = new IState.DelegateFunc(func);
    }
    public void SetCallbackExit_Wave(IState.DelegateFunc func)
    {
        if (func != null)
            m_Wave.OnCallbackExit = new IState.DelegateFunc(func);
    }
    //------------------------------------------------------------
    public void SetCallbackEnter_Result(IState.DelegateFunc func)
    {
        if (func != null)
            m_Result.OnCallbackEnter = new IState.DelegateFunc(func);
    }
    public void SetCallbackExit_Result(IState.DelegateFunc func)
    {
        if (func != null)
            m_Result.OnCallbackExit = new IState.DelegateFunc(func);
    }
    //------------------------------------------------------------
    public void SetCallbackEnter_Pause(IState.DelegateFunc func)
    {
        if (func != null)
            m_Pause.OnCallbackEnter = new IState.DelegateFunc(func);
    }
    public void SetCallbackExit_Pause(IState.DelegateFunc func)
    {
        if (func != null)
            m_Pause.OnCallbackExit = new IState.DelegateFunc(func);
    }
    //------------------------------------------------------------
    public void OnUpdate()
    {

    }


}
