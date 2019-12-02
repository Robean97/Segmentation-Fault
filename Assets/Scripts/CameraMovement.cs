using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform followedObject;
    public float smoothingCameraMovement;
    public Vector2 maxCameraPosition;
    public Vector2 minCameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position != followedObject.position)
        {
            Vector3 followedObjectPosition = new Vector3(followedObject.position.x,
                followedObject.position.y, transform.position.z);
            followedObjectPosition.x = Mathf.Clamp(followedObjectPosition.x,
                minCameraPosition.x, maxCameraPosition.x);
            followedObjectPosition.y = Mathf.Clamp(followedObjectPosition.y,
                minCameraPosition.y, maxCameraPosition.y);
            transform.position = Vector3.Lerp(transform.position,
                followedObjectPosition, smoothingCameraMovement);
        }
    }
}
