using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CommonModels;

public class VerifyFieldManage : MonoBehaviour
{
    CommonDataModel commonDataModel = CommonDataModel.Instance;

    public TMP_InputField field1st;
    public TMP_InputField field2nd;
    public TMP_InputField field3rd;
    public TMP_InputField field4th;

    private bool verifyFieldStatus = false;
    public bool GetVerifyFieldStatus() 
    {
        return this.verifyFieldStatus;
    }

    void Awake() 
    {
        FieldInit();
    }
    // Start is called before the first frame update
    void Start()
    {
        FocusOnField(field1st);
    }

    // Update is called once per frame
    void Update()
    {
        this.verifyFieldStatus = (this.GetVerifyCode().Length < 4) ? false : true;
    }

    private void FieldInit() 
    {
        field1st.characterLimit = 1;
        field2nd.characterLimit = 1;
        field3rd.characterLimit = 1;
        field4th.characterLimit = 1;

        field1st.onValueChanged.AddListener(delegate {CheckFieldnChangeFocus(field1st, null, field2nd); });
        field2nd.onValueChanged.AddListener(delegate {CheckFieldnChangeFocus(field2nd, field1st, field3rd); });
        field3rd.onValueChanged.AddListener(delegate {CheckFieldnChangeFocus(field3rd, field2nd, field4th); });
        field4th.onValueChanged.AddListener(delegate {CheckFieldnChangeFocus(field4th, field3rd, null); });
    }

    private void FocusOnField(TMP_InputField field)
    {
        field.Select();
        field.ActivateInputField();
    }

    private void CheckFieldnChangeFocus(TMP_InputField inputField, TMP_InputField previousField, TMP_InputField nextField)
    {
        if ((inputField.text == null || inputField.text.ToString() == "") &&  previousField != null) {
            FocusOnField(previousField);
        } 
        else 
        {
            if (inputField.text.Length >= 1 && nextField != null)
            {
                FocusOnField(nextField);
            }
        }
    }

    public string GetVerifyCode()
    {
        return (field1st.text.ToString() + field2nd.text.ToString() + field3rd.text.ToString() + field4th.text.ToString()).ToString();
    }
}
