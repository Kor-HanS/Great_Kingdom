using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_MainManager : MonoBehaviour
{
    [SerializeField] private Button button_single_game;

    [SerializeField] private Button button_multi_game;

    [SerializeField] private Button button_exit_game;

    [SerializeField] private GameObject panel_game_end;

    private void Awake()
    {
        button_single_game.onClick.AddListener(OnclickButton_single_game);
        button_multi_game.onClick.AddListener(OnclickButton_multi_game);
        button_exit_game.onClick.AddListener(OnclickButton_exit_game);
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
        panel_game_end.SetActive(true);
    }
}
