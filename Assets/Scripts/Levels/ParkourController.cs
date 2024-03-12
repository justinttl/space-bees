using UnityEngine;

public class ParkourController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject colObj = other.gameObject;
        if (colObj.tag == "Player")
        {
            Destroy(this);
        }
    }
}
