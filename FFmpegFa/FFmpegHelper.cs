using System;

namespace FFmpegFa
{
    static class FFmpegHelper
    {
        public static string EncodeTime(this TimeSpan time)
        {
            return $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}.{time.Milliseconds:00}";
        }
    }
}
