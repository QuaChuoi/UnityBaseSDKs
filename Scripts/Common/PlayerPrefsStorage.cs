using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyName
    {
        AccessToken,
        RefreshToken
    }

public static class PlayerPrefsStorage
{
    public static void SetString(KeyName keyName, string Value)
    {
        PlayerPrefs.SetString( keyName.ToString(), Value);
    }

    public static string GetString(KeyName keyName)
    {
        return PlayerPrefs.GetString(keyName.ToString());
            
    }

    public static void DeleteKey(KeyName keyName)
    {
        PlayerPrefs.DeleteKey(keyName.ToString());
    }

    public static bool HasKey(KeyName keyName)
    {
        return PlayerPrefs.HasKey(keyName.ToString());
    }
}
