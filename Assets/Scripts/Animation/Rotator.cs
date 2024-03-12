using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Vector3 offset = new Vector3();
    public float freq = 1f;
    public float amp = 2f;
    private void Start()
    {
        offset = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        Vector3 height = new Vector3(offset.x, offset.y + amp * Mathf.Sin(Time.fixedTime * Mathf.PI * freq), offset.z);
        transform.position = height;
    }
}
