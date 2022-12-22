using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Facebook.Unity;
using APIHandler;
using System.Runtime.InteropServices;
using AOT;

public class WelcomeSceneController : MonoBehaviour
{

    public Button loginButton_email;
    public Button loginButton_google;
    public Button loginButton_facebook;
    public Button loginButton_kakaotalk;
    public Button loginButton_naver;
    public Button loginButton_apple;
    public Button forgotPassword;
    public Button signUp;
    
    public GameObject appleLoginButton;
    public GameObject dialog;
    public GameObject googleSignInDialog;

    WelcomeSceneVM vm;
    DialogController dialogController;
    GoogleDialogController googleDialogController;

    private string Token;


    void Awake()
    {
        SceneInit();

        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    private void SceneInit() 
    {
        vm = GetComponent<WelcomeSceneVM>();
        dialogController = dialog.GetComponent<DialogController>();
        googleDialogController =  googleSignInDialog.GetComponent<GoogleDialogController>();

        // add buttons listener
        signUp.onClick.AddListener(SignUpBtnAction);
        forgotPassword.onClick.AddListener(ForgotPasswordBtnAction);
        loginButton_email.onClick.AddListener(EmailLoginBtnAction);
        loginButton_google.onClick.AddListener(GoogleLoginBtnAction);
        loginButton_facebook.onClick.AddListener(FbLoginBtnAction);
        loginButton_kakaotalk.onClick.AddListener(KakaoLoginBtnAction);
        loginButton_naver.onClick.AddListener(NaverLoginBtnAction);
        loginButton_apple.onClick.AddListener(AppleLoginBtnAction);

        googleDialogController.signInButton.onClick.AddListener(() => { 

            // SceneManager.LoadScene(6);
            
            if ((googleDialogController.emailField.text != null && googleDialogController.emailField.text != "") && (googleDialogController.passwordField.text != null && googleDialogController.passwordField.text != ""))
            {
                // vm.CallGoogleFirebaseSignIn(googleDialogController.emailField.text, googleDialogController.passwordField.text, () => { Debug.Log("Callback Called"); SceneManager.LoadScene(6); });
                FirebaseAuthHandler(googleDialogController.emailField.text, googleDialogController.passwordField.text);
            }
        });
    }

    private void SceneStart()
    {
        #if UNITY_IOS 
            appleLoginButton.SetActive(true);
        #else
            appleLoginButton.SetActive(false);
        #endif 

        if (PlayerPrefsStorage.HasKey(KeyName.RefreshToken))
        {
            vm.CallRefeshTokenRequest(() => {
                SceneManager.LoadScene(6); 
                }
            );
        }
    }

    private void FirebaseAuthHandler(string email, string password)
    {
        vm.CallGoogleFirebaseSignIn(email, password, () => { Debug.Log("Callback Called"); googleDialogController.HideDialog(); SceneManager.LoadScene(6); });
    }

    private void SignUpBtnAction()
    {
        SceneManager.LoadScene(2);
    }

    private void ForgotPasswordBtnAction() 
    {
        SceneManager.LoadScene(3);
    }

    private void EmailLoginBtnAction()
    {
        SceneManager.LoadScene(1);
    }

    private void GoogleLoginBtnAction()
    {
        googleDialogController.ShowDialog();
    }

    private void FbLoginBtnAction()
    {
        // Define the permissions
        var perms = new List<string>() { "public_profile", "email" };

        FB.LogInWithReadPermissions(perms, result =>
        {
            if (FB.IsLoggedIn)
            {
                Token = AccessToken.CurrentAccessToken.TokenString;
                Debug.Log($"Facebook Login token: {Token}");
                vm.CallSocialSignInRequest(SocialType.FACEBOOK, Token,
                () => {
                    SceneManager.LoadScene(6);
                },
                (errorString) => {
                    dialogController.ShowDialog("Fail to Login by Facebook", errorString);
                    // vm.CallSocialSignUpRequest(SocialType.FACEBOOK, Token,
                    // () => {
                    //     SceneManager.LoadScene(6);
                    // },
                    // (errorString_) => {
                    //     //to do
                    // });
                });
            }
            else
            {
                Debug.Log("[Facebook Login] User cancelled login");
            }
        });
    }


    private void KakaoLoginBtnAction()
    {
        #if UNITY_IOS
            SendLoginKakaoIOS();
        #endif
    }

    private void NaverLoginBtnAction()
    {
        
    }

    private void AppleLoginBtnAction()
    {
        // for Kakao service test
        #if UNITY_IOS
            SendLogoutKakaoIOS();
        #endif   
    }

    #if UNITY_IOS
    public static void SendLoginKakaoIOS()
    {
        Debug.Log("@kakao - login bridge function called");
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {   
            _sendKakaoLogin();
        }
    }

    public static void SendLogoutKakaoIOS()
    {
        Debug.Log("@kakao - logout bridge function called");
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {   
            _sendKakaoLogout();
        }
    }

    [DllImport("__Internal")]
    static extern void _sendKakaoLogin();

    [DllImport("__Internal")]
    static extern void _sendKakaoLogout();

    #endif
}
