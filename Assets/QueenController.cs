using UnityEngine;

public class QueenController : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator anim;
    private bool up;
    float animationPlaying = 0f;
    float newAnim;
    float oldAnim;
    public float life = 1f;
    private float timer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Queen Animator not found.");
        }
    }
    void Start()
    {
        up = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!up)
        {
            if (Random.Range(0f, 200f) < 1f)
            {
                anim.SetTrigger("ChangeSitting");
            }
        }
    }

    public void Cheer()
    {
        anim.SetTrigger("GetUp");
    }
}
