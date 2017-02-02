using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NatureSetValues : MonoBehaviour
{
    public int PointsGiven;
    public int MaxPoints;
    public string PointsUsedString = "Points Used: ";
    public Text PointsAvailableText;
    public Button ConfirmButton;

    public NatureValues[] Nature;

    [System.Serializable]
    public struct NatureValues
    {
        public string ID;
        public int value;
        public int maxValue;
        public Text valueText;
        public Text sliderInsideText;
        public string[] sliderChoicesText;
        public Button previous;
        public Button next;
    }


    void OnEnable()
    {
        //load data
        PointsGiven = 0;

        for (int i = 0; i < Nature.Length; ++i)
        {
            int index = i;
            UpdateNatureUI(ref Nature[index]);
            Nature[index].previous.onClick.AddListener(() => MinusPoints(ref Nature[index]));
            Nature[index].next.onClick.AddListener(() => PlusPoints(ref Nature[index]));
        }
        ConfirmButton.onClick.AddListener(() => OnConfirmButtonClick());
    }

    void OnDisable()
    {

        //save data

        for (int i = 0; i < Nature.Length; ++i)
        {
            int index = i;
            Nature[index].previous.onClick.RemoveAllListeners();
            Nature[index].next.onClick.RemoveAllListeners();
        }
        ConfirmButton.onClick.RemoveAllListeners();
    }


    void MinusPoints(ref NatureValues nature)
    {
        if (nature.value > 0)
        {
            nature.value--;
            PointsGiven--;
            UpdateNatureUI(ref nature);
        }
    }

    void PlusPoints(ref NatureValues nature)
    {
        if (PointsGiven >= MaxPoints)
        {
            return;
        }

        if (nature.value < nature.maxValue)
        {
            nature.value++;
            PointsGiven++;
            UpdateNatureUI(ref nature);

        }

    }

    void UpdateNatureUI(ref NatureValues nature)
    {
        PointsAvailableText.text = PointsUsedString + PointsGiven.ToString() + "/" + MaxPoints.ToString();
        nature.valueText.text = nature.value.ToString() + "/" + nature.maxValue.ToString();
        nature.sliderInsideText.text = nature.sliderChoicesText[nature.value].ToString();
    }

    void OnConfirmButtonClick()
    {

    }

}
