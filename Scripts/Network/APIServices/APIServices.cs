using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using CustomExtensions;

namespace APIHandler
{
    public enum HttpMethod
    {
        POST,
        GET,
        PUT
    }

    public enum Router
    {   
        sendOtpVerifyEmail,
        sendOtpVerifyPhone,
        verifyOtp,
        signUpNormal,
        signInNormal,
        updateUser,
        signOut,
        getRefreshToken,
        signUpSocial
    }

    public class APIServices
    {
        const string BASE_URL = "http://34.125.219.87:3000";
        const int REQUEST_TIMEOUT = 10;

        private APIServices(){}
        private static APIServices _instance = null;
        public static APIServices Instance {
            get {
                if (_instance == null) {
                    _instance = new APIServices();
                }
                return _instance;
            }
        }

        private string getRouter(Router router)
        {
            switch (router)
            {
                case Router.sendOtpVerifyEmail: return BASE_URL + "/api/v1/authentication/send-email-otp";
                case Router.sendOtpVerifyPhone: return BASE_URL + "/api/v1/authentication/send-phone-otp";
                case Router.verifyOtp: return BASE_URL + "/api/v1/authentication/verify-otp";
                case Router.signUpNormal: return BASE_URL + "/api/v1/authentication/register";
                case Router.signInNormal: return BASE_URL + "/api/v1/authentication/signin";
                case Router.updateUser: return BASE_URL + "/api/v1/user";
                case Router.signOut: return BASE_URL + "/auth/sign-out";
                case Router.getRefreshToken: return BASE_URL + "/api/v1/authentication/refresh-token";
                case Router.signUpSocial: return BASE_URL + "/api/v1/authentication/signup-by-sns";
                default: return "NULL";
            }
        }

        public IEnumerator Request<T>(string apiToken, Router router, HttpMethod httpMethod, object parameter, System.Action<T> onSuccess, System.Action<APIResponse.ErrorResponse> onFail)
        {
            var req = new UnityWebRequest(getRouter(router), httpMethod.ToString());

            Debug.Log("<color=white><<REQUEST>>\nURL: " + getRouter(router) + "</color>");
            if (parameter != null) 
            {
                string jsonData = JsonUtility.ToJson(parameter);
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
                req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
                Debug.Log("\nJSON: <color=white>"+ jsonData +"</color>");
            }

            req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            if (apiToken != null && apiToken != "")
            {
                req.SetRequestHeader("Authorization", apiToken);
            }
            req.SetRequestHeader("Content-Type", "application/json");
            req.timeout = REQUEST_TIMEOUT;

            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error While Sending: " + req.error);
            }
            else
            {
                if (!req.downloadHandler.isDone) 
                {
                    string errorJSON = req.downloadHandler.text;
                    APIResponse.ErrorResponse errorResponse = JsonUtility.FromJson<APIResponse.ErrorResponse>(errorJSON);

                    Debug.LogFormat("<color=red><<ERROR>> -- {0}\nStatus Code: {1}\nMessage: \n{2}</color>", errorResponse.error, errorResponse.statusCode.ToString(), errorResponse.message.ListJoin());
                    if (onFail != null)
                    {
                        onFail(errorResponse);
                    }
                }
                else
                {
                    string responseString = req.downloadHandler.text;

                    byte[] result = req.downloadHandler.data;
                    string dataJSON = System.Text.Encoding.Default.GetString(result);
                    
                    APIResponse.BaseResponse<T> response = JsonUtility.FromJson<APIResponse.BaseResponse<T>>(dataJSON);
                    // Debug.LogFormat("STATUS: {0}, MESS: {1}", response.status, response.message);
                    T responseData = response.data;

                    if (response.status)
                    {
                        Debug.LogFormat("<color=yellow><<SUCCESS>>\nResponse: {0}</color>", responseString);
                        if (onSuccess != null)
                        {
                            onSuccess(responseData);
                        }
                    }
                    else
                    {
                        Debug.LogFormat("<color=red><<FAIL>>\nResponse: {0}</color>", responseString);
                        if (onFail != null)
                        {                            
                            var errorResponse_ = new APIResponse.ErrorResponse(0, (new List<string> {response.message}), "");
                            onFail(errorResponse_);
                        }  
                    }
                }
            }
        }

        // params: APIToken, Router, HttpMethod
        public IEnumerator Request<T>(string apiToken, Router router, HttpMethod httpMethod)
        {
            return Request<T>( apiToken, router, httpMethod, null, null, null);
        }

        // params: APIToken, Router, HttpMethod, onSuccess, onFail
        public IEnumerator Request<T>(string apiToken, Router router, HttpMethod httpMethod,System.Action<T> onSuccess, System.Action<APIResponse.ErrorResponse> onFail)
        {
            return Request<T>( apiToken, router, httpMethod, null, onSuccess, onFail);
        }

        // params: Router, HttpMethod, parameter
        public IEnumerator Request<T>(Router router, HttpMethod httpMethod, object parameter)
        {
            return Request<T>( null, router, httpMethod, parameter, null, null);
        }

        // params: Router, HttpMethod, parameter, onFail
        public IEnumerator Request<T>(Router router, HttpMethod httpMethod, object parameter, System.Action<APIResponse.ErrorResponse> onFail)
        {
            return Request<T>( null, router, httpMethod, parameter, null, onFail);
        }

        // params: Router, HttpMethod, parameter, onSuccess, onFail
        public IEnumerator Request<T>(Router router, HttpMethod httpMethod, object parameter, System.Action<T> onSuccess, System.Action<APIResponse.ErrorResponse> onFail)
        {
            return Request<T>( null, router, httpMethod, parameter, onSuccess, onFail);
        }

        // params: APIToken, Router, HttpMethod, onSuccess
        public IEnumerator Request<T>(string apiToken, Router router, HttpMethod httpMethod, System.Action<T> onSuccess)
        {
            return Request<T>( apiToken, router, httpMethod, null, onSuccess, null);
        }
    } 
}