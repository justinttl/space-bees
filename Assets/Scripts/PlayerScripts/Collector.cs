using UnityEngine;

public class Collector : MonoBehaviour
{
    public bool hasBall = false;
    public Rigidbody ballPrefab;
    private Animator anim;
    private Player player;

    public string powerUp;
    public float speedUp;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();


        if (anim == null)
            Debug.Log("Animator could not be found");

        if (ballPrefab == null)
            Debug.Log("Ball Prefab could not be found");
    }

    // Start is called before the first frame update
    void Start()
    {
        powerUp = null;
        speedUp = 1f;
    }

    public void ReceiveBall(string attribute, float amount)
    {
        Debug.Log(attribute);
        speedUp *= amount;
        hasBall = true;
    }

    public void ReceiveBall(string attribute)
    {
        Debug.Log(attribute);
        powerUp = attribute;
        if (attribute == "Throw")
        {
            player.stats.canThrow = true;
        }
        if (attribute == "Kick")
        {
            player.stats.canKick = true;
        }
    }
}
