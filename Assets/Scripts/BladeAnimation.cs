using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class BladeAnimation : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private XRGrabInteractable m_grabbable;
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private List<AudioClip> m_audioClipList;
    [SerializeField] private GameObject m_slicer;

    private bool m_powerState = false;

    private void Start()
    {
        m_slicer.SetActive(false);
        gameObject.GetComponent<Collider>().enabled = false;
        m_grabbable.activated.AddListener(PlayAnimation);
    }


    private void PlayAnimation(ActivateEventArgs arg0)
    {
        if (m_powerState)
        {
            StartCoroutine(PlayAnimatorDelayed("Base Layer.BladeAnimatorPowerOff", 0.8f));
            m_audioSource.PlayOneShot(m_audioClipList[1]);
        }
        else
        {
            StartCoroutine(PlayAnimatorDelayed("Base Layer.BladeAnimatorPowerOn", 0.5f));
            m_audioSource.PlayOneShot(m_audioClipList[0]);
        }
    }

    private IEnumerator PlayAnimatorDelayed(string animator, float delay)
    {
        yield return new WaitForSeconds(delay);
        m_animator.Play(animator);
    }



    public void OnEndAnimator()
    {
        gameObject.GetComponent<Collider>().enabled = !gameObject.GetComponent<Collider>().enabled;
        m_powerState = !m_powerState;
        if (!m_powerState)
            m_slicer.SetActive(false);
        else
            m_slicer.SetActive(true);
    }

    public bool IsOn
    {
        get
        {
            return m_powerState;
        }
    }

}
