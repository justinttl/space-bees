using UnityEngine;
using System.Collections;

public class BossBeeMovement : BaseBeeMovement
{
    private float initialOffset;
    public float flyingOffset = 0.75f;
    public bool inAerialPosition = false;

    private void Start()
    {
        initialOffset = agent.baseOffset;
    }

    public void Defend()
    {
        ResetAnimator();
        ResetCoroutines();

        agent.isStopped = true;
        animator.SetBool("Defend", true);
    }

    public bool isAttacking
    {
        get
        {
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

            return state.IsName("BossSting") || state.IsName("SpellCast") || state.IsName("Idle -> BossSting") || state.IsName("Idle -> SpellCast");
        }
    }

    public IEnumerator FlyUp(float seconds)
    {
        ResetAnimator();
        ResetCoroutines();

        float elapsedTime = 0;

        while (elapsedTime < seconds)
        {
            agent.baseOffset = Mathf.Lerp(initialOffset, flyingOffset, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        inAerialPosition = true;
    }
    public IEnumerator FlyDown(float seconds)
    {
        ResetAnimator();
        ResetCoroutines();

        float elapsedTime = 0;

        while (elapsedTime < seconds)
        {
            agent.baseOffset = Mathf.Lerp(flyingOffset, initialOffset, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        inAerialPosition = false;
    }
    public void ClawAttack()
    {
        animator.SetTrigger("Claw Attack");
    }

}
