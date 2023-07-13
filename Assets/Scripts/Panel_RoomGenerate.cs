using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GreatKingdomClient;
using TMPro;

public class Panel_RoomGenerate : MonoBehaviour
{
    [SerializeField] private Button button_panel_off;
    [SerializeField] private Button button_room_generate;

    [SerializeField] private TMP_InputField inputfield_room_number;

    private string roomNumber_str = null;

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
        int roomNumber_int;
        roomNumber_str = inputfield_room_number.text;
        PlayerPrefs.SetString("CurrentRoomNumber", roomNumber_str);

        if (int.TryParse(roomNumber_str, out roomNumber_int))
        {
            Debug.Log("숫자 변환 완료");
        }
        else
        {
            Debug.Log("숫자가 아닙니다.");
        }

        NetworkManager.Client?.CreateGameRoom(roomNumber_int);
        button_room_generate.interactable = false;
    }
}
