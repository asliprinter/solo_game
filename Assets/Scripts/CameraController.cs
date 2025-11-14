using UnityEngine;

public class CameraController : MonoBehaviour
{
    //room-based camera movement
    [SerializeField] private float roomSpeed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    private bool isTransitioning = false;

    //player reference
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;


    void Update()
    {
            transform.position = new Vector3(
                player.position.x + lookAhead,
                player.position.y + 2.5f,
                transform.position.z
            );

            lookAhead = Mathf.Lerp(
                lookAhead,
                aheadDistance * player.localScale.x,
                Time.deltaTime * cameraSpeed
            );
    }

    //not using it?
    public void MoveToNewRoom(Transform newRo)
    {
        // Start smooth transition toward the new room
        targetPosition = new Vector3(
            newRo.position.x,
            newRo.position.y,
            transform.position.z
        );
        isTransitioning = true;
    }
 }
