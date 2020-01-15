using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public WaypointManager manager;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        for (var i = 0; i < manager.Waypoints.Length; i++)
        {
            if (manager.Waypoints[i] == gameObject)
            {
                manager.Waypoints[i + 1].gameObject.SetActive(true);
            }
        }
        manager.ScoreCount++;
        Destroy(gameObject);        
    }
}
