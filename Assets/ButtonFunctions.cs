using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{

    public void SetCanvasActiveOrDesactivate(Canvas canvas)
    {

        canvas.enabled = !canvas.enabled;

    }



}
