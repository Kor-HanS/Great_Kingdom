using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_RoomGenerate : MonoBehaviour
{
    [SerializeField] private Button button_panel_off;

    private void Awake()
    {
        button_panel_off.onClick.AddListener(OnclickButton_panel_off);
    }

    private void OnclickButton_panel_off()
    {
        this.gameObject.SetActive(false);
    }
}
