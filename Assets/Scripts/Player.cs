using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable {
    [Header ("Properties")]
    public float maxHealth = 100;
    public float health = 100f;
    public float bitePower = 10f;
    public float acidPower = 10f;
    public float fireRate = 5f;
    public float speed = 10f;
    public float glidingDistance = 10f;
    public float degradation = 0.1f;

    [Space]
    [Header ("Events")]
    public UnityEvent<float> OnHealthChange;
    public UnityEvent<float> OnStrengthChange;
    public UnityEvent<float> OnDexterityChange;
    [Space]
    [Header ("Upgrade Events")]
    public UnityEvent<float> OnHealthUpgrade;
    public UnityEvent<float> OnBitePowerUpgrade;
    public UnityEvent<float> OnAcidPowerUpgrade;
    public UnityEvent<float> OnSpeedUpgrade;
    public UnityEvent<float> OnGlidingDistanceUpgrade;
    [Space]
    [Header ("Other Events")]
    public UnityEvent<float> OnFireSetup;
    public UnityEvent<float> OnFire;

    //privates
    bool isFire = false;

    private void Start () {
        OnHealthUpgrade.Invoke (maxHealth);
        OnHealthChange.Invoke (health);
        OnStrengthChange.Invoke (GameManager.strength);
        OnDexterityChange.Invoke (GameManager.dexterity);
        OnFireSetup.Invoke (fireRate);
        StartCoroutine (Degradation (degradation));
    }

    public void Bite (MineralType mineralType, float amount) {
        switch (mineralType) {
            case MineralType.Blue:
                GameManager.dexterity += amount;
                OnDexterityChange.Invoke (GameManager.dexterity);
                break;
            case MineralType.Red:
                GameManager.strength += amount;
                OnStrengthChange.Invoke (GameManager.strength);
                break;
        }
    }

    public bool Fire (Vector3 target) {
        if (isFire) return false;
        StartCoroutine (FireCoolDown ());
        Bullet bullet = PoolManager.ReuseObject (GameManager.bullet, transform.position, transform.rotation).GetComponent<Bullet> ();
        Debug.Log (bullet);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.targetTag = "Enemy";
        bullet.damage = acidPower;
        bullet.target = target;
        bullet.gameObject.SetActive (true);
        return true;
    }

    IEnumerator FireCoolDown () {
        isFire = true;
        float time = 0;
        while (time < fireRate) {
            time += 0.1f;
            yield return new WaitForSeconds (0.1f);
            OnFire.Invoke (time);
        }
        isFire = false;
    }

    public IEnumerator Degradation (float amount) {
        while (health > 0) {
            TakeDamage (amount);
            yield return new WaitForSeconds (1f);
        }
    }
    // TODO: Implement IDamageable interface
    bool IDamageable.IsDead => false;
    public void TakeDamage (float amount) {
        health -= amount;
        OnHealthChange.Invoke (health);
    }
}