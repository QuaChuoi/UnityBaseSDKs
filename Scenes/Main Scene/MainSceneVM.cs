using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using APIHandler;
using CustomExtensions;

public class MainSceneVM : MonoBehaviour
{

    APIServices apiServices = APIServices.Instance;

    public void CallSignOutRequest()
    {
        StartCoroutine(apiServices.Request<APIResponse.BaseResponse>(
            PlayerPrefsStorage.GetString(KeyName.AccessToken),
            Router.signOut,
            HttpMethod.POST
        ));
        PlayerPrefsStorage.DeleteKey(KeyName.AccessToken);
        PlayerPrefsStorage.DeleteKey(KeyName.RefreshToken);
    }
}
