using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scene_GameManager : MonoBehaviour
{
    [SerializeField] private Button button_settings;

    [SerializeField] private GameObject panel_settings;

    private void Awake()
    {
        button_settings.onClick.AddListener(OnclickButton_settings);
    }

    private void OnclickButton_settings()
    {
        panel_settings.SetActive(true);
    }
}
