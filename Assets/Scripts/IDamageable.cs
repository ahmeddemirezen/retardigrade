using UnityEngine;
internal interface IDamageable
{
    void TakeDamage(float amount);
    bool IsDead { get; }
}