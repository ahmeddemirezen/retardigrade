using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMenuController : MonoBehaviour {
    public KeyCode openMenuKey = KeyCode.U;
    GameObject frame;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI bitePowerText;
    public TextMeshProUGUI acidPowerText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI glidingDistanceText;
    private void Start () {
        frame = transform.Find ("Frame").gameObject;
        healthText.text = GameManager.healthLvl.ToString ();
    }
    private void Update () {
        if (Input.GetKeyDown (openMenuKey)) {
            if (frame.activeSelf) {
                frame.SetActive (false);
                GameManager.ResumeGame ();
            } else {
                frame.SetActive (true);
                GameManager.PauseGame ();
            }
        }
    }

    public void UpgradeHealth () {
        GameManager.UpgradeHealthLvl ();
        healthText.text = GameManager.healthLvl.ToString ();
    }

    public void UpgradeBitePower () {
        GameManager.UpgradeBitePowerLvl ();
        bitePowerText.text = GameManager.bitePowerLvl.ToString ();
    }

    public void UpgradeAcidPower () {
        GameManager.UpgradeAcidPowerLvl ();
        acidPowerText.text = GameManager.acidPowerLvl.ToString ();
    }

    public void UpgradeSpeed () {
        GameManager.UpgradeSpeedLvl ();
        speedText.text = GameManager.speedLvl.ToString ();
    }

    public void UpgradeGlidingDistance () {
        GameManager.UpgradeGlidingDistanceLvl ();
        glidingDistanceText.text = GameManager.glidingDistanceLvl.ToString ();
    }
}