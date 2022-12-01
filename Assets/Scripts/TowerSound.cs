using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSound : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_shootAudioClip;
    [SerializeField] private AudioClip m_destroyAudioClip;

    private void OnEnable()
    {
        Tower.OnTowerShooting += PlayShootSound;
        Tower.OnTowerDestroy += PlayDestroySound;
    }

    private void OnDisable()
    {
        Tower.OnTowerShooting -= PlayShootSound;
        Tower.OnTowerDestroy -= PlayDestroySound;
    }

    private void PlayShootSound()
    {
        m_audioSource.PlayOneShot(m_shootAudioClip);
    }

    private void PlayDestroySound()
    {  
        m_audioSource.PlayOneShot(m_destroyAudioClip, 0.2f);
    }


}
