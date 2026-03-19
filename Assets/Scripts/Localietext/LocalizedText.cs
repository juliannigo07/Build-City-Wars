using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string key;

    private TextMeshProUGUI textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (textComponent == null)
            textComponent = GetComponent<TextMeshProUGUI>();

        if (textComponent != null && !string.IsNullOrEmpty(key))
        {
            textComponent.text = LocalizationManager.Instance.GetText(key);
        }
    }
}
