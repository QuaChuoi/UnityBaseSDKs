using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdatePasswordSceneController : MonoBehaviour
{

    public Button navigationBtn;
    public GameObject dialog;
    public Button UpdateBtn;

    DialogController dialogController;
    UpdatePasswordVM vm;

    void Awake()
    {
        SceneInit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SceneInit()
    {
        vm = GetComponent<UpdatePasswordVM>();
        dialogController = dialog.GetComponent<DialogController>();

        navigationBtn.onClick.AddListener(() => {SceneManager.LoadPreviousScene();});
    }
}
