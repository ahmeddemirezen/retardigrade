using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ValueController : MonoBehaviour
{
    public TMP_Text valueText;
    public float value = 0f;
    private void Start() {
        UpdateValue(value);
    }
    public void UpdateValue(float newValue) {
        value = newValue;
        valueText.text = value.ToString();
    }
}
