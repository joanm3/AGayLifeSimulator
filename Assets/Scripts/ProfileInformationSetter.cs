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
    }

    public void SetHivStatus(Text text)
    {
        hivStatus.text = text.text;
        PlayerManager.Instance.Info.Profile.HIVStatus = text.text.Contains("-") ? 0 : 1;
    }


}
