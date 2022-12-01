using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_bullet;

    [SerializeField]
    private Transform m_fireOrigin;
    
    private float m_fireSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_fireSpeed = 1000.0f;
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }

    private void FireBullet(ActivateEventArgs arg0)
    {
        GameObject spawnedBullet = Instantiate(m_bullet);
        spawnedBullet.transform.position = m_fireOrigin.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = m_fireSpeed * Time.deltaTime * m_fireOrigin.forward;
        Destroy(spawnedBullet, 5);
    }




}
