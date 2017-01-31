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


        if (features[0].previousButton != null)
            features[0].previousButton.onClick.AddListener(() => features[0].NextChoice());
        if (features[0].nextButton != null)
            features[0].nextButton.onClick.AddListener(() => features[0].PreviousChoice());

        if (features[1].previousButton != null)
            features[1].previousButton.onClick.AddListener(() => features[1].NextChoice());
        if (features[1].nextButton != null)
            features[1].nextButton.onClick.AddListener(() => features[1].PreviousChoice());

        if (features[2].previousButton != null)
            features[2].previousButton.onClick.AddListener(() => features[2].NextChoice());
        if (features[2].nextButton != null)
            features[2].nextButton.onClick.AddListener(() => features[2].PreviousChoice());

        if (features[3].previousButton != null)
            features[3].previousButton.onClick.AddListener(() => features[3].NextChoice());
        if (features[3].nextButton != null)
            features[3].nextButton.onClick.AddListener(() => features[3].PreviousChoice());

        if (features[4].previousButton != null)
            features[4].previousButton.onClick.AddListener(() => features[4].NextChoice());
        if (features[4].nextButton != null)
            features[4].nextButton.onClick.AddListener(() => features[4].PreviousChoice());

        if (features[5].previousButton != null)
            features[5].previousButton.onClick.AddListener(() => features[5].NextChoice());
        if (features[5].nextButton != null)
            features[5].nextButton.onClick.AddListener(() => features[5].PreviousChoice());


        //CHECK LATER WHY THE FUCK ITS NOT WORKING!!
        //for (int i = 0; i < features.Count /*- 1*/; i++)
        //{
        //    if (features[i].previousButton != null)
        //        features[i].previousButton.onClick.AddListener(() => features[i].NextChoice());
        //    if (features[i].nextButton != null)
        //        features[i].nextButton.onClick.AddListener(() => features[i].PreviousChoice());
        //}

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
