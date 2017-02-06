using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizationUIText : MonoBehaviour
{
    public string key;

    void Start()
    {
        ReloadLocalText();
    }

    void OnEnable()
    {
        //ReloadLocalText();
        //LocalizationManager.ReloadLocalization += ReloadLocalText;
    }

    void OnDisable()
    {
        //LocalizationManager.ReloadLocalization -= ReloadLocalText;
    }


    // Get the string value from localization manager from key & set text component text value to the returned string value
    public void ReloadLocalText()
    {
        if (LocalizationManager.Instance == null)
        {
            Debug.LogError("LocalizationManager null when loading text: " + gameObject.name);
            return;
        }
        GetComponent<Text>().text = LocalizationManager.Instance.GetText(key);

    }
}
