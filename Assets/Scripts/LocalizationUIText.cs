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
        ReloadLocalText();
        LocalizationManager.Reload += ReloadLocalText;
    }

    void OnDisable()
    {
        LocalizationManager.Reload -= ReloadLocalText;
    }


    // Get the string value from localization manager from key & set text component text value to the returned string value
    public void ReloadLocalText()
    {
        GetComponent<Text>().text = LocalizationManager.Instance.GetText(key);

    }
}
