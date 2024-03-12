using UnityEngine;

public class FlowerController : MonoBehaviour
{

    private Animator anim;
    private bool shouldBloom = false;
    private bool bloomed = false;
    public int objLayer = 7;
    public bool jump = false;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null && other.gameObject.layer == 7)
        {
            shouldBloom = true;
            Debug.Log("Should Bloom");
            if (!jump)
            {
                jump = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            shouldBloom = false;
            Debug.Log("Should UnBloom");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        anim.SetBool("bloom", shouldBloom);
        bloomed = shouldBloom;
    }

    public void ExecuteCollectJumpEvent()
    {
        EventManager.TriggerEvent<CollectJumpEvent, Vector3>(transform.position);
    }
}
