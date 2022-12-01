using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform m_origin;
    [SerializeField] private GameObject m_projectile;
    [SerializeField] private BladeAnimation m_blade;
    [SerializeField] private float m_lifetime;
    [SerializeField] private float m_maxDelta;
    [SerializeField] private float m_minDelta;


    public static event Action OnTowerShooting;
    public static event Action OnTowerDestroy;

    private float m_speed;
    private bool m_canBeginShoot;

    void Awake()
    {
        m_speed = 100f;
        m_canBeginShoot = false;
    }


    private void Update()
    {
        if (m_blade.IsOn && !m_canBeginShoot)
        {
            m_canBeginShoot = true;
            StartCoroutine(ShootCoroutine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile")
        {
            OnTowerDestroy?.Invoke();
            Destroy(gameObject, 0.25f);
        }    
    }

    private IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(m_minDelta, m_maxDelta));
        Shoot();
        while (m_canBeginShoot)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(m_minDelta, m_maxDelta));
            Shoot();
        }
    }

    private void Shoot()
    {
        InstantiatePojectile();
        OnTowerShooting?.Invoke();
    }

    private void InstantiatePojectile()
    {
        GameObject projectile = Instantiate(m_projectile);

        projectile.transform.position = m_origin.position;
        projectile.transform.rotation = m_origin.rotation;
        projectile.GetComponent<Rigidbody>().velocity = m_speed * Time.fixedDeltaTime * m_origin.up;
        
        Destroy(projectile, m_lifetime);
    }

    private void OnDestroy()
    {
        MenuManager.enemies--;
    }

}
