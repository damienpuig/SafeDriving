using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Direction.Helpers.Table
{
    public static class ActionType
    {
        public enum Action : int
        {
            HyperLink,
            OpenPopup,
            CheckBox,
            CheckBoxAction,
            HyperLinkAction
        }
    }
}