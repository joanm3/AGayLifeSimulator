using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnMouseOverInfo : MonoBehaviour, IPointerEnterHandler
{
    public string textKey;
    private Text infoText;

    void Start()
    {
        if (infoText == null)
            infoText = GameObject.FindGameObjectWithTag("InfoText").GetComponent<Text>();
    }

    public void ShowText(string text)
    {
        infoText.text = LocalizationManager.Instance.GetText(textKey);
        infoText.text = infoText.text.Replace("\\n", "\n");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoText.text = LocalizationManager.Instance.GetText(textKey);
        infoText.text = infoText.text.Replace("\\n", "\n");

    }

}
