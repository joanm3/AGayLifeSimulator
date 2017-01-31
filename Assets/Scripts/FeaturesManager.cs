using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class FeaturesManager : MonoBehaviour
{

    public List<Feature> features;
    public int currFeature;


    void OnEnable()
    {
        LoadFeatures();
    }

    void OnDisable()
    {
        SaveFeatures();
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

    public void SetCurrent(int index)
    {
        if (features == null)
            return;

        currFeature = index;
    }

    public void NextChoice()
    {
        if (features == null)
            return;
        features[currFeature].currIndex++;
        features[currFeature].UpdateFeature();
    }

    public void PreviousChoice()
    {
        if (features == null)
            return;
        features[currFeature].currIndex--;
        features[currFeature].UpdateFeature();
    }

}


[System.Serializable]
public class Feature
{
    public string ID;
    public int currIndex;
    public Sprite[] choices;
    public Image UIImage;

    public Feature(string id, Image rend)
    {
        ID = id;
        UIImage = rend;
        UpdateFeature();
    }

    public void UpdateFeature()
    {

        if (choices == null || UIImage == null)
            return;

        if (currIndex < 0)
            currIndex = choices.Length - 1;
        if (currIndex >= choices.Length)
            currIndex = 0;

        UIImage.sprite = choices[currIndex];

    }

}
