using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeLanguageButton : MonoBehaviour
{

    Button button;

    void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => ChangeLanguage(LocalizationManager.Instance.CurrentLanguageID));
    }

    void OnDisable()
    {
        button.onClick.RemoveAllListeners();

    }

    // Update is called once per frame
    void ChangeLanguage(int languageId)
    {
        int newLangId = languageId + 1;
        if (newLangId < 0)
            newLangId = LocalizationManager.Instance.languages.Count - 1;
        if (newLangId >= LocalizationManager.Instance.languages.Count)
            newLangId = 0;

        LocalizationManager.Instance.SetLanguage(newLangId);
    }
}
