using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Haptic
{
    [Range(0, 1)]
    [SerializeField] private float m_intensity;
    [SerializeField] private float m_duration;

    public void TriggerHaptic(BaseInteractionEventArgs eventArgs)
    {
        if (eventArgs.interactorObject is XRBaseControllerInteractor controllerInteractor)
            TriggerHaptic(controllerInteractor.xrController);
    }

    public void TriggerHaptic(XRBaseController controller)
    {
        if (m_intensity > 0)
            controller.SendHapticImpulse(m_intensity, m_duration);
    }
}

public class HapticInteractable : MonoBehaviour
{
    [SerializeField] private Haptic hapticOnActivaded; 
    [SerializeField] private Haptic hapticHoverEntered; 
    [SerializeField] private Haptic hapticHoverExited; 
    [SerializeField] private Haptic hapticSelectEntered; 
    [SerializeField] private Haptic hapticSelectExited; 


    // Start is called before the first frame update
    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();

        interactable.activated.AddListener(hapticOnActivaded.TriggerHaptic);
        interactable.hoverEntered.AddListener(hapticHoverEntered.TriggerHaptic);
        interactable.hoverExited.AddListener(hapticHoverExited.TriggerHaptic);
        interactable.selectEntered.AddListener(hapticSelectEntered.TriggerHaptic);
        interactable.selectExited.AddListener(hapticSelectExited.TriggerHaptic);
    }

   
}
