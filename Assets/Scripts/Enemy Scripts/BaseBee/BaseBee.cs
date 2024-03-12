using UnityEngine;
using UnityEngine.Events;

public class BaseBee : MonoBehaviour, IKillable, IDamageable, IHealth
{
    [HideInInspector]
    public UnityEvent damagedEvent = new UnityEvent();

    [HideInInspector]
    public UnityEvent deathEvent = new UnityEvent();

    public float health = 1;

    public GameObject damagetext;

    public void Kill()
    {
        deathEvent.Invoke();
        return;
    }

    public void Damage(float amount)
    {
        if (isDead()) return;
        LoseHealth(amount);
        if (health <= 0) Kill();
        else damagedEvent.Invoke();
    }
    virtual public void Knockback(Vector3 source, float distance)
    { }

    public void GainHealth(float amount)
    {
        health += amount;
    }

    public void LoseHealth(float amount)
    {
        DamageNumberController indicator = Instantiate(damagetext, transform.position, Quaternion.identity).GetComponent<DamageNumberController>();
        indicator.SetDamage(amount);
        health -= amount;
    }
    public bool isDead()
    {
        return health <= 0;
    }

}
