using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu (fileName = "Level", menuName = "ScriptableObjects/Level", order = 0)]
public class LevelSO : ScriptableObject {
    public string levelName;
    public AudioClip music;

    public void LoadLevel () {
        SceneManager.LoadScene (levelName);
    }
    // public Sprite background;
    // public Sprite foreground;
    // public Sprite[] minerals;
    // public Sprite[] enemies;
    // public Sprite[] obstacles;
    // public Sprite[] collectables;
    // public Sprite[] powerUps;
    // public Sprite[] bosses;
    // public Sprite[] bossMinerals;
    // public Sprite[] bossCollectables;
    // public Sprite[] bossPowerUps;
    // public Sprite[] bossObstacles;
    // public Sprite[] bossEnemies;
    // public Sprite[] bossBackgrounds;
    // public Sprite[] bossForegrounds;
}