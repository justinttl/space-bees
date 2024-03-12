using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsResetter : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null) Debug.Log("Player game object could not be found");
        Player player = playerObj.GetComponent<Player>();

        Debug.Log("Resetting Player Stats.");
        player.stats.Reset();
    }
}
