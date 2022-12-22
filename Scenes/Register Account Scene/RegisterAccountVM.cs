using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using APIHandler;
using CustomExtensions;

public class RegisterAccountVM: MonoBehaviour
{
    APIServices apiService = APIServices.Instance;

    public void registerAccountBehavior(string registerString, System.Action onSuccess, System.Action<string> onFail)
    {

        if (registerString.IsValidEmail()) 
        {
            var requestData = new APIRequests.OtpByEmailRequest();
            requestData.email = registerString;
            StartCoroutine(apiService.Request<APIResponse.BaseResponse>
            (
                Router.sendOtpVerifyEmail, 
                HttpMethod.POST, 
                requestData, 
                (response) => { onSuccess(); }, 
                (errorResponse) => { onFail(errorResponse.message.ListJoin()); }
            ));
        }

        if (registerString.IsValidPhoneNumber())
        {
            var requestData = new APIRequests.OtpByPhoneRequest();
            requestData.phone = registerString;
            StartCoroutine(apiService.Request<APIResponse.BaseResponse>
            (
                Router.sendOtpVerifyPhone, 
                HttpMethod.POST, 
                requestData, 
                (response) => { 
                    onSuccess(); 
                },
                (errorResponse) => { onFail(errorResponse.message.ListJoin()); }
            ));
        }   
    }
}
