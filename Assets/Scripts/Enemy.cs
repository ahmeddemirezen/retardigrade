using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable {
    public Collider rangeCollider;
    public float health = 100f;
    public float damage = 10f;
    public float fireRate = 3f;
    public bool isGuided = true;
    public bool isDead = false;
    [Space]
    [Header ("Events")]
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnDeath;
    Bullet bullet;
    bool isFire = false;

    private void Start () {
        OnHealthChange.Invoke (health);
    }

    private void Update () {
        if (isDead) return;
        if (rangeCollider.bounds.Contains (GameManager.player.transform.position)) {
            Debug.Log ("Player in range");
            if (!isFire) {
                StartCoroutine (Fire ());
            }
        }
    }

    IEnumerator Fire () {
        isFire = true;
        bullet = PoolManager.ReuseObject (GameManager.bullet, transform.position, transform.rotation).GetComponent<Bullet> ();
        Debug.Log (bullet);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.targetTag = "Player";
        bullet.damage = damage;
        bullet.target = GameManager.player.transform.position;
        bullet.targetTransform = GameManager.player.transform;
        bullet.isGuided = isGuided;
        bullet.gameObject.SetActive (true);
        yield return new WaitForSeconds (fireRate);
        isFire = false;
    }
    bool IDamageable.IsDead => isDead;
    public void TakeDamage (float amount) {
        if (isDead) return;
        health -= amount;
        OnHealthChange.Invoke (health);
        if (health <= 0) {
            OnDeath.Invoke ();
            isDead = true;
        }
    }
}