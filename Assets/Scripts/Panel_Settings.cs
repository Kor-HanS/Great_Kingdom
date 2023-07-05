using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panel_Settings : MonoBehaviour
{
    [SerializeField] private Button btn_setting_off;
    [SerializeField] private Button btn_main;
    [SerializeField] private Button btn_game_end;

    [SerializeField] private GameObject panel_game_end;
    private void Awake()
    {
        btn_setting_off.onClick.AddListener(OnclickButton_setting_off);
        btn_main.onClick.AddListener(OnclickButton_main);
        btn_game_end.onClick.AddListener(OnclickButton_game_end);
    }
    private void OnclickButton_main()
    {
        SceneManager.LoadScene("Scene_Main");
    }
    private void OnclickButton_setting_off()
    {
        gameObject.SetActive(false);
    }

    private void OnclickButton_game_end()
    {
        panel_game_end.SetActive(true);
    }
}
