using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public GameObject[] Waypoints;
    public int ScoreCount;
    public GameObject WinGame;

    // Start is called before the first frame update
    void Start()
    {
        WinGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreCount >= 3)
        {
            WinGame.SetActive(true);
        }
    }
}
