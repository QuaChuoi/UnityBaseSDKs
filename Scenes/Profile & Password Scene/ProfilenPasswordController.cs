using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CommonModels;
using CustomExtensions;

public class ProfilenPasswordController : MonoBehaviour
{

    public Button navigationBtn;
    public GameObject dialog;
    public Button submitButton;
    public GameObject ipf_DisplayName;
    public GameObject ipf_Name;
    public GameObject ipf_Password;
    public GameObject ipf_Birthday;
    public GameObject ipf_Email;
    public GameObject ipf_PhoneNumber;
    public GameObject popUp;

    FieldController fieldController_DisplayName;
    FieldController fieldController_Name;
    FieldController fieldController_Password;
    FieldController fieldController_Birthday;
    FieldController fieldController_Email;
    FieldController fieldController_PhoneNumber;

    ProfilenPasswordVM vm;
    DialogController dialogController; 
    PopUpMessageController popUpController;
    CommonDataModel commonDataModel = CommonDataModel.Instance;

    void Awake() 
    {
        SceneInit();
    }

    // Start is called before the first frame update
    void Start()
    {
        popUpController.okBtn.onClick.AddListener(() => { SceneManager.LoadScene(0); });
        if (SceneManager.GetPreviousSceneIndex() == 4)    
        {
            if (commonDataModel.registerString.IsValidEmail())
            {
                fieldController_Email.inputField.text = commonDataModel.registerString;
            } else {
                fieldController_PhoneNumber.inputField.text = commonDataModel.registerString;
            }
        }
        else
        {
            AutoFillField();
        }
    }

    // Update is called once per frame
    void Update()
    {
        submitButton.interactable = UpdateSubmitButtonState();
    }

    private void SceneInit()
    {
        vm = GetComponent<ProfilenPasswordVM>();
        dialogController = dialog.GetComponent<DialogController>();
        popUpController = popUp.GetComponent<PopUpMessageController>();
        fieldController_Birthday = ipf_Birthday.GetComponent<FieldController>();
        fieldController_DisplayName = ipf_DisplayName.GetComponent<FieldController>();
        fieldController_Name = ipf_Name.GetComponent<FieldController>();
        fieldController_Password = ipf_Password.GetComponent<FieldController>();
        fieldController_Email = ipf_Email.GetComponent<FieldController>();
        fieldController_PhoneNumber = ipf_PhoneNumber.GetComponent<FieldController>();

        fieldController_DisplayName.inputField.onValueChanged.AddListener(delegate { CheckFieldsStatus(fieldController_DisplayName); });
        fieldController_Name.inputField.onValueChanged.AddListener(delegate { CheckFieldsStatus(fieldController_Name); });
        fieldController_Password.inputField.onValueChanged.AddListener(delegate { CheckFieldsStatus(fieldController_Password); });
        fieldController_Birthday.inputField.onValueChanged.AddListener(delegate { CheckFieldsStatus(fieldController_Birthday); });
        fieldController_Email.inputField.onValueChanged.AddListener(delegate { CheckFieldsStatus(fieldController_Email); });
        fieldController_PhoneNumber.inputField.onValueChanged.AddListener(delegate { CheckFieldsStatus(fieldController_PhoneNumber); });

        submitButton.onClick.AddListener(SubmitButtonAction);
        navigationBtn.onClick.AddListener( () => {SceneManager.LoadPreviousScene();});


    }

    private void CheckFieldsStatus(FieldController controller) {
        string texts =  controller.GetText();
        if (texts == "" || texts == null) {
            controller.ShowCautionNote("This field cannot be left blank");
        } else {
            controller.fieldStatus = true;
        }
    }

    private bool UpdateSubmitButtonState()
    {
        return (fieldController_DisplayName.fieldStatus 
            && fieldController_Name.fieldStatus 
            && fieldController_Password.fieldStatus
            && fieldController_Birthday.fieldStatus
            && fieldController_Email.fieldStatus
            && fieldController_PhoneNumber.fieldStatus
        );
    }

    private void SubmitButtonAction()
    {
        // On signup flow
        if (SceneManager.GetPreviousSceneIndex() == 4)
        {
            vm.CallSignUpNormalRequest(fieldController_DisplayName.GetText(), fieldController_Name.GetText(), fieldController_Password.GetText(), fieldController_Birthday.GetText(), fieldController_Email.GetText(), fieldController_PhoneNumber.GetText(),
                () => { 
                    popUpController.ShowPopUp("Your signup was successfull.\nClick OK to go back to login screen."); 
                },
                (str) => { dialogController.ShowDialog("SignUp Error", str); }
            );
        }

        // On view and update profile flow
        if (SceneManager.GetPreviousSceneIndex() == 6)
        {
            vm.CallUpdateProfileRequest(fieldController_DisplayName.GetText(), fieldController_Name.GetText(), fieldController_Password.GetText(), fieldController_Birthday.GetText(), fieldController_Email.GetText(), fieldController_PhoneNumber.GetText(),
                () => { 
                    SceneManager.LoadScene(6);
                },
                (str) => { dialogController.ShowDialog("Update Profile Failed", str); }
            );
        }
    }

    private void AutoFillField()
    {
        if (commonDataModel.userModel != null) 
        {
            fieldController_DisplayName.inputField.text = UpdateString(commonDataModel.userModel.displayName);
            fieldController_Name.inputField.text = UpdateString(commonDataModel.userModel.name);
            fieldController_Birthday.inputField.text = UpdateString(commonDataModel.userModel.birthday.Substring(0,10));
            fieldController_Email.inputField.text = UpdateString(commonDataModel.userModel.email);
            fieldController_PhoneNumber.inputField.text = UpdateString(commonDataModel.userModel.phone);
        }
    }

    private string UpdateString(string str) 
    {
        return str != null ? str : "";
    }
}
