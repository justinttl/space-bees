using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedCombat : MonoBehaviour
{
    public static Rigidbody SpawnAndFireProjectile(Vector3 source, Vector3 target, float speed, Rigidbody projectilePrefab)
    {
        Debug.DrawLine(source, target, Color.red, .5f);

        Rigidbody projectile = Instantiate(projectilePrefab, source, Quaternion.identity);
        projectile.transform.LookAt(target, Vector3.up);
        projectile.velocity = projectile.transform.forward * speed;

        return projectile;
    }
    public static Rigidbody SpawnAndThrowProjectile(Vector3 source, Quaternion angle, float force, Rigidbody projectilePrefab)
    {
        Debug.DrawRay(source, angle * Vector3.forward * 10, Color.red, 1);

        Rigidbody projectile = Instantiate(projectilePrefab, source, angle);
        Vector3 throwForce = projectile.transform.forward * force;
        projectile.AddForce(throwForce, ForceMode.Impulse);

        return projectile;
    }
}
