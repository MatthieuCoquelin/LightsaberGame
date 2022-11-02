using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateGrabRay : MonoBehaviour
{
    [SerializeField]
    private GameObject m_leftGrabRay;
    
    [SerializeField]
    private GameObject m_rightGrabRay;


    [SerializeField]
    private XRDirectInteractor m_leftDirectGrab;
    
    [SerializeField]
    private XRDirectInteractor m_rightDirectGrab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_leftGrabRay.SetActive(m_leftDirectGrab.interactablesSelected.Count == 0);
        m_rightGrabRay.SetActive(m_rightDirectGrab.interactablesSelected.Count == 0);
    }
}
