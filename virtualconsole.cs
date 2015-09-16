using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;   // needed to call external application


namespace WindowsApplication1
{
    partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ViewConsole_CheckedChanged(object sender, EventArgs e)
        {
            if (ViewConsole.Checked)
                Win32.AllocConsole();
            else
                Win32.FreeConsole();
        }
    }

    public class Win32
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();
    }
}