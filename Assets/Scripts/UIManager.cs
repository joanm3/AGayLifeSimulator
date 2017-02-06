using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    //we should get also the other types of UI that need updating. 

    public UpdatePlayerInfoUI[] UIUpdaters;
    public LocalizationUIText[] UILocalizationText;

    void Start()
    {
        GetArrays();
    }

    void OnDestroy()
    {
        LocalizationManager.ReloadTextEvent -= UpdateAllUI;
    }

    void OnLevelWasLoaded()
    {

        LocalizationManager.ReloadTextEvent -= UpdateAllUI;
        GetArrays();
    }

    void GetArrays()
    {
        UILocalizationText = Resources.FindObjectsOfTypeAll(typeof(LocalizationUIText)) as LocalizationUIText[];
        UIUpdaters = Resources.FindObjectsOfTypeAll(typeof(UpdatePlayerInfoUI)) as UpdatePlayerInfoUI[];
        UpdateAllUI();
        LocalizationManager.ReloadTextEvent += UpdateAllUI;
    }


    public void UpdateAllUI()
    {

        if (UILocalizationText == null || UILocalizationText.Length <= 0)
            UILocalizationText = Resources.FindObjectsOfTypeAll(typeof(LocalizationUIText)) as LocalizationUIText[];



        if (UILocalizationText == null || UILocalizationText.Length <= 0)
            return;

        for (int i = 0; i < UILocalizationText.Length; i++)
        {
            UILocalizationText[i].ReloadLocalText();
        }

        if (UIUpdaters == null || UIUpdaters.Length <= 0)
            UIUpdaters = Resources.FindObjectsOfTypeAll(typeof(UpdatePlayerInfoUI)) as UpdatePlayerInfoUI[];

        if (UIUpdaters == null || UIUpdaters.Length <= 0)
            return;

        for (int i = 0; i < UIUpdaters.Length; i++)
        {
            UIUpdaters[i].UpdateUI();
        }
        Debug.Log("UI Updated");
    }

}
