using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCam_Script : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed;
   // [SerializeField] private Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + new Vector3 (2,1,-3);
        // Use SmoothDamp to smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
