using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using APIHandler;
using CommonModels;
using CustomExtensions;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class WelcomeSceneVM : MonoBehaviour
{
    APIServices apiServices = APIServices.Instance;
    CommonDataModel commonDataModel = CommonDataModel.Instance;

    public void CallRefeshTokenRequest(System.Action onSuccess)
    {
        StartCoroutine(apiServices.Request<APIResponse.SignInNormal>(
            PlayerPrefsStorage.GetString(KeyName.RefreshToken),
            Router.getRefreshToken,
            HttpMethod.GET,
            (response) => {

                // temporary | get user should be call when auto re-login by new access token
                commonDataModel.GetUserModel(response.user);
                // ----------
                Debug.Log("BIRTHDAY GOT ____ "+response.user.birthday);

                PlayerPrefsStorage.SetString(KeyName.AccessToken, response.accessToken);
                onSuccess();
            }
        ));
    }

    public void CallSocialSignInRequest(SocialType type, string token, System.Action onSuccess, System.Action<string> onFail)
    {
        SocialSignIn(type, token, onSuccess, onFail);
    }

    private void SocialSignIn(SocialType type, string token, System.Action onSuccess, System.Action<string> onFail)
    {
        var requestData = new APIRequests.SignUpSocialRequest();
        requestData.type = type.ToString();
        requestData.token = token;
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
            (errorResponse) => {
                onFail(errorResponse.message.ListJoin());
            }           
        ));
    }

    public void CallGoogleFirebaseSignIn(string email, string password, System.Action _callback)
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            Debug.Log($"email: {email} -- pass: {password}");
            if (task.IsCanceled) {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            // SceneManager.LoadScene(6); fail to load new screen affter unity 
            Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            _callback();
        });
    }

    // public void CallSocialSignUpRequest(SocialType type, string token, System.Action onSuccess, System.Action<string> onFail)
    // {
    //     SocialSignUp(type, token, onSuccess, onFail);
    // }

    // private void SocialSignUp(SocialType type, string token, System.Action onSuccess, System.Action<string> onFail)
    // {
    //     var requestData = new APIRequests.SignUpSocialRequest();
    //     requestData.type = type.ToString();
    //     requestData.token = token;

    //     StartCoroutine(apiServices.Request<APIResponse.BaseResponse>(
    //         Router.signUpSocial,
    //         HttpMethod.POST,
    //         requestData,
    //         () => { 
    //             onSuccess(); 
    //         },
    //         (errorResponse) => { onFail(errorResponse.message.ListJoin()); }
    //     ));
    // }

    

}
