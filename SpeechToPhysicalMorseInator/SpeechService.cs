using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToPhysicalMorseInator
{
    internal class SpeechService
    {
        public async Task SpeechContinuousRecognitionAsync()
        {
            // Creates an instance of a speech config with specified subscription key and region.
            // Replace with your own subscription key and service region (e.g., "westus").
            var config = SpeechConfig.FromSubscription("KEY", "WHERE");
            config.SpeechRecognitionLanguage = "pt-BR";
            config.SetProfanity(ProfanityOption.Removed);
            //config.SpeechSynthesisLanguage = "en-US";

            // Creates a speech recognizer from microphone.
            using (var recognizer = new SpeechRecognizer(config))
            {
                // Subscribes to events.
                recognizer.Recognizing += (s, e) => {
                    Console.WriteLine($"RECOGNIZING: Text={e.Result.Text}");
                };

                recognizer.Recognized += (s, e) => {
                    var result = e.Result;
                    Console.WriteLine($"Reason: {result.Reason.ToString()}");
                
                    if (result.Reason == ResultReason.RecognizedSpeech)
                    {
                        var morseText = new MorseService().ParseText(result.Text);

                        ArduinoService.SendMessage(morseText);
                    }
                };

                recognizer.Canceled += (s, e) => {
                    Console.WriteLine($"\n    Canceled. Reason: {e.Reason.ToString()}, CanceledReason: {e.Reason}");
                    Console.WriteLine(e);
                };

                recognizer.SessionStarted += (s, e) => {
                    Console.WriteLine("\n    Session started event.");
                };

                recognizer.SessionStopped += (s, e) => {
                    Console.WriteLine("\n    Session stopped event.");
                };

                // Starts continuous recognition. 
                // Uses StopContinuousRecognitionAsync() to stop recognition.
                await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

                do
                {
                    Console.WriteLine("Press Enter to stop");
                } while (Console.ReadKey().Key != ConsoleKey.Enter);

                // Stops recognition.
                await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
            }
        }
    }
}
