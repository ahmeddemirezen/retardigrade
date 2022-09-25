using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMineral : Mineral, IIntractable {
    public bool Interact () {
        Debug.Log ("Interact");
        if (isCollecting) {
            CancelCollect ();
            isCollected = false;
            return false;
        }
        if (isCollected) return false;
        StartCoroutine (StartCollect ());
        return true;
    }
}