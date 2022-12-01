using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CounterListener : MonoBehaviour
{
    public static event Action<Collider> OnCounterEnter;
    public static event Action OnCounterHappen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Counterable") && other.gameObject.GetComponent<Projectile>().IsCountered == false)
        {
            OnCounterEnter?.Invoke(other);
            OnCounterHappen?.Invoke();
        }
    }

}
