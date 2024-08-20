using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApriFolderAPI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Task.Run(() => StartHttpListener());
            Application.Run(new MainForm());
        }

        //static void StartHttpListener()
        //{
        //    HttpListener listener = new HttpListener();
        //    listener.Prefixes.Add("http://localhost:5001/");
        //    listener.Start();
        //    Console.WriteLine("Listening...");

        //    while (true)
        //    {
        //        var context = listener.GetContext();
        //        var request = context.Request;
        //        var response = context.Response;

        //        if (request.HttpMethod == "GET" && request.Url.AbsolutePath == "/open-folder")
        //        {
        //            OpenFolder(@"G:\Il mio Drive");
        //            response.StatusCode = (int)HttpStatusCode.OK;
        //        }
        //        else
        //        {
        //            response.StatusCode = (int)HttpStatusCode.NotFound;
        //        }

        //        response.Close();
        //    }
        //}

        public static void StartHttpListener()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:5001/");
            listener.Start();
            Console.WriteLine("Listening...");

            while (true)
            {
                var context = listener.GetContext();
                var request = context.Request;
                var response = context.Response;

                if (request.HttpMethod == "GET" && request.Url.AbsolutePath == "/open-folder")
                {
                    var folderPath = request.QueryString["path"];
                    if (!string.IsNullOrEmpty(folderPath))
                    {
                        OpenFolder(folderPath);
                        response.StatusCode = (int)HttpStatusCode.OK;
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes("Path parameter is missing"));
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                }

                response.Close();
            }
        }

        static void OpenFolder(string path)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true,
                Verb = "open"
            });
        }
    }
}