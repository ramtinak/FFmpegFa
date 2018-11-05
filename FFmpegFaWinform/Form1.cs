/*
 * ساخته شده توسط نصراله جوکار (رامتین)
 * 
 * ایمیل:
 * Ramtinak@live.com
 * 
 */


using System;
using System.Windows.Forms;


// فضای نامی مورد نیاز
using FFmpegFa;

namespace FFmpegFaWinform
{
    public partial class Form1 : Form
    {
        FFmpeg ffmpeg;
        public Form1()
        {
            InitializeComponent();
            // بهتر است کد ساخت کلاس زیر رو در اینجا قرار ندهید
            // زیرا برای اولین بار، فایل اف اف ام پی ای جی را استخراج میکند
            // این عمل 6 ثانیه طول خواهد کشید و تا زمانی که شما پوشه آنرا در پوشه
            // temp
            // کامپیوتر خود پاک نکنید یک بار 6 ثانیه طول خواهد کشید و دفعه های بعدی
            // چون نیازی به استخراج ندارد، پس طولی نخواهد کشید.
            ffmpeg = new FFmpeg();
            ffmpeg.OnProgressChanged += OnFFmpegProgressChanged;
        }
        private void OnFFmpegProgressChanged(FFmpeg sender, FFmpegProgress ffmpegProgress)
        {
            // چون کدهای این کلاس در یک نخ جدا اجرا میشود، شما باید آنرا 
            // Invoke
            // کنید
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnFFmpegProgressChanged(sender, ffmpegProgress)));
                return;
            }
            var info = $"Progress: {ffmpegProgress.Percent}%     Size: {ffmpegProgress.CurrentFileSize}     Speed: {ffmpegProgress.CurrentSpeed}";
            txtProgress.Text = info;
            progressBar.Value = ffmpegProgress.Percent;


        }

        private void OnConvertVideoClick(object sender, EventArgs e)
        {
            // تبدیل ویدیو به ویدیوـه دیگر 
            // برای تبدیل ویدیو به آهنگ از این کد استفاده نکنید!!!!

            ffmpeg.ConvertVideo(txtInputFile.Text, txtOutputFile.Text);
            // کد زیر و کد بالا یکی است
            // بیت رِیت پیشفرض ویدیو 3000ک و بیت رِیت صدا 192ک است
            //ffmpeg.ConvertVideo(txtInputFile.Text, txtOutputFile.Text, ConverterCodecType.x264);

            // نمونه فایل ورودی:
            // C:\abc.mp4
            // نمونه فایل خروجی
            // C:\xyz.mp4 یا C:\xyz2.mkv
        }

        private void OnConvertVideoWithSettingsClick(object sender, EventArgs e)
        {
            // تبدیل ویدیو به ویدیوـه دیگر 
            // برای تبدیل ویدیو به آهنگ از این کد استفاده نکنید!!!!
            // تنظیمات رو این شکلی باید بگذارید
            // هرچه اعداد بیشتر باشه کیفیت نهایی بیشتر خواهد بود

            // تعیین کُدِک، نکته: ایکس 265 فقط برای فایل های ام کی وی بگذارید
            //ffmpeg.ConvertVideo(txtInputFile.Text, txtOutputFile.Text, ConverterCodecType.x264);

            // تنظیم کُدِک، بیت رِیت ویدیو و بیت رِیت صدا
            ffmpeg.ConvertVideo(txtInputFile.Text, txtOutputFile.Text, ConverterCodecType.x264, VideoBitRate._5000k, AudioBitRate._320);

            // نمونه فایل ورودی:
            // C:\abc.mp4
            // نمونه فایل خروجی
            // C:\xyz.mp4 یا C:\xyz2.mkv
        }

        private void OnConvertAudioClick(object sender, EventArgs e)
        {
            // تبدیل آهنگ به آهنگ
            // نبدیل ویدیو به آهنگ
            ffmpeg.ConvertAudio(txtInputFile.Text, txtOutputFile.Text);
            // کد زیر و بالا یکی است
            //ffmpeg.ConvertAudio(txtInputFile.Text, txtOutputFile.Text, AudioFileType.Mp3);
            // صدای خروجی با این کد ضعیف خواهد بود!
            // برای تنظیم کیفیت خروجی صدا از کد رویداد زیر استفاده کنید


            // نمونه فایل ورودی:
            // C:\abc.ogg یا C:\abc.mp4
            // نمونه فایل خروجی
            // C:\xyz.mp3 
        }

        private void OnConvertAudioWithSettingsClick(object sender, EventArgs e)
        {
            // تبدیل آهنگ به آهنگ
            // نبدیل ویدیو به آهنگ
            // تنظیم فرمت خروجی
            // تنظیم سمپل رِیت خروجی صدا
            // تنظیم بیت رِیت خروجی صدا
            ffmpeg.ConvertAudio(txtInputFile.Text, txtOutputFile.Text, AudioFileType.Mp3, AudioSampleRate._48000, AudioBitRate._256);

            // نمونه فایل ورودی:
            // C:\abc.ogg یا C:\abc.mp4
            // نمونه فایل خروجی
            // C:\xyz.mp3 
        }

        private void OnRemoveAudioFromVideoClick(object sender, EventArgs e)
        {
            // با استفاده از این کد می توانید صداها را از فایل ویدیو حذف کنید
            ffmpeg.RemoveAudioFromVideo(txtInputFile.Text, txtOutputFile.Text);

            // نمونه فایل ورودی:
            // C:\abc.mp4
            // نمونه فایل خروجی
            // C:\xyz.mp4 یا C:\xyz2.mkv
        }

        private void OnExtractAudioFromVideoClick(object sender, EventArgs e)
        {
            // استخراج صدا از ویدیو
            ffmpeg.ExtractAudioFromVideo(txtInputFile.Text, txtOutputFile.Text);

            // نمونه فایل ورودی:
            // C:\abc.mp4 یا C:\abc.mkv
            // نمونه فایل خروجی
            // C:\xyz.mp3
        }

        private void OnResizeVideoClick(object sender, EventArgs e)
        {
            // تغییر اندازه عرض و طول ویدیو
            VideoSize newSize = new VideoSize();
            // اندازه عرض ویدیو
            newSize.Width = 1280;
            // اندازه طول ویدیو
            newSize.Height = 720;
            ffmpeg.ResizeVideo(txtInputFile.Text, txtOutputFile.Text, newSize);
            // تنظیم بیت رِیت خروجی ویدیو و تنظیم بیت رِیت خروجی صدا
            //ffmpeg.ResizeVideo(txtInputFile.Text, txtOutputFile.Text, newSize, VideoBitRate._3000k, AudioBitRate._320);

            // نمونه فایل ورودی:
            // C:\abc.mp4 یا C:\abc.mkv
            // نمونه فایل خروجی
            // C:\xyz.mp4 یا C:\xyz.mkv
        }

        private void OnCropVideoOrAudioClick(object sender, EventArgs e)
        {
            // برش قسمتی از آهنگ یا ویدیو
            // زمان شروع
            TimeSpan startTime = TimeSpan.FromSeconds(40);
            // زمان پایان
            // نکته: این زمان بر حسب زمان شروع می باشد
            // یعنی الان از زمان ثانیه چهلم شروع میشود و تا 20 ثانیه بعد(ثانیه شصتم) رو برش میدهد
            TimeSpan duration = TimeSpan.FromSeconds(20);

            ffmpeg.CropVideoOrAudio(txtInputFile.Text, txtOutputFile.Text, startTime, duration);

            // نمونه فایل ورودی:
            // C:\abc.mp4 یا C:\abc.mp3
            // نمونه فایل خروجی
            // C:\xyz.mp4 یا C:\xyz.mp3
        }

        private void OnBurnSubtitleIntoVideoClick(object sender, EventArgs e)
        {
            // نوشتن یک زیرنویس روی فایل ویدیویی
            // این زیرنویس به عنوان زیرنویس سخت میباشد
            // این نوع زیرنویس روی فریم های ویدیو چاپ میشود و بعدها قابل تغییر نخواهد بود
            ffmpeg.BurnSubtitleIntoVideo(txtInputFile.Text, txtOutputFile.Text, txtSubtitleFile1.Text);

            // نمونه فایل ورودی:
            // C:\abc.mp4 یا C:\abc.mkv
            // نمونه فایل خروجی
            // C:\xyz.mp4 یا C:\xyz.mkv
            // نمونه فایل ورودی زیرنویس
            // C:\zir.srt
        }

        private void OnMergeSubtitlesToVideoClick(object sender, EventArgs e)
        {
            // افزودن یک یا چند زیرنویس به یک فیلم
            // این زیرنویس به صورت زیرنویس نرم خواهد بود
            // این زیرنویس قابل تغییر و حذف خواهد بود
            // با استفاده از این کد میتوانید فیلم هایی با چندین زیرنویس ایجاد کنید
            ffmpeg.MergeSubtitlesToVideo(txtInputFile.Text, txtOutputFile.Text, txtSubtitleFile1.Text);
            // چنانچه میخواهید بیش از یک زیرنویس به فایل اضافه کنید:
            // ffmpeg.MergeSubtitlesToVideo(txtInputFile.Text, txtOutputFile.Text, txtSubtitleFile1.Text, txtSubtitleFile2.Text);
            // نکته: به هر تعدادی که بخواهید می توانید زیرنویس اضافه کنید

            // نمونه فایل ورودی:
            // C:\abc.mp4 یا C:\abc.mkv
            // نمونه فایل خروجی
            // C:\xyz.mp4 یا C:\xyz.mkv
            // نمونه فایل ورودی زیرنویس
            // C:\zir.srt
        }

        private void OnMergeAudioToVideoClick(object sender, EventArgs e)
        {
            // افزودن یک یا چند صدا به یک فیلم
            // با استفاده از این کد می توانید فیلم های چند زبانه ایجاد کنید

            // فایل ورودی
            // فایل خروجی
            // حذف صدای داخلی فایل ویدیویی؟ خیر
            // فایل های صدا
            ffmpeg.MergeAudioToVideo(txtInputFile.Text, txtOutputFile.Text, false, txtAudioFile1.Text, txtAudioFile2.Text);
            // چنانچه میخواهید بیش از یک فایل صوتی به فایل اضافه کنید:
            //ffmpeg.MergeAudioToVideo(txtInputFile.Text, txtOutputFile.Text,false, txtAudioFile1.Text, txtAudioFile2.Text);
            // نکته: به هر تعدادی که بخواهید می توانید زیرنویس اضافه کنید

            // کیفیت صدا در کد بالا ضعیف است، برای تنظیم کیفیت صدای خروجی
            //ffmpeg.MergeAudioToVideo(txtInputFile.Text, txtOutputFile.Text, false, AudioBitRate._320, txtAudioFile1.Text);
            // اضافه کردن چندین فایل صوتی + ننظیم کیفیت برای همه صداها
            //ffmpeg.MergeAudioToVideo(txtInputFile.Text, txtOutputFile.Text, false, AudioBitRate._320, txtAudioFile1.Text, txtAudioFile2.Text);


            // نمونه فایل ورودی:
            // C:\abc.mp4 یا C:\abc.mkv
            // نمونه فایل خروجی
            // C:\xyz.mp4 یا C:\xyz.mkv
            // نمونه فایل ورودی صدا
            // C:\zir.mp3 
        }
    }
}
