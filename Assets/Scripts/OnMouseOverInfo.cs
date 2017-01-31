using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMouseOverInfo : MonoBehaviour
{
    // public string information;

    private Text infoText;

    // Use this for initialization
    void Start()
    {
        infoText = GameObject.FindGameObjectWithTag("InfoText").GetComponent<Text>();
    }

    // Update is called once per frame


    void OnMouseUpdate()
    {
        Debug.Log("here mouse");
    }

    public void ShowText(string text)
    {
        infoText.text = text;
        infoText.text = infoText.text.Replace("\\n", "\n");
    }

}
