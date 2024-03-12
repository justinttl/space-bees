using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    MainMenuController controller;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    // Start is called before the first frame update
    public void StartGame(string Level)
    {
        controller = GetComponentInParent<MainMenuController>();
        Time.timeScale = 1f;
        controller.stats.nextLevel = Level;
        SceneManager.LoadScene("CutScene");
    }
}
