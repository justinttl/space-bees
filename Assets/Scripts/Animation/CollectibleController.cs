using UnityEngine;
using UnityEngine.Events;

public class CollectibleController : MonoBehaviour
{
    Rigidbody body;
    public bool doRotations = false;
    public FlowerController flower;
    private MeshRenderer mesh;

    private Vector3 offset = new Vector3();
    public float freq = 0.5f;
    public float amp = 1f;

    private UnityAction<Vector3> collectJumpEventListener;

    public enum CollectState
    {
        Hiding,
        Jumping,
        Rotating
    }

    public CollectState collectState;

    // Start is called before the first frame update

    private int groundContactCount = 0;
    public bool IsGrounded
    {
        get
        {
            return groundContactCount > 0;
        }
    }
    void Awake()
    {
        body = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();

        collectJumpEventListener = new UnityAction<Vector3>(collectJumpEventHandler);
    }

    /*
    void OnEnable()
    {
        EventManager.StartListening<CollectJumpEvent, Vector3>(collectJumpEventListener);

    }

    void OnDisable()
    {
        EventManager.StopListening<CollectJumpEvent, Vector3>(collectJumpEventListener);
    }
    */

    private void Start()
    {
        EventManager.StartListening<CollectJumpEvent, Vector3>(collectJumpEventListener);
    }

    // Update is called once per frame
    void Update()
    {

        switch (collectState)
        {
            case CollectState.Hiding:
                body.useGravity = false;
                mesh.enabled = false;
                /*
                if (flower.jump)
                {
                    doJump();
                }
                */
                break;

            case CollectState.Jumping:
                body.useGravity = true;
                mesh.enabled = true;

                if (body.velocity == Vector3.zero)
                {
                    offset = transform.position;
                    collectState = CollectState.Rotating;
                }
                break;

            case CollectState.Rotating:
                transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
                Vector3 height = new Vector3(offset.x, offset.y + amp * (Mathf.Sin(Time.fixedTime * Mathf.PI * freq)), offset.z);
                transform.position = height;
                break;

        }
    }

    void collectJumpEventHandler(Vector3 worldPos)
    {
        doJump();
    }

    public void doJump()
    {
        Vector3 direction = Random.insideUnitSphere;
        Vector3 torqueDir = Random.insideUnitSphere;
        float force = Random.Range(10f, 50f);
        float torqueForce = Random.Range(1f, 5f);
        body.AddForce((Vector3.up) * force, ForceMode.Impulse);
        body.AddTorque((torqueDir + Vector3.up) * torqueForce, ForceMode.Impulse);
        // EventManager.TriggerEvent<BombBounceEvent, Vector3>(body.position);
        collectState = CollectState.Jumping;

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.gameObject.tag == "ground")
        {
            --groundContactCount;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            Collector c = other.attachedRigidbody.gameObject.GetComponent<Collector>();
            if (c != null)
            {
                EventManager.TriggerEvent<SpeedUpSoundEvent, Vector3>(other.transform.position);
                Destroy(this.gameObject);
                c.ReceiveBall("Speed", 1.3f);
            }
        }

        if (other.transform.gameObject.tag == "ground")
        {
            --groundContactCount;
        }

    }
}
