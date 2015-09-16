using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PharmacyCondition.Properties;

namespace PharmacyCondition
{
	public static class MainFormExtensions
	{
		private static List<TextBox> _textBoxs;

		public static void GetSettings(this MainForm form)
		{
			ControlsOnMainForm(form);

			#region TextBoxs
			foreach (var t in _textBoxs)
			{
				switch (t.Name)
				{
					case "textBoxSettingsServerIpAddress":
						t.Text = Settings.Default.ServerIp;
						break;

					case "textBoxSettingsServerPortNumber":
						t.Text = Settings.Default.ServerPort.ToString();
						break;

					case "textBoxSettingsReaderPrefix":
						t.Text = Settings.Default.Prefix.ToString();
						break;
				}
			}
			#endregion
		}

		public static void SetSettings(this MainForm form)
		{
			ControlsOnMainForm(form);

			#region TextBoxs
			foreach (var t in _textBoxs)
			{
				switch (t.Name)
				{
					case "textBoxSettingsServerIpAddress":
						Settings.Default.ServerIp = t.Text;
						break;

					case "textBoxSettingsServerPortNumber":
						Settings.Default.ServerPort = Convert.ToInt32(t.Text);
						break;

					case "textBoxSettingsReaderPrefix":
						Settings.Default.Prefix = Convert.ToChar(t.Text);
						break;
				}
			}
			#endregion
		}

		private static void ControlsOnMainForm(Control form)
		{
			//tabControl -> tabPages (1 - Ustawienia) -> group boxes
			var tabPageSettingsControls = form.Controls[0].Controls[1].Controls;
			_textBoxs = new List<TextBox>();

			foreach (var control in tabPageSettingsControls)
			{
				if (control.GetType() == typeof(GroupBox))
				{
					foreach (var c in ((GroupBox)control).Controls)
					{
						if (c.GetType() == typeof(TextBox))
						{
							_textBoxs.Add((TextBox)c);
						}
					}
				}
			}
		}
	}
}