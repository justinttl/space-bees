using UnityEngine;
using UnityEngine.Events;

public class RangedBee : BaseBee, IAlertable
{
    private RangedBeeController controller;

    private RangedBeeMovement movement;

    private void Start()
    {
        controller = GetComponent<RangedBeeController>();
        movement = GetComponent<RangedBeeMovement>();
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
