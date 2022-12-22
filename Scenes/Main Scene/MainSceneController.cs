using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
    public Button backToLoginBtn;
    public Button profileBtn;
    public Button signOutBtn;

    public GameObject dialog;
    DialogController dialogController;

    MainSceneVM vm;

    void Awake()
    {
        SceneInit();
    }
    
    private void SceneInit()
    {
        vm = GetComponent<MainSceneVM>();
        dialogController = dialog.GetComponent<DialogController>();

        backToLoginBtn.onClick.AddListener(() => {SceneManager.LoadScene(0); });
        profileBtn.onClick.AddListener(() => {SceneManager.LoadScene(5); });
        signOutBtn.onClick.AddListener(SignOutButtonAction);
    }

    private void SignOutButtonAction()
    {
        vm.CallSignOutRequest();
        SceneManager.LoadScene(0);
    }

}
