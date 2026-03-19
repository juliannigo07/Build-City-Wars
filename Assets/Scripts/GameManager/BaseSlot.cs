using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSlot : MonoBehaviour
{
    public Transform torreHolder;
    private GameObject torreActual;
    private int costoTorre;

    public bool EstaLibre => torreActual == null;

    public void ColocarTorre(GameObject torrePrefab, int costo)
    {
        if (!EstaLibre) return;

        torreActual = Instantiate(torrePrefab, torreHolder.position, Quaternion.identity);
        torreActual.transform.localScale = torrePrefab.transform.localScale;
        torreActual.transform.SetParent(torreHolder);

        var tower = torreActual.GetComponent<Tower>();
        if (tower != null)
        {
            
            

      
            string nombre = torrePrefab.name.ToLower();
            if (nombre.Contains("normal")) tower.bulletType = Tower.BulletType.Normal;
            else if (nombre.Contains("explosive")) tower.bulletType = Tower.BulletType.Explosive;
            else if (nombre.Contains("slow")) tower.bulletType = Tower.BulletType.Slow;
        }

        costoTorre = costo;
    }

    public void VenderTorre()
    {
        if (torreActual != null)
        {
            Destroy(torreActual);
            torreActual = null;

            int reembolso = Mathf.FloorToInt(costoTorre / 2f);
            GameEconomy.Instance.GanarDinero(reembolso);
            costoTorre = 0;
        }
    }
}
