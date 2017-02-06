using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    //we should get also the other types of UI that need updating. 

    private UpdatePlayerInfoUI[] UIUpdaters;
    private LocalizationUIText[] UILocalizationText;

    void Start()
    {
        GetArrays();
    }

    void OnDestroy()
    {
        LocalizationManager.ReloadLocalization -= UpdateAllUI;
    }

    void OnLevelWasLoaded()
    {

        LocalizationManager.ReloadLocalization -= UpdateAllUI;
        GetArrays();
    }

    void GetArrays()
    {
        UIUpdaters = GameObject.FindObjectsOfType<UpdatePlayerInfoUI>();
        UILocalizationText = GameObject.FindObjectsOfType<LocalizationUIText>();
        UpdateAllUI();
        LocalizationManager.ReloadLocalization += UpdateAllUI;
    }


    public void UpdateAllUI()
    {

        if (UILocalizationText == null || UILocalizationText.Length <= 0)
            UILocalizationText = GameObject.FindObjectsOfType<LocalizationUIText>();

        if (UILocalizationText == null || UILocalizationText.Length <= 0)
            return;

        for (int i = 0; i < UILocalizationText.Length; i++)
        {
            UILocalizationText[i].ReloadLocalText();
        }

        if (UIUpdaters == null || UIUpdaters.Length <= 0)
            UIUpdaters = GameObject.FindObjectsOfType<UpdatePlayerInfoUI>();

        if (UIUpdaters == null || UIUpdaters.Length <= 0)
            return;

        for (int i = 0; i < UIUpdaters.Length; i++)
        {
            UIUpdaters[i].UpdateUI();
        }
        Debug.Log("UI Updated");
    }

}
