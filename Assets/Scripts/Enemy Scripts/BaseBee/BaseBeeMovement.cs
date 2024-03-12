using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BaseBeeMovement : MonoBehaviour
{
    public float updateSpeed = 0.1f;
    public string attackAnimationTrigger;

    [System.NonSerialized]
    public NavMeshAgent agent;

    [System.NonSerialized]
    public Animator animator;

    public GameObject currentTarget;
    private IEnumerator chaseCoro;
    private IEnumerator faceTargetCoro;

    private void Awake()
    {
        if (attackAnimationTrigger == null)
        {
            Debug.LogError("Missing Attack Animation Trigger");
        }
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void Chase(GameObject target)
    {
        if (chaseCoro != null && currentTarget == target)
        {
            return;
        }

        Debug.Log($"{gameObject.name} is chasing {target.name}");
        ResetAnimator();
        ResetCoroutines();
        agent.isStopped = false;
        chaseCoro = FollowTarget(target);
        StartCoroutine(chaseCoro);
        currentTarget = target;
    }

    public void Attack(GameObject target)
    {
        ResetAnimator();

        if (faceTargetCoro == null || currentTarget != target)
        {
            ResetCoroutines();
            Debug.Log($"{gameObject.name} is facing {target.name}");
            agent.isStopped = true;
            faceTargetCoro = FaceTarget(target);
            StartCoroutine(faceTargetCoro);
        }

        Debug.Log($"{gameObject.name} is attacking {target.name}");
        animator.SetTrigger(attackAnimationTrigger);
        currentTarget = target;
    }


    public void Damaged()
    {
        ResetAnimator();
        ResetCoroutines();
        agent.isStopped = true;
        animator.SetTrigger("Take Damage");
    }

    public void Die()
    {
        ResetAnimator();
        ResetCoroutines();
        agent.isStopped = true;
        animator.SetTrigger("Die");
    }

    private IEnumerator FollowTarget(GameObject target)
    {
        WaitForSeconds Wait = new WaitForSeconds(updateSpeed);
        while (enabled)
        {
            agent.SetDestination(target.transform.position);
            animator.SetBool("Fly Forward", true);
            yield return Wait;
        }
    }

    public IEnumerator FaceTarget(GameObject target)
    {
        WaitForSeconds Wait = new WaitForSeconds(updateSpeed);
        while (enabled)
        {
            Vector3 lookpos = target.transform.position - transform.position;
            lookpos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookpos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 2f);
            yield return Wait;
        }
    }

    public void ResetAnimator()
    {
        animator.SetBool("Fly Forward", false);
        animator.SetBool("Defend", false);
    }

    public void ResetCoroutines()
    {
        if (chaseCoro != null)
        {
            StopCoroutine(chaseCoro);
        }
        if (faceTargetCoro != null)
        {
            StopCoroutine(faceTargetCoro);
        }
        faceTargetCoro = null;
        chaseCoro = null;
    }
    public IEnumerator Move(Vector3 end, float seconds)
    {
        ResetAnimator();
        ResetCoroutines();

        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        while (elapsedTime < seconds)
        {
            transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = end;
    }
}
