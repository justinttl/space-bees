using UnityEngine;
using UnityEngine.UI;

public class PowerUpLabel : MonoBehaviour
{

    public Text text;
    public float life = 1f;
    public float minDist = 2f;
    public float maxDist = 3f;

    private Vector3 startPos;
    private Vector3 endPos;
    private float timer;

    public PowerUpController collectible;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(collectible.powerup);
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        SetText(collectible.powerup);
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
        /*
        timer += Time.deltaTime;

        if (timer > life) Destroy(gameObject);
        else if (timer > life / 2f) text.color = Color.Lerp(text.color, Color.clear, (timer - (life / 2f)) / (life / 2f));

        transform.position = Vector3.Lerp(startPos, endPos, Mathf.Sin(timer / life));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / life));
        */
    }

    public void SetText(string textIn)
    {
        text.text = textIn;
    }
}
