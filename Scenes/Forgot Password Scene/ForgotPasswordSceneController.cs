using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ForgotPasswordSceneController : MonoBehaviour
{
    public Button navigationBtn;
    public Button resetBtn;
    public GameObject dialog;

    DialogController dialogController;
    ForgotPasswordVM vm;
 
    void Awake()
    {
        SceneInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SceneInit()
    {
        vm = GetComponent<ForgotPasswordVM>();
        dialogController = dialog.GetComponent<DialogController>();

        navigationBtn.onClick.AddListener(() => {SceneManager.LoadPreviousScene();});
    }
}
