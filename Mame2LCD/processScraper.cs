using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mame2LCD
{
    class processScraper
    {
        List<string> procs = new List<string>();
        string watchFile = @"d:\mame\etc\data.dat";
        public string currentTitle = "";
        public bool running = true;

        public processScraper()
        {            
            while (running)
            {
                bool noMame = true;
                procs.Clear();
                System.Threading.Thread.Sleep(1000);
                foreach (Process p in Process.GetProcesses())
                {
                    if (p.MainWindowTitle != "Mame2LCD")
                    {                                                
                        procs.Add(p.ProcessName);
                        string name = p.ProcessName.ToLower();
                        string title = p.MainWindowTitle.Replace("MAME:", "")
                                    .Split('(')[0]
                                    .Split('[')[0]
                                    .Trim() + " ";;
                        
                        if (name.Contains("mame"))
                        {
                            if (title.Length > 3 && !title.Contains("No Driver Loaded"))
                            {                                
                                noMame = false;

                                try
                                {
                                    Console.WriteLine(name + " | " + title);
                                    File.WriteAllText(watchFile, title);
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                }

                if (noMame)
                {
                    if (File.ReadAllText(watchFile) != "")
                    {
                        File.WriteAllText(watchFile, "");
                    }
                }
            }
        }
    }
}
