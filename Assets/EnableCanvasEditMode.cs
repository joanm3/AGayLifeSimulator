using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnableCanvasEditMode : MonoBehaviour
{

    public bool SetEnable = true;
    public RectTransform[] transforms;


    // Update is called once per frame
    void Update()
    {
        if (transforms.Length <= 0)
            return;

        foreach (RectTransform t in transforms)
        {
            t.gameObject.SetActive(SetEnable);
            t.gameObject.SetActive(SetEnable);
        }
    }
}
