using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/*
 * https://www.youtube.com/watch?v=Rz2q3JzOOjs gave me the general idea on how to use the Service Locator pattern in unity
 * I personalized it a little bit using internal classes in order to have everything a little bit tidier than having a bunch of methods in a class
 * 
 */

public class ServiceProvider : MonoBehaviour
{
    //I don't like this naming convention, I'll change it later
    [HideInInspector]
    public VibrationHandle vibrationService;
    [HideInInspector]
    public AudioHandle audioService;
    [HideInInspector]
    public ScoreHandle scoreService;

    private void OnValidate()
    {
        vibrationService = new VibrationHandle(GetComponent<IVibrationService>());
        //https://stackoverflow.com/questions/56618167/how-to-check-a-script-for-unity-inspector-null-parameters-and-incorrect-values this could be a nice way to check every field
        Assert.IsNotNull(vibrationService);
        audioService = new AudioHandle(GetComponent<ITrackService>());
        Assert.IsNotNull(audioService);
        scoreService = new ScoreHandle(GetComponent<IScoreService>());
        Assert.IsNotNull(scoreService);
    }
    //I might use a factory method here later on
    public class VibrationHandle
    {
        public VibrationHandle(IVibrationService impl)
        {
            vibrationServiceImplementation = impl;
        }
        private readonly IVibrationService vibrationServiceImplementation;
        public void StandardVibrate() => vibrationServiceImplementation.StandardVibrate();
        public void StandardVibrate(bool isLeft) => vibrationServiceImplementation.StandardVibrate(isLeft);
        public void CustomVibrate(float vibrationAmplitude, float vibrationDuration) => vibrationServiceImplementation.CustomVibrate(vibrationAmplitude, vibrationDuration);
        public void CustomVibrate(float vibrationAmplitude, float vibrationDuration, bool isLeft) => vibrationServiceImplementation.CustomVibrate(vibrationAmplitude, vibrationDuration, isLeft);
    }

    public class AudioHandle
    {
        public AudioHandle(ITrackService impl)
        {
            audioServiceImplementation = impl;
        }
        private readonly ITrackService audioServiceImplementation;

        public float currentBeat => audioServiceImplementation.currentBeat;
    }

    public class ScoreHandle
    {
        public ScoreHandle(IScoreService impl)
        {
            scoreServiceImplementation = impl;
        }

        private readonly IScoreService scoreServiceImplementation;

        public int score => scoreServiceImplementation.score;

        public void AddScore(int amount) => scoreServiceImplementation.AddScore(amount); 
    }
    
}
