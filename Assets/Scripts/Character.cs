using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform m_ship = null;
    [SerializeField] private BladeAnimation m_blade = null;
    [SerializeField] private float m_maxDelta;
    [SerializeField] private float m_minDelta;

    private bool m_canBeginMove;
    private NavMeshAgent m_agent;

    private void Awake()
    {
        m_canBeginMove = false;
        m_agent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        Ship.OnBladeEnter += StopCharacter;
    }

    private void OnDisable()
    {
        Ship.OnBladeEnter -= StopCharacter;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_agent.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_blade.IsOn && !m_canBeginMove)
        {
            m_canBeginMove = true;
            StartCoroutine(MoveCoroutine());
        }
    }

    private IEnumerator MoveCoroutine()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(m_minDelta, m_maxDelta));
        m_agent.SetDestination(m_ship.position);
    }

    private void StopCharacter(Collider collider)
    {
        collider.gameObject.transform.parent.transform.parent.GetComponent<NavMeshAgent>().enabled = false;
    }

    private void OnDestroy()
    {
        MenuManager.enemies--;
    }
}
