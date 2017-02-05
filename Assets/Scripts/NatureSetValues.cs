using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NatureSetValues : MonoBehaviour
{
    [Header("Points Values")]
    public int PointsGiven;
    public int MaxPoints;
    public string PointsUsedString = "Points Used: ";
    public Text PointsAvailableText;

    [Header("Confirm Values")]
    public Button ConfirmButton;
    [TextArea(3, 10)]
    public string ConfirmTextWithMaxPoints;
    [TextArea(3, 10)]
    public string ConfirmTextWithoutMaxPoints;
    public Text ConfirmText;

    [Header("Canvas Open/Close")]
    public RectTransform EditProfileCanvas;
    public RectTransform[] HideTransformsBeforeChoosingNature;
    public RectTransform[] HideTransformsAfterChoosingNature;
    [Header("Reset Game")]
    public Button ResetGameButton;

    [Header("List Of Natures")]
    public NatureValues[] Nature;

    [System.Serializable]
    public struct NatureValues
    {
        public string ID;
        public NatureType type;
        public int value;
        public int maxValue;
        public Text valueText;
        public Text sliderInsideText;
        public string[] sliderChoicesText;
        public Button previous;
        public Button next;

        public enum NatureType { PrettyFace, DickSize, AssEndurance, OralTalent };
    }


    void OnEnable()
    {
        PointsGiven = 0;

        for (int i = 0; i < Nature.Length; ++i)
        {
            int index = i;
            UpdateNatureUI(ref Nature[index]);
            Nature[index].previous.onClick.AddListener(() => MinusPoints(ref Nature[index]));
            Nature[index].next.onClick.AddListener(() => PlusPoints(ref Nature[index]));
        }
        ConfirmButton.onClick.AddListener(() => OnConfirmButtonClick());
        ResetGameButton.onClick.AddListener(() => OnResetButtonClick());

        //if player hasnt been chosen yet, do this. 
        if (PlayerManager.Instance != null)
        {
            if (PlayerManager.Instance.Info.NatureInitialized == 0)
            {
                Debug.Log("Playermanager reinitialized to Standard Values");
                Debug.LogFormat("this: {0}, other: {1}", PlayerManager.Instance.Info.Profile.Name, PlayerManager.Instance.StartingDefaultPlayerInfo.Profile.Name);
                PlayerManager.Instance.Info = PlayerManager.Instance.StartingDefaultPlayerInfo.Clone();
                EditProfileCanvas.gameObject.SetActive(true);
                for (int i = 0; i < HideTransformsBeforeChoosingNature.Length; i++)
                {
                    HideTransformsBeforeChoosingNature[i].gameObject.SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < HideTransformsAfterChoosingNature.Length; i++)
                {
                    HideTransformsAfterChoosingNature[i].gameObject.SetActive(false);
                }
            }
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < Nature.Length; ++i)
        {
            int index = i;
            Nature[index].previous.onClick.RemoveAllListeners();
            Nature[index].next.onClick.RemoveAllListeners();
        }
        ConfirmButton.onClick.RemoveAllListeners();
        ResetGameButton.onClick.RemoveAllListeners();
    }

    void Start()
    {
        if (PlayerManager.Instance.Info.NatureInitialized == 1)
        {
            for (int i = 0; i < HideTransformsBeforeChoosingNature.Length; i++)
            {
                HideTransformsBeforeChoosingNature[i].gameObject.SetActive(true);
            }
            EditProfileCanvas.gameObject.SetActive(false);
        }
        else
        {
            EditProfileCanvas.gameObject.SetActive(true);
            for (int i = 0; i < HideTransformsBeforeChoosingNature.Length; i++)
            {
                HideTransformsBeforeChoosingNature[i].gameObject.SetActive(false);
            }
        }
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
        PointsAvailableText.text = LocalizationManager.Instance.GetText(PointsUsedString) + PointsGiven.ToString() + "/" + MaxPoints.ToString();
        nature.valueText.text = nature.value.ToString() + "/" + nature.maxValue.ToString();
        nature.sliderInsideText.text = LocalizationManager.Instance.GetText(nature.sliderChoicesText[nature.value]);
        ConfirmText.text = (PointsGiven >= MaxPoints) ? LocalizationManager.Instance.GetText(ConfirmTextWithMaxPoints) :
                                                        LocalizationManager.Instance.GetText(ConfirmTextWithoutMaxPoints);
    }

    void OnConfirmButtonClick()
    {
        for (int i = 0; i < Nature.Length; i++)
        {
            switch (Nature[i].type)
            {
                case NatureValues.NatureType.PrettyFace:
                    PlayerManager.Instance.Info.Nature.PrettyFace = Nature[i].value;
                    break;
                case NatureValues.NatureType.DickSize:
                    PlayerManager.Instance.Info.Nature.DickSize = Nature[i].value;
                    break;
                case NatureValues.NatureType.AssEndurance:
                    PlayerManager.Instance.Info.Nature.AssEndurance = Nature[i].value;
                    break;
                case NatureValues.NatureType.OralTalent:
                    PlayerManager.Instance.Info.Nature.OralTalent = Nature[i].value;
                    break;
            }
            PlayerManager.Instance.Info.NatureInitialized = 1;
            PlayerManager.Instance.SaveFeatures();

            if (UIManager.Instance == null)
                UIManager.Init();
            UIManager.Instance.UpdateAllUI();
        }
    }

    void OnResetButtonClick()
    {
        PlayerManager.Instance.Info.NatureInitialized = 0;
        PlayerManager.Instance.ReinitializeGame();

        EditProfileCanvas.gameObject.SetActive(true);
        for (int i = 0; i < HideTransformsBeforeChoosingNature.Length; i++)
        {
            HideTransformsBeforeChoosingNature[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < HideTransformsAfterChoosingNature.Length; i++)
        {
            HideTransformsAfterChoosingNature[i].gameObject.SetActive(true);
        }
    }

}
