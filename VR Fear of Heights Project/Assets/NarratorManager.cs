using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorManager : MonoBehaviour
{
    public int narrationStage;
    public AudioSource narrationAudio;
    public AudioClip[] narrations;
    // Start is called before the first frame update
    void Start()
    {
        narrationStage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadNextNarration()
    {
        narrationStage++;
        narrationAudio.clip = narrations[narrationStage];
        narrationAudio.Play();

    }
}
