using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class SliceListener : MonoBehaviour
{
    public static event Action OnSlicerEnter;
    public static event Action<int> OnSliceHappen;
    public static event Action OnSlicerExit;

    public static bool IsSlicing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsSlicing )
        {
            IsSlicing = true;
            OnSlicerEnter?.Invoke();
            OnSliceHappen?.Invoke(other.gameObject.layer);
        }
    } 
    
    private void OnTriggerExit(Collider other)
    {
        OnSlicerExit?.Invoke();
    }



}
