using UnityEngine;
using UnityEngine.UI;

public class DamageNumberController : MonoBehaviour
{

    public Text text;
    public float life = 1f;
    public float minDist = 2f;
    public float maxDist = 3f;

    private Vector3 startPos;
    private Vector3 endPos;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        startPos = transform.position + new Vector3(0f, 2f, 0f) + (LevelManager.Instance.player.transform.position - transform.position) / 2;

        float dir = Random.rotation.eulerAngles.z;
        float dist = Random.Range(minDist, maxDist);
        endPos = startPos + (Quaternion.Euler(0, 0, dir) * new Vector3(dist, dist, 0f));
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > life) Destroy(gameObject);
        else if (timer > life / 2f) text.color = Color.Lerp(text.color, Color.clear, (timer - (life / 2f)) / (life / 2f));

        transform.position = Vector3.Lerp(startPos, endPos, Mathf.Sin(timer / life));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / life));
    }

    public void SetDamage(float dmg)
    {
        text.text = dmg.ToString();
    }
}
