using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using CommonModels;

public class VerificationSceneController : MonoBehaviour
{
    CommonDataModel commonDataModel = CommonDataModel.Instance;

    public Button navigationBtn;
    public Button reSendBtn;
    public Button continueBtn;
    public GameObject dialog;
    public GameObject verifyField;
    public TextMeshProUGUI descriptionText;

    VerifyFieldManage verifyFieldController;
    DialogController dialogController;
    VerificationVM vm;

    void Awake()
    {
        SceneInit();
    }

    // Start is called before the first frame update
    void Start()
    {
        descriptionText.text = "Verification Code was sent to " + commonDataModel.registerString;
    }

    // Update is called once per frame
    void Update()
    {
        continueBtn.interactable = verifyFieldController.GetVerifyFieldStatus() ? true : false;
    }

    private void SceneInit()
    {
        vm = GetComponent<VerificationVM>();
        verifyFieldController = verifyField.GetComponent<VerifyFieldManage>();
        dialogController = dialog.GetComponent<DialogController>();

        navigationBtn.onClick.AddListener(() => {SceneManager.LoadPreviousScene();});
        reSendBtn.onClick.AddListener(reSendBtnAction);
        continueBtn.onClick.AddListener(continueBtnAction);

        continueBtn.interactable = false;
    }

    private void reSendBtnAction()
    {
        if (commonDataModel.verifyOtpCode != null && commonDataModel.verifyOtpCode != "")
        {
            CallVMMethod();
        }
    }

    private void continueBtnAction()
    {
        commonDataModel.verifyOtpCode = verifyFieldController.GetVerifyCode();
        CallVMMethod();
    }

    private void CallVMMethod()
    {
        vm.CallVerifyOtpRequest(verifyFieldController.GetVerifyCode(), (errorString) => {
            dialogController.ShowDialog("Verification Error", errorString); 
        });
    }
}
