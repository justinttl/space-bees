using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class DeathMenuToggle : MonoBehaviour
{
    CanvasGroup canvasGroup;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            Debug.Log("Canvas Group could not be found");
    }
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        Time.timeScale = 1f;
        LevelManager.Instance.deathEvent.AddListener(DeathMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeathMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        Time.timeScale = 0f;
    }
}
