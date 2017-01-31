using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileInformationSetter : MonoBehaviour
{
    public Text profileName;
    public Text age;
    public Text role;
    public Text hivStatus;



    void OnEnable()
    {
        LoadFeatures();
    }

    void OnDisable()
    {
        SaveFeatures();
    }

    void LoadFeatures()
    {

        string key = "PROFILE_NAME";
        if (!PlayerPrefs.HasKey(key))
            PlayerPrefs.SetString(key, profileName.text);
        profileName.text = PlayerPrefs.GetString(key);

        key = "PROFILE_AGE";
        if (!PlayerPrefs.HasKey(key))
            PlayerPrefs.SetString(key, age.text);
        age.text = PlayerPrefs.GetString(key);

        key = "PROFILE_ROLE";
        if (!PlayerPrefs.HasKey(key))
            PlayerPrefs.SetString(key, role.text);
        role.text = PlayerPrefs.GetString(key);

        key = "PROFILE_HIVSTATUTS";
        if (!PlayerPrefs.HasKey(key))
            PlayerPrefs.SetString(key, hivStatus.text);
        hivStatus.text = PlayerPrefs.GetString(key);
    }

    void SaveFeatures()
    {
        string key = "PROFILE_NAME";
        PlayerPrefs.SetString(key, profileName.text);

        key = "PROFILE_AGE";
        PlayerPrefs.SetString(key, age.text);

        key = "PROFILE_ROLE";
        PlayerPrefs.SetString(key, role.text);

        key = "PROFILE_HIVSTATUTS";
        PlayerPrefs.SetString(key, hivStatus.text);
    }



    public void SetProfileName(Text text)
    {
        profileName.text = text.text;
    }

    public void SetAge(Text text)
    {
        age.text = text.text;
    }

    public void SetRole(Text text)
    {
        StartCoroutine(SetRoleCoroutine(text));
    }

    IEnumerator SetRoleCoroutine(Text text)
    {
        yield return new WaitForEndOfFrame();
        role.text = text.text;
        for (int i = 0; i < FeaturesManager.Instance.features.Count; i++)
        {
            if (FeaturesManager.Instance.features[i].ID == "Role")
            {
                PlayerManager.Instance.Info.Nature.PreferedRole = FeaturesManager.Instance.features[i].currIndex;
            }
        }

    }

    public void SetHivStatus(Text text)
    {

        hivStatus.text = text.text;
        PlayerManager.Instance.Info.Nature.HIVStatus = text.text.Contains("-") ? 0 : 1;
        //StartCoroutine(SetRoleCoroutine(text));

    }

    IEnumerator SetHIVStatusCoroutine(Text text)
    {
        yield return new WaitForEndOfFrame();
        hivStatus.text = text.text;
        PlayerManager.Instance.Info.Nature.HIVStatus = text.text.Contains("-") ? 0 : 1;

    }

}
