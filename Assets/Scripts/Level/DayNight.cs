using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNight : MonoBehaviour
{

    [SerializeField] private float _secInHour;
    [SerializeField] private float _speedChangeIntensity;

    private float saveTime;
    private float time;
    private bool bIsDay = true;

    public Light2D light;

    private void SetTime()
    {
        int hour = (int)(((Time.time + saveTime + (_secInHour * 6)) / _secInHour) % 24);
        if (hour >= 23 || hour <= 5)
        {
            //Set day time

            bIsDay = false;
        }
        else
        {
            //Set night time

            bIsDay = true;
        }
        bIsDay = !bIsDay;
    }

    private void Update()
    {
        SetTime();
        //Change intensity globalLight
        if (!bIsDay)
        {
            light.intensity = Mathf.Lerp(light.intensity, 1, 1 / (_secInHour * _speedChangeIntensity));
        }
        else
        {
            light.intensity = Mathf.Lerp(light.intensity, 0.1f, 1 / (_secInHour * _speedChangeIntensity));
        }
    }

    public string GetTimeAtString()
    {
        string timeReturn = "";
        int hour = (int)(((Time.time + saveTime + (_secInHour * 6)) / _secInHour) % 24);
        int minutes = (int)(((((Time.time + saveTime + (_secInHour * 6)) / _secInHour) % 24) - hour) * 100 * 0.6);
        timeReturn = $"{hour.ToString("00")}:{minutes.ToString("00")}";
        return timeReturn;
    }

    public bool IsDay()
    {
        return bIsDay;
    }

    public void LoadSavedTime(float value)
    {
        saveTime = value;
    }

    public float GetSavedTime()
    {
        return Time.time + saveTime;
    }
}
