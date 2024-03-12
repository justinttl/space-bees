using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public bool levelComplete;
    public GameObject player;
    public GameObject queen;
    public List<GameObject> doors;
    public List<GameObject> enemies;
    public string nextScene = "Level_1";

    private string sceneName;

    public UnityEvent deathEvent;

    public int enemiesAlive
    {
        get
        {
            return enemies.Count(obj => obj != null && !obj.GetComponent<IKillable>().isDead());
        }
    }

    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        sceneName = SceneManager.GetActiveScene().name;

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) Debug.Log("Player could not be found");

        queen = GameObject.FindGameObjectWithTag("Queen");
        if (queen == null) Debug.Log("Queen could not be found");

        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        Debug.Log($"{enemies.Count} enemies in level");

        doors = new List<GameObject>(GameObject.FindGameObjectsWithTag("Door"));
        Debug.Log($"{doors.Count} doors in level");

        if (deathEvent == null)
        {
            deathEvent = new UnityEvent();
        }
    }

    void Start()
    {
        levelComplete = false;
    }

    void Update()
    {
        if (!levelComplete)
        {
            if (enemiesAlive == 0) CompleteLevel();
        }
    }
    void CompleteLevel()
    {
        Debug.Log("Level Complete");
        levelComplete = true;
        UnlockNextLevel();
    }

    void UnlockNextLevel()
    {
        // TODO: Assign next level to door
        // TODO: Assign powerups to door

        // Open Door
        foreach (GameObject door in doors)
        {
            door.GetComponent<DoorController>().Open();
        }

        if (sceneName == "Level_2")
        {
            queen.GetComponent<QueenController>().Cheer();
        }
    }
    void Respawn()
    {
        Debug.Log("Player Respawned");
    }
}
