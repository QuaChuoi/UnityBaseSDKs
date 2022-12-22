using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FieldController : MonoBehaviour
{
    // public TextMeshProUGUI fiedldTitle;
    public TMP_InputField inputField;
    // public TextMeshProUGUI fieldPlaceholder;
    public TextMeshProUGUI fieldCautionNote;
    public GameObject bottomNote;
    public bool fieldStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        FieldInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (fieldStatus) 
        {
            HideCautionNote();
        }
    }

    private void FieldInit()
    {
        HideCautionNote();
    }

    public string GetText()
    {
        return inputField.text.ToString();
    }

    public void ShowCautionNote(string note)
    {
        if (note != null) {
            fieldCautionNote.text = note;
        }
        fieldStatus = false;
        bottomNote.SetActive(true);
    }

    public void HideCautionNote()
    {
        bottomNote.SetActive(false);
    }
}
