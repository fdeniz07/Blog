using System;

namespace CoreLayer.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        public static string FullDateAndTimeStringWithUnderscore(this DateTime dateTime)
        {
            return
                $"{dateTime.Millisecond}_{dateTime.Second}_{dateTime.Minute}_{dateTime.Hour}_{dateTime.Day}_{dateTime.Month}_{dateTime.Year}";

            /*
             * FatihDeniz_587_5_38_12_28_09_2021.png
             * FatihDeniz_601_5_38_12_28_09_2021_userFatihDenizResmi.png
             */
        }
    }
}
