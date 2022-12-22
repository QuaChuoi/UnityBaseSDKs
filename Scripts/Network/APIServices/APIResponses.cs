using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace APIHandler
{
    public class APIResponse
    {
        [Serializable]
        public class ErrorResponse
        {
            public int statusCode;
            public List<string> message;
            public string error;

            public ErrorResponse()
            {
            }

            public ErrorResponse(int statusCode, List<String> messages, string error)
            {
                this.statusCode = statusCode;
                this.message = messages;
                this.error = error;
            }
        }
        
        [Serializable]
        public class BaseResponse
        {
            public bool status;
        }

        [Serializable]
        public class VerifyOtpData
        {
            public DateTime created_at;
            public DateTime updated_at;
            public int id;
            public string code;
            public object email;
            public string phone;
        }

        [Serializable]
        public class Root<T>
        {
            public bool status;
            public string message;
            public List<T> data;
        }

        [Serializable]
        public class SignUpNormal
        {
            public string name;
            public string displayName;
            public string email;
            public string phone;
            public string birthday;
            public DateTime created_at;
            public DateTime updated_at;
            public int id;
            public bool isActive;
        }

        [Serializable]
        public class SignInNormal
        {
            public User user;
            public string accessToken;
            public string refreshToken;
        }

        [Serializable]
        public class BaseResponse<T>
        {
            public bool status;
            public string message;
            public T data;
        }

        [Serializable]
        public class User
        {
            public DateTime created_at;
            public DateTime updated_at;
            public int id;
            public string name;
            public string displayName;
            public string birthday;
            public string email;
            public string phone;
            public object snsType;
            public object snsId;
            public bool isActive;
        }
    
    }
}
