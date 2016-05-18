using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{

    public Transform target;
    public float distanceUp;
    public float distanceBack;
    public float minimumHeight;
    private GameObject player;

    // Not used directly
    private Vector3 positionVelocity;

    void Awake()
    {
<<<<<<< HEAD

    }
    void FixedUpdate()
    {
        if (player != null)//control if there is a player
        {
            // Calculate a new position to place the camera
            Vector3 newPosition = target.position + (target.forward * distanceBack);
            newPosition.y = Mathf.Max(newPosition.y + distanceUp, minimumHeight);

            // Move the camera
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref positionVelocity, 0.18f);

=======
       /*player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();*/
    }
    void FixedUpdate () {
        if (player != null)
        {
            // Calculate a new position to place the camera
            Vector3 newPosition = target.position + (target.forward * distanceBack);
            newPosition.y = Mathf.Max(newPosition.y + distanceUp, minimumHeight);

            // Move the camera
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref positionVelocity, 0.18f);

>>>>>>> origin/master
            // Rotate the camera to look at where the car is pointing
            Vector3 focalPoint = target.position + (target.forward * 5);
            transform.LookAt(focalPoint);
        }
        else {
            player = GameObject.FindGameObjectWithTag("Player");
            target = player.GetComponent<Transform>();
        }
<<<<<<< HEAD
    }
=======
	}
>>>>>>> origin/master
}
