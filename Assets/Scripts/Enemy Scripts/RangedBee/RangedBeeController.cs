using System.Collections.Generic;
using UnityEngine;

public class RangedBeeController : BaseBeeController
{
    public float rangedAttackRadius = 4;
    public float rangedAttackProjectileSpeed = 20f;
    public Rigidbody projectilePrefab;

    private RangedBee rangedBee;
    private RangedBeeMovement movement;
    private Vector3 mouthPos
    {
        get { return this.transform.Find("RigRMouthGizmo").position; }
    }

    void Awake()
    {
        state = AIState.Patrol;
        movement = GetComponent<RangedBeeMovement>();
        rangedBee = GetComponent<RangedBee>();
    }


    protected override void TransitionState()
    {
        if (state == AIState.Dead) { return; }
        else if (target)
        {
            if (targetInRange && targetInSight) state = AIState.Attack;
            else state = AIState.Chase;
        }
        else { state = AIState.Patrol; }
    }

    protected override void ExecuteAnimations()
    {
        switch (state)
        {
            case AIState.Dead:
                break;
            case AIState.Patrol:
                break;
            case AIState.Attack:
                if (canAttack)
                {
                    movement.Attack(target);
                    lastAttackTime = Time.time;
                }
                break;
            case AIState.Chase:
                movement.Chase(target);
                break;
        }
    }

    // Callback from Spell Attack animation
    private void FireProjectileAtTarget()
    {
        if (target == null) return;

        // Spawn Projectile

        Rigidbody projectile = RangedCombat.SpawnAndFireProjectile(mouthPos, target.transform.position, rangedAttackProjectileSpeed, projectilePrefab);

        // Configure Projectile
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.source = gameObject;
        projectileScript.hurtTags = new List<string> { target.tag };
        projectileScript.explosionDamage = attackDamage;
        projectileScript.explosionRadius = rangedAttackRadius;
    }
    private bool targetInSight
    {
        get
        {
            if (target == null) return false;

            bool rayHitCollider = Physics.Raycast(
                mouthPos, target.transform.position - mouthPos, out RaycastHit hit, attackRange,
                Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore
            );

            return Vector3.Distance(hit.point, target.transform.position) < rangedAttackRadius;
        }
    }

    public void Stun()
    {
        stunDuration = 1f;
        movement.Damaged();
    }
    public void Kill()
    {
        state = AIState.Dead;
        movement.Die();
    }
}
