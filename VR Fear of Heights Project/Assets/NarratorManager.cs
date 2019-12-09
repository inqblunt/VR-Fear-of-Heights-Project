using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarratorManager : MonoBehaviour
{
    public int narrationStage;
    public AudioSource narrationAudio;
    public AudioClip[] narrations;

    public TextMeshPro[] narrationTexts;
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

    public IEnumerator startGameEnumerator()
    {
        while (true)
        {
            narrationAudio.clip = narrations[0];
            yield return new WaitForSeconds(narrationAudio.clip.length);
        }
    }
}
