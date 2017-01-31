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

    public void CopyTextFromOther(Text[] fromText)
    {
        //toText.text = fromText.text;
    }

    public void CopyTextFromOther(string fromText)
    {

    }

}
