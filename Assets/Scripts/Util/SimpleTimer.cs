using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTimer
{
    private float counter;
    public float interval;

    public SimpleTimer(float _interval)
    {
        interval = _interval;
    }

    public void MarkTimer(float newInterval = 0)
    {
        if(newInterval > 0)
        {
            interval = newInterval;
        }
        counter = Time.time;
    }

    public bool IsTimerComplete()
    {
        if(Time.time - counter > interval)
        {
            return true;
        }

        return false;
    }
}
