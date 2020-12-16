using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Sound 클래스
 *   
 *   SFX (= Sound Effects 의 약어 )
 */
public class CSound : MonoBehaviour
{
    public bool m_isBGM = false;
    [HideInInspector] public AudioSource m_AudioSource = null;


    private void Start()
    {
        if (m_AudioSource == null)
            m_AudioSource = GetComponent<AudioSource>();

        Initialize_Bgm();
        Initialize_SFX();
    }

    private void Initialize_Bgm()
    {
        if (m_isBGM)
        {
            if (LocalSave.Inst().IsUseBgm && (m_AudioSource.loop || m_AudioSource.playOnAwake))
                m_AudioSource.Play();
            else
                m_AudioSource.Stop();
        }
    }
    private void Initialize_SFX()
    {
        if (!m_isBGM)
        {
            if (LocalSave.Inst().IsUseSFX && (m_AudioSource.loop || m_AudioSource.playOnAwake))
                m_AudioSource.PlayOneShot(m_AudioSource.clip);
            else
                m_AudioSource.Stop();
        }
    }

    public void Play()
    {
        if (m_isBGM && LocalSave.Inst().IsUseBgm)
            m_AudioSource.Play();

        if (!m_isBGM && LocalSave.Inst().IsUseSFX)
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
    }

    public void Stop()
    {
        m_AudioSource.Stop();

    }
 }

/*
 * BGM 옵션만으로 sound on/off 하기 
 *  
 */
public class CSound2 : MonoBehaviour
{
    [HideInInspector] public AudioSource m_AudioSource = null;


    private void OnEnable()
    {
        if (m_AudioSource == null)
            m_AudioSource = GetComponent<AudioSource>();

        if (LocalSave.Inst().IsUseBgm && (m_AudioSource.loop || m_AudioSource.playOnAwake))
            m_AudioSource.Play();
        else
            m_AudioSource.Stop();
    }


    public void Play()
    {
        if (LocalSave.Inst().IsUseBgm)
            m_AudioSource.Play();
    }

    public void Stop()
    {
        m_AudioSource.Stop();
    }

}
