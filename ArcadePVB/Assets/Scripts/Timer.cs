using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    float startTime = 2;
    float duration;
    bool isPaused;

    float remainingTime;
    public Timer()
    {
        duration = 2F;
    }
    public void StartTimer(float minDuration, float maxDuration)
    {
        startTime = Time.time;
        this.duration = Random.Range(minDuration, maxDuration);
    }
    public void StartTimer(float duration)
    {
        startTime = Time.time;
        this.duration = duration;
    }

    public bool IsDone()
    {
        return !isPaused && Time.time > startTime + duration;
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
                duration = remainingTime;

            remainingTime = 0;
        }
    }
}
