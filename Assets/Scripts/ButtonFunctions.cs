﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{

    public void SetCanvasActiveOrDesactivate(Canvas canvas)
    {
        canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy);
    }

    public void SetCanvasActiveOrDesactivate(RectTransform transform)
    {
        transform.gameObject.SetActive(!transform.gameObject.activeInHierarchy);
    }

}
