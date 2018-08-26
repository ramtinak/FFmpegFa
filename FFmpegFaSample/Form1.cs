using FFmpegFa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace FFmpegFaSample
{
    public partial class Form1 : Form
    {
        FFmpeg fFmpeg;
        public Form1()
        {
            InitializeComponent();

            fFmpeg = new FFmpeg(/*Application.StartupPath*/);
            fFmpeg.OnProgressChanged += FFmpeg_OnProgressChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void FFmpeg_OnInformation(FFmpeg sender, FFmpegInfo FFmpegInfo)
        {
            Debug.WriteLine(FFmpegInfo.Name);
        }

        private void button2_Click(object sender, EventArgs e)
        {
     
        }
        const string FFmpegPath = @"\ffmpeg.exe";

        private void button3_Click(object sender, EventArgs e)
        {
            ///-i {textBox1.Text} -map single_highest_quality_video_stream_from_all_inputs -map single_highest_quality_audio_stream_from_all_inputs {textBox2.Text}
            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = Application.StartupPath + FFmpegPath;
            //--txt_format text -i input_file out.srt
            var st = "C:\\Users\\Ramti\\Documents\\Visual Studio 2017\\Projects\\FFmpegFa\\FFmpegFaSample\\bin\\Debug\\New folder\\";

            //process.StartInfo.Arguments = $"-txt_format text -i {textBox1.Text} out.srt";
            process.StartInfo.Arguments = $"-i {textBox1.Text} -map 0:s:? \"{st}subs.srt\" -map 0:s:1 \"{st}rmt2.srt\"";
            // -vn -an -codec:s srt 
            //process.StartInfo.Arguments = $"-i {textBox1.Text} -c:s copy sub.srt";

            //process.StartInfo.Arguments = $"-i {textBox1.Text} -an -vcodec copy {textBox2.Text}";
            //process.StartInfo.Arguments = $"-i {textBox1.Text} -map single_highest_quality_video_stream_from_all_inputs -map single_highest_quality_audio_stream_from_all_inputs {textBox2.Text}";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();

            StreamReader sr = process.StandardError;
            richTextBox2.Text += sr.ReadToEnd();
          
            //fFmpeg.GetInformation3();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //h264 (High) (avc1 / 0x31637661), yuv420p(tv, bt709), 1920x1080 [SAR 1:1 DAR 16:9], 3107 kb/s
            //h264 (High), yuv420p(tv, bt709, progressive), 1920x1080 [SAR 1:1 DAR 16:9], 25 fps
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var ff = fFmpeg.GetInformation(textBox1.Text);
            Debug.WriteLine(ff.Duration); 
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.Elapsed.ToString());

            return;


            var content = "h264 (High), yuv420p(tv, bt709, progressive), 1920x1080 [SAR 1:1 DAR 16:9], 25 fps";

            //var aontent = Regex.Replace(content, @"\(([^)]*)\)","");
            //foreach(var item in content.ToCharArray())
            //richTextBox2.Text = /*ExtractNumber*/(aontent);

            //var match = Regex.Match(content, @"\(([^)]*)\)");

         
            MatchCollection reg = Regex.Matches(content, @"\(([^)]*)\)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var content2 = "";
            if (reg.Count > 0)
            {
                foreach(Capture item in reg)
                    content2 = content.Replace(item.Value, item.Value.Replace(",", "%$"));
            }
          
            var split = content2.Split(',');
            stopwatch.Stop();
           Debug.WriteLine(stopwatch.Elapsed.ToString());
            richTextBox2.Text += Environment.NewLine + Environment.NewLine + content2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text = System.Reflection.Assembly.GetExecutingAssembly().Location +"\r\n\r\n" +
            //    System.Reflection.Assembly.GetExecutingAssembly().CodeBase + "\r\n\r\n" +

            //Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            ////using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
            ////{
            ////    archive.CreateEntryFromFile(newFile, "NewEntry.txt");
            ////    archive.ExtractToDirectory(extractPath);
            ////}

            fFmpeg.ConvertVideo(textBox1.Text, textBox2.Text, ConverterCodecType.x265);

        }

        private void FFmpeg_OnProgressChanged(FFmpeg sender, FFmpegProgress progress)
        {
            Debug.WriteLine(progress.Percent + "%   " + progress.CurrentSpeed + "    " 
                +progress.CurrentFileSize + "    " + progress.CurrentBitRate + "    "+
                progress.CurrentFrame + "    " +progress.CurrentFrameRate + "    "+
                progress.CurrentTime+"   ");
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => FFmpeg_OnProgressChanged(sender,progress)));
                return;
            }
            progressBar1.Value = progress.Percent;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            fFmpeg.ConvertAudio(textBox1.Text, textBox2.Text, AudioFileType.M4a);
            return;




            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = Application.StartupPath + FFmpegPath;
            //--txt_format text -i input_file out.srt


            //process.StartInfo.Arguments = $"-txt_format text -i {textBox1.Text} out.srt";
            process.StartInfo.Arguments =textBox3.Text;
            // -vn -an -codec:s srt 
            //process.StartInfo.Arguments = $"-i {textBox1.Text} -c:s copy sub.srt";

            //process.StartInfo.Arguments = $"-i {textBox1.Text} -an -vcodec copy {textBox2.Text}";
            //process.StartInfo.Arguments = $"-i {textBox1.Text} -map single_highest_quality_video_stream_from_all_inputs -map single_highest_quality_audio_stream_from_all_inputs {textBox2.Text}";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            new Thread(new ThreadStart(() =>
            {
                StreamReader sr = process.StandardError;
                while (!sr.EndOfStream)
                {
                    var v = (sr.ReadLine());
                    SSS(v);
                }
                //new Action(() => { richTextBox2.Text += sr.ReadToEnd(); }).Invoke();
  
            })).Start();
        }
        void SSS(string content)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => SSS(content)));
                return;
            }

            richTextBox2.Text += content+"\r\n\r\n";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            fFmpeg.RemoveAudioFromVideo(textBox1.Text, textBox2.Text);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            fFmpeg.ExtractAudioFromVideo(textBox1.Text, textBox2.Text);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            fFmpeg.BurnSubtitleIntoVideo(textBox1.Text, textBox2.Text, textBox3.Text);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            fFmpeg.CropVideoOrAudio(textBox1.Text, textBox2.Text, TimeSpan.FromSeconds(40),TimeSpan.FromSeconds(20));

        }

        private void button11_Click(object sender, EventArgs e)
        {
            fFmpeg.ResizeVideo(textBox1.Text, textBox2.Text, new VideoSize(320,240));

        }

        private void button12_Click(object sender, EventArgs e)
        {
            fFmpeg.MergeAudioToVideo(textBox1.Text, textBox2.Text, false,AudioBitRate._192,  textBox3.Text, textBox4.Text);
        }
    }
}



/* 
        private void button4_Click(object sender, EventArgs e)
        {
            //h264 (High) (avc1 / 0x31637661), yuv420p(tv, bt709), 1920x1080 [SAR 1:1 DAR 16:9], 3107 kb/s
            //h264 (High), yuv420p(tv, bt709, progressive), 1920x1080 [SAR 1:1 DAR 16:9], 25 fps
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var content = "h264 (High), yuv420p(tv, bt709, progressive), 1920x1080 [SAR 1:1 DAR 16:9], 25 fps";

            //var aontent = Regex.Replace(content, @"\(([^)]*)\)","");
            //foreach(var item in content.ToCharArray())
            //richTextBox2.Text = 
(aontent);

            //var match = Regex.Match(content, @"\(([^)]*)\)");

         
            MatchCollection reg = Regex.Matches(content, @"\(([^)]*)\)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
var content2 = "";
            if (reg.Count > 0)
            {
                foreach(Capture item in reg)
                    content2 = content.Replace(item.Value, item.Value.Replace(",", "%$"));
            }
          
            var split = content2.Split(',');
stopwatch.Stop();
           Debug.WriteLine(stopwatch.Elapsed.ToString());
            richTextBox2.Text += Environment.NewLine + Environment.NewLine + content2;
        }

        private void button5_Click(object sender, EventArgs e)
{
    var content = richTextBox1.Text;

    var aontent = Regex.Replace(content, @"\(([^)]*)\)", "");
    //foreach(var item in content.ToCharArray())
 

    //var match = Regex.Match(content, @"\(([^)]*)\)");

    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    string str = "h264 (High), yuv420p(tv, bt709, progressive), 1920x1080 [SAR 1:1 DAR 16:9], 25 fps";
    var c = str.ToCharArray();
    List<string> result = new List<string>();
    for (int i = 0; i < c.Length; i++)
    {
        var temp = string.Empty;
        if (c[i] == '(')
        {
            i++;
            for (; i < c.Length; i++)
            {
                if (c[i] != ')')
                {
                    temp += c[i];
                }
                else
                    break;
            }
            if (temp.Length != 0)
                result.Add(temp);

        }
    }
    richTextBox2.Text = string.Join(",", result);
    stopwatch.Stop();
    Debug.WriteLine(stopwatch.Elapsed.ToString());

}*/