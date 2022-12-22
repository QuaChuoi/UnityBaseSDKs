using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using APIHandler;
using CustomExtensions;
using CommonModels;

public class LoginByEmailVM : MonoBehaviour
{
    APIServices apiServices = APIServices.Instance;
    CommonDataModel commonDataModel = CommonDataModel.Instance;

    public void CallSignInByEmailOrPhoneRequest(string emailOrPhone, string password, System.Action onSuccess, System.Action<string> errorHandler)
    {
        if (emailOrPhone.IsValidEmail())
        {
            var requestData = new APIRequests.SignInByEmailRequest();
            requestData.email = emailOrPhone;
            requestData.password = password;

            SignInApiRequest<APIRequests.SignInByEmailRequest>(requestData, onSuccess, errorHandler);
        }

        if (emailOrPhone.IsValidPhoneNumber())
        {
            var requestData = new APIRequests.SignInByPhoneRequest();
            requestData.phone = emailOrPhone;
            requestData.password = password;

            SignInApiRequest<APIRequests.SignInByPhoneRequest>(requestData, onSuccess, errorHandler);
        }        
    }

    private void SignInApiRequest<T> (T requestData, System.Action onSuccess, System.Action<string> errorHandler)
    {
        StartCoroutine(apiServices.Request<APIResponse.SignInNormal>(
            Router.signInNormal,
            HttpMethod.POST,
            requestData,
            (response) => { 
                commonDataModel.GetUserModel(response.user);
                PlayerPrefsStorage.SetString(KeyName.AccessToken, response.accessToken);
                PlayerPrefsStorage.SetString(KeyName.RefreshToken, response.refreshToken);
                onSuccess(); 
            },
            (errorResponse) => { errorHandler(errorResponse.message.ListJoin()); }
        ));
    }
}
