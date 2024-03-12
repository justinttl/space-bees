using UnityEngine;

public class MeleeBeeController : BaseBeeController
{
    private MeleeBeeMovement movement;

    void Awake()
    {
        state = AIState.Patrol;
        movement = GetComponent<MeleeBeeMovement>();
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
        switch (state)
        {
            case AIState.Dead:
                break;
            case AIState.Patrol:
                break;
            case AIState.Attack:
                if (targetInRange && canAttack)
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
