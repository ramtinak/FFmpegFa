using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFmpegFa
{
    /// <summary>
    /// کدک تایپ
    /// </summary>
    public enum ConverterCodecType
    {
        /// <summary>
        /// کدک ایکس 264 تقریبا بهترین کدک هست و همه ی فایل های تصویری رو پشتیبانی میکنه
        /// </summary>
        x264,
        /// <summary>
        /// کدک ایکس 265 فقط مخصوص فایل های ام کی وی هست
        /// </summary>
        x265
    }
}
