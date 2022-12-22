using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using CustomExtensions; 
using CommonModels;

public class RegisterAccountController : MonoBehaviour
{
    CommonDataModel commonDataModel = CommonDataModel.Instance;

    public Button navigationBtn;
    public Button signInBtn;
    public Button continueBtn;

    public GameObject inputField;
    public GameObject dialog;
    
    FieldController inputFieldController;
    DialogController dialogController;
    RegisterAccountVM vm;

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
        UpdateContinueBtnState();
    }

    private void SceneInit()
    {
        inputFieldController = inputField.GetComponent<FieldController>();
        vm = GetComponent<RegisterAccountVM>();
        dialogController = dialog.GetComponent<DialogController>();

        navigationBtn.onClick.AddListener(() => { SceneManager.LoadPreviousScene();});
        signInBtn.onClick.AddListener(BackToWelcomeScene);
        continueBtn.onClick.AddListener(ContinueBtnAction);
        continueBtn.interactable = false;

        inputFieldController.inputField.onValueChanged.AddListener(delegate {CheckInputFieldStatus(); });
    }

    private void CheckInputFieldStatus() {
        string texts = inputFieldController.GetText();

        if (texts != null && texts != "")
        {
            if (texts.IsValidEmail() || texts.IsValidPhoneNumber())
            {
                inputFieldController.fieldStatus = true;
                continueBtn.interactable = true;
            }
            else
            {
                continueBtn.interactable = false;
                inputFieldController.fieldStatus = false;
                inputFieldController.ShowCautionNote("Your input is not email or phone number format");
            }
        }
        else
        {
            continueBtn.interactable = false;
            inputFieldController.fieldStatus = false;
            inputFieldController.ShowCautionNote("Please input your email or phone number");
        }
    }

    private void UpdateContinueBtnState() 
    {
        continueBtn.interactable = inputFieldController.fieldStatus;
    }

    private void BackToWelcomeScene() 
    {
        SceneManager.LoadScene(0);
    }

    private void ContinueBtnAction()
    {
        commonDataModel.registerString = inputFieldController.GetText();
        vm.registerAccountBehavior
        (
            inputFieldController.GetText(), 
            () => { SceneManager.LoadScene(4);},
            (str) => 
                {
                    if (str == "Request Exist") 
                    {
                        SceneManager.LoadScene(4);
                    }
                    else
                    {
                        dialogController.ShowDialog("Login Error", str); 
                    };
                }
        );
    }    
}
