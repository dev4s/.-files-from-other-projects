using System;
using System.IO;
using System.Net;
using System.Threading;

namespace OpineoDownloader
{
    public static class WebRequester
    {
        public static string GetDataFromSite(string siteName)
        {
            var request = (HttpWebRequest)WebRequest.Create(siteName);
            var response = (HttpWebResponse)request.GetResponse();

            try
            {
                if (response.GetResponseStream() == null)
                {
                    MessageWindow.ShowInfo("Serwer nie odpowiada. Proszę spróbuj później i/lub skontaktuj się z twórcą oprogramowania.");
                    return null;
                }

                using (var output = new StreamReader(response.GetResponseStream()))
                {
                    return output.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                MessageWindow.ShowError(e.Message);
            }

            response.Close();
            request.Abort();

            return null;
        }
    }
}