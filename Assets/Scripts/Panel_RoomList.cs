using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreatKingdomClient;
using TMPro;
using UnityEngine.UI;

public class Panel_RoomList : MonoBehaviour
{

    [SerializeField] private Button button_panel_off;
    [SerializeField] private Button button_page_left;
    [SerializeField] private Button button_page_right;

    [SerializeField] private GameObject[] room_obj;

    private int page_now = 1; // 0 ~4 까지 
    private RoomDatas roomData;

    private void Awake()
    {
        button_page_left.interactable = false;
        button_panel_off.onClick.AddListener(OnclickButton_panel_off);
        button_page_left.onClick.AddListener(OnclickButton_page_left);
        button_page_right.onClick.AddListener(OnclickButton_page_right);
    }

    private void Start()
    {
        Update_room_list();
    }

    private void OnclickButton_panel_off()
    {
        this.gameObject.SetActive(false);
    }

    private void OnclickButton_page_left()
    {
        if (page_now == 4)
            button_page_right.interactable = true;

        page_now--;
        if (page_now == 1)
            button_page_left.interactable = false;

        // 방 리스트 정보 수정
        Update_room_list();
    }
    private void OnclickButton_page_right()
    {
        if (page_now == 1)
            button_page_left.interactable = true;

        page_now++;

        if (page_now == 4)
            button_page_right.interactable = false;

        // 방 리스트 정보 수정
        Update_room_list();
    }

    private void Update_room_list()
    {
        roomData = NetworkManager.Client.GetGameRooms(0); // 0번 부터 20번까지 방 정보 업데이트
        for (int i = 0; i < 5; i++)
            room_obj[i].SetActive(false);

        int now = (page_now - 1)*5;
        for(int i = now; i < now +5; i++)
        {
            if(i < roomData.roomNum)
            {
                room_obj[i].SetActive(true);
                room_obj[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = $"방 번호 #{i} | 방 이름 : {roomData.roomInfo[i].roomID}";
            }
        }
    }


}
