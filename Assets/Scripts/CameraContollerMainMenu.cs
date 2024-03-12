using UnityEngine;

public class CameraContollerMainMenu : MonoBehaviour
{

    public float rotateSpeed = 2.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * (Time.deltaTime / rotateSpeed));
    }
}
