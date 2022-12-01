using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    private bool m_isCountered = false;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Tower" && m_isCountered == true) || other.gameObject.tag == "Ship")
        {
            Destroy(gameObject);
        }
    }

    public bool IsCountered
    {
        get
        {
            return m_isCountered;
        }
        set
        {
            m_isCountered = value;
        }
    }

}
