using System.Collections.Generic;
using UnityEngine;

public class CameraController : SingletonMonoBehaviour<CameraController>
{
    [SerializeField]
    private Transform playerToFollow;

    [SerializeField]
    private float cameraSpeed = 0.10f;

    [SerializeField]
    private float cameraAngle = 45f;
    
    [SerializeField]
    private float elevationY = 20.0f;

    [SerializeField]
    private float zoom = 15.0f;

    private Vector2 cameraCenter;
    private Vector3 cameraPosition;
    private Camera orthCamera;

    public void Initialize()
    {
        orthCamera = GetComponent<Camera>();
    }

    void Start()
    {
        //this.transform.Rotate(new Vector3(cameraAngle, 0, 0));
        orthCamera = GetComponent<Camera>();
    }
    
    void Update()
    {
		if (playerToFollow == null) 
		{
			return;
		}
        cameraCenter = new Vector2(playerToFollow.position.x, playerToFollow.position.z);
        
        cameraPosition = new Vector3((cameraCenter.x),
                                     elevationY * Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.x)) + 1.0f, //Deal
                                     (cameraCenter.y) - (elevationY * Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.x)))
                                    );

        // Smooth movement
        transform.position = Vector3.Lerp(transform.position, cameraPosition, cameraSpeed);

        orthCamera.orthographicSize = zoom;
    }

    public void SetPlayerTarget(Transform player)
    {
        playerToFollow = player;
    }
}