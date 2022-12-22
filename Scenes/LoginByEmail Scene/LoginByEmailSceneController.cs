using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CustomExtensions;

public class LoginByEmailSceneController : MonoBehaviour
{
    public Button navigationBtn;
    public Button signInBtn;
    public Button forgotPasswordBtn;
    public Button signUpBtn;
    public GameObject emailField;
    public GameObject passwordField;
    public GameObject dialog;

    FieldController emailFieldController;
    FieldController passwordFieldController;
    DialogController dialogController;

    LoginByEmailVM vm;

    void Awake() 
    {
        SceneInit();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSignInButtonState();
    }

    private void SceneInit()
    {
        vm = GetComponent<LoginByEmailVM>();
        dialogController = dialog.GetComponent<DialogController>();

        signInBtn.interactable = false;
         
        navigationBtn.onClick.AddListener(() => {SceneManager.LoadPreviousScene();});
        signInBtn.onClick.AddListener(SignInBtnAction);
        forgotPasswordBtn.onClick.AddListener(() => { /*SceneManager.LoadScene(3);*/ dialogController.ShowDialog("SignIn Error", "hả ai biết j đâu");});
        signUpBtn.onClick.AddListener(() => { SceneManager.LoadScene(2); });

        emailFieldController = emailField.GetComponent<FieldController>();
        passwordFieldController = passwordField.GetComponent<FieldController>();

        passwordFieldController.inputField.onValueChanged.AddListener(delegate { CheckFieldsStatus(passwordFieldController); });
        emailFieldController.inputField.onValueChanged.AddListener(delegate { CheckEmailFieldStatus(emailFieldController); });

    }

    private void SignInBtnAction()
    {
        vm.CallSignInByEmailOrPhoneRequest(emailFieldController.GetText(), passwordFieldController.GetText(),
        () => {
            SceneManager.LoadScene(6);
        }, 
        (errorString) => {
            dialogController.ShowDialog("SignIn Error", errorString); 
        });
    }

    private void UpdateSignInButtonState()
    {
        if (emailFieldController.fieldStatus && passwordFieldController.fieldStatus) 
        {
            if (!signInBtn.interactable)
            {
                signInBtn.interactable = true;
            }
        }
        else
        {
            if (signInBtn.interactable)
            {
                signInBtn.interactable = false;
            }
        }
    }    

    private void CheckFieldsStatus(FieldController controller)
    {
        string texts =  controller.GetText();
        if (texts == "" || texts == null) {
            controller.ShowCautionNote("This field cannot be left blank");
        } else {
            controller.fieldStatus = true;
        }
    }

    private void CheckEmailFieldStatus(FieldController controller)
    {
        string texts = controller.GetText();

        if (texts != null && texts != "")
        {
            if (texts.IsValidEmail() || texts.IsValidPhoneNumber())
            {
                controller.fieldStatus = true;
            }
            else
            {
                controller.fieldStatus = false;
                controller.ShowCautionNote("Your input is not email or phone number format");
            }
        }
        else
        {
            controller.fieldStatus = false;
            controller.ShowCautionNote("Please input your email or phone number");
        }
    }
    
}
