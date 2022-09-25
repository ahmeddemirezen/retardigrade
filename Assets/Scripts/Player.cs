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

    private void Start () {
        OnHealthUpgrade.Invoke (maxHealth);
        OnHealthChange.Invoke (health);
        OnStrengthChange.Invoke (GameManager.strength);
        OnDexterityChange.Invoke (GameManager.dexterity);
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

    public IEnumerator Degradation (float amount) {
        while (health > 0) {
            TakeDamage (amount);
            yield return new WaitForSeconds (1f);
        }
    }

    public void TakeDamage (float amount) {
        health -= amount;
        OnHealthChange.Invoke (health);
    }
}