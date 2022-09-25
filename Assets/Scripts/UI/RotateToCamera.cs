using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    public Camera cam;

    private void Start() {
        cam = Camera.main;
    }

    private void Update() {
        // Rotate 180 degrees around the y axis to face the camera
        transform.rotation = Quaternion.LookRotation(cam.transform.position - transform.position ) * Quaternion.Euler(0, 180, 0);
        
    }
}
