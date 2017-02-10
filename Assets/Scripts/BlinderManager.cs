using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinderManager : Singleton<BlinderManager>
{

    public RectTransform childCanvas;
    public Button GoOnDateButton;
    public Text profileName;
    public Text age;
    public Text role;
    public Text dickSize;
    public Text description;
    public Image image;
    public RectTransform resultsCanvas;
    public Text resultsText;
    public RectTransform blinderProfilesParent;

    public bool dateOfTheDay = false;

    public delegate void ActivityEventType();
    public static event ActivityEventType OnDate;

    void Start()
    {
        dateOfTheDay = false;
        GoOnDateButton.onClick.AddListener(() => LaunchDate());
        GoOnDateButton.GetComponentInChildren<Text>().text = LocalizationManager.Instance.GetText("BL_DATE");
        childCanvas.gameObject.SetActive(false);
        resultsCanvas.gameObject.SetActive(false);

    }

    public void LaunchDate()
    {
        if (OnDate != null)
        {
            dateOfTheDay = true;
            OnDate();
            GoOnDateButton.interactable = false;
            GoOnDateButton.GetComponentInChildren<Text>().text = LocalizationManager.Instance.GetText("BL_WAIT");
            UIManager.Instance.UpdateAllUI();
        }
    }

    public bool HadADateToday()
    {
        return (OnDate != null); 
    }

    public void ResetLaunchDate()
    {
        dateOfTheDay = false;
        Toggle[] _toggles = blinderProfilesParent.GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in _toggles)
        {
            toggle.isOn = false;
        }

        GoOnDateButton.interactable = true;
        GoOnDateButton.GetComponentInChildren<Text>().text = LocalizationManager.Instance.GetText("BL_DATE");
    }

    public void ResetOnDate()
    {
        OnDate = null;
    }




}

[System.Serializable]
public class BlinderProfile
{
    public enum Role { bttm, top, vers }
    public enum DickSize { XS, S, M, L, XL }
    public Sprite sprite;
    public string profileNameKey;
    public string age;
    public Role role;
    public DickSize dickSize;
    public string descriptionKey;
}
