using UnityEngine;
using UnityEngine.UI;

public class BeeCounterController : MonoBehaviour
{
    public Text text;
    private int startingBees;

    // Start is called before the first frame update
    void Start()
    {
        startingBees = LevelManager.Instance.enemiesAlive;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = (startingBees - LevelManager.Instance.enemiesAlive).ToString() + " / " + startingBees.ToString();
    }
}
