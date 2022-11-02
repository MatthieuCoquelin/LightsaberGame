using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TeleportToogle : MonoBehaviour
{
    [SerializeField]
    private InputActionReference m_teleportToogleBtn;

    public UnityEvent OnTeleportActivate;
    public UnityEvent OnTeleportCancel;

    private void OnEnable()
    {
        m_teleportToogleBtn.action.performed += ActivateTeleport;
        m_teleportToogleBtn.action.canceled += DeactivateTeleport;
    }

    private void OnDisable()
    {
        m_teleportToogleBtn.action.performed -= ActivateTeleport;
        m_teleportToogleBtn.action.canceled -= DeactivateTeleport;
    }

    private void ActivateTeleport(InputAction.CallbackContext context)
    {
        OnTeleportActivate.Invoke();
    }
    
    private void DeactivateTeleport(InputAction.CallbackContext context)
    {
        Invoke("TurnOffTeleport", .1f);
    }

    private void TurnOffTeleport()
    {
        OnTeleportCancel.Invoke();
    }

}
