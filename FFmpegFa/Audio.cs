using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFmpegFa
{
    /// <summary>
    /// سمپل رِیت صدا
    /// </summary>
    public enum AudioSampleRate
    {
        /// <summary>
        /// 8000
        /// </summary>
        _8000,
        /// <summary>
        /// 16000
        /// </summary>
        _16000,
        /// <summary>
        /// 22050
        /// </summary>
        _22050,
        /// <summary>
        /// 32000
        /// </summary>
        _32000,
        /// <summary>
        /// 44100
        /// </summary>
        _44100 = 0,
        /// <summary>
        /// 48000
        /// </summary>
        _48000 = 1,
        /// <summary>
        /// 72000
        /// </summary>
        _72000,
        /// <summary>
        /// 96000
        /// </summary>
        _96000
    }
    /// <summary>
    /// بیت رِیت صدا
    /// </summary>
    public enum AudioBitRate
    {
        /// <summary>
        /// 32k
        /// </summary>
        _32,
        /// <summary>
        /// 64k
        /// </summary>
        _64,
        /// <summary>
        /// 96k
        /// </summary>
        _96,
        /// <summary>
        /// 112k
        /// </summary>
        _112,
        /// <summary>
        /// 128k
        /// </summary>
        _128,
        /// <summary>
        /// 160k
        /// </summary>
        _160,
        /// <summary>
        /// 192k
        /// </summary>
        _192,
        /// <summary>
        /// 256k
        /// </summary>
        _256,
        /// <summary>
        /// 320k
        /// </summary>
        _320,
        /// <summary>
        /// 448k
        /// </summary>
        _448,
        /// <summary>
        /// 512k
        /// </summary>
        _512,
        /// <summary>
        /// 640k
        /// </summary>
        _640
    }
}
