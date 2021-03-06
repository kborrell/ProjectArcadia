﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIAMovement : MonoBehaviour {

    public float m_positionRange, m_timeToRecalculate, m_escapeRangeMin = 10, m_escapeRangeMax = 20;
    public bool m_recalculateOrigin;

	public bool m_avoidPlayer;
	public float m_avoidFrequence;

	private bool m_angry = false;

    private Vector3 m_origin, m_facingDirection;
    private float m_timeLeftToRecalculate;

	public GameObject AngrySprite;

    private UnityEngine.AI.NavMeshAgent m_navMeshAgent;
    void Awake()
    {
        // init navmeshagent
        m_navMeshAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_navMeshAgent.speed = gameObject.GetComponent<CharacterMovement>().GetSpeed();
        m_navMeshAgent.angularSpeed = 0;
		m_navMeshAgent.updateRotation = false;

        // init position
        m_origin = gameObject.transform.position;
        m_timeLeftToRecalculate = m_timeToRecalculate;
        m_navMeshAgent.destination = getNextTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if ((m_timeLeftToRecalculate -= Time.deltaTime) <= 0)
        {
            m_timeLeftToRecalculate = m_timeToRecalculate;
            m_navMeshAgent.destination = getNextTargetPosition();
        }
    }

    public bool IsMoving()
    {
		if (!enabled) {
			return false;
		}
        if (!m_navMeshAgent.pathPending)
        {
            if (m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
            {
                if (!m_navMeshAgent.hasPath || m_navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private Vector3 getNextTargetPosition()
    {
		Character playerCharacter = CharactersManager.Instance.getPlayerController ().controlledCharacter;

        Vector3 delta = new Vector3();

		if (!m_avoidPlayer && m_angry)
		{
			delta = playerCharacter.transform.position;
		}
		else if (m_avoidPlayer && playerCharacter != null && /*Random.Range (0.0f, 1.0f) < m_avoidFrequence &&*/
			Vector3.Distance(transform.position, playerCharacter.transform.position) < m_escapeRangeMax/* &&
            Vector3.Distance(transform.position, playerCharacter.transform.position) > m_escapeRangeMin*/)
		{
			delta.x = transform.position.x - playerCharacter.transform.position.x ;
			delta.z = transform.position.z - playerCharacter.transform.position.z ;
			delta.Normalize ();

			delta *= m_positionRange;

			delta += transform.position;
		}
		else 
		{
			if (m_recalculateOrigin)
			{
				m_origin = gameObject.transform.position;
			}
			delta.x = Random.Range(m_origin.x - m_positionRange, m_origin.x + m_positionRange);
			delta.z = Random.Range(m_origin.z - m_positionRange, m_origin.z + m_positionRange);
		}
			
        m_facingDirection = delta - transform.position;
		
        m_facingDirection.Normalize();

        return delta;

    }

    public void SetEnabled(bool enabled)
    {
        this.enabled = enabled;
        m_navMeshAgent.enabled = enabled;
    }

    public Vector3 GetFacingDirection()
    {
        return m_facingDirection;
    }

    void OnDrawGizmosSelected() {
        //{
        //    // position range
        //    Gizmos.color = new Color(0, 0, 1, 0.2f);
        //    Gizmos.DrawSphere(m_origin, m_positionRange);

        // position range
        if(m_avoidPlayer)
        {
            Gizmos.color = new Color(1, 0, 0, 0.2f);
            Gizmos.DrawSphere(transform.position, m_escapeRangeMin);
            Gizmos.DrawSphere(transform.position, m_escapeRangeMax);
        }

        if (!IsMoving())
        {
            Gizmos.color = new Color(0, 1, 1);
            Gizmos.DrawSphere(transform.position, 0.3f);
        }


        //    // target position
        //    Gizmos.color = new Color(0, 0, 1);
        //    Gizmos.DrawSphere(m_navMeshAgent.destination, 0.2f);

        //Gizmos.DrawRay(transform.position, m_facingDirection);
    }


	public void SetOrigin(Vector3 newOrigin)
	{
		m_origin = newOrigin;
	}

	public void SetScared()
	{
		Debug.Log("I GOT SCARED");
		m_avoidPlayer = true;
		m_avoidFrequence = 1;
		m_timeLeftToRecalculate = 0;
	}

	public void SetAngry()
	{
		Debug.Log("I GOT ANGRY");
		m_angry = true;
		m_timeToRecalculate = 0.1f;
		m_timeToRecalculate = 0;
		AngrySprite.SetActive(true);
		/*foreach (Renderer rend in GetComponentsInChildren<Renderer>())
		{
			rend.material.color = Color.red;
		}*/
	}
	public bool IsAngry()
	{
		return m_angry;
	}
}
