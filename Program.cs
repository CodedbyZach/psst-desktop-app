using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var form = new Form
        {
            Text = "PSST Desktop",
            Width = 420,
            Height = 720,
            FormBorderStyle = FormBorderStyle.FixedSingle,
            MaximizeBox = false,
            StartPosition = FormStartPosition.CenterScreen
        };

        var web = new WebView2 { Dock = DockStyle.Fill };
        form.Controls.Add(web);

        form.Load += async (_, __) =>
        {
            try
            {
                await web.EnsureCoreWebView2Async();
                web.Source = new Uri("https://codedbyzach.com/psst_login");
            }
            catch
            {
                MessageBox.Show("WebView2 Runtime is required. Opening installer...");
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://go.microsoft.com/fwlink/p/?LinkId=2124703",
                    UseShellExecute = true
                });
                Application.Exit();
            }
        };

        Application.Run(form);
    }
}