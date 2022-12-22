using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace APIHandler
{
    public enum SocialType
    {
        EMAIL,
        PHONE,
        FACEBOOK,
        KAKAO,
        NAVER
    }
    
    public class APIRequests
    {
        [Serializable]
        public class OtpByEmailRequest
        {
            public string email;
        }

        [Serializable]
        public class OtpByPhoneRequest
        {
            public string phone;
        }

        [Serializable]
        public class VerifyOtpByPhoneRequest
        {
            public string otp;
            public string phone;
        }

        [Serializable]
        public class VerifyOtpByEmailRequest
        {
            public string otp;
            public string email;
        }

        [Serializable]
        public class SignUpNormalRequest
        {
            public string name;
            public string displayName;
            public string email;
            public string phone;
            public string birthday;
            public string password;
            public string otp;
        }

        [Serializable]
        public class UpdateProfileRequest
        {
            public string name;
            public string displayName;
            public string email;
            public string phone;
            public string birthday;
            public string password;
        }

        [Serializable]
        public class SignInByEmailRequest
        {
            public string type = "EMAIL";
            public string email;
            public string password;
        }

        [Serializable]
        public class SignInByPhoneRequest
        {
            public string type = "PHONE";
            public string phone;
            public string password;
        }

        [Serializable]
        public class SignUpSocialRequest
        {
            public string token;
            public string type;
        }
    }
}

