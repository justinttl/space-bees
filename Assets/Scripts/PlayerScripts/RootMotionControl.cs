using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterInputConverter))]
[RequireComponent(typeof(Collector))]

public class RootMotionControl : MonoBehaviour
{

    private Animator anim;
    private Rigidbody rbody;
    private CharacterInputConverter cinput;
    private Collector col;
    private CapsuleCollider capsuleCollider;
    private Player player;

    public GameObject camTarget;
    private bool canDouble = true;

    public float jumpForce = 1f;

    public float vDeadzone = 0.05f;
    public float hDeadzone = 0.05f;

    // classic input system only polls in Update()
    // so must treat input events like discrete button presses as
    // "triggered" until consumed by FixedUpdate()...

    // ...however constant input measures like axes can just have most recent value
    // cached.
    float _inputForward = 0f;
    float _inputTurn = 0f;
    float _camOffset = 0f;
    float _totalInputVel = 0f;

    //Useful if you implement jump in the future...
    public float animationSpeed = 1f;
    public float rootMovementSpeed = 1f;
    public float rootRotationSpeed = 1f;
    public float speedUp = 1f;

    private bool isGrounded = false;

    private string powerUp;

    private bool timeslow = false;

    public void doPunch1()
    {
        if (cinput.canMove)
        {
            anim.SetTrigger("AttackPunch1");
        }
    }
    public void doKick1()
    {
        if (player.stats.canKick)
        {
            anim.SetTrigger("AttackKick1");
        }
    }

    public void doThrow()
    {
        if (player.stats.canThrow)
        {
            anim.SetTrigger("Throw");
        }
    }

    public void doDash()
    {
        if (cinput.canMove)
        {
            anim.SetTrigger("Dash");
            this.tag = "Invulnerable";
            Debug.Log("I Frames!");
        }
    }

    public void doJump()
    {
        if (isGrounded)
        {
            anim.SetTrigger("Jump");
        }
        else if (canDouble)
        {
            canDouble = false;
            anim.SetTrigger("DoubleJump");
        }
    }

    void Awake()
    {
        player = GetComponent<Player>();
        if (player == null)
            Debug.Log("Player could not be found");

        anim = GetComponent<Animator>();
        if (anim == null)
            Debug.Log("Animator could not be found");

        rbody = GetComponent<Rigidbody>();
        if (rbody == null)
            Debug.Log("Rigid body could not be found");

        cinput = GetComponent<CharacterInputConverter>();
        if (cinput == null)
            Debug.Log("CharacterInput could not be found");

        col = GetComponent<Collector>();
        if (col == null)
            Debug.Log("Collector could not be found");

        capsuleCollider = GetComponent<CapsuleCollider>();
        if (capsuleCollider == null)
            Debug.Log("Collider could not be found");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    // Use this for initialization
    void Start()
    {
    }

    private void Update()
    {
        CharacterCommon.CheckGroundNear(rbody.transform.position, 45, 1.1f, 0, out isGrounded);

        if (isGrounded)
        {
            canDouble = true;
        }

        if (cinput.enabled)
        {
            _inputForward = cinput.Forward;
            _inputTurn = cinput.Turn;
            _camOffset = cinput.CamOffset;
            _totalInputVel = cinput.Velo;
        }

        anim.speed = animationSpeed;

        if (col.enabled)
        {
            speedUp = col.speedUp;
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
        {
            //Debug.Log("Enabled");
            this.tag = "Player";
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("General Movement"))
        {
            ResetTriggers();
        }

        if (
            anim.GetCurrentAnimatorStateInfo(0).IsName("Landing") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
        {
            ResetTriggers();
            anim.ResetTrigger("Dash");
        }

    }


    void FixedUpdate()
    {
        anim.SetFloat("SpeedUp", speedUp);
        anim.SetFloat("SidewaysVel", _inputTurn);
        anim.SetFloat("ForwardVel", _inputForward);
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        anim.SetFloat("TotalVel", _totalInputVel);
        anim.SetBool("Moving", Mathf.Abs((Mathf.Abs(_inputForward) + Mathf.Abs(_inputTurn)) * (_inputForward / (Mathf.Abs(_inputForward)))) > 0);
        anim.SetBool("VerticalMovement", (Mathf.Abs(_inputForward) > vDeadzone));
        anim.SetBool("HorizontalMovement", (Mathf.Abs(_inputTurn) > hDeadzone));
        anim.SetFloat("camOffset", _camOffset);
        anim.SetBool("Grounded", isGrounded);
    }


    private void OnAnimatorIK(int layerIndex)
    {
    }

    private void ResetTriggers()
    {
        anim.ResetTrigger("Jump");
        anim.ResetTrigger("AttackPunch1");
        anim.ResetTrigger("AttackKick1");
        anim.ResetTrigger("Throw");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "OB")
        {
            //Kill player
            //Debug.Log("DIE");
            anim.SetBool("Death", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
    }

    void OnAnimatorMove()
    {
        Vector3 newRootPosition;
        Quaternion newRootRotation;

        newRootPosition = anim.rootPosition;
        newRootRotation = anim.rootRotation;

        newRootPosition = Vector3.LerpUnclamped(this.transform.position, newRootPosition, rootMovementSpeed);
        newRootRotation = Quaternion.LerpUnclamped(this.transform.rotation, newRootRotation, rootRotationSpeed);

        rbody.MovePosition(newRootPosition);
        rbody.MoveRotation(newRootRotation);
    }

    public void TimeSlowToggle()
    {
        if (timeslow)
        {
            Time.timeScale = 0.25f;
            timeslow = false;
        }
        else
        {
            Time.timeScale = 1f;
            timeslow = true;
        }
    }
}
