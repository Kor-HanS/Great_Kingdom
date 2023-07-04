using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMainManager : MonoBehaviour
{
    [SerializeField] private Button _button_single_game;

    [SerializeField] private Button _button_multi_game;

    [SerializeField] private Button _button_exit_game;

    [SerializeField] private GameObject _panel_game_end;

    private void Awake()
    {
        _button_single_game.onClick.AddListener(OnclickButton_single_game);
        _button_multi_game.onClick.AddListener(OnclickButton_multi_game);
        _button_exit_game.onClick.AddListener(OnclickButton_exit_game);
    }


    private void OnclickButton_single_game()
    {
        SceneManager.LoadScene("Scene_Game");
    }

    private void OnclickButton_multi_game()
    {
       
    }

    private void OnclickButton_exit_game()
    {
        _panel_game_end.SetActive(true);
    }
}
