
using UnityEngine;
using UnityEngine.UI;

public class BossBeeHealthBar : MonoBehaviour
{
    public Slider healthBar;
    public BossBee boss;

    void Start()
    {
        healthBar.maxValue = boss.health;
    }


    void Update()
    {
        healthBar.value = boss.health;
        if (boss.isDead())
        {
            healthBar.gameObject.SetActive(false);
        }
    }
}
