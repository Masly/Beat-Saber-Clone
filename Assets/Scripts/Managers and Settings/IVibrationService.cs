using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVibrationService
{
    public void StandardVibrate();
    public void StandardVibrate(bool isLeft);
    public void CustomVibrate(float vibrationAmplitude, float vibrationDuration);
    public void CustomVibrate(float vibrationAmplitude, float vibrationDuration, bool isLeft);
}
