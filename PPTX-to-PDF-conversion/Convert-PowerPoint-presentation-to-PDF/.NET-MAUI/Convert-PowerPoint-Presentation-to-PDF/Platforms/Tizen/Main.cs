using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System;

namespace Convert_PowerPoint_Presentation_to_PDF
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
