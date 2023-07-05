using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Scene_TitleManager : MonoBehaviour
{

    [SerializeField] private TMP_Text text_title;
    private void Start()
    {
        StartCoroutine("Title_dynamic");
    }

    IEnumerator Title_dynamic()
    {

        while(text_title.alpha < 1)
        {
            text_title.alpha += 0.1f;

            yield return new WaitForSeconds(0.2f);

        }

        SceneManager.LoadScene("Scene_Main");
    }
}
