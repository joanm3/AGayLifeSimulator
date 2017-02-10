using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivitiesManager : MonoBehaviour
{
    public Button LaunchButton;
    public Toggle[] TogglesThatNeedMoney;
    public Toggle[] TogglesThatNeedFatigue;


    public delegate void ActivityEventType();
    public static event ActivityEventType OnMorning;
    public static event ActivityEventType OnAfternoon;
    public static event ActivityEventType OnNight;



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
            toggle.onValueChanged.AddListener((on) => SetLaunchButtonInteractable(on));
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

        if (BlinderManager.Instance.HadADateToday())
        {
            ResultsOfTheDay.Instance.dateResults.text = LocalizationManager.Instance.GetText("BL_NO_DATE");
        }

        BlinderManager.Instance.ResetLaunchDate();

        PlayerManager.Instance.Info.State.Fatigue -= 1;
        PlayerManager.Instance.Info.State.Fatigue =
            Mathf.Clamp(PlayerManager.Instance.Info.State.Fatigue, 0, PlayerManager.Instance.Info.State.MaxFatigue);

        PlayerManager.Instance.Info.State.DaysLeft -= 1;
        if (PlayerManager.Instance.Info.State.DaysLeft == 0)
            PlayerManager.Instance.Info.State.DaysLeft = 30;


        SetButtonsInteractable();

        Debug.Log("Day Computed");
        if (UIManager.Instance == null)
            UIManager.Init();
        UIManager.Instance.UpdateAllUI();
        if (SoundManager.Instance.isPlayingWorkMusic)
            SoundManager.Instance.ReloadSong();

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

    void SetLaunchButtonInteractable(bool on)
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

    void SetButtonsInteractable()
    {
        if (TogglesThatNeedFatigue.Length <= 0 || TogglesThatNeedFatigue.Length <= 0)
        {
            Debug.LogError("Buttons not initialized for ActivitiesManager");
            return;
        }

        if (PlayerManager.Instance.Info.State.Money <= 0)
        {
            foreach (Toggle t in TogglesThatNeedMoney)
            {
                t.isOn = false;
                t.interactable = false;
            }
        }
        else
        {
            foreach (Toggle t in TogglesThatNeedMoney)
            {
                t.interactable = true;
            }
        }


        if (PlayerManager.Instance.Info.State.Fatigue >= PlayerManager.Instance.Info.State.MaxFatigue)
        {
            foreach (Toggle t in TogglesThatNeedFatigue)
            {
                t.isOn = false;
                t.interactable = false;
            }
        }
        else
        {
            foreach (Toggle t in TogglesThatNeedFatigue)
            {
                t.interactable = true;
            }
        }
    }





}
