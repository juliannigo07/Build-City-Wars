using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    [SerializeField] private GameObject[] screens;

    private Dictionary<string, GameObject> screenMap = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        foreach (GameObject screen in screens)
        {
            screenMap[screen.name] = screen;
        }
    }

    public void Show(string screenName)
    {
        foreach (var screen in screenMap.Values)
        {
            screen.SetActive(false);
        }

        if (screenMap.ContainsKey(screenName))
        {
            screenMap[screenName].SetActive(true);
        }
        else
        {
            Debug.LogWarning("Screen not found: " + screenName);
        }
    }

    public void HideAll()
    {
        foreach (var screen in screenMap.Values)
        {
            screen.SetActive(false);
        }
    }
}
