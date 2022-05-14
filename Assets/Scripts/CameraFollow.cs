using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float smoothSpeed;
    [SerializeField] Vector3 offset;
    [SerializeField] Transform target;

    void Start()
    {
        transform.position = target.position + offset;
    }
    void LateUpdate()
    {
        Follow();
    }
    void Follow()
    {
        Vector3 desiredPosition = new Vector3(0, target.position.y + offset.y, target.position.z + offset.z);
        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = SmoothedPosition;
    }
}
