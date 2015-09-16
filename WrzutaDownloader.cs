using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace WrzutaDownloader
{
	public partial class WrzutaDownloader : Form
	{
		private const string _urlTextBoxTemp = "Tutaj wpisz/wklej URL";

		//TODO: Progress bar resetting
		//TODO: Add information to message show which element was downloaded
		//TODO: glue windows together if there are more than 1 runed

		public WrzutaDownloader()
		{
			InitializeComponent();
		}

		private void WrzutaDownloader_Load(object sender, EventArgs e)
		{
			Text += " ver. 0.1";
			//urlTextBox.Text = @"monsterrr.wrzuta.pl/audio/4epxIYDCAy5/o2._lady_gaga_-_alejandro";
		}

		private void urlTextBox_Enter(object sender, EventArgs e)
		{
			if (urlTextBox.Text == _urlTextBoxTemp)
			{
				urlTextBox.Text = "";
			}
		}

		private void urlTextBox_Leave(object sender, EventArgs e)
		{
			if (urlTextBox.Text == "")
			{
				urlTextBox.Text = _urlTextBoxTemp;
			}
		}

		private void Download_Click(object sender, EventArgs e)
		{
			if (!PrimaryCheckings()) return;
			urlTextBox.Enabled = false;

			//2 warunki: 1. jest http, 2. nie ma http

			var stringFromUrlTextBox = urlTextBox.Text;
			var urlRegexHttp = new Regex("http:");
			var tempRegex = new Regex("/");
			string tempString;
			string tempName;
			if (urlRegexHttp.Match(stringFromUrlTextBox).Success)
			{
				//jest http
				var tempStringArray = tempRegex.Split(stringFromUrlTextBox);
				tempString = String.Format("{0}//{1}/xml/plik/{2}", tempStringArray[0], tempStringArray[2], tempStringArray[4]);
				tempName = tempStringArray[5];
			}
			else
			{
				//nie ma http
				var tempStringArray = tempRegex.Split(stringFromUrlTextBox);
				tempString = String.Format("http://{0}/xml/plik/{1}", tempStringArray[0], tempStringArray[2]);
				tempName = tempStringArray[3];
			}

			var stream = "";
			try 
			{
				var request = (HttpWebRequest)WebRequest.Create(tempString);
				var response = (HttpWebResponse)request.GetResponse();
				var input = new StreamReader(response.GetResponseStream());
				stream = input.ReadToEnd();
			}
			catch(Exception)
			{
				ShowError("Wystąpił błąd w połączeniu! Najprawdopodobniej został podany zły adres.");
				urlTextBox.Enabled = true;
				return;
			}

			//TODO: <mime>audio/mpeg</mime>

			const string errorMsg = "error";
			if (new Regex(errorMsg).Match(stream).Success)
			{
				ShowError("Czy ten plik na pewno istnieje jeszcze na serwerze wrzuta.pl?");
				urlTextBox.Enabled = true;
				return;
			}

			const string regexFileIdStart = "\\<fileId\\>\\<!\\[CDATA\\[";
			const string regexFileIdEnd = "\\]\\]\\>\\</fileId\\>";
			tempRegex = new Regex(regexFileIdStart);
			var tempRegex2 = new Regex(regexFileIdEnd);

			var start = tempRegex.Match(stream);
			var end = tempRegex2.Match(stream);

			var urlForDownload = stream.Substring(start.Index + start.Length, end.Index - start.Index - start.Length);

			saveFileDialog.FileName = tempName;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (String.IsNullOrEmpty(saveFileDialog.FileName))
				{
					ShowError("Błędna nazwa pliku! Spróbuj jeszcze raz");
					urlTextBox.Enabled = true;
					return;
				}
			}
			else
			{
				urlTextBox.Enabled = true;
				return;
			}

			var webClient = new WebClient();
			webClient.DownloadFileCompleted += Completed;
			webClient.DownloadProgressChanged += ProgressChanged;
			webClient.DownloadFileAsync(new Uri(urlForDownload), saveFileDialog.FileName);
		}

		private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			progressBar.Value = e.ProgressPercentage;
		}

		private void Completed(object sender, AsyncCompletedEventArgs e)
		{
			MessageBox.Show("Zakończono ściąganie!", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
			urlTextBox.Enabled = true;
		}

		private bool PrimaryCheckings()
		{
			if (urlTextBox.Text == _urlTextBoxTemp)
			{
				ShowError("Nie wpisano poprawnego URL'a");
				return false;
			}
			else
			{
				try
				{
					var tempRegex = new Regex("wrzuta.pl");
					if (!tempRegex.Match(urlTextBox.Text).Success)
					{
						ShowError("Nie wpisano poprawnego URL'a");
						return false;
					}
				}
				catch (Exception)
				{
					ShowError("Nie wpisano poprawnego URL'a");
					return false;
				}
			}
			return true;
		}

		private static void ShowError(string value)
		{
			MessageBox.Show(value, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
