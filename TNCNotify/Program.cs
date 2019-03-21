using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kinvey;

namespace TNCNotify
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
       {
            String appKey = "kid_S1_gbMQ0";
            String appSecret = "21893ef7a99d446a89f9f75e6c4da15d";
            String filePath = "";

            Client.Builder builder = new Client.Builder(appKey, appSecret)
            .setLogger(delegate (string msg) { Console.WriteLine(msg); });
            //.setFilePath(filePath);
            //.setOfflinePlatform(new SQLite.Net.Platform.Win32.SQLitePlatformWin32());

            Client kinveyClient = builder.Build();
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());

        } 

    }

}
