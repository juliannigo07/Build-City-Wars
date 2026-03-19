using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set; }

    public enum Language { Español, English }
    [SerializeField] private Language currentLanguage = Language.Español;

    private Dictionary<string, string> localizedText = new();

    [SerializeField] private TextAsset spanishJSON;
    [SerializeField] private TextAsset englishJSON;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        LoadLanguage(currentLanguage);
    }

    public void LoadLanguage(Language lang)
    {
        localizedText.Clear();
        currentLanguage = lang;

        string json = lang == Language.Español ? spanishJSON.text : englishJSON.text;
        LocalizationData data = JsonUtility.FromJson<LocalizationData>(json);

        foreach (var entry in data.entries)
        {
            localizedText[entry.key] = entry.value;
        }
    }

    public string GetText(string key)
    {
        return localizedText.ContainsKey(key) ? localizedText[key] : $"#{key}";
    }

    public Language GetCurrentLanguage() => currentLanguage;

    [System.Serializable]
    public class LocalizationEntry
    {
        public string key;
        public string value;
    }

    [System.Serializable]
    public class LocalizationData
    {
        public List<LocalizationEntry> entries;
    }
}
