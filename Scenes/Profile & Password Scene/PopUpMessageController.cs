using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpMessageController : MonoBehaviour
{
    public GameObject popUpObject;
    public TextMeshProUGUI message;
    public Button okBtn;

    void Start()
    {
        popUpObject.SetActive(false);
        okBtn.onClick.AddListener(() => {popUpObject.SetActive(false); });
    }

    public void ShowPopUp(string _message)
    {
        message.text = _message;

        popUpObject.SetActive(true);
    }

}
