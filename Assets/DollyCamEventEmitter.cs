using UnityEngine;

public class DollyCamEventEmitter : MonoBehaviour
{
    public void ExecuteHUDToggle()
    {
        EventManager.TriggerEvent<HUDToggleEvent, float>(0f);
    }
    public void ExecutePlayerControlEvent()
    {
        EventManager.TriggerEvent<PlayerControlEvent, Vector3>(transform.position);
    }
    public void StartEncounter()
    {
        EventManager.TriggerEvent<EncounterStartEvent>();
    }
}
