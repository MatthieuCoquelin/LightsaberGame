using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using EzySlice;
using System;

public class Slicer : MonoBehaviour
{
    [SerializeField] private Material m_materialAfterSlice;
    [SerializeField] private LayerMask m_sliceMask;
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_audioClip;


    private void OnEnable()
    {
        SliceListener.OnSlicerEnter += SliceGameObject;
        SliceListener.OnSliceHappen += SliceSound;
        SliceListener.OnSlicerExit += SliceEnd;
    }

    private void OnDisable()
    {
        SliceListener.OnSlicerEnter -= SliceGameObject;
        SliceListener.OnSliceHappen -= SliceSound;
        SliceListener.OnSlicerExit -= SliceEnd;
    }

    private void SliceGameObject()
    {
        Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, m_sliceMask);

        foreach (Collider objectToBeSliced in objectsToBeSliced)
        {
            SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, m_materialAfterSlice);

            GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, m_materialAfterSlice);
            GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, m_materialAfterSlice);

            upperHullGameobject.transform.position = objectToBeSliced.transform.position;
            lowerHullGameobject.transform.position = objectToBeSliced.transform.position;


            MakeItPhysical(upperHullGameobject);
            MakeItPhysical(lowerHullGameobject);

            Destroy(objectToBeSliced.gameObject);
        }
    }


    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();
        obj.layer = LayerMask.NameToLayer("Sliceable");
        Destroy(obj, 1.5f);
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }

    private void SliceSound(int layer)
    {
        if ( layer == LayerMask.NameToLayer("Sliceable"))
        {
            m_audioSource.PlayOneShot(m_audioClip);
        }
    }

    private void SliceEnd()
    {
        SliceListener.IsSlicing = false;
    }

}
