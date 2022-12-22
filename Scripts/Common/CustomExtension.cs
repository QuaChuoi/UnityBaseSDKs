using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

namespace CustomExtensions
{
    //Extension methods must be defined in a static class
    public static class StringExtension
    {
        const string EMAIL_PATTERN = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        const string PHONE_NUMBER_PATTER = @"^[0-9]*$";

        // This is the extension method.
        // The first parameter takes the "this" modifier and specifies the type for which the method is defined.
        public static bool IsValidEmail (this string str)
        {
            return (Regex.IsMatch(str, EMAIL_PATTERN)) ? true : false;
        }

        public static bool IsValidPhoneNumber (this string str)
        {
            return (Regex.IsMatch(str, PHONE_NUMBER_PATTER)) ? true : false;
        }
    }

    public static class ListExtension
    {
        public static string ListJoin (this List<string> list)
        {
            return string.Join("\n", list);
        }
    }

    public static class DateTimeExtension
    {
        public static string ToDateString (this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}

