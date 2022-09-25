using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public bool isGuided = false;
    public float damage = 10f;
    public string targetTag;
    public Vector3 target;
    public Transform targetTransform;
    public float speed = 10f;
    public float lifeTime = 3f;
    public float distance = 100f;
    public float distanceTraveled = 0f;
    public bool isTravelling = false;
    public ParticleSystem hitEffect;

    private void OnEnable () {
        distanceTraveled = 0f;
        isTravelling = true;
        hitEffect.Play ();
    }

    private void Update () {
        if (isTravelling) {
            transform.Translate (((isGuided ? targetTransform.position : target) - transform.position).normalized * speed * Time.deltaTime);
            distanceTraveled += speed * Time.deltaTime;
            if (distanceTraveled >= distance) {
                isTravelling = false;
                gameObject.SetActive (false);
                PoolManager.ReturnObject (gameObject);
            }
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.CompareTag (targetTag) && other.gameObject.TryGetComponent<IDamageable> (out var damageable)) {
            damageable.TakeDamage (damage);
        }
        if (other.gameObject.CompareTag ("Range") || other.gameObject.CompareTag ("Player") || other.gameObject.CompareTag ("Enemy")) return;
        PoolManager.ReturnObject (gameObject);
    }
}