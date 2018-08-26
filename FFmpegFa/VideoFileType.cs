using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFmpegFa
{
    /// <summary>
    /// سایز ویدیو
    /// </summary>
    public class VideoSize
    {
        /// <summary>
        /// عرض ویدیو
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// طول ویدیو
        /// </summary>
        public int Height { get; set; }

        public VideoSize() : this(0, 0) { }
        /// <summary>
        /// تعیین عرض و طول ویدیو
        /// </summary>
        /// <param name="width">عرض ویدیو</param>
        /// <param name="height">طول ویدیو</param>
        public VideoSize(int width, int height) { Width = width; Height = height; }
    }
    /// <summary>
    /// بیت ریت ویدیو ها(هرچی بالاتر باشه کیفیت ویدیو بالاتر هست)
    /// </summary>
    public enum VideoBitRate
    {
        _96k,
        _128k,
        _160k,
        _192k,
        _256k,
        _300k,
        _400k,
        _500k,
        _600k,
        _800k,
        _1000k,
        _1200k,
        _1500k,
        _1600k,
        _1800k,
        _2000k,
        _2500k,
        _3000k,
        _3500k,
        _4000k,
        _4500k,
        _5000k,
        _5500k,
        _6000k,
        _8000k,
        _10000k,
        _15000k,
        _20000k
    }
    /// <summary>
    /// تابپ فایل های خروجی ویدیوها
    /// </summary>
    public enum VideoFileType
    {
        Mkv,
        Avi,
        Mp4,
        Mov,
        Mpg,
        Mpeg,
        Wmv,
        Asf,
        M4p,
        M4v,
        Webm,
        _3gp,
        _3g2,
        _3gp2,
        _3ga,
        Mp4v,
        M2ts,
        Wm,
        Flv,
        Vob,
        Qt,
        M4b,
        M4r,
        Ts,
        F4v,
        Hevc,
        x265,
        Hdmov,
        Moov,
        Mpe,
        Mpg2,
        Mpeg1,
        Mpeg4,
        Divx,
        Ogv,
        Mxf,
        M2p,
        Mts,
        Rm,
        Rmvb
    }
}
