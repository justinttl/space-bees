using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    Rigidbody body;
    public string powerup;
    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    //private Vector3 offset = new Vector3();
    public float freq;
    public float amp;
    private void Start()
    {
        //offset = transform.position;
        //freq = Random.Range(0.5f, 1.5f);
        //amp = Random.Range(0.1f, 3f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        //Vector3 height = new Vector3(offset.x, offset.y + amp * Mathf.Sin(Time.fixedTime * Mathf.PI * freq), offset.z);
        //transform.position = height;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            Collector c = other.attachedRigidbody.gameObject.GetComponent<Collector>();
            if (c != null)
            {
                EventManager.TriggerEvent<SpeedUpSoundEvent, Vector3>(other.transform.position);
                GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
                foreach (GameObject go in allObjects)
                    if (go.tag == "Pickup")
                        Destroy(go);
                c.ReceiveBall(powerup);
            }
        }
    }
}
