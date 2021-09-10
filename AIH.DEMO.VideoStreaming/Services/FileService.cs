using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Events;

namespace AIH.DEMO.VideoStreaming.Services
{
    public class FileService
    {
        private readonly string _targetFFpegExePath;
        private readonly string _targetFilePath;

        public FileService(IConfiguration config)
        {
            _targetFilePath = config.GetValue<string>("StoredFilesPath");
            _targetFFpegExePath = config.GetValue<string>("FFMPEGExePath");
        }
        public void UploadVideo(IFormFile video)
        {

        }

        public async Task ProcessVideo()
        {
            var randomFileName = Path.GetRandomFileName();

            FFmpeg.SetExecutablesPath(_targetFFpegExePath);
            var inputFile = $"{_targetFilePath}\\2_3mg.mp4";
            var outputFile = $"{_targetFilePath}\\processed\\{randomFileName}_output.mp4";


            IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(inputFile);

            IStream videoStream = mediaInfo.VideoStreams.FirstOrDefault()?.SetCodec(VideoCodec.h264);



            await FFmpeg.Conversions.New().
                AddStream(videoStream).
                AddParameter($"-map 0").
                AddParameter("-c copy").
                AddParameter($"-hls_time 10").
                AddParameter($"-hls_playlist_type vod").
                AddParameter($"-hls_segment_filename {_targetFilePath}\\processed\\32s_new\\30s_new_%03d.ts {_targetFilePath}\\processed\\32s_new\\30s_new.m3u8").
                //SetOutput(outputFile).
                //SetOutputFormat(Format.mp4).

                Start();

        }


        public void OnPregress(ConversionProgressEventHandler e)
        {

        }
    }
}
