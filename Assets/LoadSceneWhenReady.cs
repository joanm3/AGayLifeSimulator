using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneWhenReady : MonoBehaviour
{

    void Update()
    {
        if (LocalizationManager.Instance != null)
            if (LocalizationManager.Instance.IsReady)
                SceneManager.LoadScene("Main");
    }

}
