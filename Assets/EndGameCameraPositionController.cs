using UnityEngine;

public class EndGameCameraPositionController : MonoBehaviour
{
    public GameObject queen;
    public GameObject player;

    public Vector3 camPos;
    public Vector3 lineToFollow;

    private
    // Start is called before the first frame update
    void Start()
    {
        lineToFollow = queen.transform.position - player.transform.position;
        camPos = player.transform.position - (5 * lineToFollow.normalized);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lineToFollow = queen.transform.position - player.transform.position;
        camPos = player.transform.position - (5 * lineToFollow.normalized) + new Vector3(0f, 2f, 0f);

        this.transform.position = camPos;
    }
}
