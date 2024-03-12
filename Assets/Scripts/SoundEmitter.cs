using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public void ExecutePunch()
    {
        EventManager.TriggerEvent<PunchEvent, Vector3>(transform.position);
    }

    public void ExecuteKick()
    {
        EventManager.TriggerEvent<KickEvent, Vector3>(transform.position);
    }

    public void ExecuteStep()
    {
        EventManager.TriggerEvent<FootstepEvent, Vector3>(transform.position);
    }

    public void ExecuteJump()
    {
        EventManager.TriggerEvent<JumpEvent, Vector3>(transform.position);
    }

    public void ExecutePlayerLand()
    {
        EventManager.TriggerEvent<PlayerLandsEvent, Vector3, float>(transform.position, 305f);
    }

    public void ExecuteOpenDoor()
    {
        EventManager.TriggerEvent<DoorOpenEvent, Vector3>(transform.position);
    }

    public void ExecuteDeathEvent()
    {
        EventManager.TriggerEvent<DeathEvent, GameObject>(this.gameObject);
    }

    public void ExecuteSpeedUpSoundEvent()
    {
        EventManager.TriggerEvent<SpeedUpSoundEvent, Vector3>(transform.position);
    }

    public void Death()
    {
        LevelManager.Instance.deathEvent.Invoke();
    }

    public void HitboxToggle(int Toggle)
    {
        EventManager.TriggerEvent<HitboxToggleEvent, int>(Toggle);
    }

    public void ExecuteSpellCast()
    {
        EventManager.TriggerEvent<SpellCastSoundEvent, Vector3>(transform.position);
    }

    public void ExecutePlayerSpellCast()
    {
        EventManager.TriggerEvent<PlayerSpellCastSoundEvent, Vector3>(transform.position);
    }

    public void ExecuteBuzz()
    {
        EventManager.TriggerEvent<BuzzEvent, Vector3>(transform.position);
    }

    public void ExecuteRoar()
    {
        EventManager.TriggerEvent<RoarEvent, Vector3>(transform.position);
    }
}

