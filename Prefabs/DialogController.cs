using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class DialogController : MonoBehaviour
{
    public GameObject dialogObject;
    public TextMeshProUGUI dialogTitle;
    public TextMeshProUGUI dialogDescription;
    public TextMeshProUGUI dialogMessages;
    public Button closeBtn;

    private UnityAction customAction;

    void Start()
    {
        dialogObject.SetActive(false);
        closeBtn.onClick.AddListener(() => {dialogObject.SetActive(false);});
    }

    public void ShowDialog(string title, string description, string messages)
    {
        dialogTitle.text = title;
        if (description != null && description != "")
        {
            dialogDescription.text = description;
        }
        dialogMessages.text = messages;

        dialogObject.SetActive(true);
    }

    public void ShowDialog(string title, string messages)
    {
        ShowDialog(title, null, messages);
    }
}
