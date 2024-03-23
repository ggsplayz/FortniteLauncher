using Newtonsoft.Json; // REMEMBER TO CHECK IF PACKAGE IS INSTALLED!
using System.Diagnostics;
using System.Net;

namespace ggsLauncher
{
    public class Program
    {
        public static string path;
        public static string mail;
        public static string pass;
        public static string downloadpath = Path.GetTempPath();
        public static string configFile = "config.json";
        public static string logFile = "LOG.txt";

        public static void Main()
        {
            Console.Title = "FortniteLauncher - Made by ggsplayz";
            
            if (File.Exists(logFile))
            {
                File.Delete(logFile);

            } else if (!File.Exists(logFile))
            {
                using (StreamWriter sw = File.CreateText(logFile)) { }
            }

            if (File.Exists(configFile))
            {
                LoadConfig();
                PortSelection();
            }
            else
            {
                ActualProgram();
            }
            
        }

        static void Log(string message)
        {
            try
            {
                string finalLog = $"[{DateTime.Now}] - {message}";
                using (StreamWriter sw = File.AppendText(logFile))
                {
                    sw.WriteLine(finalLog);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        // V1.5
        public static void LoadConfig()
        {
            if (File.Exists(configFile))
            {
                string json = File.ReadAllText(configFile);
                dynamic config = JsonConvert.DeserializeObject(json);
                path = config.path;
                mail = config.mail;
                pass = config.pass;
                Console.WriteLine("Loading Saved Configuration...");
                Thread.Sleep(1000); 
            }
            else
            {
                File.Create(configFile);
                path = "";
                mail = "";
                pass = "";
            }
        }

        public static void SaveConfig()
        {
            dynamic config = new
            {
                path,
                mail,
                pass
            };
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(configFile, json);
        }

        public static void ActualProgram()
        {
            Log("The launcher was run without any previous configuration");
            Console.WriteLine(@"
______         _         _ _       _                            _               
|  ___|       | |       (_) |     | |                          | |              
| |_ ___  _ __| |_ _ __  _| |_ ___| |     __ _ _   _ _ __   ___| |__   ___ _ __ 
|  _/ _ \| '__| __| '_ \| | __/ _ \ |    / _` | | | | '_ \ / __| '_ \ / _ \ '__|
| || (_) | |  | |_| | | | | ||  __/ |___| (_| | |_| | | | | (__| | | |  __/ |   
\_| \___/|_|   \__|_| |_|_|\__\___\_____/\__,_|\__,_|_| |_|\___|_| |_|\___|_|");
            Console.WriteLine(" ");    
            Console.WriteLine(" ");
            Console.WriteLine("Enter your Fortnite Path (Folder with Engine and FortniteGame)");
            Console.WriteLine(" ");
            path = Console.ReadLine(); 
            Log($"Local variable ´path´ was changed to {path}");
            Console.WriteLine(" ");
            Console.WriteLine("Enter the name/email that you use");
            Console.WriteLine(" ");
            mail = Console.ReadLine();
            Log($"Local variable ´mail´ was changed to {mail}");
            Console.WriteLine(" ");
            Console.WriteLine("Enter the password, put a random one if not needed");
            Console.WriteLine(" ");
            pass = Console.ReadLine();
            Log($"Local variable ´pass´ was changed to {pass}");
            Console.WriteLine(" ");
            try
            {
                SaveConfig();
                Console.WriteLine($"Saved Configuration to \\config.json");
                Log("Configuration saved successfully");
                Thread.Sleep(1000);
            }
            catch 
            {
                Console.WriteLine("Configuration not saved! Continuing... (Check if launcher was run as administrator)");
                Log("Configuration was not saved successfully (Try running as administrator)");
                Thread.Sleep(1000);
            }
            PortSelection();
        }

        public static void PortSelection()
        {
            Console.Clear();
            Console.WriteLine("Select the PORT to redirect to. (1,2,3...)");
            Console.WriteLine(" ");
            Console.WriteLine("1. 3551 (Typically used by LawinServer)");
            Console.WriteLine("2. 8008");
            Console.WriteLine("3. 7777 ");
            Console.WriteLine(" ");
            int port;
            if (int.TryParse(Console.ReadLine(), out port))
            {
                switch (port)
                {
                    case 1:
                        Launch3551();
                        break;
                    case 2:
                        Launch8008();
                        break;
                    case 3:
                        Launch7777();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Pick a port and press enter 1,2 or 3");
                Console.ReadKey();
                Console.Clear();
                ActualProgram();
            }
        }

        // Void for every Port, add more if needed

        public static void Launch3551()
        {
            //If download link stops working, means that I deleted the server where the dll was allocated. You can compile other https://github.com/Milxnor/Cobalt
            /*
            3551 dll: https://github.com/ggsplayz/FortniteLauncher/raw/main/DLLs/3551.dll
            */
            Console.Clear();
            Console.WriteLine("[LOG] USING 3551.dll"); Log("Launch process started using 3551.dll");
            string FortniteEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe");
            string EasyACEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_EAC.exe");
            string FortniteBEEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe");

            WebClient n1 = new WebClient();
            try
            {
                n1.DownloadFile("https://github.com/ggsplayz/FortniteLauncher/raw/main/DLLs/3551.dll", Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"));
                Console.WriteLine("[LOG] PATCHED FILES"); Log("Successfully patched game files");
            }
            catch
            {
                Console.WriteLine("[LOG] FAILED DOWNLOADING & PATCHING FILES, THE FILES MAYBE ARE DELETED"); Log("Failed patching game files, maybe the files were removed from server?");
                Thread.Sleep(1000);
                ActualProgram();
            }
            

            //FortniteClient-Win64-Shipping.exe
            string PrimerosArgs = $"-epicapp=Fortnite -epicenv=Prod -epicportal -noeac -fromfl=be -fltoken=h1cdhchd10150221h130eB56 -skippatchcheck -AUTH_TYPE=epic -AUTH_LOGIN={mail} -AUTH_PASSWORD={pass}";
            Process Fortnite = new Process
            {
                StartInfo = new ProcessStartInfo(FortniteEXE, PrimerosArgs)
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = false
                }
            };

            //FortniteLauncher.exe / BattleEye
            Process Bee = new Process();
            Bee.StartInfo.FileName = FortniteBEEXE;
            Bee.Start();
            foreach (ProcessThread thread in Bee.Threads)
                Win32.SuspendThread(Win32.OpenThread(2, false, thread.Id));

            //FortniteClient-Win64-Shipping_EAC.exe
            Process EasyAntiCheet = new Process();
            EasyAntiCheet.StartInfo.FileName = EasyACEXE;
            EasyAntiCheet.Start();
            foreach (ProcessThread thread in EasyAntiCheet.Threads)
                Win32.SuspendThread(Win32.OpenThread(2, false, thread.Id));

            //Start Fortnite
            Fortnite.Start(); Log("Fortnite was launched successfully");
            Console.WriteLine("[LOG] LAUNCHED FORTNITE, THIS MAY TAKE SOME MINUTES");
            Fortnite.WaitForExit();
            Console.WriteLine("Press any key to go back to the main screen...");
            Console.ReadKey();
            Console.Clear();
            ActualProgram();

        }

        public static void Launch8008()
        {
            //If download link stops working, means that I deleted the server where the dll was allocated. You can compile other https://github.com/Milxnor/Cobalt
            /*
            8008 dll: https://github.com/ggsplayz/FortniteLauncher/raw/main/DLLs/8008.dll
            */
            Console.Clear();
            Console.WriteLine("[LOG] USING 8008.dll"); Log("Launch process started using 8008.dll");
            string FortniteEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe");
            string EasyACEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_EAC.exe");
            string FortniteBEEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe");

            WebClient n1 = new WebClient();
            try
            {
                n1.DownloadFile("https://github.com/ggsplayz/FortniteLauncher/raw/main/DLLs/8008.dll", Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"));
                Console.WriteLine("[LOG] PATCHED FILES"); Log("Successfully patched game files");
            }
            catch
            {
                Console.WriteLine("[LOG] FAILED DOWNLOADING & PATCHING FILES, THE FILES MAYBE ARE DELETED"); Log("Failed patching game files, maybe the files were removed from server?");
                Thread.Sleep(1000);
                ActualProgram();
            }


            //FortniteClient-Win64-Shipping.exe
            string PrimerosArgs = $"-epicapp=Fortnite -epicenv=Prod -epicportal -noeac -fromfl=be -fltoken=h1cdhchd10150221h130eB56 -skippatchcheck -AUTH_TYPE=epic -AUTH_LOGIN={mail} -AUTH_PASSWORD={pass}";
            Process Fortnite = new Process
            {
                StartInfo = new ProcessStartInfo(FortniteEXE, PrimerosArgs)
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = false
                }
            };

            //FortniteLauncher.exe / BattleEye
            Process Bee = new Process();
            Bee.StartInfo.FileName = FortniteBEEXE;
            Bee.Start();
            foreach (ProcessThread thread in Bee.Threads)
                Win32.SuspendThread(Win32.OpenThread(2, false, thread.Id));

            //FortniteClient-Win64-Shipping_EAC.exe
            Process EasyAntiCheet = new Process();
            EasyAntiCheet.StartInfo.FileName = EasyACEXE;
            EasyAntiCheet.Start();
            foreach (ProcessThread thread in EasyAntiCheet.Threads)
                Win32.SuspendThread(Win32.OpenThread(2, false, thread.Id));

            //Start Fortnite
            Fortnite.Start(); Log("Fortnite was launched successfully");
            Console.WriteLine("[LOG] LAUNCHED FORTNITE, THIS MAY TAKE SOME MINUTES");
            Fortnite.WaitForExit();
            Console.WriteLine("Press any key to go back to the main screen...");
            Console.ReadKey();
            Console.Clear();
            ActualProgram();
        }

        public static void Launch7777()
        {
            //If download link stops working, means that I deleted the server where the dll was allocated. You can compile other https://github.com/Milxnor/Cobalt
            /*
            7777 dll: https://github.com/ggsplayz/FortniteLauncher/raw/main/DLLs/7777.dll
            */
            Console.Clear();
            Console.WriteLine("[LOG] USING 7777.dll"); Log("Launch process started using 7777.dll");
            string FortniteEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe");
            string EasyACEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_EAC.exe");
            string FortniteBEEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe");

            WebClient n1 = new WebClient();
            try
            {
                n1.DownloadFile("https://github.com/ggsplayz/FortniteLauncher/raw/main/DLLs/7777.dll", Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"));
                Console.WriteLine("[LOG] PATCHED FILES"); Log("Successfully patched game files");
            }
            catch
            {
                Console.WriteLine("[LOG] FAILED DOWNLOADING & PATCHING FILES, THE FILES MAYBE ARE DELETED"); Log("Failed patching game files, maybe the files were removed from server?");
                Thread.Sleep(1000);
                ActualProgram();
            }


            //FortniteClient-Win64-Shipping.exe
            string PrimerosArgs = $"-epicapp=Fortnite -epicenv=Prod -epicportal -noeac -fromfl=be -fltoken=h1cdhchd10150221h130eB56 -skippatchcheck -AUTH_TYPE=epic -AUTH_LOGIN={mail} -AUTH_PASSWORD={pass}";
            Process Fortnite = new Process
            {
                StartInfo = new ProcessStartInfo(FortniteEXE, PrimerosArgs)
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = false
                }
            };

            //FortniteLauncher.exe / BattleEye
            Process Bee = new Process();
            Bee.StartInfo.FileName = FortniteBEEXE;
            Bee.Start();
            foreach (ProcessThread thread in Bee.Threads)
                Win32.SuspendThread(Win32.OpenThread(2, false, thread.Id));

            //FortniteClient-Win64-Shipping_EAC.exe
            Process EasyAntiCheet = new Process();
            EasyAntiCheet.StartInfo.FileName = EasyACEXE;
            EasyAntiCheet.Start();
            foreach (ProcessThread thread in EasyAntiCheet.Threads)
                Win32.SuspendThread(Win32.OpenThread(2, false, thread.Id));

            //Start Fortnite
            Fortnite.Start(); Log("Fortnite was launched successfully");
            Console.WriteLine("[LOG] LAUNCHED FORTNITE, THIS MAY TAKE SOME MINUTES");
            Fortnite.WaitForExit();
            Console.WriteLine("Press any key to go back to the main screen...");
            Console.ReadKey();
            Console.Clear();
            ActualProgram();
        }
    }
}

// If you see this, leave a start and have a great day!
