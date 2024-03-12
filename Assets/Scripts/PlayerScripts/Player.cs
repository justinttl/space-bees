using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour, IDamageable, IKillable, IHealth
{
    public PlayerStats stats;

    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();

        if (anim == null)
            Debug.Log("Animator could not be found");

        if (stats == null)
            Debug.Log("Stats object not found");
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead())
        {
            Kill();
        }
    }

    public void Damage(float amount)
    {
        LoseHealth(amount);
        EventManager.TriggerEvent<PlayerLandsEvent, Vector3, float>(transform.position, 400f);
        if (!isDead())
        {
            anim.SetTrigger("GetHit");
        }
        // Could play sound?
    }
    public void Knockback(Vector3 source, float distance)
    {

    }

    public bool isDead()
    {
        return stats.health <= 0;
    }

    public void Kill()
    {
        anim.SetBool("Death", true);
    }

    public void GainHealth(float amount)
    {
        stats.health += amount;
    }

    public void LoseHealth(float amount)
    {
        stats.health -= amount;
    }

}
