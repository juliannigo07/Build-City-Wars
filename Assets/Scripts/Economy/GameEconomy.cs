using UnityEngine;
using TMPro;

public class GameEconomy : MonoBehaviour
{
    public static GameEconomy Instance;

    public int monedas = 10;
    public TextMeshProUGUI monedasText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        ActualizarUI(); 
    }
    public void GanarDinero(int cantidad)
    {
        monedas += cantidad;
        ActualizarUI();
    }

    public bool GastarMonedas(int cantidad)
    {
        if (monedas >= cantidad)
        {
            monedas -= cantidad;
            ActualizarUI();
            return true;
        }
        return false;
    }

    private void ActualizarUI()
    {
        if (monedasText != null)
            monedasText.text = $"Monedas: {monedas}";
        else
            Debug.LogWarning("monedasText no está asignado.");
    }
}
