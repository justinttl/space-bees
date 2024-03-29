using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damage(float damageTaken);
    void Knockback(Vector3 source, float distance);
}
