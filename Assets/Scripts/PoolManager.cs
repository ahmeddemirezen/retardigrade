using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {
    #region Singleton Pattern
    private static PoolManager instance;
    private PoolManager () { } // Thread-Safety for Singleton
    static PoolManager () { }
    public static PoolManager Instance {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<PoolManager> ();
                if (instance == null) {
                    GameObject container = new GameObject ("PoolManager");
                    instance = container.AddComponent<PoolManager> ();
                }
            }
            return instance;
        }

        set {
            instance = value;
        }
    }

    private void Awake () {
        if (instance != null && instance != this) {
            Destroy (this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad (this.gameObject);
    }
    #endregion
    private static Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>> ();

    public static void CreatePool (GameObject prefab, int poolSize) {
        string poolName = prefab.name;
        if (!poolDictionary.ContainsKey (poolName)) {
            poolDictionary.Add (poolName, new Queue<GameObject> ());
            for (int i = 0; i < poolSize; i++) {
                GameObject obj = Instantiate (prefab);
                obj.SetActive (false);
                poolDictionary[poolName].Enqueue (obj);
            }
        }
    }

    public static GameObject ReuseObject (GameObject prefab, Vector3 position, Quaternion rotation) {
        string poolName = prefab.name;
        if (poolDictionary.ContainsKey (poolName)) {
            GameObject obj = poolDictionary[poolName].Dequeue ();
            obj.SetActive (true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }
        return null;
    }

    public static void ReturnObject (GameObject obj) {
        string poolName = obj.name.Remove(obj.name.Length - 7);
        if (poolDictionary.ContainsKey (poolName)) {
            obj.SetActive (false);
            poolDictionary[poolName].Enqueue (obj);
        }
    }
}