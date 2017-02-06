using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneWhenReady : MonoBehaviour
{

    public bool locReady = false;
    public bool loadLevel = false;
    public Button[] buttonsOfMenu;

    void Awake()
    {
        loadLevel = false;
        locReady = false;
        foreach (Button button in buttonsOfMenu)
        {
            button.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (LocalizationManager.Instance != null)
            if (LocalizationManager.Instance.IsReady)
            {
                locReady = true;
                foreach (Button button in buttonsOfMenu)
                {
                    button.gameObject.SetActive(true);
                }
            }

        if (locReady && loadLevel)
            SceneManager.LoadScene("Main");
    }

    public void NewGame()
    {
        //here will be null. call it in title scene for this. 
        //HERE HERE HERE HERE HERE IMPORTANT
        //PlayerManager.Instance.ReinitializeGame();
        loadLevel = true;
    }

    public void Continue()
    {
        loadLevel = true;
    }

}
