using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnableCanvasEditMode : MonoBehaviour
{

    public bool SetEnable = true;
    public RectTransform[] transforms;

    void Awake()
    {
        if (Application.isPlaying)
            foreach (RectTransform t in transforms)
            {
                t.gameObject.SetActive(true);
                t.gameObject.SetActive(true);
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
            return;

        if (transforms.Length <= 0)
            return;

        foreach (RectTransform t in transforms)
        {
            t.gameObject.SetActive(SetEnable);
            t.gameObject.SetActive(SetEnable);
        }
    }
}
