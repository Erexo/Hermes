using System;

namespace Hermes.Infrastructure.Extensions
{
    public static class DateTimeExtension
    {
        public static int ToEpoch(this DateTime date)
            => (int)(date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }
}
