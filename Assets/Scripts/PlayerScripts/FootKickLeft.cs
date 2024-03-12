using UnityEngine;

public class FootKickLeft : MonoBehaviour
{
    private PlayerAttack player;
    public float amount = 15f;
    public float knockbackDistance = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponentInParent<PlayerAttack>();
        if (player == null)
        {
            Debug.LogError("Cannot find Player Attack component.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject colObj = other.gameObject;
        if (colObj.tag == "Enemy")
        {
            player.DoDamage(colObj, amount, knockbackDistance);
        }
    }
}
