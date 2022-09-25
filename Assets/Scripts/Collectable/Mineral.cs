using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum MineralType {
    Blue,
    Red
}

public class Mineral : MonoBehaviour {
    public GameObject mineral;
    public MineralType mineralType;
    public float amount = 1f;
    protected bool isCollected = false;
    protected bool isCollecting = false;
    public List<AudioClip> collectSounds;
    public UnityEvent OnStart;
    public UnityEvent<float> OnUpdate;
    public UnityEvent OnEnd;
    public UnityEvent OnCollect;

    public void Collect () {
        if (isCollected) return;
        OnCollect.Invoke ();
        mineral?.SetActive (false);
        isCollected = true;
    }

    protected IEnumerator StartCollect () {
        var instancePlayer = FindObjectOfType<Player> ();
        // var instanceController = FindObjectOfType<SUPERCharacterAIO> ();
        // instanceController.PausePlayer (PauseModes.MakeKinematic);
        OnStart.Invoke ();
        isCollecting = true;
        StartCoroutine (PlayCollectSound ());
        var time = (100 / instancePlayer?.bitePower) ?? 0f;
        for (int i = 0; i < time; i++) {
            // parse int to float
            OnUpdate.Invoke ((float) time - i);
            yield return new WaitForSeconds (1f);
        }
        // instanceController.UnpausePlayer ();
        instancePlayer?.Bite (mineralType, amount);
        Collect ();
        OnEnd.Invoke ();
        isCollecting = false;
    }

    protected void CancelCollect () {
        StopAllCoroutines ();
        // var instanceController = FindObjectOfType<SUPERCharacterAIO> ();
        // instanceController.UnpausePlayer ();
        OnEnd.Invoke ();
        isCollecting = false;
    }

    public IEnumerator PlayCollectSound () {
        AudioSource instanceAudio;

        if (!gameObject.TryGetComponent<AudioSource> (out instanceAudio)) {
            instanceAudio = gameObject.AddComponent<AudioSource> ();
        }

        if (instanceAudio == null) yield break;
        while (isCollecting) {
            var randomIndex = Random.Range (0, collectSounds.Count);
            instanceAudio.clip = collectSounds[randomIndex];
            instanceAudio.Play ();
            yield return new WaitForSeconds (instanceAudio.clip.length);
        }
    }
}