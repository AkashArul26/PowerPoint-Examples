﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint_Presentation_to_Image
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Reads the template PowerPoint document.
            FileStream fs = new FileStream(@"../../Data/Input.pptx", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            fs.Position = 0;
            //Saves the PowerPoint document in memory stream.
            MemoryStream inputStream = new MemoryStream();
            fs.CopyTo(inputStream);
            inputStream.Position = 0;

            try
            {
                Console.WriteLine("Please enter your Azure Functions URL :");
                string functionURL = Console.ReadLine();
                //Create HttpWebRequest with hosted azure functions URL.                
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(functionURL);
                //Set request method as POST
                req.Method = "POST";
                //Get the request stream to save the PowerPoint document stream
                Stream stream = req.GetRequestStream();
                //Write the PowerPoint document stream into request stream
                stream.Write(inputStream.ToArray(), 0, inputStream.ToArray().Length);
                //Gets the responce from the Azure Functions.
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                //Saves the image stream.
                FileStream fileStream = File.Create("PPTXtoImage.Jpeg");
                res.GetResponseStream().CopyTo(fileStream);
                //Dispose the streams
                inputStream.Dispose();
                fileStream.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            //Launch the output image.
            System.Diagnostics.Process.Start("PPTXtoImage.Jpeg");
        }
    }
}
