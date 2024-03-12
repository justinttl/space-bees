using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterInputConverter : MonoBehaviour
{
    private float filteredForwardInput = 0f;
    private float filteredTurnInput = 0f;

    private PlayerInput playerinput;

    public bool InputMapToCircular = true;

    public float forwardInputFilter = 5f;
    public float turnInputFilter = 5f;

    private float forwardSpeedLimit = 1f;

    public float rotationSpeed;

    public Transform orientation;

    public bool canMove = false;

    public float Forward
    {
        get;
        private set;
    }

    public float Turn
    {
        get;
        private set;
    }

    public float CamOffset
    {
        get;
        private set;
    }

    public float Velo
    {
        get;
        private set;
    }

    void Start()
    {
    }

    void Update()
    {
        if (canMove)
        {
            //GetAxisRaw() so we can do filtering here instead of the InputManager
            float h = Input.GetAxisRaw("Horizontal");// setup h variable as our horizontal input axis
            float v = Input.GetAxisRaw("Vertical"); // setup v variables as our vertical input axis

            if (InputMapToCircular)
            {
                // make coordinates circular
                //based on http://mathproofs.blogspot.com/2005/07/mapping-square-to-circle.html
                h *= Mathf.Sqrt(1f - 0.5f * v * v);
                v *= Mathf.Sqrt(1f - 0.5f * h * h);

            }

            filteredForwardInput = Mathf.Clamp(Mathf.Lerp(filteredForwardInput, v,
                Time.deltaTime * forwardInputFilter), -forwardSpeedLimit, forwardSpeedLimit);

            filteredTurnInput = Mathf.Lerp(filteredTurnInput, h,
                Time.deltaTime * turnInputFilter);

            Velo = Mathf.Sqrt(filteredForwardInput * filteredForwardInput + filteredTurnInput * filteredTurnInput);

            Forward = filteredForwardInput;
            Turn = filteredTurnInput;
        }
    }

    public static Vector2 rotate(Vector2 v, float delta)
    {
        delta *= Mathf.Deg2Rad;
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

    private UnityAction<Vector3> PlayerControlListener;
    // Start is called before the first frame update

    void Awake()
    {
        PlayerControlListener = new UnityAction<Vector3>(PlayerControlEventHandler);
        playerinput = GetComponent<PlayerInput>();
        if (playerinput == null)
        {
            Debug.Log("Error Getting Player Input");
        }
    }

    void OnEnable()
    {
        EventManager.StartListening<PlayerControlEvent, Vector3>(PlayerControlListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<PlayerControlEvent, Vector3>(PlayerControlListener);
    }

    void PlayerControlEventHandler(Vector3 impactForce)
    {
        if (canMove)
        {
            canMove = false;
        }
        if (!canMove)
        {
            canMove = true;
        }
    }
}
