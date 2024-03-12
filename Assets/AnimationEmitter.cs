using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEmitter : MonoBehaviour
{
    public PlayerStats stats;
    public void ExecuteAnimation(string animation)
    {
        EventManager.TriggerEvent<DoAnimationEvent, string>(animation);
    }

    public void LoadNext()
    {
        SceneManager.LoadScene(stats.nextLevel);
        Debug.Log("Should Be Changing Scenes to: " + LevelManager.Instance.nextScene);
    }
}
