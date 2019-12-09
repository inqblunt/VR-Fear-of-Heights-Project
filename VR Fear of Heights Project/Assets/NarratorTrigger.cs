using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorTrigger : MonoBehaviour
{
    public NarratorManager narrator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            narrator.ReadNextNarration();
        }
        Destroy(gameObject);
    }
}
