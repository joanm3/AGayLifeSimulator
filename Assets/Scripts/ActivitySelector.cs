
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivitySelector : MonoBehaviour
{

    public ActivityType activityType;
    public FoldoutActivities[] Activities;
    public int SelectedActivity;

    public enum ActivityType { Morning, Afternoon, Night };

    private bool[] currentToggles;
    private bool valueChanged = false;



    void Start()
    {

        if (Activities == null || Activities.Length <= 0 || SelectedActivity >= Activities.Length)
            return;

        currentToggles = new bool[Activities.Length];
        for (int i = 0; i < Activities.Length; i++)
        {
            Activities[i].Initialize();
            Activities[i].subActivitiesCanvas.enabled = false;
            Activities[i].activityToggle.isOn = false;
            currentToggles[i] = Activities[i].activityToggle.isOn;
            //lambda expression + callback function (i dont know if lambda is redundant but i found it like this in the community forums)
            Activities[i].activityToggle.onValueChanged.AddListener((value) => { HandleSelectors(value); });
        }
    }



    void HandleSelectors(bool value)
    {
        if (value == true)
        {
            for (int i = 0; i < Activities.Length; i++)
            {
                if (Activities[i].activityToggle.isOn != currentToggles[i])
                {
                    SelectedActivity = i;
                }
            }
            valueChanged = true;
        }

        if (valueChanged)
        {
            Activities[SelectedActivity].activityToggle.isOn = true;
            Activities[SelectedActivity].subActivitiesCanvas.enabled = true;
            for (int i = 0; i < Activities.Length; i++)
            {
                if (i != SelectedActivity)
                {
                    Activities[i].activityToggle.isOn = false;
                    for (int j = 0; j < Activities[i].subActivitiesToggles.Length; j++)
                    {
                        Activities[i].subActivitiesToggles[j].isOn = false;
                    }
                    Activities[i].subActivitiesCanvas.enabled = false;
                }
                currentToggles[i] = Activities[i].activityToggle.isOn;
            }
            valueChanged = false;
        }
        else
        {
            for (int j = 0; j < Activities[SelectedActivity].subActivitiesToggles.Length; j++)
            {
                Activities[SelectedActivity].subActivitiesToggles[j].isOn = false;
            }
            Activities[SelectedActivity].subActivitiesCanvas.enabled = false;
        }
    }

}

[System.Serializable]
public class FoldoutActivities
{
    public string ID;
    public Toggle activityToggle;
    public Canvas subActivitiesCanvas;

    public Toggle[] subActivitiesToggles;


    public void Initialize()
    {
        subActivitiesToggles = subActivitiesCanvas.GetComponentsInChildren<Toggle>();
    }

}