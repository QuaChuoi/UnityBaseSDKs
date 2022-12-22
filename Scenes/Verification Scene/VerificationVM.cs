using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using APIHandler;
using CommonModels;
using CustomExtensions;

public class VerificationVM : MonoBehaviour
{
    APIServices apiServices = APIServices.Instance;
    CommonDataModel commonDataModel = CommonDataModel.Instance;

    public void CallVerifyOtpRequest(string OtpString, System.Action<string> errorHandler)
    {
        if (commonDataModel.registerString.IsValidEmail())
        {
            var requestData = new APIRequests.VerifyOtpByEmailRequest();
            requestData.email = commonDataModel.registerString;
            requestData.otp = OtpString;

            VerifyApiRequest<APIRequests.VerifyOtpByEmailRequest>(requestData, errorHandler);
        }

        if (commonDataModel.registerString.IsValidPhoneNumber())
        {
            var requestData = new APIRequests.VerifyOtpByPhoneRequest();
            requestData.phone = commonDataModel.registerString;
            requestData.otp = OtpString;

            VerifyApiRequest<APIRequests.VerifyOtpByPhoneRequest>(requestData, errorHandler);
        }        
    }

    private void VerifyApiRequest<T> (T requestData, System.Action<string> errorHandler)
    {
        StartCoroutine(apiServices.Request<APIResponse.Root<APIResponse.VerifyOtpData>>(
            Router.verifyOtp,
            HttpMethod.POST,
            requestData,
            (response) => {
                // Debug.Log(">>> CHECK "+response.status+response.message+"-----"+response.data[0].email);
                SceneManager.LoadScene(5); 
            },
            (errorResponse) => { errorHandler(errorResponse.message.ListJoin()); }
        ));
    }
}
