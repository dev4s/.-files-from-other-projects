using System;
using Microsoft.Win32;

namespace KISKeyReset
{
	static class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Program resetuje klucz dla Kaspersky'ego. Naciśnij jakikolwiek przycisk by kontynuować.");
			Console.ReadKey();

			var localMachineKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
			// ReSharper disable PossibleNullReferenceException
			var kisEnvironment = localMachineKey.OpenSubKey("SOFTWARE").OpenSubKey("Wow6432Node")
								.OpenSubKey("KasperskyLab").OpenSubKey("protected")
								.OpenSubKey("PURE13").OpenSubKey("environment", true);

			

			var pcidValue = kisEnvironment.GetValue("PCID");
			// ReSharper restore PossibleNullReferenceException
			var subValues = pcidValue.ToString().ToCharArray();

			bool lastF;
			var key1 = subValues[subValues.Length - 2].CheckFValue(out lastF);
			var key2 = lastF ? subValues[subValues.Length - 3].CheckFValue(out lastF) : subValues[subValues.Length - 3];
			var key3 = lastF ? subValues[subValues.Length - 4].CheckFValue(out lastF) : subValues[subValues.Length - 4];

			var newPcidValue = pcidValue.ToString().Remove(pcidValue.ToString().Length - 4) + key3 + key2 + key1 + '}';

			kisEnvironment.SetValue("PCID", newPcidValue);
		}

		private static char CheckFValue(this char key, out bool lastF)
		{
			if (key == 'F')
			{
				key = '0';
				lastF = true;
			}
			else
			{
				if (key == '9')
				{
					key = 'A';
				}
				else
				{
					key = (char)(key + 1);
				}

				lastF = false;
			}

			return key;
		}
	}
}
