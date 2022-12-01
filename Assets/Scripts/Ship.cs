using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ship : MonoBehaviour
{
    public static event Action<Collider> OnBladeEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BladeEnemy")
        {
            OnBladeEnter?.Invoke(other);
        }

    }
}
