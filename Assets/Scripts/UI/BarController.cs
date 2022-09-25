using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour {
    public RectTransform bar;
    public TextMeshProUGUI text;
    public float maxVal = 100f;
    public float curVal = 50f;
    public void UpdateBar (float newVal) {
        curVal = newVal;
        float fillAmount = curVal / maxVal;
        bar.localScale = new Vector3 (fillAmount, 1f, 1f);
        text.text = curVal.ToString () + " / " + maxVal.ToString ();
    }

    public void UpdateMaxVal (float newVal) {
        maxVal = newVal;
        float fillAmount = curVal / maxVal;
        bar.localScale = new Vector3 (fillAmount, 1f, 1f);
        text.text = curVal.ToString () + " / " + maxVal.ToString ();
    }
}