using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketmanCamera : MonoBehaviour
{
    [SerializeField] Transform Rocketman;
    [SerializeField] Vector3 Offset;

    [SerializeField] float smoothSpeed = 0.125f;

    private void Update()
    {      
        Vector3 desiredPos = Rocketman.position + Offset;
        Vector3 smootedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smootedPos;

        transform.LookAt(Rocketman);
    }
}
