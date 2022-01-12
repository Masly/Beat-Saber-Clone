using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackService : MonoBehaviour,ITrackService
{   
    private float currentBeat;
    [SerializeField]
    private int bpm = 60;
    [SerializeField]
    private AudioSource track;
    [SerializeField]
    private GameObject spawner;
    double secondsPlaying;
    double songStart;

    float ITrackService.currentBeat => currentBeat;

    // Start is called before the first frame update
    void Start()
    {
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
