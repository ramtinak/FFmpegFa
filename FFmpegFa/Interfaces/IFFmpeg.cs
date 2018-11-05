using System;

namespace FFmpegFa.Interfaces
{
    /// <summary>
    /// اینترفیس فایل
    /// </summary>
    public interface IFFmpeg
    {
        string LibraryVersion();
        string AboutAuthor();
        //void OpenFile(string filePath);
        FFmpegInfo GetInformation(string filePath);
        void ConvertVideo(string inputFile, string outputFile);
        void ConvertVideo(string inputFile, string outputFile, ConverterCodecType outputCodecType);
        void ConvertVideo(string inputFile, string outputFile, ConverterCodecType outputCodecType, VideoBitRate outputVideoBitRate, AudioBitRate outputAudioBitRate);

        void ConvertAudio(string inputFile, string outputFile);
        void ConvertAudio(string inputFile, string outputFile, AudioFileType outputAudioType);
        void ConvertAudio(string inputFile, string outputFile, AudioFileType outputAudioType, AudioSampleRate outputAudioSampleRate, AudioBitRate outputAudioBitRate);

        void RemoveAudioFromVideo(string inputFile, string outputFile);

        void ExtractAudioFromVideo(string inputFile, string outputFile);
        //void ExtractAudioFromVideo(string inputFile, string outputFile, AudioBitRate outputAudioBitRate);

        void ResizeVideo(string inputFile, string outputFile, VideoSize newSize);
        void ResizeVideo(string inputFile, string outputFile, VideoSize newSize, VideoBitRate outputVideoBitRate, AudioBitRate outputAudioBitRate);

        void BurnSubtitleIntoVideo(string inputFile, string outputFile, string subtitleFile);
        void MergeSubtitlesToVideo(string inputFile, string outputFile, params string[] subtitlesFilePaths);

        void CropVideoOrAudio(string inputFile, string outputFile, TimeSpan startTime, TimeSpan duration);

        //void ExtractFrameToImage(string inputFile, string outputImageFile, VideoSize size);
        
        void MergeAudioToVideo(string inputFile, string outputFile, bool removeDefaultAudio, params string[] audiosFilesPaths);
        void MergeAudioToVideo(string inputFile, string outputFile, bool removeDefaultAudio, AudioBitRate outputAudioBitRate, params string[] audiosFilesPaths);
        void ExtractImageFromVideo(string inputFile, string outputFile);

        void AddWatermarkText(string inputFile, string outputFile, WatermarkText watermarkText);
        void AddWatermarkImage(string inputFile, string outputFile, WatermarkImage watermarkImage);
    }
}
