using UnityEngine;

public class MeleeBee : BaseBee, IAlertable
{
    private MeleeBeeController controller;
    private MeleeBeeMovement movement;

    private void Start()
    {
        controller = GetComponent<MeleeBeeController>();
        movement = GetComponent<MeleeBeeMovement>();
        damagedEvent.AddListener(controller.Stun);
        deathEvent.AddListener(controller.Kill);
    }

    public void AlertToEnemy(GameObject enemy)
    {
        Debug.Log("Alerted!");
        controller.target = enemy;
    }

    override public void Knockback(Vector3 source, float distance)
    {
        StartCoroutine(movement.Move(transform.position + (transform.position - source).normalized * distance, .5f));
    }
}
