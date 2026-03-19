using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSellHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Clic derecho
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var baseSlot = hit.collider.GetComponent<BaseSlot>();
                if (baseSlot != null && !baseSlot.EstaLibre)
                {
                    baseSlot.VenderTorre();
                }
            }
        }
    }
}
