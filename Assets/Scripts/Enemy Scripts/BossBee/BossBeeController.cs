using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BossBeeController : BaseBeeController
{
    public float rangedAttackRadius = 3;
    public float rangedAttackProjectileSpeed = 15f;
    public PlayerStats stats;

    public int MeleeBeeAdds;
    public int RangedBeeAdds;

    public Rigidbody projectilePrefab;

    private BossBeeMovement movement;
    public float chargeDistance = 15;
    public float chargeDuration = 0.5f;
    public float chargeCooldown = 33f;

    public Vector3 airPhasePosition;

    private Vector3 mouthPos
    {
        get { return this.transform.Find("RigRMouthGizmo").position; }
    }
    public float rangedAttackInterval = 3;

    public bool canRangedAttack
    {
        get { return Time.time - lastAttackTime >= rangedAttackInterval; }
    }

    // Adds
    public List<BaseBee> adds;

    [System.NonSerialized]
    public bool spawnedMeleeBees = false;
    public BaseBee meleeBeePrefab;

    [System.NonSerialized]
    public bool spawnedRangedBees = false;
    public BaseBee rangedBeePrefab;

    public enum Phase
    {
        Idle,
        Ground,
        Air,
        Final
    }
    public Phase currentPhase = Phase.Idle;


    void Awake()
    {
        state = AIState.Patrol;
        movement = GetComponent<BossBeeMovement>();
        currentPhase = Phase.Idle;
    }

    public void StartEncounter()
    {
        target = LevelManager.Instance.player;
        movement.ClawAttack(); // Just for the animation
        currentPhase = Phase.Ground;
    }

    private void Update()
    {
        if (stunDuration > 0)
        {
            stunDuration = Mathf.Max(0, stunDuration - Time.deltaTime);
            return;
        }

        switch (currentPhase)
        {
            case Phase.Ground:
                TransitionState();
                ExecuteAnimations();
                break;
            case Phase.Air:
                if (!movement.inAerialPosition)
                {
                    return;
                }

                if (!spawnedMeleeBees)
                {
                    SpawnAdds(meleeBeePrefab, MeleeBeeAdds, 40);
                    spawnedMeleeBees = true;
                }
                else
                {
                    UpdateAliveAdds();
                }

                movement.attackAnimationTrigger = "Spell Cast";
                state = AIState.Attack;
                ExecuteAnimations();
                break;
            case Phase.Final:
                if (movement.inAerialPosition)
                {
                    return;
                }

                if (!spawnedRangedBees)
                {
                    SpawnAdds(rangedBeePrefab, RangedBeeAdds, 40);
                    spawnedRangedBees = true;
                }
                else
                {
                    UpdateAliveAdds();
                }

                movement.attackAnimationTrigger = "Sting Attack";
                TransitionState();
                ExecuteAnimations();
                break;
        }
    }

    private void UpdateAliveAdds()
    {
        adds = adds.Where(bee => !bee.isDead()).ToList();
    }

    protected override void TransitionState()
    {
        if (state == AIState.Dead) { return; }
        else if (targetInRange) { state = AIState.Attack; }
        else if (target && !targetInRange) { state = AIState.Chase; }
        else { state = AIState.Patrol; }
    }

    protected override void ExecuteAnimations()
    {
        if (movement.isAttacking)
        {
            return;
        }

        switch (state)
        {
            case AIState.Dead:
                break;
            case AIState.Patrol:
                break;
            case AIState.Attack:
                if (currentPhase == Phase.Ground || currentPhase == Phase.Final)
                {
                    if (targetInRange && canAttack)
                    {
                        movement.Attack(target);
                        lastAttackTime = Time.time;
                    }
                    else
                    {
                        movement.Chase(target);
                    }
                }
                else if (currentPhase == Phase.Air)
                {
                    if (canRangedAttack)
                    {
                        movement.Attack(target);
                        lastAttackTime = Time.time;
                    }
                }
                break;
            case AIState.Chase:
                movement.Chase(target);
                break;
        }
    }

    public void Knockback()
    {
        Vector3 knockBackVector = (transform.position - target.transform.position).normalized;
        StartCoroutine(movement.Move(transform.position + knockBackVector * 3, .1f));
    }

    public void Kill()
    {
        state = AIState.Dead;
        stats.nextLevel = "Credits";
        movement.Die();
    }
    public void KillAdds()
    {
        foreach (BaseBee bee in adds)
        {
            bee.Kill();
        }
    }
    public void Charge()
    {
        StartCoroutine(movement.Move(transform.position + transform.forward * chargeDistance, chargeDuration));
    }
    public void PostChargeStun()
    {
        movement.Defend();
        stunDuration = chargeCooldown;
    }
    public void StartAirPhase()
    {
        currentPhase = Phase.Air;
        movement.agent.isStopped = true;
        StartCoroutine(movement.Move(airPhasePosition, 5));
        StartCoroutine(movement.FlyUp(5));
    }

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

    private void SpawnAdds(BaseBee enemyPrefab, int number, float spawnRadius)
    {
        int numSpawned = 0;

        while (numSpawned < number)
        {
            // Sample random location on NavMesh
            bool valid = false;
            NavMeshHit hit;

            Vector3 randomPos = Random.insideUnitSphere * spawnRadius + transform.position;
            valid = NavMesh.SamplePosition(randomPos, out hit, spawnRadius, NavMesh.AllAreas);
            if (!valid)
            {
                continue;
            }

            // Spawn
            BaseBee bee = Instantiate(enemyPrefab, hit.position, Quaternion.identity);
            bee.transform.LookAt(target.transform.position, Vector3.up);

            // Set player aggro
            BaseBeeController controller = bee.gameObject.GetComponent<BaseBeeController>();
            controller.target = target;

            adds.Add(bee);
            numSpawned += 1;
        }
    }
    public void StartFinalPhase()
    {
        currentPhase = Phase.Final;
        StartCoroutine(movement.FlyDown(5));
    }
}
