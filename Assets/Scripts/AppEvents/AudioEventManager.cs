using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public EventSound3D eventSound3DPrefab;

    public AudioClip[] minionJabberAudio = null;
    public AudioClip[] boxAudio = null;
    public AudioClip playerLandsAudio;
    public AudioClip explosionAudio;
    public AudioClip deathAudio;
    public AudioClip bombBounceAudio;
    public AudioClip jumpAudio;
    public AudioClip gruntAudio;
    public AudioClip minionDeathAudio;
    public AudioClip minionOuchAudio;
    public AudioClip minionSpawnAudio;
    public AudioClip[] minionFootstepAudio;
    public AudioClip punchAudio;
    public AudioClip footstepAudio;
    public AudioClip doorOpenAudio;
    public AudioClip buzzAudio;
    public AudioClip roarAudio;
    public AudioClip speedUpAudio;
    public AudioClip spellcastAudio;
    public AudioClip playerSpellcastAudio;

    private UnityAction<Vector3, float> boxCollisionEventListener;

    private UnityAction<Vector3, float> playerLandsEventListener;

    private UnityAction<Vector3> explosionEventListener;

    private UnityAction<Vector3> bombBounceEventListener;

    private UnityAction<Vector3> jumpEventListener;

    private UnityAction<GameObject> deathEventListener;

    private UnityAction<Vector3> punchEventListener;

    private UnityAction<Vector3> footStepEventListener;

    private UnityAction<Vector3> doorOpenEventListener;

    private UnityAction<Vector3> buzzEventListener;
    private UnityAction<Vector3> roarEventListener;
    private UnityAction<Vector3> speedUpSoundEventListener;

    private UnityAction<Vector3> spellcastSoundEventListener;
    private UnityAction<Vector3> playerSpellcastSoundEventListener;


    void Awake()
    {

        boxCollisionEventListener = new UnityAction<Vector3, float>(boxCollisionEventHandler);

        playerLandsEventListener = new UnityAction<Vector3, float>(playerLandsEventHandler);

        explosionEventListener = new UnityAction<Vector3>(explosionEventHandler);

        bombBounceEventListener = new UnityAction<Vector3>(bombBounceEventHandler);

        jumpEventListener = new UnityAction<Vector3>(jumpEventHandler);

        deathEventListener = new UnityAction<GameObject>(deathEventHandler);

        punchEventListener = new UnityAction<Vector3>(punchEventHandler);

        footStepEventListener = new UnityAction<Vector3>(footstepEventHandler);

        doorOpenEventListener = new UnityAction<Vector3>(doorOpenEventHandler);

        buzzEventListener = new UnityAction<Vector3>(buzzEventHandler);
        roarEventListener = new UnityAction<Vector3>(roarEventHandler);

        speedUpSoundEventListener = new UnityAction<Vector3>(speedUpSoundEventHandler);

        spellcastSoundEventListener = new UnityAction<Vector3>(spellcastSoundEventHandler);
        playerSpellcastSoundEventListener = new UnityAction<Vector3>(playerSpellcastSoundEventHandler);

    }


    // Use this for initialization
    void Start()
    {



    }


    void OnEnable()
    {

        EventManager.StartListening<BoxCollisionEvent, Vector3, float>(boxCollisionEventListener);
        EventManager.StartListening<PlayerLandsEvent, Vector3, float>(playerLandsEventListener);
        EventManager.StartListening<ExplosionEvent, Vector3>(explosionEventListener);
        EventManager.StartListening<BombBounceEvent, Vector3>(bombBounceEventListener);
        EventManager.StartListening<JumpEvent, Vector3>(jumpEventListener);
        EventManager.StartListening<DeathEvent, GameObject>(deathEventListener);
        EventManager.StartListening<PunchEvent, Vector3>(punchEventListener);
        EventManager.StartListening<FootstepEvent, Vector3>(footStepEventListener);
        EventManager.StartListening<DoorOpenEvent, Vector3>(doorOpenEventListener);
        EventManager.StartListening<BuzzEvent, Vector3>(buzzEventListener);
        EventManager.StartListening<RoarEvent, Vector3>(roarEventListener);
        EventManager.StartListening<SpeedUpSoundEvent, Vector3>(speedUpSoundEventListener);
        EventManager.StartListening<SpellCastSoundEvent, Vector3>(spellcastSoundEventListener);
        EventManager.StartListening<PlayerSpellCastSoundEvent, Vector3>(playerSpellcastSoundEventListener);
    }

    void OnDisable()
    {

        EventManager.StopListening<BoxCollisionEvent, Vector3, float>(boxCollisionEventListener);
        EventManager.StopListening<PlayerLandsEvent, Vector3, float>(playerLandsEventListener);
        EventManager.StopListening<ExplosionEvent, Vector3>(explosionEventListener);
        EventManager.StopListening<BombBounceEvent, Vector3>(bombBounceEventListener);
        EventManager.StopListening<JumpEvent, Vector3>(jumpEventListener);
        EventManager.StopListening<DeathEvent, GameObject>(deathEventListener);
        EventManager.StopListening<PunchEvent, Vector3>(punchEventListener);
        EventManager.StopListening<FootstepEvent, Vector3>(footStepEventListener);
        EventManager.StopListening<DoorOpenEvent, Vector3>(doorOpenEventListener);
        EventManager.StopListening<BuzzEvent, Vector3>(buzzEventListener);
        EventManager.StopListening<RoarEvent, Vector3>(roarEventListener);
        EventManager.StopListening<SpeedUpSoundEvent, Vector3>(speedUpSoundEventListener);
        EventManager.StopListening<SpellCastSoundEvent, Vector3>(spellcastSoundEventListener);
        EventManager.StopListening<PlayerSpellCastSoundEvent, Vector3>(playerSpellcastSoundEventListener);
    }

    void playerSpellcastSoundEventHandler(Vector3 worldPos)
    {
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.playerSpellcastAudio;

            snd.audioSrc.minDistance = 50f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }
    void spellcastSoundEventHandler(Vector3 worldPos)
    {
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.spellcastAudio;

            snd.audioSrc.minDistance = 50f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }
    void speedUpSoundEventHandler(Vector3 worldPos)
    {
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.speedUpAudio;

            snd.audioSrc.minDistance = 50f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }
    void footstepEventHandler(Vector3 worldPos)
    {
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.footstepAudio;

            snd.audioSrc.minDistance = 50f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }

    void doorOpenEventHandler(Vector3 worldPos)
    {
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.doorOpenAudio;

            snd.audioSrc.minDistance = 50f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }

    void buzzEventHandler(Vector3 worldPos)
    {
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.buzzAudio;

            snd.audioSrc.minDistance = 50f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }

    void roarEventHandler(Vector3 worldPos)
    {
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.roarAudio;

            snd.audioSrc.minDistance = 50f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }

    void punchEventHandler(Vector3 worldPos)
    {
        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.punchAudio;

            snd.audioSrc.minDistance = 50f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }



    void boxCollisionEventHandler(Vector3 worldPos, float impactForce)
    {
        //AudioSource.PlayClipAtPoint(this.boxAudio, worldPos);

        const float halfSpeedRange = 0.2f;

        EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

        snd.audioSrc.clip = this.boxAudio[Random.Range(0, boxAudio.Length)];

        snd.audioSrc.pitch = Random.Range(1f - halfSpeedRange, 1f + halfSpeedRange);

        snd.audioSrc.minDistance = Mathf.Lerp(1f, 8f, impactForce / 200f);
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();
    }

    void playerLandsEventHandler(Vector3 worldPos, float collisionMagnitude)
    {
        //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

        if (eventSound3DPrefab)
        {
            if (collisionMagnitude > 300f)
            {

                EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

                snd.audioSrc.clip = this.playerLandsAudio;

                snd.audioSrc.minDistance = 5f;
                snd.audioSrc.maxDistance = 100f;

                snd.audioSrc.Play();

                if (collisionMagnitude > 500f)
                {

                    EventSound3D snd2 = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

                    snd2.audioSrc.clip = this.gruntAudio;

                    snd2.audioSrc.minDistance = 5f;
                    snd2.audioSrc.maxDistance = 100f;

                    snd2.audioSrc.Play();
                }
            }


        }
    }


    void explosionEventHandler(Vector3 worldPos)
    {
        //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.explosionAudio;

            snd.audioSrc.minDistance = 50f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }

    void bombBounceEventHandler(Vector3 worldPos)
    {
        //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.bombBounceAudio;

            snd.audioSrc.minDistance = 10f;
            snd.audioSrc.maxDistance = 500f;

            snd.audioSrc.Play();
        }
    }

    void jumpEventHandler(Vector3 worldPos)
    {
        //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.jumpAudio;

            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }

    void deathEventHandler(GameObject go)
    {
        //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

        if (eventSound3DPrefab)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, go.transform);

            snd.audioSrc.clip = this.deathAudio;

            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }

}
