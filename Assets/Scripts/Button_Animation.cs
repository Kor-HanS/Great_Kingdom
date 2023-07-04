using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Animation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    TMP_Text button_text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        button_text.fontSize *= 1.25f;
        gameObject.GetComponent<RectTransform>().transform.Translate(new Vector3(-2.8f, 0, 0));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button_text.fontSize *= 0.8f;
        gameObject.GetComponent<RectTransform>().transform.Translate(new Vector3(2.8f, 0, 0));
    }
}
