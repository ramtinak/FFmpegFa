using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FFmpegFa
{
    public class WatermarkText
    {
        public string Text { get; set; }

        /// <summary>
        ///    By Pixel
        /// </summary>
        public int Padding { get; set; } = 15;

        public string FontPath { get; set; }

        public int FontSize { get; set; } = 13;
    }
    public class WatermarkImage
    {
        public string ImagePath { get; set; }

        /// <summary>
        ///    By Pixel
        /// </summary>
        public int Padding { get; set; }
        
        public WatermarkPlace Place { get; set; }
    }




    public enum WatermarkPlace
    {
        TopLeft,
        TopRight,
        Center,
        BottomLeft,
        BottomRight
    }
}
