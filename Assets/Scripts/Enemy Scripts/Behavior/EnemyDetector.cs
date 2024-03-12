using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyDetector : MonoBehaviour
{
    public string enemyTag;
    private IAlertable alertable;

    void Awake()
    {
        alertable = GetComponentInParent<IAlertable>();
        if (alertable == null)
        {
            Debug.LogError("Cannot find alertable component.");
        }
    }

    void Start()
    {
        SphereCollider detectionSphere = GetComponent<SphereCollider>();
        float originalRadius = detectionSphere.radius;
        detectionSphere.radius = 0;
        detectionSphere.radius = originalRadius;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            alertable.AlertToEnemy(other.gameObject);
        }
    }
}
