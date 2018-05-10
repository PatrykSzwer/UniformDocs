using System;
using System.Threading.Tasks;
using Joozek78.Star.Async;
using Starcounter;

namespace KitchenSink.ViewModels.HowTo
{
    partial class ProgressBarPage : Json
    {
        private void Handle(Input.StartProgressTrigger action)
        {
            if (this.TaskIsRunnning)
            {
                return;
            }

            AsyncInputHandlers.Run(StartSimpleProgressBarAsync);
        }

        private async Task StartSimpleProgressBarAsync()
        {
            // Set up view-model properties
            this.Progress = 0;
            this.TaskIsRunnning = true;
            this.FileDownloadText = "Downloading File";

            // Set up progress reports. They will be executed on Starcounter Scheduler
            IProgress<long> progress = new Progress<long>(SimpleProgressUpdate);

            // Fire the background task
            await Task.Run(() => PerformBackgroundJobAsync(progress));

            // After the background task is completed, clean up - back on Starcounter Sheduler
            this.FileDownloadText = "Download Complete";
            this.DownloadButtonText = "Download another (imaginary) file!";
            this.TaskIsRunnning = false;
        }

        private async Task PerformBackgroundJobAsync(IProgress<long> progress)
        {
            // This method is executed on a Thread Pool. It doesn't block the Starcounter Scheduler
            long tempProgress = 0;
            while (tempProgress < 100)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(30))
                    .ConfigureAwait(false);
                tempProgress++;

                // Progress class takes care that SimpleProgressUpdate method is called on Starcounter Scheduler
                progress.Report(tempProgress);
            }
        }

        private void SimpleProgressUpdate(long progress)
        {
            this.Progress = progress;
        }
    }
}
