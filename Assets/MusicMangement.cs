using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMangement : MonoBehaviour
{
    public AudioSource m_ASource;
    public AudioClip[] m_Music1;
   

   
    void Update()
    {
        if(!m_ASource.isPlaying)
        {
            m_ASource.clip = m_Music1[Random.Range(0, m_Music1.Length)];
            m_ASource.Play();
        }
    }
}
