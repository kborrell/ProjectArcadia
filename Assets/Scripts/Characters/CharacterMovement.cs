using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public void SetMovementEnabled(bool enabled)
    {
        m_movementEnabled = enabled;
    }

    private void Start()
    {
        if (m_invertedMovement)
        {
            m_speed = -m_speed;
        }
    }

    void Update()
    {
        if (m_movementEnabled)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            if (moveHorizontal != 0 || moveVertical != 0)
            {
                Vector3 move = new Vector3(moveHorizontal, 0, moveVertical);
                transform.position += move * m_speed * Time.deltaTime;
            }
        }
    }

    [SerializeField] private float m_speed = 1.0f;
    [SerializeField] private bool m_invertedMovement = false;
    private bool m_movementEnabled = true;
}
