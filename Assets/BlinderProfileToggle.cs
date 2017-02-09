using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class BlinderProfileToggle : MonoBehaviour
{

    public BlinderProfile profileInfo;
    public GEvent date;
    private Toggle toggle;

    // Use this for initialization
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener((on) => UpdateBlinderCanvas(on));
    }

    // Update is called once per frame
    void UpdateBlinderCanvas(bool on)
    {
        if (on)
        {
            BlinderManager.Instance.image.sprite = profileInfo.sprite;
            BlinderManager.Instance.age.text = profileInfo.age;
            BlinderManager.Instance.profileName.text = LocalizationManager.Instance.GetText(profileInfo.profileNameKey);
            BlinderManager.Instance.role.text = profileInfo.role.ToString();
            BlinderManager.Instance.dickSize.text = profileInfo.dickSize.ToString();
            BlinderManager.Instance.description.text = LocalizationManager.Instance.GetText(profileInfo.descriptionKey);
            BlinderManager.Instance.childCanvas.gameObject.SetActive(true);
            BlinderManager.Instance.GoOnDateButton.interactable = !BlinderManager.Instance.dateOfTheDay;
            BlinderManager.Instance.GoOnDateButton.GetComponentInChildren<Text>().text = !BlinderManager.Instance.dateOfTheDay ?
                LocalizationManager.Instance.GetText("BL_DATE") : LocalizationManager.Instance.GetText("BL_WAIT");
            BlinderManager.Instance.ResetOnDate();
            BlinderManager.OnDate += SetDate;
        }
    }

    void SetDate()
    {
        date.ComputeEvent();
        date.ComputeResultsText(ActivitiesManager.EventSchedule.Date);
    }
}
