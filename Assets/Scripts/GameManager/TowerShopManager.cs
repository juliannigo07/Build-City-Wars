using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShopManager : MonoBehaviour
{
    [SerializeField] private GameObject torreNormalPrefab;
    [SerializeField] private GameObject torreExplosivaPrefab;
    [SerializeField] private GameObject torreHieloPrefab;

    private BaseSlot[] bases;

    private void Start()
    {
        bases = FindObjectsOfType<BaseSlot>();
    }

    public void ComprarTorreNormal() => Comprar(torreNormalPrefab, 5);
    public void ComprarTorreHielo() => Comprar(torreHieloPrefab, 15);
    public void ComprarTorreExplosiva() => Comprar(torreExplosivaPrefab, 30);

    private void Comprar(GameObject torrePrefab, int costo)
    {
        if (!GameEconomy.Instance.GastarMonedas(costo))
        {
            
            return;
        }

        foreach (var baseSlot in bases)
        {
            if (baseSlot.EstaLibre)
            {
                baseSlot.ColocarTorre(torrePrefab, costo);
                return;
            }
        }

       
      
        GameEconomy.Instance.GanarDinero(costo);
    }
}
