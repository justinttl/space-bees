using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/PlayerStats", order = 0)]
public class PlayerStats : ScriptableObject
{
    [Header("Health")]
    [SerializeField]
    private float baseHealth = 10;

    public float health = 10;

    public float maxHealth = 10;

    [Header("Throw Ability")]
    public bool canThrow = false;
    public Rigidbody projectilePrefab;
    public float projectileDamage = 5;
    public float projectileRadius = 3;

    [Header("Kick Ability")]
    public bool canKick = false;

    [Header("Modifiers")]
    public float dmgMod = 1f;

    public float speedMod = 1f;

    public float jumpHeight = 1f;

    [Header("LevelSelect")]
    public string nextLevel = "Level_1";

    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        health = maxHealth = baseHealth;
        canThrow = canKick = false;
        dmgMod = speedMod = jumpHeight = 1f;
    }
}
