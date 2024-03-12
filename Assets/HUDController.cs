using UnityEngine;
using UnityEngine.Events;

public class HUDController : MonoBehaviour
{

    private UnityAction<float> HUDToggleListener;
    // Start is called before the first frame update
    bool HUDon = false;

    CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            Debug.Log("Canvas Group could not be found");
        HUDToggleListener = new UnityAction<float>(HUDToggleEventHandler);
    }

    void OnEnable()
    {
        EventManager.StartListening<HUDToggleEvent, float>(HUDToggleListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<HUDToggleEvent, float>(HUDToggleListener);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (HUDon)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
        }
    }

    void HUDToggleEventHandler(float impactForce)
    {
        HUDon = true;
    }
}
