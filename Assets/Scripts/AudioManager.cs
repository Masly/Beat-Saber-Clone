using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager singleton;
    public float trackDurationInSeconds = 300;
    public int bpm = 60;
    public float currentBeat;
    public int beatsToPlayer0 = 8;
    public AudioSource track;
    public GameObject spawner;
    double secondsPlaying;
    double songStart;
    
    // Start is called before the first frame update
    void Start()
    {
        //Set up singleton
        if (singleton && singleton != this)
            Destroy(this);
        else
            singleton = this;

        StartCoroutine("StartWithDelay", 3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        secondsPlaying = Time.realtimeSinceStartupAsDouble - songStart;
        currentBeat = (float)(secondsPlaying*60) / bpm;
    }
    void StartSong()
    {
        track.Play();
        songStart = Time.realtimeSinceStartupAsDouble;
    }
    IEnumerator StartWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartSong();
        spawner.SetActive(true);
    }

}
