using System.Collections;
using System.Collections.Generic;
using SUPERCharacter;
using UnityEngine;

public class RedMineral : Mineral, IIntractable {
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