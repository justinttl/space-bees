using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float explosionRadius = 3f;
    private float explosionEffectsRadiusScalingFactor = 0.66f;
    public float explosionForce = 5f;
    public float explosionDamage = 10f;
    public List<string> hurtTags = new List<string> { "Enemy" };
    public GameObject source;

    public float lifetime = 3f;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == source)
        {
            return;
        }

        SpawnExplosion();
        DoAreaDamage();
        Destroy(gameObject);
    }

    private void DoAreaDamage()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider obj in objectsInRange)
        {
            // Check if object is damage-able and the intended recipient of damage
            if (hurtTags.Contains(obj.tag))
            {
                if (obj.GetComponent<IDamageable>() is IDamageable damageable)
                {
                    damageable.Damage(explosionDamage);

                    // Apply Knockback
                    Rigidbody rb = obj.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 1.5f, ForceMode.Impulse);
                    }
                }
            }
        }
    }

    private void SpawnExplosion()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        EventManager.TriggerEvent<BombBounceEvent, Vector3>(transform.position);
        // Set scale on child objects to scale particle effects.
        // It is unclear why scaling the parent explosion does not work.
        foreach (Transform childTransform in explosion.transform)
        {
            float effectRadius = explosionRadius * explosionEffectsRadiusScalingFactor;
            childTransform.localScale = new Vector3(effectRadius, effectRadius, effectRadius);
        }

        Destroy(explosion, 1);
    }

}
