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
        if (isTransitioning)
        {
            // Smoothly move toward the new room position
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref velocity,
                roomSpeed
            );

            // Stop transitioning when close enough
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
                isTransitioning = false;
        }
        else
        {
            // Follow player normally
            transform.position = new Vector3(
                player.position.x + lookAhead,
                transform.position.y,
                transform.position.z
            );

            lookAhead = Mathf.Lerp(
                lookAhead,
                aheadDistance * player.localScale.x,
                Time.deltaTime * cameraSpeed
            );
        }
    }

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


        /*
            void Update()
            {
               // room camera
                // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), 
               //     ref velocity, speed);


                transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
                lookAhead = Mathf.Lerp(lookAhead, aheadDistance * player.localScale.x, Time.deltaTime * cameraSpeed);
            }

            public void MoveToNewRoom(Transform newRo)
            {
                currentPosX = newRo.position.x;
            }
        */
    }
