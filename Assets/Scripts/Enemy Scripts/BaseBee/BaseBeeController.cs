using UnityEngine;

public abstract class BaseBeeController : MonoBehaviour, IController
{
    public AIState state;
    public float stunDuration = 0;
    public float attackDamage;
    public float attackRange;
    public float attackInterval;

    public float lastAttackTime;

    public GameObject target;

    public bool targetInRange
    {
        get
        {
            return target != null && Vector3.Distance(transform.position, target.transform.position) <= attackRange;
        }
    }

    public bool canAttack
    {
        get { return Time.time - lastAttackTime >= attackInterval; }
    }

    void Update()
    {
        if (stunDuration > 0)
        {
            stunDuration = Mathf.Max(0, stunDuration - Time.deltaTime);
        }
        else
        {
            TransitionState();
            ExecuteAnimations();
        }
    }

    protected abstract void TransitionState();

    protected abstract void ExecuteAnimations();

}
