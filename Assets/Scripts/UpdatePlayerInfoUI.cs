using GayProject.Reflection;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerInfoUI : MonoBehaviour
{
    public UITextField[] UITextFields;

    [System.Serializable]
    public struct UITextField
    {
        /// <summary>
        /// Use points to separate inheritance. 
        /// Example: Info.State.Money
        /// </summary>
        [Tooltip("Use points to separate inheritance. Example: Info.State.Money")]
        public string FieldName;
        public Text UIText;
        public string text;
        public bool useTextInstead;
        [Tooltip("be careful to not get out of scope!")]
        public string[] choicesText;
        [Tooltip("if false, will print the string as is")]
        public bool useFieldAsAfterText;
        public string afterText;
    }


    IEnumerator Start()
    {
        while (!LocalizationManager.Instance.IsReady)
        {
            yield return null;
        }
        UpdateUI();
    }

    void OnEnable()
    {
        LocalizationManager.Reload += UpdateUI;
    }

    void OnDisable()
    {
        LocalizationManager.Reload -= UpdateUI;
    }

    public void UpdateUI()
    {
        for (int i = 0; i < UITextFields.Length; i++)
        {
            var result = PlayerManager.Instance.GetFieldValue(UITextFields[i].FieldName);
            if (!UITextFields[i].useTextInstead)
            {

                UITextFields[i].UIText.text = (!UITextFields[i].useFieldAsAfterText) ?
                    LocalizationManager.Instance.GetText(UITextFields[i].text) + " " + result.ToString() + " " + LocalizationManager.Instance.GetText(UITextFields[i].afterText) :
                    LocalizationManager.Instance.GetText(UITextFields[i].text) + " " + result.ToString() + "/" + PlayerManager.Instance.GetFieldValue(UITextFields[i].afterText).ToString();
            }
            else
            {
                for (int j = 0; j < UITextFields[i].choicesText.Length; j++)
                {
                    if (j == (int)result)
                        UITextFields[i].UIText.text = (!UITextFields[i].useFieldAsAfterText) ?
                            LocalizationManager.Instance.GetText(UITextFields[i].text) + " " + LocalizationManager.Instance.GetText(UITextFields[i].choicesText[j]) + " " + LocalizationManager.Instance.GetText(UITextFields[i].afterText) :
                            LocalizationManager.Instance.GetText(UITextFields[i].text) + " " + LocalizationManager.Instance.GetText(UITextFields[i].choicesText[j]) + "/" + PlayerManager.Instance.GetFieldValue(UITextFields[i].afterText).ToString();
                }
            }
        }
    }

}


