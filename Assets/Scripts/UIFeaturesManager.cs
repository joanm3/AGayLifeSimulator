using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIFeaturesManager : MonoBehaviour
{


    private FeaturesManager manager;




    // Use this for initialization
    void Start()
    {
        //make singleton
        //manager = FindObjectOfType<FeaturesManager>();
        //InitializeFeatureButtons();


        //for (int i = 0; i < manager.features.Count; i++)
        //{
        //    if (manager.features[i].previousButton != null)
        //        manager.features[i].previousButton.onClick.AddListener(() => manager.PreviousChoice(i));
        //    if (manager.features[i].nextButton != null)
        //        manager.features[i].nextButton.onClick.AddListener(() => manager.NextChoice(i));
        //}


        //lambda expressions
        //if (previousButton != null)
        //    previousButton.onClick.AddListener(() => manager.PreviousChoice());
        //if (nextButton != null)
        //    nextButton.onClick.AddListener(() => manager.NextChoice());


    }

    // Update is called once per frame
    void Update()
    {
        ////UpdateFeatureButtons();
        //EventSystem.current.SetSelectedGameObject(buttons[manager.currFeature].gameObject);
        //descText.text = manager.features[manager.currFeature].ID + " " + (manager.features[manager.currFeature].currIndex + 1);
    }


    void InitializeFeatureButtons()
    {
        //buttons = new List<Button>();

        //float height = btnPrefab.rect.height;
        //float width = btnPrefab.rect.width;

        ////dont do it like that make the menu you yourself for more control. 
        //for (int i = 0; i < manager.features.Count; i++)
        //{
        //    RectTransform temp = Instantiate<RectTransform>(btnPrefab);
        //    temp.name = i.ToString();
        //    temp.SetParent(featuresTransform);
        //    temp.localScale = Vector3.one;
        //    temp.localPosition = Vector3.zero;
        //    temp.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, width);
        //    temp.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * height, height);

        //    Button b = temp.GetComponent<Button>();
        //    //lambda expression
        //    b.onClick.AddListener(() => manager.SetCurrent(int.Parse(temp.name)));
        //    buttons.Add(b);

        //}

    }

    void UpdateFeatureButtons()
    {
        //for (int i = 0; i < manager.features.Count; i++)
        //{
        //    //check. 
        //    buttons[i].transform.FindChild("FeatureImg").GetComponent<Image>().sprite = manager.features[i].UIImage.sprite;
        //}
    }


}

