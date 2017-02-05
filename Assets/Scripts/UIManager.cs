using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private UpdatePlayerInfoUI[] UIUpdaters;

    void Start()
    {
        UIUpdaters = GameObject.FindObjectsOfType<UpdatePlayerInfoUI>();
        UpdateAllUI();
    }

    public void UpdateAllUI()
    {
        if (UIUpdaters == null || UIUpdaters.Length <= 0)
            UIUpdaters = GameObject.FindObjectsOfType<UpdatePlayerInfoUI>();

        for (int i = 0; i < UIUpdaters.Length; i++)
        {
            UIUpdaters[i].UpdateUI();
        }
        Debug.Log("UI Updated");
    }

}
