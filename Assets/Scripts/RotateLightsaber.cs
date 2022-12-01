using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLightsaber : MonoBehaviour
{ 
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.up, Time.fixedDeltaTime * 25f);
    }
}
