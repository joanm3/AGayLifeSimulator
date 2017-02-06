using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using UnityEngine;


// based off tutorial here: http://www.6502b.com/Simple_text_localization_in_Unity.html 
//if time add a serializer to create new languages on editor. 
public class LocalizationManager : Singleton<LocalizationManager>
{

    [SerializeField]
    public List<TextAsset> languageFiles = new List<TextAsset>();
    public List<Language> languages = new List<Language>();

    public bool IsReady
    {
        set
        {
            isReady = value;
            if (isReady == true)
            {
                OnIsReady();
            }
        }
        get
        {
            return isReady;
        }

    }
    public int CurrentLanguageID
    {
        set
        {
            int _previousValue = currentLanguageID;
            currentLanguageID = value;
            if (_previousValue != currentLanguageID)
                OnLanguageChanged();

        }
        get
        {
            return currentLanguageID;
        }

    }

    [SerializeField]
    private int currentLanguageID = 0;
    private bool isReady = false;

    public delegate void ReloadText();
    public static event ReloadText ReloadLocalization;

    override protected void OnAwake()
    {
        base.OnAwake();
        IsReady = false;
        foreach (TextAsset languageFile in languageFiles)
        {
            XDocument languageXMLData = XDocument.Parse(languageFile.text);
            Language language = new Language();
            language.languageID = int.Parse(languageXMLData.Element("Language").Attribute("ID").Value);
            language.languageString = languageXMLData.Element("Language").Attribute("LANG").Value;
            foreach (XElement textx in languageXMLData.Element("Language").Elements())
            {
                TextKeyValue textKeyValue = new TextKeyValue();
                textKeyValue.key = textx.Attribute("key").Value;
                textKeyValue.value = textx.Value;
                language.textKeyValueList.Add(textKeyValue);
            }
            languages.Add(language);
        }
        IsReady = true;

    }

    public string GetText(string key)
    {
        foreach (Language language in languages)
        {
            if (language.languageID == CurrentLanguageID)
            {
                foreach (TextKeyValue textKeyValue in language.textKeyValueList)
                {
                    if (textKeyValue.key == key)
                    {
                        return textKeyValue.value;
                    }

                }
            }
        }
        return "undifined";
    }

    public void SetLanguage(int languageID)
    {
        CurrentLanguageID = languageID;
    }

    public void OnIsReady()
    {
        SetLanguage(CurrentLanguageID);
        //if (ReloadLocalization != null) ReloadLocalization();
    }

    public void OnLanguageChanged()
    {
        if (ReloadLocalization != null) ReloadLocalization();
    }


}



[System.Serializable]
public class Language
{
    [XmlAttribute("LANG")]
    public string languageString;
    [XmlAttribute("ID")]
    public int languageID;
    [XmlArray("values")]
    [XmlArrayItem("text")]
    public List<TextKeyValue> textKeyValueList = new List<TextKeyValue>();
}

[System.Serializable]
public class TextKeyValue
{
    [XmlAttribute("key")]
    public string key;
    [XmlElement]
    public string value;
}