using System;

namespace Microservice.CQRS.Premium.Client.Common.Extensions
{
    public static class NumberExtensions
    {
        public static string ToDefaultIfNegativeOrZero(this int number)
        {
            return number <= 0 ? String.Empty : number.ToString();
        }
    }
}