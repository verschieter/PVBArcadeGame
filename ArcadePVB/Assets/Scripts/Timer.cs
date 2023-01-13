using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    float startTime = 2;
    float duration;
    bool isPaused;
    bool hasStarted;
    float remainingTime;
    public Timer()
    {
        duration = 0.1f;
    }
    public void StartTimer(float minDuration, float maxDuration)
    {
        startTime = Time.time;
        this.duration = Random.Range(minDuration, maxDuration);
        hasStarted = true;
    }
    public void StartTimer(float duration)
    {
        startTime = Time.time;
        this.duration = duration;
        hasStarted = true;
    }

    public bool IsDone()
    {
        return !isPaused && Time.time > startTime + duration && hasStarted;
    }

    public float RemainingTime()
    {
        return (startTime + duration) - Time.time;
    }
    public bool IsTimerPause()
    {
        return isPaused;
    }
    public void PauseTimer()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            remainingTime = (startTime + duration) - Time.time;
        }
        else
        {
            if (remainingTime > 0)
                StartTimer(remainingTime);

            remainingTime = 0;
        }
    }
}
