using UnityEngine;

public class AttackBeeStinger : MonoBehaviour
{
    private BaseBeeController controller;
    private void Awake()
    {
        controller = GetComponentInParent<BaseBeeController>();
        if (controller == null)
        {
            Debug.Log("Unable to find controller class.");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject colObj = other.gameObject;
        if (colObj.tag == "Player")
        {
            var damageScript = colObj.GetComponent<IDamageable>();
            damageScript.Damage(controller.attackDamage);
        }
    }
}
