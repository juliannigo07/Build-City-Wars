using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeSystem : MonoBehaviour
{
   
    public Button botonDanio;


    public int aumentoDanio = 1;

   
    public int costoDanio = 5;

    void Start()
    {
       
        if (botonDanio != null)
            botonDanio.onClick.AddListener(MejorarDanioGlobal);
    }

    
    void MejorarDanioGlobal()
    {
        if (!GameEconomy.Instance.GastarMonedas(costoDanio))
        {
            return;
        }

        foreach (Tower torre in FindObjectsOfType<Tower>())
        {
            torre.damage += aumentoDanio;
        }
    }
}
