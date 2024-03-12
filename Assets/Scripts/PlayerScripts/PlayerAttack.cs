using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Player))]
public class PlayerAttack : MonoBehaviour
{
    private Player player;
    public new Camera camera;

    void Awake()
    {
        player = GetComponent<Player>();

        if (player == null)
            Debug.Log("Player script could not be found");
    }
    public void DoDamage(GameObject colObj, float damageAmount, float knockbackDistance)
    {
        // play sound (Cause event)
        // damage number UI event
        EventManager.TriggerEvent<PunchEvent, Vector3>(transform.position);
        if (colObj != null)
        {
            var damageScript = colObj.GetComponent<IDamageable>();
            damageScript.Damage(damageAmount * player.stats.dmgMod);
            damageScript.Knockback(transform.position, knockbackDistance);

            Debug.Log("Attack for: " + damageAmount * player.stats.dmgMod);
        }
    }
    public void ThrowProjectile()
    {
        Debug.Log("Throwing Projectile");
        Vector3 rightHand = this.transform.Find("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:RightShoulder/mixamorig1:RightArm/mixamorig1:RightForeArm/mixamorig1:RightHand").position;

        Quaternion cameraRotation = camera.transform.rotation;
        Vector3 cameraForward = camera.transform.forward;

        Vector3 lookPos = cameraForward;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;

        Quaternion upwardAdjustmentRotation = Quaternion.Euler(-22, 0, 0);
        Rigidbody projectile = RangedCombat.SpawnAndThrowProjectile(rightHand, cameraRotation * upwardAdjustmentRotation, 10, player.stats.projectilePrefab);

        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.source = gameObject;
        projectileScript.hurtTags = new List<string> { "Enemy" };
        projectileScript.explosionDamage = player.stats.projectileDamage;
        projectileScript.explosionRadius = player.stats.projectileRadius;
    }
}
