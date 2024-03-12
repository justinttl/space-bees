using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    Player player;

    void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogError("Player cannot be found.");
        }
        player = playerObj.GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player script cannot be found.");
        }
    }

    void Start()
    {
        healthBar.maxValue = player.stats.maxHealth;
    }


    void Update()
    {
        healthBar.value = player.stats.health;
    }
}
