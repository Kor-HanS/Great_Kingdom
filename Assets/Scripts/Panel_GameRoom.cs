using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Panel_GameRoom : MonoBehaviour
{
    [SerializeField] private Button button_panel_off;
    [SerializeField] private Button button_room_generate;
    [SerializeField] private Button button_room_list;
    [SerializeField] private Button button_room_enter;


    [SerializeField] private GameObject panel_room_generate;

    private void Awake()
    {
        button_panel_off.onClick.AddListener(OnclickButton_panel_off);
        button_room_generate.onClick.AddListener(OnclickButton_room_generate);
    }
    private void OnclickButton_panel_off()
    {
        this.gameObject.SetActive(false);
    }
    private void OnclickButton_room_generate()
    {
        panel_room_generate.SetActive(true);
    }


}
