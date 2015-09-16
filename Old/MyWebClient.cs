using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpineoDownloader
{
    public class MyWebClient
    {
        private Label _reportLabel;
        private ProgressBar _reportProgressBar;
        private readonly BackgroundWorker _backgroundWorker;

        public MyWebClient(BackgroundWorker backgroundWorker, ProgressBar reportProgressBar = null, Label reportLabel = null)
        {
            _reportLabel = reportLabel;
            _reportProgressBar = reportProgressBar;
            _backgroundWorker = backgroundWorker;

            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.ProgressChanged += ProgressChanged;
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (_reportLabel != null)
            {

            }

            if (_reportProgressBar != null)
            {

            }
        }

        private void ReportToLabel(string text)
        {
            throw new NotImplementedException();
        }

        private void ReportToProgressBar(int value)
        {
            throw new NotImplementedException();
        }
    }
}