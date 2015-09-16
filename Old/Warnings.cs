using System.Collections.Generic;

namespace Dev4s.WebClient
{
    public class Warning
    {
        public string Message { get; set; }

        private WarningEnum _warningType;
        public WarningEnum WarningType
        {
            get { return _warningType; }
            set
            {
                switch (value)
                {
                    case WarningEnum.MissingParenthesis:
                        Message = "There is not enough ";
                        break;
                }

                _warningType = value;
            }
        }
    }

    public static class Warnings
    {
        public static void AddWarning(this Warning warnDict, string message)
        {
            //warnDict.Add(warnDict.Count, message);
        }

        public static void AddWarning(this Warning warnDict, WarningEnum warning)
        {

        }
    }

    public enum WarningEnum
    {
        MissingParenthesis
    }
}