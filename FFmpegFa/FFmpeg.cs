using FFmpegFa.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
//ffmpeg 
//-i input 
//-b 1200k 
//-minrate 1200k 
//-maxrate 1200k 
//-bufsize 1200k 
//-ab 64k 
//-vcodec libx264 
//-acodec aac -strict -2 
//-ac 2 
//-ar 44100 
//-s 320x240 
//-y output.mp4
/// <summary>
/// FFmpeg namespace
/// </summary>
namespace FFmpegFa
{
    /// <summary>
    /// کلاس نمایش پروسه
    /// </summary>
    public class FFmpegProgress
    {
        //frame=   18 fps=0.0 q=0.0 size=       7kB time=00:00:00.88 bitrate=  68.8kbits/s speed=1.69x    
        /// <summary>
        /// میزان پروسه طی شده
        /// </summary>
        public int Percent { get; set; } = 0;
        /// <summary>
        /// زمان فیلم اِنکُد شده
        /// </summary>
        public TimeSpan CurrentTime { get; set; } = TimeSpan.FromMilliseconds(0);
        /// <summary>
        /// فریم فعلی
        /// </summary>
        public int CurrentFrame { get; set; } = 0;
        /// <summary>
        /// فریم رِیت فعلی
        /// </summary>
        public double CurrentFrameRate { get; set; } = 0;
        /// <summary>
        /// سایز فعلی فایل نهایی
        /// </summary>
        public string CurrentFileSize { get; set; } = string.Empty;
        /// <summary>
        /// بیت رِیت فعلی
        /// </summary>
        public string CurrentBitRate { get; set; } = string.Empty;
        /// <summary>
        /// سرعت اِنکُدینگ کردن
        /// </summary>
        public string CurrentSpeed { get; set; } = string.Empty;
        /// <summary>
        /// اطلاعات فایل ورودی
        /// </summary>
        public FFmpegInfo InputFileInfo { get; set; }
    }
    /// <summary>
    /// دِلیگِت وُید پروسه
    /// </summary>
    /// <param name="sender">سِندِر</param>
    /// <param name="ffmpegProgress">پروسه فایل</param>
    public delegate void FFmpegProgressChanged(FFmpeg sender, FFmpegProgress ffmpegProgress);
    /// <summary>
    /// کلاس اصلی، تمامی کارها در این کلاس انجام میشود
    /// </summary>
    public class FFmpeg: Interfaces.IFFmpeg, IDisposable
    {
        string StartupPath;
        const string FFmpegPath = @"\ffmpeg.exe";
        /// <summary>
        /// رویداد تغییرات
        /// </summary>
        public event FFmpegProgressChanged OnProgressChanged;
      
        /// <summary>
        /// نسخه کتابخانه را به شما باز میگرداند
        /// </summary>
        /// <returns>نسخه کتابخانه</returns>
        public string LibraryVersion() { return "1.0.0.0"; }
        /// <summary>
        /// درباره سازنده این کتابخانه
        /// </summary>
        /// <returns>اطلاعات سازنده</returns>
        public string AboutAuthor()
        {
            return @"Nasrollah Jokar [Ramtin] Ramtinak@live.com [2018 - Shahrivar 1397]";
        }

        /// <summary>
        /// ساخت کُنستراکتور
        /// </summary>
        public FFmpeg()
        {
            //StartupPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            StartupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ffmpegfa");
            if (!Directory.Exists(StartupPath))
                Directory.CreateDirectory(StartupPath);
            ExtractFFmpeg();
        }

        /// <summary>
        /// دیستِراکتور
        /// </summary>
        ~FFmpeg()
        {
            Dispose();
        }

        /// <summary>
        /// اکسترکت کردن اف اف ام پی ای جی
        /// </summary>
        void ExtractFFmpeg()
        {
            try
            {
                if (!File.Exists(StartupPath + FFmpegPath))
                {
                    //new Thread(new ThreadStart(() =>
                    //{
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    if (!Directory.Exists(StartupPath))
                        Directory.CreateDirectory(StartupPath);
                    var filePath = StartupPath + @"\ffmpeg.zip";
                    File.WriteAllBytes(filePath, Properties.Resources.ffmpeg);
                    //Thread.Sleep(4000);
                    ArchiveManager.UnArchive(filePath, StartupPath);
                    //Thread.Sleep(1200);
                    //File.Delete(tempFilePath);
                    filePath = null;
                    stopwatch.Stop();
                    Debug.WriteLine(stopwatch.Elapsed.ToString());
                    //})).Start();

                }
            }
            catch (Exception ex) { Debug.WriteLine("ExtractFFmpeg ex: " + ex.Message); }
        }
        
        /// <summary>
        /// گرفتن اطلاعات یک فایل ویدیویی
        /// </summary>
        /// <param name="filePath">مسیر فایل</param>
        /// <returns>مقدار بازگشتی</returns>
        public FFmpegInfo GetInformation(string filePath)
        {
            var name = "";
            if (filePath.Contains("\\"))
                name = Path.GetFileName(filePath);
            else name = filePath;
            
            var content = RunCommandAndGetResponse($"-i \"{filePath}\"");
            FFmpegInfo fFmpegInfo = new FFmpegInfo(content, name);
            //OnInformation?.Invoke(this, fFmpegInfo);

            return fFmpegInfo;
        }

        //ffprobe -v quiet -print_format json -show_format -show_streams somefile.asf
        /*void RunCommandFFProbe(string command)
        {
            //var ffmpegPath = Application.StartupPath + @"\ffmpeg.exe";
            //var args = $"-show_streams -i \"{textBox1.Text}\"";
            //Show(ffmpegPath);
            //Show(args);
            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = StartupPath + @"\ffprobe.exe";
            process.StartInfo.Arguments = command;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            new Thread(new ThreadStart(() => {
                StreamReader sr = process.StandardError;
                GetResult("Start act");
                GetResult("Start Act\r\n" +sr.ReadToEnd() +"\r\nEnd Act");
                //while (!sr.EndOfStream)
                //{
                //    GetResult(sr.ReadLine());
                //}
            })).Start();
        }
        void RunCommand(string command)
        {
            //var ffmpegPath = Application.StartupPath + @"\ffmpeg.exe";
            //var args = $"-show_streams -i \"{textBox1.Text}\"";
            //Show(ffmpegPath);
            //Show(args);
            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = StartupPath + FFmpegPath;
            process.StartInfo.Arguments = command;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            new Thread(new ThreadStart(() => {
                StreamReader sr = process.StandardError;
                GetResult("Start act\r\n"+sr.ReadToEnd() + "\r\nEnd Act");
                //while (!sr.EndOfStream)
                //{
                //    GetResult(sr.ReadLine());
                //}
                //GetResult("End act");
            })).Start();
        }
        */

        /// <summary>
        /// اجرای دستور و باز گردانی
        /// </summary>
        /// <param name="command">دستور برای ارسال</param>
        /// <returns>بازگردانی اطلاعات</returns>
        string RunCommandAndGetResponse(string command)
        {
            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = StartupPath + FFmpegPath;
            process.StartInfo.Arguments = command;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            StreamReader sr = process.StandardError;
            var content = sr.ReadToEnd();

            return content;
        }
        
        /// <summary>
        /// مبدل آهنگ(پیشفرض به ام پی تری تبدیل میکند)
        /// </summary>
        /// <param name="inputFile">فایل ورودی(هم میتواند ویدیو باشد هم آهنگ)</param>
        /// <param name="outputFile">آدرس و نام فایل خروجی</param>
        public void ConvertAudio(string inputFile, string outputFile)
        {
            ConvertAudio(inputFile, outputFile, AudioFileType.Mp3);
        }

        /// <summary>
        /// مبدل آهنگ
        /// </summary>
        /// <param name="inputFile">فایل ورودی(هم میتواند ویدیو باشد هم آهنگ)</param>
        /// <param name="outputFile">آدرس و نام فایل خروجی</param>
        /// <param name="outputAudioType">نوع خروجی آهنگ</param>
        public void ConvertAudio(string inputFile, string outputFile,AudioFileType outputAudioType)
        {
            ConvertAudio(inputFile, outputFile, AudioFileType.Mp3, AudioSampleRate._44100, AudioBitRate._128);
        }

        /// <summary>
        /// مبدل آهنگ و تنظیمات کیفیت
        /// </summary>
        /// <param name="inputFile">فایل ورودی(هم میتواند ویدیو باشد هم آهنگ)</param>
        /// <param name="outputFile">آدرس و نام فایل خروجی</param>
        /// <param name="outputAudioType">نوع خروجی آهنگ</param>
        /// <param name="outputAudioSampleRate">سمپل رِیت آهنگ</param>
        /// <param name="outputAudioBitRate">بیت رِیت آهنگ</param>
        public void ConvertAudio(string inputFile, string outputFile, AudioFileType outputAudioType,
            AudioSampleRate outputAudioSampleRate, AudioBitRate outputAudioBitRate)
        {
            if (string.IsNullOrEmpty(inputFile))
                throw new Exception("Inputfile is empty. Choose an input file.");
            if (string.IsNullOrEmpty(outputFile))
                throw new Exception("Outputfile is empty. Choose an output file.");

            //////////////////////////////////////////////////////////////////////////////////////////
            //ffmpeg -i input.wav -vn -ar 44100 -ac 2 -ab 192k -f mp3 output.mp3
            //Explanation of the used arguments in this example:
            //-i - input file
            //-vn - Disable video, to make sure no video is included if the source would be a video file
            //-ar - Set the audio sampling frequency. For output streams it is 
            //set by default to the frequency of the corresponding input stream. 
            //For input streams this option only makes sense for audio grabbing devices 
            //and raw demuxers and is mapped to the corresponding demuxer options.

            //-ac - Set the number of audio channels. For output streams it is set by default to the number 
            //of input audio channels. For input streams this option only makes sense for audio grabbing 
            //devices and raw demuxers and is mapped to the corresponding demuxer options. So used here 
            //to make sure it is stereo (2 channels)

            //-ab - actually seems to be changed, so should be replaced for newer ffmpeg version to -b:a 192k Converts the audio bitrate to be exact 192kbit per second

            //-f - Force input or output file format. The format is normally auto detected for input files and guessed from the file extension for output files, so this option is not needed in most cases
            //////////////////////////////////////////////////////////////////////////////////////////


            //ffmpeg -i input.mp4 -b:v 1M -b:a 192k output.avi
            //audio.ogg -f mp3 newfile.mp3

            //var cmd = $"-i \"{ inputFile}\" -vn -f {audioFileType.ToString().ToLower()} \"{outputFile}\"";
            //ffmpeg -i input.wav -vn -ar 44100 -ac 2 -ab 192k -f mp3 output.mp3

            var bitRate = "128k";
            try
            {
                bitRate = outputAudioBitRate.ToString().Replace("_", "") + "k";
            }
            catch { }
            var sampleRate = "44100";
            try
            {
                sampleRate = outputAudioSampleRate.ToString().Replace("_", "") ;
            }
            catch { }
            //ffmpeg -i input.mp3 -c:a libfdk_aac -b:a 128k output.m4a
            var cmd = string.Empty;
            //if (outputAudioType == AudioFileType.M4a)
            //    //ffmpeg -i input.mp3 -c:a libfdk_aac output.m4a
            //    //cmd = $"-i \"{inputFile}\" -vn -c:a libfdk_aac -b:a {bitRate} \"{outputFile}\"";
            //    //cmd = $"-i \"{inputFile}\" -vn -ar {sampleRate} -ac 2 -ab {bitRate} -f aac \"{outputFile}\"";
            //    //ffmpeg -i input.mp3 -c:a aac -b:a 192k output.m4a
            //    //-i input.mp3 -c:a aac -b:a 128k output.m4a
            //    cmd = $"-i \"{inputFile}\" -c:a aac -b:a 128k \"{outputFile}\"";
            //else
                cmd = $"-i \"{inputFile}\" -vn -ar {sampleRate} -ac 2 -ab {bitRate} -f {outputAudioType.ToString().ToLower()} \"{outputFile}\"";

            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = StartupPath + FFmpegPath;
            process.StartInfo.Arguments = cmd;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            //Thread.Sleep(3000);
            //Debug.WriteLine(process.ProcessName);
            //return;

            new Thread(new ThreadStart(() => {

                StreamReader sr = process.StandardError;
                Debug.WriteLine("Start.....");
                //GetResult("Start act\r\n" + sr.ReadToEnd() + "\r\nEnd Act");

                var name = "";
                if (inputFile.Contains("\\"))
                    name = Path.GetFileName(inputFile);
                else name = inputFile;


                progress = new FFmpegProgress();
                bool d = false;
                TimeSpan totalTimeSpan = TimeSpan.FromMilliseconds(0);
                Debug.WriteLine("Total:" + totalTimeSpan.TotalMilliseconds);
                //frame=   18 fps=0.0 q=0.0 size=       7kB time=00:00:00.88 bitrate=  68.8kbits/s speed=1.69x   
                while (!sr.EndOfStream)
                {
                    var v = (sr.ReadLine());
                    if (v.ToLower().Contains("duration") && !d)
                    {
                        try
                        {
                            FFmpegInfo fFmpegInfo = new FFmpegInfo(v, name);
                            totalTimeSpan = fFmpegInfo.Duration;
                            progress.InputFileInfo = fFmpegInfo;


                            //Debug.WriteLine(fFmpegInfo.Duration.ToString());
                            d = true;
                        }
                        catch { }
                    }
                    if (v.Contains("time=") && totalTimeSpan.TotalMilliseconds != 0)
                    {
                        try
                        {
                            SendProgress(totalTimeSpan, v);
                        }
                        catch { }
                    }
                }
                try
                {
                    SendProgress(totalTimeSpan, ".:::END:::.");
                    // 100%
                }
                catch { }
                Debug.WriteLine("End.....");

                //GetResult("End act");
            })).Start();

        }


        /// <summary>
        /// مبدل ویدیو
        /// </summary>
        /// <param name="inputFile">فایل ورودی ویدیو</param>
        /// <param name="outputFile">فایل خروجی ویدیو</param>
        public void ConvertVideo(string inputFile, string outputFile)
        {
            ConvertVideo(inputFile, outputFile, ConverterCodecType.x264);
        }

        /// <summary>
        /// مبدل ویدیو(پیشفرض: ویدیو بیت 3000 و ایدیو بیت 192)
        /// </summary>
        /// <param name="inputFile">فایل ورودی ویدیو</param>
        /// <param name="outputFile">فایل خروجی ویدیو</param>
        /// <param name="outputCodecType">کدک برای مبدل(فقط برای فایل های ام کی وی)</param>
        public void ConvertVideo(string inputFile, string outputFile,ConverterCodecType outputCodecType)
        {

            ConvertVideo(inputFile, outputFile, outputCodecType, VideoBitRate._3000k, AudioBitRate._192);

        }

        /// <summary>
        /// مبدل ویدیو
        /// </summary>
        /// <param name="inputFile">فایل ورودی ویدیو</param>
        /// <param name="outputFile">فایل خروجی ویدیو</param>
        /// <param name="outputCodecType">کدک برای مبدل(فقط برای فایل های ام کی وی)</param>
        /// <param name="outputVideoBitRate">بیت رِیت ویدیو خروجی</param>
        /// <param name="outputAudioBitRate">بیت رِیت آهنگ ویدیو خروجی</param>
        public void ConvertVideo(string inputFile, string outputFile, ConverterCodecType outputCodecType, VideoBitRate outputVideoBitRate, AudioBitRate outputAudioBitRate)
        {
            if (string.IsNullOrEmpty(inputFile))
                throw new Exception("Inputfile is empty. Choose an input file.");
            if (string.IsNullOrEmpty(outputFile))
                throw new Exception("Outputfile is empty. Choose an output file.");

            var lib = "libx264";
            if (Path.GetExtension(outputFile).ToLower() == ".mkv")
            {
                switch (outputCodecType)
                {

                    case ConverterCodecType.x265:
                        lib = "libx265";
                        break;
                    default:
                    case ConverterCodecType.x264:
                        lib = "libx264";
                        break;
                }
            }
            var audioBitRate = "192k";
            try
            {
                audioBitRate = outputAudioBitRate.ToString().Replace("_", "") + "k";
            }
            catch { }
            var videoBitRate = "3000k";
            try
            {
                videoBitRate = outputAudioBitRate.ToString().Replace("_", "");
            }
            catch { }
            //ffmpeg -i input.mp4 -b:v 1M -b:a 192k output.avi

            //var cmd = (string.Format(FFmpegCommands.ConvertUltraFast, inputFile, outputFile, lib + " -b:v 4M -b:a 192k "));
            var cmd = (string.Format(FFmpegCommands.ConvertUltraFast, inputFile, outputFile, lib + $" -b:v {videoBitRate} -b:a {audioBitRate} "));

            //var cmd = $"-i \"{ inputFile}\" -b:v 4M -b:a 192k \"{outputFile}\"";

            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = StartupPath + FFmpegPath;
            process.StartInfo.Arguments = cmd;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            //Thread.Sleep(3000);
            //Debug.WriteLine(process.ProcessName);
            //return;

            new Thread(new ThreadStart(() => {

                StreamReader sr = process.StandardError;
                Debug.WriteLine("Start.....");
                //GetResult("Start act\r\n" + sr.ReadToEnd() + "\r\nEnd Act");

                var name = "";
                if (inputFile.Contains("\\"))
                    name = Path.GetFileName(inputFile);
                else name = inputFile;


                progress = new FFmpegProgress();
                bool d = false;
                TimeSpan totalTimeSpan = TimeSpan.FromMilliseconds(0);
                Debug.WriteLine("Total:" + totalTimeSpan.TotalMilliseconds);
                //        //frame=   18 fps=0.0 q=0.0 size=       7kB time=00:00:00.88 bitrate=  68.8kbits/s speed=1.69x   
                while (!sr.EndOfStream)
                {
                    var v = (sr.ReadLine());
                    if (v.ToLower().Contains("duration") && !d)
                    {
                        try
                        {
                            FFmpegInfo fFmpegInfo = new FFmpegInfo(v, name);
                            totalTimeSpan = fFmpegInfo.Duration;
                            progress.InputFileInfo = fFmpegInfo;

                            Debug.WriteLine(fFmpegInfo.Duration.ToString());
                            d = true;
                        }
                        catch { }
                    }
                    if (v.Contains("time=") && totalTimeSpan.TotalMilliseconds != 0)
                    {
                        try
                        {
                            SendProgress(totalTimeSpan, v);
                        }
                        catch { }
                    }
                }
                try
                {
                    SendProgress(totalTimeSpan, ".:::END:::.");
                    // 100%
                }
                catch { }
                Debug.WriteLine("End.....");

                //GetResult("End act");
            })).Start();



        }

        /// <summary>
        /// حذف صدا از ویدیو
        /// </summary>
        /// <param name="inputFile">فایل ورودی ویدیو</param>
        /// <param name="outputFile">فایل خروجی ویدیو</param>
        public void RemoveAudioFromVideo(string inputFile, string outputFile)
        {
            if (string.IsNullOrEmpty(inputFile))
                throw new Exception("Inputfile is empty. Choose an input file.");
            if (string.IsNullOrEmpty(outputFile))
                throw new Exception("Outputfile is empty. Choose an output file.");
        
            //var bitRate = "128k";

            //var sampleRate = "44100";
    
            var cmd = string.Empty;
            // -c:v copy -c:a copy -map 0:v -map 0:a
            cmd = $"-i \"{inputFile}\" -an -c:v copy -map 0:v \"{outputFile}\"";

            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = StartupPath + FFmpegPath;
            process.StartInfo.Arguments = cmd;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            //Thread.Sleep(3000);
            //Debug.WriteLine(process.ProcessName);
            //return;

            new Thread(new ThreadStart(() => {

                StreamReader sr = process.StandardError;
                Debug.WriteLine("Start.....");
                //GetResult("Start act\r\n" + sr.ReadToEnd() + "\r\nEnd Act");

                var name = "";
                if (inputFile.Contains("\\"))
                    name = Path.GetFileName(inputFile);
                else name = inputFile;


                progress = new FFmpegProgress();
                bool d = false;
                TimeSpan totalTimeSpan = TimeSpan.FromMilliseconds(0);
                Debug.WriteLine("Total:" + totalTimeSpan.TotalMilliseconds);
                //frame=   18 fps=0.0 q=0.0 size=       7kB time=00:00:00.88 bitrate=  68.8kbits/s speed=1.69x   
                while (!sr.EndOfStream)
                {
                    var v = (sr.ReadLine());
                    Debug.WriteLine(v);
                    if (v.ToLower().Contains("duration") && !d)
                    {
                        try
                        {
                            FFmpegInfo fFmpegInfo = new FFmpegInfo(v, name);
                            totalTimeSpan = fFmpegInfo.Duration;
                            progress.InputFileInfo = fFmpegInfo;

                            //Debug.WriteLine(fFmpegInfo.Duration.ToString());
                            d = true;
                        }
                        catch { }
                    }
                    if (v.Contains("time=") && totalTimeSpan.TotalMilliseconds != 0)
                    {
                        try
                        {
                            SendProgress(totalTimeSpan, v);
                        }
                        catch { }
                    }
                }
                try
                {
                    SendProgress(totalTimeSpan, ".:::END:::.");
                    // 100%
                }
                catch { }
                Debug.WriteLine("End.....");

                //GetResult("End act");
            })).Start();

        }

        /// <summary>
        /// استخراج صدا از ویدیو
        /// </summary>
        /// <param name="inputFile">فایل ورودی ویدیو</param>
        /// <param name="outputFile">فایل خروجی آهنگ</param>
        public void ExtractAudioFromVideo(string inputFile, string outputFile)
        {
            ExtractAudioFromVideo(inputFile, outputFile, AudioBitRate._256);
        }

        /// <summary>
        /// استخراج صدا از ویدیو به همراه تنظیم بیت رِیت صدا
        /// </summary>
        /// <param name="inputFile">فایل ورودی ویدیو</param>
        /// <param name="outputFile">فایل خروجی آهنگ</param>
        /// <param name="outputAudioBitRate">بیت رِیت خروجی صدا</param>
        void ExtractAudioFromVideo(string inputFile, string outputFile, AudioBitRate outputAudioBitRate)
        {
            if (string.IsNullOrEmpty(inputFile))
                throw new Exception("Inputfile is empty. Choose an input file.");
            if (string.IsNullOrEmpty(outputFile))
                throw new Exception("Outputfile is empty. Choose an output file.");

            var bitRate = "192";
            try
            {

                bitRate = outputAudioBitRate.ToString().Replace("_", "");
            } catch { }
            //var sampleRate = "44100";

            var cmd = string.Empty;
            //-i video.mp4 -vn -ab 256 audio.mp3
            cmd = $"-i \"{inputFile}\" -vn -ab {bitRate} \"{outputFile}\"";
            cmd = $"-i \"{inputFile}\" -vn -b:a {bitRate} \"{outputFile}\"";

            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = StartupPath + FFmpegPath;
            process.StartInfo.Arguments = cmd;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            //Thread.Sleep(3000);
            //Debug.WriteLine(process.ProcessName);
            //return;

            new Thread(new ThreadStart(() => {

                StreamReader sr = process.StandardError;
                Debug.WriteLine("Start.....");
                //GetResult("Start act\r\n" + sr.ReadToEnd() + "\r\nEnd Act");

                var name = "";
                if (inputFile.Contains("\\"))
                    name = Path.GetFileName(inputFile);
                else name = inputFile;


                progress = new FFmpegProgress();
                bool d = false;
                TimeSpan totalTimeSpan = TimeSpan.FromMilliseconds(0);
                Debug.WriteLine("Total:" + totalTimeSpan.TotalMilliseconds);
                //frame=   18 fps=0.0 q=0.0 size=       7kB time=00:00:00.88 bitrate=  68.8kbits/s speed=1.69x   
                while (!sr.EndOfStream)
                {
                    var v = (sr.ReadLine());
                    if (v.ToLower().Contains("duration") && !d)
                    {
                        try
                        {
                            FFmpegInfo fFmpegInfo = new FFmpegInfo(v, name);
                            totalTimeSpan = fFmpegInfo.Duration;
                            progress.InputFileInfo = fFmpegInfo;

                            //Debug.WriteLine(fFmpegInfo.Duration.ToString());
                            d = true;
                        }
                        catch { }
                    }
                    if (v.Contains("time=") && totalTimeSpan.TotalMilliseconds != 0)
                    {
                        try
                        {
                            SendProgress(totalTimeSpan, v);
                        }
                        catch { }
                    }
                }
                try
                {
                    SendProgress(totalTimeSpan, ".:::END:::.");
                    // 100%
                }
                catch { }
                Debug.WriteLine("End.....");

                //GetResult("End act");
            })).Start();

        }
                
        /// <summary>
        /// تغییر سایز ویدیو
        /// </summary>
        /// <param name="inputFile">فایل ورودی ویدیو</param>
        /// <param name="outputFile">فایل خروجی ویدیو</param>
        /// <param name="newSize">سایز جدید</param>
        public void ResizeVideo(string inputFile, string outputFile, VideoSize newSize)
        {
            if (string.IsNullOrEmpty(inputFile))
                throw new Exception("Inputfile is empty. Choose an input file.");
            if (string.IsNullOrEmpty(outputFile))
                throw new Exception("Outputfile is empty. Choose an output file.");

           
      
            var cmd = string.Empty;
            //-i input.mp4 -s 480x320 -c:a copy output.mp4
            var wh = string.Format("{0}x{1}", newSize.Width, newSize.Height);

            cmd = $"-i \"{inputFile}\" -s {wh} -c:a copy \"{outputFile}\"";

            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = StartupPath + FFmpegPath;
            process.StartInfo.Arguments = cmd;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            new Thread(new ThreadStart(() => {

                StreamReader sr = process.StandardError;
                Debug.WriteLine("Start.....");
                var name = "";
                if (inputFile.Contains("\\"))
                    name = Path.GetFileName(inputFile);
                else name = inputFile;


                progress = new FFmpegProgress();
                bool d = false;
                TimeSpan totalTimeSpan = TimeSpan.FromMilliseconds(0);
                Debug.WriteLine("Total:" + totalTimeSpan.TotalMilliseconds);
                //frame=   18 fps=0.0 q=0.0 size=       7kB time=00:00:00.88 bitrate=  68.8kbits/s speed=1.69x   
                while (!sr.EndOfStream)
                {
                    var v = (sr.ReadLine());
                    Debug.WriteLine(v);
                    if (v.ToLower().Contains("duration") && !d)
                    {
                        try
                        {
                            FFmpegInfo fFmpegInfo = new FFmpegInfo(v, name);
                            totalTimeSpan = fFmpegInfo.Duration;
                            progress.InputFileInfo = fFmpegInfo;

                            //Debug.WriteLine(fFmpegInfo.Duration.ToString());
                            d = true;
                        }
                        catch { }
                    }
                    if (v.Contains("time=") && totalTimeSpan.TotalMilliseconds != 0)
                    {
                        try
                        {
                            SendProgress(totalTimeSpan, v);
                        }
                        catch { }
                    }
                }
                try
                {
                    SendProgress(totalTimeSpan, ".:::END:::.");
                    // 100%
                }
                catch { }
                Debug.WriteLine("End.....");

                //GetResult("End act");
            })).Start();
        }

        /// <summary>
        /// تغییر سایز ویدیو به همراه تنظیمات کیفیت
        /// </summary>
        /// <param name="inputFile">فایل ورودی ویدیو</param>
        /// <param name="outputFile">فایل خروجی ویدیو</param>
        /// <param name="newSize">سایز جدید</param>
        /// <param name="outputVideoBitRate">بیت رِیت خروجی ویدیو</param>
        /// <param name="outputAudioBitRate">بیت رِیت خروجی صدا</param>
        public void ResizeVideo(string inputFile, string outputFile, VideoSize newSize, VideoBitRate outputVideoBitRate, AudioBitRate outputAudioBitRate)
        {
            if (string.IsNullOrEmpty(inputFile))
                throw new Exception("Inputfile is empty. Choose an input file.");
            if (string.IsNullOrEmpty(outputFile))
                throw new Exception("Outputfile is empty. Choose an output file.");


            var audioBitRate = "192k";
            try
            {
                audioBitRate = outputAudioBitRate.ToString().Replace("_", "") + "k";
            }
            catch { }
            var videoBitRate = "3000k";
            try
            {
                videoBitRate = outputAudioBitRate.ToString().Replace("_", "");
            }
            catch { }
            var cmd = string.Empty;
            //-i input.mp4 -s 480x320 -c:a copy output.mp4
            var wh = string.Format("{0}x{1}", newSize.Width, newSize.Height);

            cmd = $"-i \"{inputFile}\" -s {wh} -b:v {videoBitRate} -b:a {audioBitRate} \"{outputFile}\"";
            //var cmd = $"-i \"{ inputFile}\" -b:v 4M -b:a 192k \"{outputFile}\"";
            ////cmd = $"-i \"{inputFile}\" -s {wh} -c:a copy \"{outputFile}\"";

            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = StartupPath + FFmpegPath;
            process.StartInfo.Arguments = cmd;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            new Thread(new ThreadStart(() => {

                StreamReader sr = process.StandardError;
                Debug.WriteLine("Start.....");
                var name = "";
                if (inputFile.Contains("\\"))
                    name = Path.GetFileName(inputFile);
                else name = inputFile;


                progress = new FFmpegProgress();
                bool d = false;
                TimeSpan totalTimeSpan = TimeSpan.FromMilliseconds(0);
                Debug.WriteLine("Total:" + totalTimeSpan.TotalMilliseconds);
                //frame=   18 fps=0.0 q=0.0 size=       7kB time=00:00:00.88 bitrate=  68.8kbits/s speed=1.69x   
                while (!sr.EndOfStream)
                {
                    var v = (sr.ReadLine());
                    Debug.WriteLine(v);
                    if (v.ToLower().Contains("duration") && !d)
                    {
                        try
                        {
                            FFmpegInfo fFmpegInfo = new FFmpegInfo(v, name);
                            totalTimeSpan = fFmpegInfo.Duration;
                            progress.InputFileInfo = fFmpegInfo;

                            //Debug.WriteLine(fFmpegInfo.Duration.ToString());
                            d = true;
                        }
                        catch { }
                    }
                    if (v.Contains("time=") && totalTimeSpan.TotalMilliseconds != 0)
                    {
                        try
                        {
                            SendProgress(totalTimeSpan, v);
                        }
                        catch { }
                    }
                }
                try
                {
                    SendProgress(totalTimeSpan, ".:::END:::.");
                    // 100%
                }
                catch { }
                Debug.WriteLine("End.....");

                //GetResult("End act");
            })).Start();
        }

        /// <summary>
        /// اضافه کردن زیر نویس درون فایل ویدیویی
        /// </summary>
        /// <param name="inputFile">فایل ورودی ویدیو</param>
        /// <param name="outputFile">فایل خروجی ویدیو</param>
        /// <param name="subtitlesFilePaths">فایل های زیرنویس</param>
        public void MergeSubtitlesToVideo(string inputFile, string outputFile, /*string subtitleFile*/params string[] subtitlesFilePaths)
        {
            if (string.IsNullOrEmpty(inputFile))
                throw new Exception("Inputfile is empty. Choose an input file.");
            if (string.IsNullOrEmpty(outputFile))
                throw new Exception("Outputfile is empty. Choose an output file.");


            if (Path.GetExtension(outputFile).Replace(".", "").ToLower() != "mkv")
                throw new Exception("Output file most be an MKV file.");

            var cmd = string.Empty;
            //ffmpeg -newsubtitle subtitles.srv -i video.avi ...
            //ffmpeg -i video.avi -vf subtitles=subtitle.srt out.avi
            //ffmpeg - i video.avi - vf subtitles = subtitle.srt out.avi

            //ffmpeg -i video.mp4 -i subtitle1.srt -i subtitle2.srt -map 0 -map 1 -map 2 \
            //-c copy -metadata:s:s:0 language=eng -metadata:s:s:1 language=ipk output.mkv

            //ffmpeg -i Clean.mp4 -i spanish.ass -i english.ass -c:s mov_text -c:v copy -c:a copy -map 0:v -map 0:a -map 1 -map 2 -metadata:s:s:0 language=spa -metadata:s:s:1 language=eng With2CC.mp4

            var subtitle = "";
            var map = "";
            var ix = 1;
            var cs = "";
            var meta = "";
            foreach (var item in subtitlesFilePaths)
            {
                subtitle += $"-i \"{item}\" ";
                map += $"-map {ix}:0 ";
                //-c:s srt
                var extension = Path.GetExtension(item).Replace(".", "").ToLower();
                cs += $"-c:s {extension} ";
                //-metadata:s:a:0 language=eng -metadata:s:a:0 title="Title 1" \

                meta += $"-metadata:s:s:{ix-1} language=unknown -metadata:s:s:{ix - 1} title=\"Subtitle Track #{ix}\" "; 
                ix++;
            }
            //ffmpeg -f concat -i files.lst -c copy -scodec copy output.mp4
            //cmd = $"-i \"{inputFile}\" \"{subtitle}\" -c:s mov_text -c:v copy -c:a copy -map 0:v -map 0:a -map 1 -map 2 -metadata:s:s:0 language=spa -metadata:s:s:1 language=eng";

            //cmd = $"-f concat -i \"{inputFile}\" \"{subtitle}\" -c copy -scodec copy \"{outputFile}\"";
            //ffmpeg -i infile.mp4 -i infile.srt -c copy -c:s mov_text outfile.mp4
            //cmd = $"-i \"{inputFile}\" \"{subtitle}\" -c copy -c:s mov_text -c:v copy -c:a copy \"{outputFile}\"";
            //ffmpeg -i input.mp4 -f srt -i input.srt -i input2.srt\ -map 0:0 -map 0:1 -map 1:0 -map 2:0 -c:v copy -c:a copy \ -c:s srt -c:s srt output.mkv
            cmd = $"-i \"{inputFile}\" -f srt {subtitle.TrimEnd()} -map 0:0 -map 0:1 {map.TrimEnd()} -c:v copy -c:a copy {cs.TrimEnd()} {meta.TrimEnd()} \"{outputFile}\"";
            Debug.WriteLine(cmd);
            //var lib = "libx264";
            //cmd = (string.Format(FFmpegCommands.ConvertUltraFast, inputFile, outputFile, lib));
            //ffmpeg -i video.avi -vf subtitles=subtitle.srt out.avi
            //cmd = $"-i \"{inputFile}\" -vf subtitles=\"{subtitleFile}\" -c copy -c:s mov_text -c:v copy -c:a copy \"{outputFile}\"";


            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = StartupPath + FFmpegPath;
            process.StartInfo.Arguments = cmd;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            new Thread(new ThreadStart(() => {

                StreamReader sr = process.StandardError;
                Debug.WriteLine("Start.....");
                var name = "";
                if (inputFile.Contains("\\"))
                    name = Path.GetFileName(inputFile);
                else name = inputFile;


                progress = new FFmpegProgress();
                bool d = false;
                TimeSpan totalTimeSpan = TimeSpan.FromMilliseconds(0);
                Debug.WriteLine("Total:" + totalTimeSpan.TotalMilliseconds);
                //frame=   18 fps=0.0 q=0.0 size=       7kB time=00:00:00.88 bitrate=  68.8kbits/s speed=1.69x   
                while (!sr.EndOfStream)
                {
                    var v = (sr.ReadLine());
                    Debug.WriteLine(v);
                    if (v.ToLower().Contains("duration") && !d)
                    {
                        try
                        {
                            FFmpegInfo fFmpegInfo = new FFmpegInfo(v, name);
                            totalTimeSpan = fFmpegInfo.Duration;
                            progress.InputFileInfo = fFmpegInfo;

                            //Debug.WriteLine(fFmpegInfo.Duration.ToString());
                            d = true;
                        }
                        catch { }
                    }
                    if (v.Contains("time=") && totalTimeSpan.TotalMilliseconds != 0)
                    {
                        try
                        {
                            SendProgress(totalTimeSpan, v);
                        }
                        catch { }
                    }
                }
                try
                {
                    SendProgress(totalTimeSpan, ".:::END:::.");
                    // 100%
                }
                catch { }
                Debug.WriteLine("End.....");

                //GetResult("End act");
            })).Start();
        }

        /// <summary>
        /// نوشتن زیرنویس روی فایل ویدیویی(این زیرنویس ها به عنوان هارد سابتایتل شناخته میشوند)
        /// </summary>
        /// <param name="inputFile">فایل ورودی ویدیو</param>
        /// <param name="outputFile">فایل خروجی ویدیو</param>
        /// <param name="subtitleFile">فایل زیرنویس</param>
        public void BurnSubtitleIntoVideo(string inputFile, string outputFile, string subtitleFile)
        {
            if (string.IsNullOrEmpty(inputFile))
                throw new Exception("Inputfile is empty. Choose an input file.");
            if (string.IsNullOrEmpty(outputFile))
                throw new Exception("Outputfile is empty. Choose an output file.");



            //ffmpeg -i video.avi -vf subtitles=subtitle.srt out.avi
            var cmd = $"-i \"{inputFile}\" -vf subtitles=\"{subtitleFile}\" \"{outputFile}\"";


            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = StartupPath + FFmpegPath;
            process.StartInfo.Arguments = cmd;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            new Thread(new ThreadStart(() => {

                StreamReader sr = process.StandardError;
                Debug.WriteLine("Start.....");
                var name = "";
                if (inputFile.Contains("\\"))
                    name = Path.GetFileName(inputFile);
                else name = inputFile;


                progress = new FFmpegProgress();
                bool d = false;
                TimeSpan totalTimeSpan = TimeSpan.FromMilliseconds(0);
                //Debug.WriteLine("Total:" + totalTimeSpan.TotalMilliseconds);
                //frame=   18 fps=0.0 q=0.0 size=       7kB time=00:00:00.88 bitrate=  68.8kbits/s speed=1.69x   
                while (!sr.EndOfStream)
                {
                    var v = (sr.ReadLine());
                    Debug.WriteLine(v);
                    if (v.ToLower().Contains("duration") && !d)
                    {
                        try
                        {
                            FFmpegInfo fFmpegInfo = new FFmpegInfo(v, name);
                            totalTimeSpan = fFmpegInfo.Duration;
                            progress.InputFileInfo = fFmpegInfo;

                            //Debug.WriteLine(fFmpegInfo.Duration.ToString());
                            d = true;
                        }
                        catch { }
                    }
                    if (v.Contains("time=") && totalTimeSpan.TotalMilliseconds != 0)
                    {
                        try
                        {
                            SendProgress(totalTimeSpan, v);
                        }
                        catch { }
                    }
                }
                try
                {
                    SendProgress(totalTimeSpan, ".:::END:::.");
                    // 100%
                }
                catch { }
                Debug.WriteLine("End.....");

                //GetResult("End act");
            })).Start();
        }

        /// <summary>
        /// بریدن قسمتی از فایل ویدیویی یا فایل موزیک
        /// </summary>
        /// <param name="inputFile">فایل ورودی ویدیو یا آهنگ</param>
        /// <param name="outputFile">فایل خروجی ویدیو یا آهنگ</param>
        /// <param name="startTime">زمان شروع</param>
        /// <param name="duration">مدت زمان</param>
        public void CropVideoOrAudio(string inputFile, string outputFile, TimeSpan startTime, TimeSpan duration)
        {
            if (string.IsNullOrEmpty(inputFile))
                throw new Exception("Inputfile is empty. Choose an input file.");
            if (string.IsNullOrEmpty(outputFile))
                throw new Exception("Outputfile is empty. Choose an output file.");



            //ffmpeg -i source-file.foo -ss 0 -t 600 first-10-min.m4v
            //ffmpeg -i input.mp4 -ss 00:00:50.0 -codec copy -t 20 output.mp4

            var cmd = $"-i \"{inputFile}\" -ss {startTime.EncodeTime()} -codec copy -t {duration.TotalSeconds} \"{outputFile}\"";


            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = StartupPath + FFmpegPath;
            process.StartInfo.Arguments = cmd;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            new Thread(new ThreadStart(() => {

                StreamReader sr = process.StandardError;
                Debug.WriteLine("Start.....");
                var name = "";
                if (inputFile.Contains("\\"))
                    name = Path.GetFileName(inputFile);
                else name = inputFile;


                progress = new FFmpegProgress();
                bool d = false;
                TimeSpan totalTimeSpan = TimeSpan.FromMilliseconds(0);
                //Debug.WriteLine("Total:" + totalTimeSpan.TotalMilliseconds);
                //frame=   18 fps=0.0 q=0.0 size=       7kB time=00:00:00.88 bitrate=  68.8kbits/s speed=1.69x   
                while (!sr.EndOfStream)
                {
                    var v = (sr.ReadLine());
                    Debug.WriteLine(v);
                    if (v.ToLower().Contains("duration") && !d)
                    {
                        try
                        {
                            FFmpegInfo fFmpegInfo = new FFmpegInfo(v, name);
                            totalTimeSpan = fFmpegInfo.Duration;
                            progress.InputFileInfo = fFmpegInfo;

                            //Debug.WriteLine(fFmpegInfo.Duration.ToString());
                            d = true;
                        }
                        catch { }
                    }
                    if (v.Contains("time=") && totalTimeSpan.TotalMilliseconds != 0)
                    {
                        try
                        {
                            SendProgress(totalTimeSpan, v);
                        }
                        catch { }
                    }
                }
                try
                {
                    SendProgress(totalTimeSpan, ".:::END:::.");
                    // 100%
                }
                catch { }
                Debug.WriteLine("End.....");

                //GetResult("End act");
            })).Start();
        }
        
        /// <summary>
        /// افزودن صدا(ها) به یک فایل ویدیویی
        /// </summary>
        /// <param name="inputFile">فایل ورودی ویدیو</param>
        /// <param name="outputFile">فایل خروجی ویدیو</param>
        /// <param name="removeDefaultAudio">حذف صدای ویدیوی ورودی</param>
        /// <param name="audiosFilesPaths">فایل های صدا</param>
        public void MergeAudioToVideo(string inputFile, string outputFile, bool removeDefaultAudio, params string[] audiosFilesPaths)
        {
            MergeAudioToVideo(inputFile, outputFile, removeDefaultAudio, AudioBitRate._256, audiosFilesPaths);
        }

        /// <summary>
        /// افزودن صدا(ها) به یک فایل ویدیویی به همراه تنظیم بیت رِیت
        /// </summary>
        /// <param name="inputFile">فایل ورودی ویدیو</param>
        /// <param name="outputFile">فایل خروجی ویدیو</param>
        /// <param name="removeDefaultAudio">حذف صدای ویدیوی ورودی</param>
        /// <param name="outputAudioBitRate">بیت رِیت خروجی صدا</param>
        /// <param name="audiosFilesPaths">فایل های صدا</param>
        public void MergeAudioToVideo(string inputFile, string outputFile, bool removeDefaultAudio, AudioBitRate outputAudioBitRate, params string[] audiosFilesPaths)
        {
            if (string.IsNullOrEmpty(inputFile))
                throw new Exception("Inputfile is empty. Choose an input file.");
            if (string.IsNullOrEmpty(outputFile))
                throw new Exception("Outputfile is empty. Choose an output file.");


            //if (Path.GetExtension(outputFile).Replace(".", "").ToLower() != "mkv")
            //    throw new Exception("Output file most be an container file like MP4 or MKV.");

            //if (Path.GetExtension(outputFile).Replace(".", "").ToLower() != "mp4")
            //    throw new Exception("Output file most be an container file like MP4 or MKV.");

            var cmd = string.Empty;
       
            var audio = "";
            var map = "";
            var ix = 1;
            //if (removeDefaultAudio)
            //    ix = 0;
            var meta = "";
            foreach (var item in audiosFilesPaths)
            {
                audio += $"-i \"{item}\" ";
                map += $"-map {ix}:0 ";
                //-c:s srt
                var extension = Path.GetExtension(item).Replace(".", "").ToLower();
                //-metadata:s:a:0 language=eng -metadata:s:a:0 title="Title 1" \

                meta += $"-metadata:s:a:{ix} language=unknown -metadata:s:a:{ix} title=\"Audio Track #{ix}\" ";
                ix++;
            }

            //ffmpeg -i video.mp4 -i audio.mp3 -map 0:0 -map 1:0 -shortest output.mp4

            var bitRate = "192k";
            try
            {

                bitRate = outputAudioBitRate.ToString().Replace("_", "") +"k";
            }
            catch { }

            //ffmpeg -i input.mp4 -f srt -i input.srt -i input2.srt\ -map 0:0 -map 0:1 -map 1:0 -map 2:0 -c:v copy -c:a copy \ -c:s srt -c:s srt output.mkv
            if (removeDefaultAudio)
                cmd = $"-i \"{inputFile}\" {audio.TrimEnd()} -map 0:0 {map.TrimEnd()} -c:v copy -b:a {bitRate} {meta.TrimEnd()} \"{outputFile}\"";
            else
                cmd = $"-i \"{inputFile}\" {audio.TrimEnd()} -map 0:0 -map 0:1 {map.TrimEnd()} -c:v copy -b:a {bitRate} {meta.TrimEnd()} \"{outputFile}\"";


            Debug.WriteLine(cmd);


            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = StartupPath + FFmpegPath;
            process.StartInfo.Arguments = cmd;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            new Thread(new ThreadStart(() => {

                StreamReader sr = process.StandardError;
                Debug.WriteLine("Start.....");
                var name = "";
                if (inputFile.Contains("\\"))
                    name = Path.GetFileName(inputFile);
                else name = inputFile;


                progress = new FFmpegProgress();
                bool d = false;
                TimeSpan totalTimeSpan = TimeSpan.FromMilliseconds(0);
                Debug.WriteLine("Total:" + totalTimeSpan.TotalMilliseconds);
                //frame=   18 fps=0.0 q=0.0 size=       7kB time=00:00:00.88 bitrate=  68.8kbits/s speed=1.69x   
                while (!sr.EndOfStream)
                {
                    var v = (sr.ReadLine());
                    Debug.WriteLine(v);
                    if (v.ToLower().Contains("duration") && !d)
                    {
                        try
                        {
                            FFmpegInfo fFmpegInfo = new FFmpegInfo(v, name);
                            totalTimeSpan = fFmpegInfo.Duration;
                            progress.InputFileInfo = fFmpegInfo;

                            //Debug.WriteLine(fFmpegInfo.Duration.ToString());
                            d = true;
                        }
                        catch { }
                    }
                    if (v.Contains("time=") && totalTimeSpan.TotalMilliseconds != 0)
                    {
                        try
                        {
                            SendProgress(totalTimeSpan, v);
                        }
                        catch { }
                    }
                }
                try
                {
                    SendProgress(totalTimeSpan, ".:::END:::.");
                    // 100%
                }
                catch { }
                Debug.WriteLine("End.....");

                //GetResult("End act");
            })).Start();
        }

        public void ExtractImageFromVideo(string inputFile, string outputFile)
        {
            if (string.IsNullOrEmpty(inputFile))
                throw new Exception("Inputfile is empty. Choose an input file.");
            if (string.IsNullOrEmpty(outputFile))
                throw new Exception("Outputfile is empty. Choose an output file.");
            //ffmpeg -i input_file.mp4 -ss 01:23:45 -vframes 1 output.jpg
            //-i input file           the path to the input file
            //-ss 01:23:45            seek the position to the specified timestamp
            //-vframes 1              only handle one video frame
            //output.jpg              output filename, should have a well-known extension
            var cmd = $"-i \"{inputFile}\" -ss 00:00:06 -vframes 1 \"{outputFile}\"";


            Debug.WriteLine(cmd);


            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = StartupPath + FFmpegPath;
            process.StartInfo.Arguments = cmd;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            new Thread(new ThreadStart(() => {

                StreamReader sr = process.StandardError;
                Debug.WriteLine("Start.....");
                while (!sr.EndOfStream)
                {
                    var v = (sr.ReadLine());
                    Debug.WriteLine(v);
                }
                Debug.WriteLine("End.....");

                //GetResult("End act");
            })).Start();
        }
        /// <summary>
        /// پروسه فایل
        /// </summary>
        FFmpegProgress progress = new FFmpegProgress();

        /// <summary>
        /// ارسال پروسه به رویداد
        /// </summary>
        /// <param name="totalTime">زمان کل</param>
        /// <param name="content">رشته ورودی</param>
        void SendProgress(TimeSpan totalTime, string content)
        {
            //frame=   18 fps=0.0 q=0.0 size=       7kB time=00:00:00.88 bitrate=  68.8kbits/s speed=1.69x       
            string[] split = content.Split(' ');
            if (!content.Contains(".:::END:::."))
            {
                foreach (var row in split)
                {
                    if (row.Contains("time=") && totalTime.TotalMilliseconds != 0)
                    {
                        var time = row.Split('=');
                        var currentTime = TimeSpan.Parse(time[1]);

                        var percent = (int)/*Math.Round*/((currentTime.TotalMilliseconds / totalTime.TotalMilliseconds) * 100);
                        Debug.WriteLine(percent + "%     " + time[1]);
                        progress.Percent = percent;
                        progress.CurrentTime = currentTime;

                    }
                    if (row.Contains("speed=") && totalTime.TotalMilliseconds != 0)
                    {
                        //frame=   18 fps=0.0 q=0.0 size=       7kB time=00:00:00.88 bitrate=  68.8kbits/s speed=1.69x
                        var speed = row.Split('=');
                        progress.CurrentSpeed = speed[1];
                    }

                }
                //frame=   18 fps=0.0 q=0.0 size=       7kB time=00:00:00.88 bitrate=  68.8kbits/s speed=1.69x
                if (content.Contains("frame=") && content.Contains("fps"))
                {
                    var frame = content.Substring(content.IndexOf("frame=") + "frame=".Length);
                    frame = frame.Substring(0, frame.IndexOf("fps"));
                    frame = frame.Trim().TrimEnd().TrimStart();
                    progress.CurrentFrame = int.Parse(frame);
                }
                if (content.Contains("fps=") && content.Contains("q"))
                {
                    var frameRate = content.Substring(content.IndexOf("fps=") + "fps=".Length);
                    frameRate = frameRate.Substring(0, frameRate.IndexOf("q"));
                    frameRate = frameRate.Trim().TrimEnd().TrimStart();
                    progress.CurrentFrameRate = double.Parse(frameRate);
                }
                if (content.Contains("size=") && content.Contains("time"))
                {
                    var size = content.Substring(content.IndexOf("size=") + "size=".Length);
                    size = size.Substring(0, size.IndexOf("time"));
                    size = size.Trim().TrimEnd().TrimStart();
                    progress.CurrentFileSize = size;
                }
                if (content.Contains("bitrate=") && content.Contains("speed"))
                {
                    var bitrate = content.Substring(content.IndexOf("bitrate=") + "bitrate=".Length);
                    bitrate = bitrate.Substring(0, bitrate.IndexOf("speed"));
                    bitrate = bitrate.Trim().TrimEnd().TrimStart();
                    progress.CurrentBitRate = bitrate;
                }
            }
            else
            {
                progress.Percent = 100;
            }

            OnProgressChanged?.Invoke(this, progress);

        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            //foreach (var process in ProcessList)
            //{
            //    process.na
            try
            {
                Process[] workers = Process.GetProcessesByName("ffmpeg");
                foreach (Process worker in workers)
                {
                    try
                    {
                        worker.Kill();
                        worker.WaitForExit();
                        worker.Dispose();
                    }
                    catch { }
                }
            }
            catch { }
            //}
            Dispose(true);

            // Use SupressFinalize in case a subclass 
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">IsDisposing</param>
        protected virtual void Dispose(bool disposing)
        {
            //if (!_disposed)
            {
                if (disposing)
                {
                    // Clear all property values that maybe have been set
                    // when the class was instantiated
                    //InputPath = null;
                    StartupPath = null;
                }

                // Indicate that the instance has been disposed.
                //_disposed = true;
            }
        }
    }

    /// <summary>
    /// اطلاعات فایل صوتی یا تصویری
    /// </summary>
    public class FFmpegInfo
    {
        /// <summary>
        /// اِنکُدر فایل
        /// </summary>
        public string Encoder { get; set; }
        //public string Duration { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// بیت رِیت فایل
        /// </summary>
        public string BitRate { get; set; }
        /// <summary>
        /// زمان فایل
        /// </summary>
        public TimeSpan Duration/*2*/ { get; set; }
        /// <summary>
        /// استریم های صدا
        /// </summary>
        public List<AudioStreamInfo> AudioStreams { get; internal set; } = new List<AudioStreamInfo>();
        /// <summary>
        /// استریم های ویدیو
        /// </summary>
        public List<VideoStreamInfo> VideoStreams { get; internal set; } = new List<VideoStreamInfo>();
        internal FFmpegInfo(string content,string name)
        {
            if (string.IsNullOrEmpty(content))
                return;
            try
            {
                Name = name;
                var list = new List<string>(content.Split(new string[]
                {
                        Environment.NewLine,"\r\n","\n"
                }, StringSplitOptions.RemoveEmptyEntries));

                foreach (var item in list)
                {
                    if (item.Contains("encoder"))
                    {
                        try
                        {
                            //encoder         : Lavf56.1.100
                            var encoder = item.Substring(item.IndexOf("encoder"));
                            encoder = encoder.Substring(encoder.IndexOf(":") + 1);
                            Encoder = encoder.Trim();
                        }
                        catch { }
                    }
                    else if (item.Contains("Duration"))
                    {
                        try
                        {
                            //  Duration: 00:04:01.26, start: 0.000000, bitrate: 3239 kb/s
                            var split = item.Split(',');
                            var dur = split[0].Substring(split[0].IndexOf("Duration:") + "Duration:".Length).Trim().TrimStart().TrimEnd();

                            Duration = TimeSpan.Parse(dur);



                            foreach (var it in split)
                                if (it.Contains("kb"))
                                {
                                    BitRate = it.Substring(it.IndexOf("bitrate:") + "bitrate:".Length).Trim();
                                    break;
                                }
                        }
                        catch { }

                    }
                }

                var streams = new List<string>(content.Split(new string[]
                {
                   "Stream #"
                }, StringSplitOptions.RemoveEmptyEntries));
                if (streams.Count > 0)
                    streams.RemoveAt(0);

                VideoStreams = new List<VideoStreamInfo>();
                AudioStreams = new List<AudioStreamInfo>();
                int ix = 0;
                foreach (var item in streams)
                {
                    if(item.Contains("Video"))
                    {
                        VideoStreams.Add(new VideoStreamInfo(item, ix));
                    }
                    else if (item.Contains("Audio"))
                    {
                        AudioStreams.Add(new  AudioStreamInfo(item, ix));
                    }
                    ix++;
                }
            }
            catch { }
        }
    }

    /// <summary>
    /// استریم صدا
    /// </summary>
    public class AudioStreamInfo
    {
        //Stream #0:1(und): Audio: aac (LC) (mp4a / 0x6134706D), 44100 Hz, stereo, fltp, 125 kb/s (default)
        //Stream #0:1:      Audio: aac (LC), 44100 Hz, stereo, fltp (default)
        /// <summary>
        /// نام استریم صدا
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// نام کُدِک
        /// </summary>
        public string CodecName { get; private set; }
        /// <summary>
        /// بیت رِیت صدا
        /// </summary>
        public string BitRate { get; private set; }
        /// <summary>
        /// سمپل رِیت صدا
        /// </summary>
        public string SamplingRate { get; private set; }

        internal AudioStreamInfo(string content,int id)
        {
            if (string.IsNullOrEmpty(content))
                return;
            try
            {
                if (content.ToLower().Contains("audio"))
                { 
                    //Stream #0:1(und): Audio: aac (LC) (mp4a / 0x6134706D), 44100 Hz, stereo, fltp, 125 kb/s (default)
                     //Stream #0:1:      Audio: aac (LC), 44100 Hz, stereo, fltp (default)
                    content = content.Trim();
                    MatchCollection reg = Regex.Matches(content, @"\(([^)]*)\)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    if (reg.Count > 0)
                    {
                        foreach (Capture item in reg)
                            content = content.Replace(item.Value, item.Value.Replace(",", "%$"));
                    }
                    var split = content.Split(',');
                    Name = $"Stream #{id} -Audio";
                    CodecName = split[0].Substring(split[0].IndexOf("Audio") + 6).Trim().TrimStart().TrimEnd().Replace("%$", ",");
                    foreach (var item in split)
                    {
                        if (item.Contains("Hz"))
                        {
                            SamplingRate = item.Trim();
                            SamplingRate = SamplingRate.Substring(0, SamplingRate.IndexOf(" "));
                            float i = float.Parse(SamplingRate.Trim());
                            i = i / 1000;
                            SamplingRate = i + " kHz";
                        }
                        else if (item.Contains("kb"))
                        {
                            BitRate = item.Substring(0,item.IndexOf("\r\n")).Trim().TrimStart().TrimEnd().Replace("%$", ",");
                        }
                    }
                }
            }
            catch { }
        }
    }

    /// <summary>
    /// استریم ویدیو
    /// </summary>
    public class VideoStreamInfo
    {
        //Stream #0:0(und): Video: h264 (High) (avc1 / 0x31637661), yuv420p(tv, bt709), 1920x1080 [SAR 1:1 DAR 16:9], 3107 kb/s, 23.98 fps, 23.98 tbr, 90k tbn, 47.95 tbc (default)
        //Video: h264 (High), yuv420p(tv, bt709, progressive), 1920x1080 [SAR 1:1 DAR 16:9], 25 fps, 25 tbr, 1k tbn, 50 tbc (default)
        /// <summary>
        /// نام استریم ویدیو
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// نام کُدِک
        /// </summary>
        public string CodecName { get; private set; }
        /// <summary>
        /// رنگ فضا
        /// </summary>
        public string ColorSpace { get; private set; }
        /// <summary>
        /// عرض ویدیو بر حسب پیکسل
        /// </summary>
        public int PixelWidth { get; private set; }
        /// <summary>
        /// طول ویدیو بر حسب پیکسل
        /// </summary>
        public int PixelHeight { get; private set; }
        /// <summary>
        /// فریم رِیت ویدیو
        /// </summary>
        public string FrameRate { get; private set; }
        /// <summary>
        /// بیت رِیت ویدیو
        /// </summary>
        public string BitRate { get; private set; }
        internal VideoStreamInfo(string content,int id)
        {
            if (string.IsNullOrEmpty(content))
                return;
            try
            {
                if (content.ToLower().Contains("video"))
                {
                    //Stream #0:0(und): Video: h264 (High) (avc1 / 0x31637661), yuv420p(tv, bt709), 1920x1080 [SAR 1:1 DAR 16:9], 3107 kb/s, 23.98 fps, 23.98 tbr, 90k tbn, 47.95 tbc (default)
                    //Video: h264 (High), yuv420p(tv, bt709, progressive), 1920x1080 [SAR 1:1 DAR 16:9], 25 fps, 25 tbr, 1k tbn, 50 tbc (default)

                    content = content.Trim();
                    MatchCollection reg = Regex.Matches(content, @"\(([^)]*)\)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    if (reg.Count > 0)
                    {
                        foreach (Capture item in reg)
                            content = content.Replace(item.Value, item.Value.Replace(",", "%$"));
                    }

                    //var split = content.Split(',');
                    var split = content.Split(',');
                    Name = $"Stream #{id} -Video";

                    CodecName = split[0].Substring(split[0].IndexOf("Video:") + 6).Trim().TrimStart().TrimEnd().Replace("%$", ",");
                    if (split[1].Contains("("))
                        ColorSpace = split[1].Substring(0, split[1].IndexOf("(")).Trim().TrimStart().TrimEnd().Replace("%$", ",").ToUpper();
                    else
                        ColorSpace = split[1].Trim().TrimStart().TrimEnd().Replace("%$", ",").ToUpper();
                    var heightWidth = "";
                    split[2] = split[2].Trim().TrimStart().TrimEnd();
                    if (split[2].Contains(" "))
                        heightWidth = split[2].Substring(0, split[2].IndexOf(" ")).Trim().TrimStart().TrimEnd();
                    else heightWidth = split[2];
                    PixelWidth = int.Parse(heightWidth.Split('x')[0]);
                    PixelHeight = int.Parse(heightWidth.Split('x')[1]);

                    foreach (var item in split)
                    {
                        if (item.Contains("fps"))
                        {
                            FrameRate = item.Trim().TrimStart().TrimEnd().Replace("%$", ",").ToUpper();
                        }
                        else if (item.Contains("kb"))
                        {
                            BitRate = item.Trim().TrimStart().TrimEnd().Replace("%$", ",");
                        }
                    }
                }
            }
            catch { }
        }

    }
}
