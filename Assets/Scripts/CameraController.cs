using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * 2;
        float vertical = Input.GetAxis("Mouse Y") * 2;
        transform.RotateAround(Vector3.zero, Vector3.up, horizontal);
        transform.RotateAround(Vector3.zero, transform.right, vertical);
    }
}
