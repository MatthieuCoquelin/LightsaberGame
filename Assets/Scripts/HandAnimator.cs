using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimator : MonoBehaviour
{
    [SerializeField]
    private InputActionProperty m_triggerAnimationAction;

    [SerializeField]
    private InputActionProperty m_gripAnimationAction;

    [SerializeField]
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = m_triggerAnimationAction.action.ReadValue<float>();
        m_animator.SetFloat("Trigger", triggerValue);
        
        float gripValue = m_gripAnimationAction.action.ReadValue<float>();
        m_animator.SetFloat("Grip", gripValue);
    }
}
