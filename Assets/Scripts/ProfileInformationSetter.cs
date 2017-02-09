using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GayProject.DataManagement;


public class ProfileInformationSetter : MonoBehaviour
{
    public Text profileName;
    public Text age;
    public Text role;
    public Text hivStatus;

    void OnEnable()
    {
        UpdateProfileUI();
        LocalizationManager.ReloadTextEvent += UpdateProfileUI;
    }

    void OnDisable()
    {
        LocalizationManager.ReloadTextEvent -= UpdateProfileUI;
    }

    void UpdateProfileUI()
    {
        if (profileName == null || age == null || hivStatus == null || role == null)
        {
            Debug.LogError("Please assign name, age, role and HIVStatus text UI to Player Manager");
        }
        else
        {
            profileName.text = PlayerManager.Instance.Info.Profile.Name;
            age.text = PlayerManager.Instance.Info.Profile.Age;
            hivStatus.text = (PlayerManager.Instance.Info.Profile.HIVStatus == 0) ?
               LocalizationManager.Instance.GetText("HIV_NEG") :
               LocalizationManager.Instance.GetText("HIV_POS");
            if (PlayerManager.Instance.Info.Profile.Role == 0)
                role.text = LocalizationManager.Instance.GetText("BTTM");
            else if (PlayerManager.Instance.Info.Profile.Role == 1)
                role.text = LocalizationManager.Instance.GetText("TOP");
            else
                role.text = LocalizationManager.Instance.GetText("VERS");
        }
    }


    public void SetProfileName(Text text)
    {
        profileName.text = text.text;
        PlayerManager.Instance.Info.Profile.Name = text.text;
    }

    public void SetAge(Text text)
    {
        age.text = int.Parse(text.text).ToString();
        PlayerManager.Instance.Info.Profile.Age = text.text;
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
                PlayerManager.Instance.Info.Profile.Role = FeaturesManager.Instance.features[i].currIndex;
            }
        }
        UpdateProfileUI();
    }

    public void SetHivStatus(Text text)
    {
        hivStatus.text = text.text;
        PlayerManager.Instance.Info.Profile.HIVStatus = text.text.Contains("-") ? 0 : 1;
        UpdateProfileUI();

    }


}
