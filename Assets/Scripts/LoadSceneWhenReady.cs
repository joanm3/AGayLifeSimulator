using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneWhenReady : MonoBehaviour
{
    [SerializeField]
    private bool locReady = false;
    [SerializeField]
    private bool playerReady = false;
    [SerializeField]
    private bool loadLevel = false;
    [SerializeField]
    private Button[] buttonsOfMenu;

    [SerializeField]
    private Text mainTitle;
    [SerializeField]
    private Button newGameButton;
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Dropdown languageDropdown;

    private LocalizationUIText[] texts;

    void Awake()
    {
        loadLevel = false;
        locReady = false;
        playerReady = false;

        if (newGameButton == null || continueButton == null || mainTitle == null || languageDropdown == null)
        {
            Debug.LogError("Please assign all parameters for load scene");
            Destroy(this);
            return;
        }

        newGameButton.onClick.AddListener(() => NewGame());
        continueButton.onClick.AddListener(() => Continue());

        newGameButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        mainTitle.gameObject.SetActive(false);
    }

    void Start()
    {
        texts = Resources.FindObjectsOfTypeAll(typeof(LocalizationUIText)) as LocalizationUIText[];
        languageDropdown.value = LocalizationManager.Instance.CurrentLanguageID;
        languageDropdown.onValueChanged.AddListener((value) => DropdownChanged(value));
        languageDropdown.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        if (newGameButton != null) newGameButton.onClick.RemoveAllListeners();
        if (continueButton != null) continueButton.onClick.RemoveAllListeners();
    }

    void Update()
    {
        if (LocalizationManager.Instance != null)
            if (LocalizationManager.Instance.IsReady)
            {
                locReady = true;
                newGameButton.gameObject.SetActive(true);
                continueButton.gameObject.SetActive(true);
                mainTitle.gameObject.SetActive(true);
                languageDropdown.gameObject.SetActive(true);
            }


        if (PlayerManager.Instance.IsReady)
            playerReady = true;

        continueButton.interactable = (PlayerManager.Instance.Info.NatureInitialized == 1);


        if (locReady && loadLevel && playerReady)
            SceneManager.LoadScene("Main");
    }

    public void NewGame()
    {
        PlayerManager.Instance.ReinitializeGame();
        loadLevel = true;
    }

    public void Continue()
    {
        loadLevel = true;
    }

    private void DropdownChanged(int value)
    {
        LocalizationManager.Instance.CurrentLanguageID = value;
        foreach (LocalizationUIText text in texts)
        {
            text.ReloadLocalText();
        }
    }

}
