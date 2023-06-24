using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_GameEnd : MonoBehaviour
{
    [SerializeField] private Button btn_yes;
    [SerializeField] private Button btn_no;

    private void Awake()
    {
        btn_yes.onClick.AddListener(OnclickButton_yes);
        btn_no.onClick.AddListener(OnclickButton_no);
    }

    private void OnclickButton_yes()
    {
        Application.Quit();
    }

    private void OnclickButton_no()
    {
        gameObject.SetActive(false);
    }

}
