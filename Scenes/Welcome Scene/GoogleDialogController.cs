using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoogleDialogController : MonoBehaviour
{
    public TMP_InputField emailField;
    public TMP_InputField passwordField;

    public Button closeButton;
    public Button signInButton;

    public GameObject gameObject;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(() => { gameObject.SetActive(false); });
    }

    public void ShowDialog() {
        gameObject.SetActive(true);
    }

    public void HideDialog() {
        gameObject.SetActive(false);
    }
}
