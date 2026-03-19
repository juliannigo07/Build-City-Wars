using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Enemy> activeEnemies = new List<Enemy>();
    public Transform[] path;

    [Header("UI Game Over")]
    public GameObject gameOverPanel;

    [Header("Configuración")]
    public int enemigosPermitidos = 5; 

    private int enemigosQueLlegaron = 0;

   

    [Header("UI Base")]
    [SerializeField] private Image baseHealthBar;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        UpdateBaseHealthBar();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<GameUIController>().PauseGame();
        }
    }
    public void RegisterEnemy(Enemy e)
    {
        activeEnemies.Add(e);
    }

    public void OnEnemyKilled(Enemy e)
    {
        activeEnemies.Remove(e);
    }

    public void EnemyReachedEnd(Enemy e)
    {
        activeEnemies.Remove(e);

        enemigosQueLlegaron++;

        UpdateBaseHealthBar();

        if (enemigosQueLlegaron >= enemigosPermitidos)
        {
            if (gameOverPanel != null)
            {
                ScreenManager.Instance.Show("GameOverPanel");
                Time.timeScale = 0f; 
            }
        }
    }

    private void UpdateBaseHealthBar()
    {
        if (baseHealthBar != null)
        {
            float porcentaje = (float)(enemigosPermitidos - enemigosQueLlegaron) / enemigosPermitidos;
            baseHealthBar.fillAmount = porcentaje;
        }
    }

}
