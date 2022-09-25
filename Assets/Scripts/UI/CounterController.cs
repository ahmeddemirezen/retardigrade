using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CounterController : MonoBehaviour
{
    public TMP_Text counterText;

    public void UpdateCounter(float newValue) {
        counterText.text = newValue.ToString();
    }
}
