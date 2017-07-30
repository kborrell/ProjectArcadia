using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public void SetMovementEnabled(bool enabled)
    {
        m_movementEnabled = enabled;
    }

    public void SetEnabled(bool enabled)
    {
        this.enabled = enabled;
    }

    private void Start()
    {
    }

    void Update()
    {
        if (m_movementEnabled)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            if (moveHorizontal != 0 || moveVertical != 0)
            {
                m_facingDirection = new Vector3(moveHorizontal, 0, moveVertical) * ((m_invertedMovement) ? -1 : 1);
				m_facingDirection.Normalize();
                transform.position += m_facingDirection * m_speed * Time.deltaTime;
            }
        }
    }

    public float GetSpeed() { return m_speed; }
    public Vector3 GetFacingDirection()
    {
        return m_facingDirection;
    }
    [SerializeField] private float m_speed = 1.0f;
    [SerializeField] private bool m_invertedMovement = false;
    private Vector3 m_facingDirection;
    private bool m_movementEnabled = true;

    public void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, m_facingDirection);
    }
}
