using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    // Start is called before the first frame update
    public void StartGame()
    {
        Debug.Log("Restart!");
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartingMenu");
    }
}
