using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    
    private void Start()
    {

    }

    void Update()
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