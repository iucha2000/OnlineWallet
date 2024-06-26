﻿using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;


namespace OnlineWallet.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CurrencyCode
    {
         AED = 0,
         ARS = 1,   
         AUD = 2,
         BGN = 3,
         BRL = 4,
         BSD = 5,
         CAD = 6,
         CHF = 7,
         CLP = 8,
         CNY = 9,
         COP = 10,
         CZK = 11,
         DKK = 12,
         DOP = 13,
         EGP = 14,
         EUR = 15,
         FJD = 16,
         GBP = 17,
         GTQ = 18,
         HKD = 19,
         HRK = 20,
         HUF = 21,
         IDR = 22,
         ILS = 23,
         INR = 24,
         ISK = 25,
         JPY = 26,
         KRW = 27,
         KZT = 28,
         MXN = 29,
         MYR = 30,
         NOK = 31,
         NZD = 32,
         PAB = 33,
         PEN = 34,
         PHP = 35,
         PKR = 36,
         PLN = 37,
         PYG = 38,
         RON = 39,
         RUB = 40,
         SAR = 41,
         SEK = 42,
         SGD = 43,
         THB = 44,
         TRY = 45,
         TWD = 46,
         UAH = 46,
         USD = 48,
         UYU = 49,
         ZAR = 50,
    }
}
