using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivitiesManager : MonoBehaviour
{
    public delegate void ActivityEventType(GameObject g);
    static event ActivityEventType OnMorning;
    static event ActivityEventType OnAfternoon;
    static event ActivityEventType OnNight;
    static event ActivityEventType OnDate;


    public GameObject StatsManager;

    public void LaunchDay()
    {
        if (StatsManager == null)
            return;

        if (OnMorning != null) { OnMorning(StatsManager); }
        if (OnAfternoon != null) { OnAfternoon(StatsManager); }
        if (OnNight != null) { OnNight(StatsManager); }
        if (OnDate != null) { OnDate(StatsManager); }
    }

}
