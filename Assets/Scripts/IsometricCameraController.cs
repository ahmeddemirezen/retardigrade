using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCameraController : MonoBehaviour {
    public Camera cam;
    public Rigidbody rb;

    public float interactDistance = 5f;

    public float zoomSpeed = 1f;
    public float zoomMin = 1f;
    public float zoomMax = 10f;
    public float zoom = 1f;
    public float panSpeed = 1f;
    public float panMin = 1f;
    public float panMax = 10f;
    public float pan = 1f;

    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode zoomIn = KeyCode.Q;
    public KeyCode zoomOut = KeyCode.E;

    private Vector3 _mousePosition;
    private Vector3 _mousePositionLastFrame;
    private Vector3 _mousePositionDelta;

    private void Start () {
        rb = GetComponent<Rigidbody> ();
    }

    private void Update () {
        if (Input.GetKey (moveUp)) {
            // transform.position += (Vector3.forward + Vector3.right) * panSpeed * pan * Time.deltaTime;
            rb.AddForce ((Vector3.forward + Vector3.right) * panSpeed * pan * Time.deltaTime);
        }
        if (Input.GetKey (moveDown)) {
            rb.AddForce ((Vector3.back + Vector3.left) * panSpeed * pan * Time.deltaTime);
        }
        if (Input.GetKey (moveLeft)) {
            // transform.position += (Vector3.forward + Vector3.left) * panSpeed * pan * Time.deltaTime;
            rb.AddForce ((Vector3.forward + Vector3.left) * panSpeed * pan * Time.deltaTime);
        }
        if (Input.GetKey (moveRight)) {
            // transform.position += (Vector3.back + Vector3.right) * panSpeed * pan * Time.deltaTime;
            rb.AddForce ((Vector3.back + Vector3.right) * panSpeed * pan * Time.deltaTime);
        }
        if (Input.GetKey (zoomIn)) {
            zoom -= zoomSpeed * Time.deltaTime;
        }
        if (Input.GetKey (zoomOut)) {
            zoom += zoomSpeed * Time.deltaTime;
        }
        if (Input.GetMouseButton (0)) {
            Interact ();
        }

        zoom = Mathf.Clamp (zoom, zoomMin, zoomMax);
        pan = Mathf.Clamp (pan, panMin, panMax);
        cam.orthographicSize = zoom;
        _mousePosition = Input.mousePosition;
        _mousePositionDelta = _mousePosition - _mousePositionLastFrame;
        _mousePositionLastFrame = _mousePosition;
        if (Input.GetMouseButtonDown (2)) {
            transform.position -= _mousePositionDelta * panSpeed * pan * Time.deltaTime;
        }
    }

    private bool clickable = true;
    private void Interact () {
        RaycastHit hit;
        if (Physics.Raycast (cam.ScreenPointToRay (Input.mousePosition), out hit) && clickable) {
            StartCoroutine (ClickDelay ());
            Debug.Log (hit.collider.name);
            var intractable = hit.collider.GetComponent<IIntractable> ();
            if (intractable != null && Vector3.Distance (transform.position, hit.point) < interactDistance) {
                intractable.Interact ();
                return;
            }
            var damageable = hit.collider.GetComponent<IDamageable> ();
            if (damageable != null) {
                GameManager.player.Fire (hit.point);
                return;
            }
            GameManager.player.Fire (new Vector3 (hit.point.x, GameManager.player.transform.position.y, hit.point.z));
        }
    }

    IEnumerator ClickDelay () {
        clickable = false;
        yield return new WaitForSeconds (0.1f);
        clickable = true;
    }

    private void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube (transform.position, new Vector3 (1, 1, 1));

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere (transform.position, interactDistance);
    }

}