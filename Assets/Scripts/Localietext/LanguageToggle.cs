using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageToggle : MonoBehaviour
{
    public void ToggleLanguage()
    {
        var current = LocalizationManager.Instance.GetCurrentLanguage();
        var newLang = current == LocalizationManager.Language.Español ? LocalizationManager.Language.English : LocalizationManager.Language.Español;

        LocalizationManager.Instance.LoadLanguage(newLang);
        UIManager.Instance.RefreshTexts(); 
    }
}
