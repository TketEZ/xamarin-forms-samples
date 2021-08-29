using System;
using AVFoundation;
using CognitiveSpeechService.Services;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(CognitiveSpeechService.iOS.Services.IOSAudioSessionService))]
namespace CognitiveSpeechService.iOS.Services
{
    public class IOSAudioSessionService : IAudioSessionService
    {
        public void ActivateAudioPlaybackSession()
        {
            var session = AVAudioSession.SharedInstance();
            session.SetCategory(AVAudioSessionCategory.Playback, AVAudioSessionCategoryOptions.DuckOthers);
            session.SetMode(AVAudioSession.ModeSpokenAudio, out NSError error);
            session.SetActive(true);
        }

        public void ActivateAudioRecordingSession()
        {
            try
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    var session = AVAudioSession.SharedInstance();
                    session.SetCategory(AVAudioSessionCategory.Record);
                    session.SetActive(true);
                })).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
