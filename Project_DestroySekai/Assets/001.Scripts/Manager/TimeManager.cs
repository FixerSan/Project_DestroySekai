using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;

public class TimeManager : Singleton<TimeManager>
{
    public List<Timer> timers = new List<Timer>();
    private float defaultTimeScale = 1f;
    private float nowTimeScale = 1f;

    void Update()
    {
        CheckTimers();
    }

    public void AddTimer(float _time, Action _callback)
    {
        timers.Add(new Timer(_time, _callback));
    }

    public void CheckTimers()
    {
        for (int i = timers.Count - 1; i >= 0; i--)
        {
            if (timers[i].CheckTimer())
                timers.Remove(timers[i]);
        }
    }

    public void SetTimeScale(float _timeScale)
    {
        Time.timeScale = _timeScale;
        nowTimeScale = _timeScale;
    }

    public void TimePause()
    {
        SetTimeScale(0);
    }

    public void TimeContinue()
    {
        SetTimeScale(defaultTimeScale);
    }

}
public class Timer
{
    public float currentTime;
    public float time;
    public Action callback;

    public Timer(float _time, Action _callback)
    {
        currentTime = 0;
        time = _time;
        callback = _callback;
    }

    public bool CheckTimer()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= time)
        {
            callback?.Invoke();
            callback = null;
            currentTime = 0;
            time = 0;
            return true;
        }
        return false;
    }
}
