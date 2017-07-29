using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public void SetMovementEnabled(bool enabled)
    {
        m_movementEnabled = enabled;
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
                transform.position += move * _speed * Time.deltaTime;
            }
        }
    }

    [SerializeField] private float _speed = 1.0f;

    private bool m_movementEnabled = true;
}
