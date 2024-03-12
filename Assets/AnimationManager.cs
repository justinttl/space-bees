using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Animator))]
public class AnimationManager : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rbody;

    private UnityAction<string> animationEventListener;


    void Awake()
    {
        animationEventListener = new UnityAction<string>(animationEventHandler);

        anim = GetComponent<Animator>();
        if (anim == null)
            Debug.Log("Animator could not be found");

        rbody = GetComponent<Rigidbody>();
        if (rbody == null)
            Debug.Log("Rigid body could not be found");
    }


    // Use this for initialization
    void Start()
    {
    }


    void OnEnable()
    {
        EventManager.StartListening<DoAnimationEvent, string>(animationEventListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<DoAnimationEvent, string>(animationEventListener);
    }

    void animationEventHandler(string animation)
    {
        anim.SetTrigger(animation);
    }
}
