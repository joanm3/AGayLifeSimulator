using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMouseOverInfo : MonoBehaviour
{
    // public string information;

    public Text infoText;

    // Use this for initialization
    void Start()
    {
        if (infoText == null)
            infoText = GameObject.FindGameObjectWithTag("InfoText").GetComponent<Text>();
    }

    // Update is called once per frame


    public void ShowText(string text)
    {
        infoText.text = text;
        infoText.text = infoText.text.Replace("\\n", "\n");
    }

}
