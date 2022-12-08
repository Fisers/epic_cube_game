using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public GameObject ball;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Vector3 camera_position = Camera.main.transform.position;
        Vector3 cube_position = Vector3.zero;
        Vector3 direction = cube_position - camera_position;

        transform.RotateAround(Vector3.zero, -direction, Input.GetAxis("Horizontal"));
        transform.RotateAround(Vector3.zero, -Vector3.Cross(direction, Vector3.up), Input.GetAxis("Vertical"));
    }
}
