using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivitiesManager : MonoBehaviour
{
    public Button LaunchButton;
    public delegate void ActivityEventType();
    public static event ActivityEventType OnMorning;
    public static event ActivityEventType OnAfternoon;
    public static event ActivityEventType OnNight;
    public static event ActivityEventType OnDate;

    //public delegate void ResultEventType(EventSchedule schedule);
    //public static event ResultEventType OnResults;

    public enum EventSchedule { Morning, Afternoon, Evening, Date };

    private Button[] allButtons;
    private Toggle[] allToggles;

    void Start()
    {

        LaunchButton.onClick.AddListener(() => LaunchDay());

        allButtons = Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[];
        allToggles = Resources.FindObjectsOfTypeAll(typeof(Toggle)) as Toggle[];

        foreach (Button button in allButtons)
        {
            button.onClick.AddListener(() => SetInteractable());
        }

        foreach (Toggle toggle in allToggles)
        {
            toggle.onValueChanged.AddListener((on) => SetInteractable(on));
        }

    }

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


    }

    void SetInteractable()
    {
        if (OnMorning == null || OnAfternoon == null || OnNight == null)
        {
            LaunchButton.interactable = false;
        }
        else
        {
            LaunchButton.interactable = true;
        }
    }

    void SetInteractable(bool on)
    {
        if (OnMorning == null || OnAfternoon == null || OnNight == null)
        {
            LaunchButton.interactable = false;
        }
        else
        {
            LaunchButton.interactable = true;
        }
    }





}
