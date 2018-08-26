using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//-i "INPUTFILE.OGG" -vn -ar 44100 -ac 2 -ab 192k -f mp3 "OUTPUTFILE.mp3"
namespace FFmpegFa
{
    /// <summary>
    /// https://www.labnol.org/internet/useful-ffmpeg-commands/28490/
    /// </summary>
    class FFmpegCommands
    {
        
        public const string Information = "-i {0}";
        public const string ExtractSub = "-i {textBox1.Text} -map 0:s:? \"{st}subs.srt\" -map 0:s:1 \"{st}rmt2.srt\"";
        //00:00:50.0
        public const string Cut = "-i {0} -ss {2} -codec copy -t 20 {1}";
        /*/// <summary>
        /// -i video.mp4 -t 00:00:50 -c copy small-1.mp4 -ss 00:00:50 -codec copy small-2.mp4
        /// </summary>
        //public const string Split = "-i video.mp4 -t 00:00:50 -c copy small-1.mp4 -ss 00:00:50 -codec copy small-2.mp4";
        */



        /// <summary>
        /// -i youtube.flv -c:v libx264 filename.mp4
        /// </summary>
        public const string Convert = "-i \"{0}\" -c:v {2} \"{1}\"";
        /// <summary>
        /// -i video.wmv -c:v libx264 -preset ultrafast video.mp4
        /// </summary>
        public const string ConvertUltraFast = "-i \"{0}\" -c:v {2} -preset ultrafast \"{1}\"";

        /// <summary>
        /// -f concat -i file-list.txt -c copy output.mp4
        /// </summary>
        public const string Concat = "-f concat -i file-list.txt -c copy output.mp4";

        /// <summary>
        /// Use the -an parameter to disable the audio portion of a video stream.
        /// -i video.mp4 -an mute-video.mp4
        /// </summary>
        public const string MuteAudio = "-i video.mp4 -an mute-video.mp4";

        /// <summary>
        /// The -vn switch extracts the audio portion from a video and we are using the -ab switch to save the audio as a 256kbps MP3 audio file.
        /// </summary>
        public const string ExtractAudio = "-i video.mp4 -vn -ab 256 audio.mp3";

        /// <summary>
        /// FFmpeg is an excellent tool for converting videos into animated GIFs and the quality isn’t bad either. Use the scale filter to specify the width of the GIF, the -t parameter specific the duration while -r specifies the frame rate (fps).
        /// </summary>
        public const string ConvertToGif = "-i video.mp4 -vf scale=500:-1 -t 10 -r 10 image.gif";


        /// <summary>
        /// This command will extract the video frame at the 15s mark and saves it as a 800px wide JPEG image. You can also use the -s switch (like -s 400×300) to specify the exact dimensions of the image file though it will probably create a stretched image if the image size doesn’t follow the aspect ratio of the original video file.
        /// </summary>
        public const string ExtractFrames = "-ss 00:00:15 -i video.mp4 -vf scale=800:-1 -vframes 1 image.jpg";

        /// <summary>
        /// You can use FFmpeg to automatically extract image frames from a video every ‘n’ seconds and the images are saved in a sequence. This command saves image frame after every 4 seconds.
        /// </summary>
        public const string ConvertToImages = "ffmpeg -i movie.mp4 -r 0.25 frames_%04d.png";

        /// <summary>
        /// You can also specify the -shortest switch to finish the encoding when the shortest clip ends.
        /// </summary>
        public const string MergingAudio = "-i video.mp4 -i audio.mp3 -c:v copy -c:a aac -strict experimental output.mp4";
        /// <summary>
        /// You can also specify the -shortest switch to finish the encoding when the shortest clip ends.
        /// </summary>
        public const string MergingAudio2 = "-i video.mp4 -i audio.mp3 -c:v copy -c:a aac -strict experimental -shortest output.mp4";

        /// <summary>
        /// Use the size (-s) switch with ffmpeg to resize a video while maintaining the aspect ratio.
        /// </summary>
        public const string Resize = "-i input.mp4 -s 480x320 -c:a copy output.mp4";


        /// <summary>
        /// This command creates a video slideshow using a series of images that are named as img001.png, img002.png, etc. Each image will have a duration of 5 seconds (-r 1/5).
        /// </summary>
        public const string CreateVideoSlideshowFromImages = "-r 1/5 -i img%03d.png -c:v libx264 -r 30 -pix_fmt yuv420p slideshow.mp4";



        /// <summary>
        /// You can add a cover image to an audio file and the length of the output video will be the same as that of the input audio stream. This may come handy for uploading MP3s to YouTube.
        /// </summary>
        public const string AddAPosterImageToAudio = "-loop 1 -i image.jpg -i audio.mp3 -c:v libx264 -c:a aac -strict experimental -b:a 192k -shortest output.mp4";



        /// <summary>
        /// Use the -t parameter to specify the duration of the video.
        /// </summary>
        public const string ConvertASingleImageToVideo = "-loop 1 -i image.png -c:v libx264 -t 30 -pix_fmt yuv420p video.mp4";



        /// <summary>
        /// This will take the subtitles from the .srt file. FFmpeg can decode most common subtitle formats.
        /// </summary>
        public const string AddSubtitleToVideo = "-i movie.mp4 -i subtitles.srt -map 0 -map 1 -c copy -c:v libx264 -crf 23 -preset veryfast output.mkv";

        /// <summary>
        /// This will create a 30 second audio file starting at 90 seconds from the original audio file without transcoding.
        /// </summary>
        public const string CropAnAudio = "-ss 00:01:30 -t 30 -acodec copy -i inputfile.mp3 outputfile.mp3";

        /// <summary>
        /// You can use the volume filter to alter the volume of a media file using FFmpeg. This command will half the volume of the audio file.
        /// </summary>
        public const string ChangeAudioVolume = "-i input.wav -af 'volume=0.5' output.wav";

        /// <summary>
        /// This command will rotate a video clip 90° clockwise. You can set transpose to 2 to rotate the video 90° anti-clockwise.
        /// </summary>
        public const string RotateVideo = "-i input.mp4 -filter:v 'transpose=1' rotated-video.mp4";
        /// <summary>
        /// This will rotate the video 180° counter-clockwise.
        /// </summary>
        public const string RotateVideo2 = "-i input.mp4 -filter:v 'transpose=2,transpose=2' rotated-video.mp4";

        /// <summary>
        /// You can change the speed of your video using the setpts (set presentation time stamp) filter of FFmpeg. This command will make the video 8x (1/8) faster or use setpts=4*PTS to make the video 4x slower.
        /// </summary>
        public const string SpeedUpDownVideo = "-i input.mp4 -filter:v \"setpts=0.125*PTS\" output.mp4";
        /// <summary>
        /// For changing the speed of audio, use the atempo audio filter. This command will double the speed of audio. You can use any value between 0.5 and 2.0 for audio.
        /// </summary>
        public const string SpeedUpDownAudio = "-i input.mkv -filter:a \"atempo=2.0\" -vn output.mkv";
    }
}
