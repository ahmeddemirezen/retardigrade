using System.Collections;
using System.Collections.Generic;
using SUPERCharacter;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    #region Singleton Pattern
    private static GameManager instance;
    private GameManager () { } // Thread-Safety for Singleton
    static GameManager () { }
    public static GameManager Instance {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<GameManager> ();
                if (instance == null) {
                    GameObject container = new GameObject ("GameManager");
                    instance = container.AddComponent<GameManager> ();
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

        var nameOfScene = SceneManager.GetActiveScene ().name;
        var levels = Resources.LoadAll<LevelSO> ("Levels");
        foreach (var level in levels) {
            if (level.levelName == nameOfScene) {
                audioSource.PlayOneShot (level.music);
                break;
            }
        }
    }
    #endregion

    private static AudioSource audioSource {
        get {
            AudioSource temp;
            if (Instance.TryGetComponent<AudioSource> (out temp)) {
                temp.loop = false;
                temp.volume = 0.5f;
                return temp;
            } else {
                temp = Instance.gameObject.AddComponent<AudioSource> ();
                temp.loop = false;
                temp.volume = 0.5f;
                return temp;
            }
        }
    }

    public enum GameState {
        MainMenu,
        InGame,
        PauseMenu,
        GameOver,
        Win
    }

    public static GameState gameState = GameState.MainMenu;

    public static float strength = 0f;
    public static float dexterity = 0f;
    public static float level = 1f;

    public static int healthLvl = 1;
    public static int bitePowerLvl = 1;
    public static int acidPowerLvl = 1;
    public static int speedLvl = 1;
    public static int glidingDistanceLvl = 1;

    public static Player player => FindObjectOfType<Player> ();

    private void Start () { }

    public static void ChangeGameState (GameState state) {
        gameState = state;
    }

    public static bool IsInGame () {
        return gameState == GameState.InGame;
    }

    public static bool IsInMainMenu () {
        return gameState == GameState.MainMenu;
    }

    public static bool IsInPauseMenu () {
        return gameState == GameState.PauseMenu;
    }

    public static bool IsGameOver () {
        return gameState == GameState.GameOver;
    }

    public static bool IsWin () {
        return gameState == GameState.Win;
    }

    public static void PauseGame () {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ChangeGameState (GameState.PauseMenu);
    }

    public static void ResumeGame () {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ChangeGameState (GameState.InGame);
    }

    public static void GameOver () {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ChangeGameState (GameState.GameOver);
    }

    public static void Win () {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ChangeGameState (GameState.Win);
    }

    public static void RestartGame () {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ChangeGameState (GameState.InGame);
    }

    public static void StartGame () {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ChangeGameState (GameState.InGame);
    }

    public static void QuitGame () {
        Application.Quit ();
    }

    public static void LoadMainMenu () {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ChangeGameState (GameState.MainMenu);
    }

    public static void UpgradeHealthLvl () {
        if (strength >= 1) {
            healthLvl++;
            player.maxHealth = player.maxHealth + 20f;
            player.OnHealthUpgrade.Invoke (player.maxHealth);
            strength--;
            player.OnStrengthChange.Invoke (strength);
        }
    }

    public static void UpgradeBitePowerLvl () {
        if (strength >= 1) {
            bitePowerLvl++;
            player.bitePower = player.bitePower + 10f;
            player.OnBitePowerUpgrade.Invoke (player.bitePower);
            strength--;
            player.OnStrengthChange.Invoke (strength);
        }
    }

    public static void UpgradeAcidPowerLvl () {
        if (strength >= 1) {
            acidPowerLvl++;
            player.acidPower = player.acidPower + 10f;
            player.OnAcidPowerUpgrade.Invoke (player.acidPower);
            strength--;
            player.OnStrengthChange.Invoke (strength);
        }
    }

    public static void UpgradeSpeedLvl () {
        if (dexterity >= 1) {
            speedLvl++;
            player.speed = player.speed + 1f;
            player.OnSpeedUpgrade.Invoke (player.speed);
            dexterity--;
            player.OnDexterityChange.Invoke (dexterity);
        }
    }

    public static void UpgradeGlidingDistanceLvl () {
        if (dexterity >= 1) {
            glidingDistanceLvl++;
            player.glidingDistance = player.glidingDistance + 5f;
            player.OnGlidingDistanceUpgrade.Invoke (player.glidingDistance);
            dexterity--;
            player.OnDexterityChange.Invoke (dexterity);
        }
    }
}