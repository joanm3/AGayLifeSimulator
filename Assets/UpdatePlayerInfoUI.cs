using GayProject.Reflection;
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


    public void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < UITextFields.Length; i++)
        {
            var result = PlayerManager.Instance.GetPropValue(UITextFields[i].FieldName);
            if (!UITextFields[i].useTextInstead)
            {

                UITextFields[i].UIText.text = (!UITextFields[i].useFieldAsAfterText) ?
                    UITextFields[i].text + " " + result.ToString() + " " + UITextFields[i].afterText :
                    UITextFields[i].text + " " + result.ToString() + "/" + PlayerManager.Instance.GetPropValue(UITextFields[i].afterText).ToString();
            }
            else
            {
                for (int j = 0; j < UITextFields[i].choicesText.Length; j++)
                {
                    if (j == (int)result)
                        UITextFields[i].UIText.text = (!UITextFields[i].useFieldAsAfterText) ?
                            UITextFields[i].text + " " + UITextFields[i].choicesText[j] + " " + UITextFields[i].afterText :
                             UITextFields[i].text + " " + UITextFields[i].choicesText[j] + "/" + PlayerManager.Instance.GetPropValue(UITextFields[i].afterText).ToString();
                }
            }
        }
    }

}


