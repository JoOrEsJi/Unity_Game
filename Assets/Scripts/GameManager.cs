using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    public Protagonist protagonist;
    public FloatingTextManager floatingTextManager;
    public Animator deathMenuAnimation;

    public int coins;
    public int experience;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        protagonist = FindObjectOfType<Protagonist>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()    
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject hud = GameObject.Find("HUD");
        if (hud != null)
        {
            Transform panelTransform = hud.transform.Find("Panel");
            if (panelTransform != null)
            {
                deathMenuAnimation = panelTransform.GetComponent<Animator>();
                if (deathMenuAnimation != null)
                {
                    Debug.Log("Animator del Panel asignado correctamente.");
                }
                else
                {
                    Debug.LogWarning("El Panel no tiene un componente Animator.");
                }

                Transform deathMenuTransform = panelTransform.Find("DeathMenu");
                if (deathMenuTransform != null)
                {
                    Transform respawnButtonTransform = deathMenuTransform.Find("Respawn");
                    if (respawnButtonTransform != null)
                    {
                        Button respawnButton = respawnButtonTransform.GetComponent<Button>();
                        if (respawnButton != null)
                        {
                            respawnButton.onClick.RemoveAllListeners();

                            respawnButton.onClick.AddListener(() => Respawn());
                            Debug.Log("Función Respawn asignada al botón.");
                        }
                        else
                        {
                            Debug.LogWarning("El objeto Respawn no tiene un componente Button.");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("No se encontró el objeto Respawn dentro del DeathMenu.");
                    }
                }
                else
                {
                    Debug.LogWarning("No se encontró el objeto DeathMenu dentro del Panel.");
                }
            }
            else
            {
                Debug.LogWarning("No se encontró el objeto 'Panel' dentro del HUD.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto 'HUD' en la escena.");
        }
        protagonist = FindObjectOfType<Protagonist>();
        if (protagonist == null)
        {
            Debug.LogError("Protagonist no encontrado en la escena.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Game")
            {
                SceneManager.LoadScene("Menu");
            }
            else if (SceneManager.GetActiveScene().name == "Menu")
            {
                Application.Quit();
            }
            else if (SceneManager.GetActiveScene().name == "Lobby")
            {
                SceneManager.LoadScene("Menu");
            }
            else
            {
                Debug.Log("Botón de retroceso presionado en una escena no manejada.");
            }
        }
    }

    public void Respawn()
    {
        deathMenuAnimation.SetTrigger("Hiding");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        if (floatingTextManager != null)
        {
            floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
        }
        else
        {
            Debug.LogError("FloatingTextManager no está asignado.");
        }
    }

    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add && r < xpTable.Count)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count)
                return r;
        }

        return r;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level && r < xpTable.Count)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }

        FindObjectOfType<PauseMenu>()?.UpdateMenu();
    }

    public void OnLevelUp()
    {
        Debug.Log("Level Up");
        protagonist?.OnLevelUp();
    }

    public void SaveState()
    {
        string s = "";
        s += "0" + "|";
        s += coins.ToString() + "|";
        s += experience.ToString() + "|";
        s += "0";

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode save)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        coins = int.Parse(data[1]);
        experience = int.Parse(data[2]);

        protagonist = FindObjectOfType<Protagonist>();
        if (protagonist == null)
        {
            Debug.LogError("Protagonist no encontrado en la escena.");
            return;
        }

        if (GetCurrentLevel() != 1)
            protagonist.SetLevel(GetCurrentLevel());

        Debug.Log("Load state");
    }
}

