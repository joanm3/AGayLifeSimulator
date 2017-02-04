using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivitiesManager : MonoBehaviour
{
    public delegate void ActivityEventType();
    public static event ActivityEventType OnMorning;
    public static event ActivityEventType OnAfternoon;
    public static event ActivityEventType OnNight;
    public static event ActivityEventType OnDate;




    public void LaunchDay()
    {
        if (PlayerManager.Instance == null)
        {
            Debug.LogError("Player Manager not found", this);
            return;
        }

        if (OnMorning != null) { OnMorning(); }
        if (OnAfternoon != null) { OnAfternoon(); }
        if (OnNight != null) { OnNight(); }
        //donno if put it here
        if (OnDate != null) { OnDate(); }

        Debug.Log("Day Computed");
        if (UIManager.Instance == null)
            UIManager.Init();
        UIManager.Instance.UpdateAllUI();
        Debug.Log("UI Updated");

    }



}
