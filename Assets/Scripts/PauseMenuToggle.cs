using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public bool pause = false;
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
        LevelManager.Instance.deathEvent.AddListener(deathBeOntoYou);
    }

    void deathBeOntoYou()
    {
        this.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || pause)
        {
            if (canvasGroup.interactable)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                Time.timeScale = 1f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                Time.timeScale = 0f;
            }
        }
    }

    public void doPause()
    {
        if (pause)
        {
            pause = false;
        }
        else
        {
            pause = true;
        }
    }
}
