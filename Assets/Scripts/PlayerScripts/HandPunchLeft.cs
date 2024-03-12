using UnityEngine;

public class HandPunchLeft : MonoBehaviour
{
    private PlayerAttack playerAttack;
    public float amount = 10f;
    public float knockbackDistance = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        playerAttack = GetComponentInParent<PlayerAttack>();
        if (playerAttack == null)
        {
            Debug.LogError("Cannot find Player Attack component.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject colObj = other.gameObject;
        if (colObj.tag == "Enemy")
        {
            playerAttack.DoDamage(colObj, amount, knockbackDistance);
        }
    }
}
