using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject m_menu;
    [SerializeField] private InputActionProperty m_showButton;

    // Update is called once per frame
    void Update()
    {
        if(m_showButton.action.WasPressedThisFrame())
        {
            m_menu.SetActive(!m_menu.activeSelf);           
        }
    }
}
