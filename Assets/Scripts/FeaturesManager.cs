using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class FeaturesManager : MonoBehaviour
{

    public List<Feature> features;

    void OnEnable()
    {
        for (int i = 0; i < features.Count; ++i)
        {
            int index = i;

            if (features[index].previousButton != null)
                features[index].previousButton.onClick.AddListener(() => features[index].NextChoice());
            if (features[index].nextButton != null)
                features[index].nextButton.onClick.AddListener(() => features[index].PreviousChoice());
        }

        LoadFeatures();
    }

    void OnDisable()
    {
        SaveFeatures();
    }

    void Test(int index)
    {
        Debug.Log("pressed: " + index);
    }

    void LoadFeatures()
    {

        for (int i = 0; i < features.Count; i++)
        {
            string key = "FEATURE_" + i;
            if (!PlayerPrefs.HasKey(key))
                PlayerPrefs.SetInt(key, features[i].currIndex);

            features[i].currIndex = PlayerPrefs.GetInt(key);
            features[i].UpdateFeature();
        }


    }

    void SaveFeatures()
    {
        for (int i = 0; i < features.Count; i++)
        {
            string key = "FEATURE_" + i;
            PlayerPrefs.SetInt(key, features[i].currIndex);
        }

    }

}


[System.Serializable]
public class Feature
{
    public string ID;
    public int currIndex;
    public FeatureProps[] choices;
    public Image UIImage;
    public Text UIText;
    public Button previousButton;
    public Button nextButton;

    [System.Serializable]
    public struct FeatureProps
    {
        public Sprite sprite;
        public string text;
    }

    public Feature(string id, Image rend)
    {
        ID = id;
        UIImage = rend;
        UpdateFeature();
    }

    public void NextChoice()
    {
        currIndex++;
        UpdateFeature();
    }

    public void PreviousChoice()
    {
        currIndex--;
        UpdateFeature();
    }

    public void UpdateFeature()
    {

        if (choices == null || UIImage == null)
            return;

        if (choices.Length < 1)
            return;

        if (currIndex < 0)
            currIndex = choices.Length - 1;
        if (currIndex >= choices.Length)
            currIndex = 0;

        UIImage.sprite = choices[currIndex].sprite;
        UIText.text = choices[currIndex].text;
    }

}
