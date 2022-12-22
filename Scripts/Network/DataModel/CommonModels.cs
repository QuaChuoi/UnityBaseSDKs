using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using APIHandler;

namespace CommonModels
{
    public class CommonDataModel
    {
        private CommonDataModel(){}
        private static CommonDataModel _instance = null;
        public static CommonDataModel Instance {
            get {
                if (_instance == null) {
                    _instance = new CommonDataModel();
                }
                return _instance;
            }
        }

        public string registerString { get; set; }
        public string verifyOtpCode { get; set; }

        public APIResponse.User userModel = new APIResponse.User();

        public void GetUserModel(APIResponse.User userInfor)
        {
            userModel.displayName = userInfor.displayName;
            userModel.name = userInfor.name;
            userModel.email = userInfor.email;
            userModel.birthday = userInfor.birthday;
            userModel.phone = userInfor.phone;
            userModel.id = userInfor.id;
        }
    }
}
