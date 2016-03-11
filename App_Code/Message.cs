using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helper
{
    public static class Message
    {
        //Put all your message type here. Make sure that you add a CSS Class with same name of this type
        public enum Type
        {
            error
        };

        //all Messages
        public static class Text
        {
            public const string ERROR_SERVER = "Login Error! Invalid Username or Password";
            public const string GroupRegisterError = "Group Name or Admin Name Already Taken";
        }
    }
}