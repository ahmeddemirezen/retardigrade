using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownController : MonoBehaviour {
    public Image coolDownImage;
    public TMP_Text coolDownText;

    float max = 5;
    float current = 0;
    private void Start () {
        coolDownImage.fillAmount = 0f;
    }

    public void UpdateCooldown (float value) {
        coolDownImage.fillAmount = value / max;        
        coolDownText.text = ((max - value <= 0 ? 0 : max - value)).ToString ("0.0");
    }

    public void SetMax (float value) {
        max = value;
        current = value;
        UpdateCooldown (value);
    }
}