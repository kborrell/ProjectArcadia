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
        Initialize();
    }

    void Update()
    {
        if (playerToFollow != null)
        {
            var playerPosition = playerToFollow.position;
            var transformRotationEulerAngles = transform.rotation.eulerAngles.x;

            cameraCenter = new Vector2(playerPosition.x, playerPosition.z);
            cameraPosition = new Vector3((cameraCenter.x),
                                         elevationY * Mathf.Sin(Mathf.Deg2Rad * (transformRotationEulerAngles)) + 1.0f, //Deal
                                         (cameraCenter.y) - (elevationY * Mathf.Cos(Mathf.Deg2Rad * (transformRotationEulerAngles)))
                                        );

            // Smooth movement
            transform.position = Vector3.Lerp(transform.position, cameraPosition, cameraSpeed);

            orthCamera.orthographicSize = zoom;
        }
    }

    public void SetPlayerTarget(Transform player)
    {
        playerToFollow = player;
    }
}