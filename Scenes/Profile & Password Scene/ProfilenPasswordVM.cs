using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using APIHandler;
using CommonModels;
using CustomExtensions;

public class ProfilenPasswordVM : MonoBehaviour
{
    APIServices apiServices = APIServices.Instance;
    CommonDataModel commonDataModel = CommonDataModel.Instance;

    public void CallSignUpNormalRequest(string displayName, string name, string password, string birthday, string email, string phoneNumber, System.Action onSuccess, System.Action<string> onFail) 
    {
        var requestData = new APIRequests.SignUpNormalRequest();
        requestData.displayName = displayName;
        requestData.name = name;
        requestData.password = password;
        requestData.birthday = birthday;
        requestData.email = email;
        requestData.phone = phoneNumber;
        requestData.otp = commonDataModel.verifyOtpCode;

        StartCoroutine(apiServices.Request<APIResponse.SignUpNormal>(
            Router.signUpNormal, 
            HttpMethod.POST, 
            requestData, 
            (response) => { onSuccess(); }, 
            (errorResponse) => { onFail(errorResponse.message.ListJoin()); }
        ));
    }

    public void CallUpdateProfileRequest(string displayName, string name, string password, string birthday, string email, string phoneNumber, System.Action onSuccess, System.Action<string> onFail)
    {
        var requestData = new APIRequests.UpdateProfileRequest();
        requestData.displayName = displayName;
        requestData.name = name;
        requestData.password = password;
        requestData.birthday = birthday;
        requestData.email = email;
        requestData.phone = phoneNumber;

        StartCoroutine(apiServices.Request<APIResponse.User>(
            PlayerPrefsStorage.GetString(KeyName.AccessToken),
            Router.updateUser, 
            HttpMethod.PUT, 
            requestData, 
            (response) => { 
                commonDataModel.GetUserModel(response);
                onSuccess(); 
            }, 
            (errorResponse) => { onFail(errorResponse.message.ListJoin()); }
        ));
    }
}
