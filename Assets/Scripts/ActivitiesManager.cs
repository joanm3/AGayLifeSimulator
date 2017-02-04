using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivitiesManager : MonoBehaviour
{
    public delegate void ActivityEventType(PlayerManager g);
    public static event ActivityEventType OnMorning;
    public static event ActivityEventType OnAfternoon;
    public static event ActivityEventType OnNight;
    public static event ActivityEventType OnDate;


    public PlayerManager PlayerManager;

    public void LaunchDay()
    {
        if (PlayerManager == null)
        {
            PlayerManager = PlayerManager.Instance;
            if (PlayerManager == null)
            {
                Debug.LogError("Player Manager not found", this);
                return;
            }
        }

        if (OnMorning != null) { OnMorning(PlayerManager); }
        if (OnAfternoon != null) { OnAfternoon(PlayerManager); }
        if (OnNight != null) { OnNight(PlayerManager); }
        //donno if put it here
        if (OnDate != null) { OnDate(PlayerManager); }
    }



}
