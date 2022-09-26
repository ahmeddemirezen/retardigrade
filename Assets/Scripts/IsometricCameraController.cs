using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCameraController : MonoBehaviour {
    [Header ("Components")]
    public Camera cam;
    public Rigidbody rb;
    public GameObject body;
    public Animator anim;

    [Header ("Interaction Settings")]
    public float interactDistance = 5f;

    [Header ("Camera Settings")]
    public float zoomSpeed = 1f;
    public float zoomMin = 1f;
    public float zoomMax = 10f;
    public float zoom = 1f;
    public float panSpeed = 1f;
    public float panMin = 1f;
    public float panMax = 10f;
    public float pan = 1f;

    [Header ("Animation Settings")]
    [Tooltip ("Bool param")]
    public string IdleState = "Idle";
    [Tooltip ("Bool param")]
    public string WalkState = "Walk";
    [Tooltip ("Bool param")]
    public string RunState = "Run";
    [Tooltip ("Trigger param")]
    public string AttackState = "Attack";
    [Tooltip ("Bool param")]
    public string DeathState = "Death";
    [Tooltip ("Bool param")]
    public string EatState = "Eat";

    [Header ("Inputs")]
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode dash = KeyCode.LeftShift;
    public KeyCode attack = KeyCode.Mouse0;
    public KeyCode interact = KeyCode.E;
    public KeyCode zoomIn = KeyCode.Q;
    public KeyCode zoomOut = KeyCode.E;

    private Vector3 _mousePosition;
    private Vector3 _mousePositionLastFrame;
    private Vector3 _mousePositionDelta;

    private Vector3 _lookDirection;

    private void Start () {
        rb = GetComponent<Rigidbody> ();
    }

    private void Update () {
        Move ();

        if (Input.GetKey (zoomIn)) {
            zoom -= zoomSpeed * Time.deltaTime;
        }
        if (Input.GetKey (zoomOut)) {
            zoom += zoomSpeed * Time.deltaTime;
        }
        if (Input.GetKey (interact)) {
            Interact ();
        }
        if (Input.GetKey (attack)) {
            Attack ();
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
    private void Move () {
        if (Input.GetKey (moveUp) && !Input.GetKey (moveDown) && !Input.GetKey (moveLeft) && !Input.GetKey (moveRight)) {
            transform.position += (Vector3.forward + Vector3.right) * panSpeed * pan * Time.deltaTime;
            body.transform.eulerAngles = new Vector3 (body.transform.rotation.eulerAngles.x, Mathf.LerpAngle (body.transform.rotation.eulerAngles.y, 45f, 0.1f), body.transform.rotation.eulerAngles.z);
            anim.SetBool (IdleState, false);
            anim.SetBool (WalkState, true);
        }
        if (Input.GetKey (moveDown) && !Input.GetKey (moveUp) && !Input.GetKey (moveLeft) && !Input.GetKey (moveRight)) {
            transform.position += (Vector3.back + Vector3.left) * panSpeed * pan * Time.deltaTime;
            body.transform.eulerAngles = new Vector3 (body.transform.rotation.eulerAngles.x, Mathf.LerpAngle (body.transform.rotation.eulerAngles.y, 225f, 0.1f), body.transform.rotation.eulerAngles.z);
            anim.SetBool (IdleState, false);
            anim.SetBool (WalkState, true);
        }
        if (Input.GetKey (moveLeft) && !Input.GetKey (moveRight) && !Input.GetKey (moveUp) && !Input.GetKey (moveDown)) {
            transform.position += (Vector3.forward + Vector3.left) * panSpeed * pan * Time.deltaTime;
            body.transform.eulerAngles = new Vector3 (body.transform.rotation.eulerAngles.x, Mathf.LerpAngle (body.transform.rotation.eulerAngles.y, 315f, 0.1f), body.transform.rotation.eulerAngles.z);
            anim.SetBool (IdleState, false);
            anim.SetBool (WalkState, true);
        }
        if (Input.GetKey (moveRight) && !Input.GetKey (moveLeft) && !Input.GetKey (moveUp) && !Input.GetKey (moveDown)) {
            transform.position += (Vector3.back + Vector3.right) * panSpeed * pan * Time.deltaTime;
            body.transform.eulerAngles = new Vector3 (body.transform.rotation.eulerAngles.x, Mathf.LerpAngle (body.transform.rotation.eulerAngles.y, 135f, 0.1f), body.transform.rotation.eulerAngles.z);
            anim.SetBool (IdleState, false);
            anim.SetBool (WalkState, true);
        }

        if (Input.GetKey (moveUp) && Input.GetKey (moveLeft)) {
            transform.position += Vector3.forward * panSpeed * pan * Time.deltaTime;
            body.transform.eulerAngles = new Vector3 (body.transform.rotation.eulerAngles.x, Mathf.LerpAngle (body.transform.rotation.eulerAngles.y, 0, 0.1f), body.transform.rotation.eulerAngles.z);
            anim.SetBool (IdleState, false);
            anim.SetBool (WalkState, true);
        }
        if (Input.GetKey (moveUp) && Input.GetKey (moveRight)) {
            transform.position += Vector3.right * panSpeed * pan * Time.deltaTime;
            body.transform.eulerAngles = new Vector3 (body.transform.rotation.eulerAngles.x, Mathf.LerpAngle (body.transform.rotation.eulerAngles.y, 90f, 0.1f), body.transform.rotation.eulerAngles.z);
            anim.SetBool (IdleState, false);
            anim.SetBool (WalkState, true);
        }
        if (Input.GetKey (moveDown) && Input.GetKey (moveLeft)) {
            transform.position += Vector3.left * panSpeed * pan * Time.deltaTime;
            body.transform.eulerAngles = new Vector3 (body.transform.rotation.eulerAngles.x, Mathf.LerpAngle (body.transform.rotation.eulerAngles.y, 270f, 0.1f), body.transform.rotation.eulerAngles.z);
            anim.SetBool (IdleState, false);
            anim.SetBool (WalkState, true);
        }
        if (Input.GetKey (moveDown) && Input.GetKey (moveRight)) {
            transform.position += Vector3.back * panSpeed * pan * Time.deltaTime;
            body.transform.eulerAngles = new Vector3 (body.transform.rotation.eulerAngles.x, Mathf.LerpAngle (body.transform.rotation.eulerAngles.y, 180f, 0.1f), body.transform.rotation.eulerAngles.z);
            anim.SetBool (IdleState, false);
            anim.SetBool (WalkState, true);
        }

        if (!Input.GetKey (moveUp) && !Input.GetKey (moveDown) && !Input.GetKey (moveLeft) && !Input.GetKey (moveRight)) {
            anim.SetBool (IdleState, true);
            anim.SetBool (WalkState, false);
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
                anim.SetBool (EatState, true);
                return;
            }
        }
    }

    public void StopEating () {
        anim.SetBool (EatState, false);
    }

    private void Attack () {
        RaycastHit hit;
        if (Physics.Raycast (cam.ScreenPointToRay (Input.mousePosition), out hit) && clickable) {
            StartCoroutine (ClickDelay ());
            var damageable = hit.collider.GetComponent<IDamageable> ();
            if (damageable != null) {
                if (GameManager.player.Fire (hit.point))
                    anim.SetTrigger (AttackState);
                return;
            }
            if (GameManager.player.Fire (new Vector3 (hit.point.x, GameManager.player.transform.position.y, hit.point.z)))
                anim.SetTrigger (AttackState);
        }
    }

    IEnumerator ClickDelay () {
        clickable = false;
        yield return new WaitForSeconds (0.1f);
        clickable = true;
    }

    private void OnCollisionStay (Collision other) {
        var hitPos = new Vector3 (other.contacts[0].point.x, 0, other.contacts[0].point.z);
        var myPos = new Vector3 (transform.position.x, 0, transform.position.z);
        var direction = (myPos - hitPos).normalized;
        // stops the camera from getting stuck in walls
        transform.position += direction;
    }

    private void OnDrawGizmos () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube (transform.position, new Vector3 (1, 1, 1));

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere (transform.position, interactDistance);
    }

}