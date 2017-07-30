using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIAMovement : MonoBehaviour {

    public float m_positionRange, m_timeToRecalculate;
    public bool m_recalculateOrigin;

	public bool m_avoidPlayer;
	public float m_avoidFrequence;

    private Vector3 m_origin;
    private float m_timeLeftToRecalculate;

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

    private Vector3 getNextTargetPosition()
    {
		Character playerCharacter = CharactersManager.Instance.getPlayerController ().controlledCharacter;

		if (m_avoidPlayer && playerCharacter!= null && Random.Range (0.0f, 1.0f) < m_avoidFrequence) 
		{
			Vector3 delta = new Vector3 ();
			delta.x = transform.position.x - playerCharacter.transform.position.x ;
			delta.z = transform.position.z - playerCharacter.transform.position.z ;
			delta.Normalize ();

			delta *= m_positionRange;

			return delta;
		}
		else 
		{
			Vector3 delta = new Vector3();
			if (m_recalculateOrigin)
			{
				m_origin = gameObject.transform.position;
			}
			delta.x = Random.Range(m_origin.x - m_positionRange, m_origin.x + m_positionRange);
			delta.z = Random.Range(m_origin.z - m_positionRange, m_origin.z + m_positionRange);
			return delta;
		}

    }

    public void SetEnabled(bool enabled)
    {
        this.enabled = enabled;
        m_navMeshAgent.enabled = enabled;
    }

    //void OnDrawGizmosSelected()
    //{
    //    // position range
    //    Gizmos.color = new Color(0, 0, 1, 0.2f);
    //    Gizmos.DrawSphere(m_origin, m_positionRange);

    //    // target position
    //    Gizmos.color = new Color(0, 0, 1);
    //    Gizmos.DrawSphere(m_navMeshAgent.destination, 0.2f);
    //}
}
