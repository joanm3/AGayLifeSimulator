using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneWhenReady : MonoBehaviour
{

    public bool locReady = false;
    public bool loadLevel = false;

    void Awake()
    {
        loadLevel = false;
        locReady = false;
    }

    void Update()
    {
        if (LocalizationManager.Instance != null)
            if (LocalizationManager.Instance.IsReady)
                locReady = true;

        if (locReady && loadLevel)
            SceneManager.LoadScene("Main");
    }

}
