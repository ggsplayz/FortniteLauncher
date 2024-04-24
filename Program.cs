using Newtonsoft.Json; // REMEMBER TO CHECK IF PACKAGE IS INSTALLED!
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace ggsLauncher
{
    public class Program
    {
        public static string path;
        public static string mail;
        public static string pass;
        public static string RedirFilePath;
        public static string downloadpath = Path.GetTempPath();
        public static string configFile = "config.json";
        public static string logFile = "LOG.txt";
        public static bool InjectConsole = false; //Adding in future release

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
            Console.WriteLine("Please, select an option and then press ENTER");
            Console.WriteLine(" ");
            Console.WriteLine(" * [1] Private");
            Console.WriteLine(" * [2] Hybrid");
            Console.WriteLine(" ");
            Console.Write(" Selection: ");
            string HybridOrNot = Console.ReadLine();
            if (HybridOrNot == "1")
            {

            } 
            else if (HybridOrNot == "2")
            {
                Hybrid();
            } 
            else
            {
                Environment.Exit(0);
            }
            Console.Clear();
            Console.WriteLine(@"
______         _         _ _       _                            _               
|  ___|       | |       (_) |     | |                          | |              
| |_ ___  _ __| |_ _ __  _| |_ ___| |     __ _ _   _ _ __   ___| |__   ___ _ __ 
|  _/ _ \| '__| __| '_ \| | __/ _ \ |    / _` | | | | '_ \ / __| '_ \ / _ \ '__|
| || (_) | |  | |_| | | | | ||  __/ |___| (_| | |_| | | | | (__| | | |  __/ |   
\_| \___/|_|   \__|_| |_|_|\__\___\_____/\__,_|\__,_|_| |_|\___|_| |_|\___|_|");
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
                RedirFilePath = Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll");
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
            File.Delete(RedirFilePath);

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
                RedirFilePath = Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll");
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
            File.Delete(RedirFilePath);

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
                RedirFilePath = Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll");
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
            File.Delete(RedirFilePath);

            Console.WriteLine("Press any key to go back to the main screen...");
            Console.ReadKey();
            Console.Clear();
            ActualProgram();
        }

        // S13 Hybrid

        public static string token;
        public static string exchange;
        public static string username;

        public static void Hybrid()
        {
            Console.Clear();
            
            
            Log("Using S13 Hybrid");
            

            Console.WriteLine(@"
______         _         _ _       _                            _               
|  ___|       | |       (_) |     | |                          | |              
| |_ ___  _ __| |_ _ __  _| |_ ___| |     __ _ _   _ _ __   ___| |__   ___ _ __ 
|  _/ _ \| '__| __| '_ \| | __/ _ \ |    / _` | | | | '_ \ / __| '_ \ / _ \ '__|
| || (_) | |  | |_| | | | | ||  __/ |___| (_| | |_| | | | | (__| | | |  __/ |   
\_| \___/|_|   \__|_| |_|_|\__\___\_____/\__,_|\__,_|_| |_|\___|_| |_|\___|_|    (S13 Hybrid)");
            Console.WriteLine(" ");
            Console.WriteLine("Please remember that using Hybrid Season 13 may result on a account ban, using an alt account is recommended!");
            Console.WriteLine("You will be removed from the match after 5 minutes, because the AntiCheat process will be suspended.");
            Console.WriteLine(" ");
            Console.WriteLine("Please, make sure you are using the correct Fortnite Build (13.40-CL-14113327-Windows)");
            Console.WriteLine(" ");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            LaunchS13();
        }

        public static void LaunchS13()
        {
            Console.Clear();
            Console.WriteLine(" ");
            Console.Write(" When you press any key, you will be redirected to the Epic Games login page...");
            Console.ReadKey();

            string devicecode = HybridUtils.GetDevicecode(HybridUtils.GetDevicecodetoken());
            string[] strArray = devicecode.Split(new char[1]
                {
                ','
                }, 2);
            if (devicecode.Contains("error"))
                return;
            token = strArray[0];
            username = strArray[1];

            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine($"Logged in as: {username}"); Log($"Logged to an Epic Games Account, id {username}");
            Console.WriteLine(" ");
            Console.WriteLine("Enter your Fortnite Path (Folder with Engine and FortniteGame)");
            Console.WriteLine(" ");
            path = Console.ReadLine();
            Log($"Local variable ´path´ was changed to {path}");

            exchange = HybridUtils.GetExchange(token);

            string FortniteEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe");
            string EAC = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_EAC.exe");
            string FNLauncher = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe");

            try
            {
                WebClient n1 = new WebClient();

                // Hybrid (Redir. to port 3551, hybrid backend recommended)
                n1.DownloadFile("https://github.com/ggsplayz/FortniteLauncher/raw/main/DLLs/S13.dll", Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"));
                Log("Patched game files for S13 Hybrid");
                RedirFilePath = Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll");

                string arguments1 = "-AUTH_LOGIN=unused -AUTH_PASSWORD=" + exchange + " -AUTH_TYPE=exchangecode -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -noeac -fromfl=be -fltoken=1d2ae436h94ad05b56f91fhc - skippatchcheck";
                Process Fortnite = new Process
                {
                    StartInfo = new ProcessStartInfo(FortniteEXE, arguments1)
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = false,
                        CreateNoWindow = true
                    }
                };
                Process FNLP = new Process();
                FNLP.StartInfo.FileName = FNLauncher;
                FNLP.Start();
                foreach (ProcessThread thread in FNLP.Threads)
                    Win32.SuspendThread(Win32.OpenThread(2, false, thread.Id));
                Console.WriteLine("[LOG] Suspended FortniteLauncher process"); Log("Suspended FortniteLauncher process");
                Process EACP = new Process();
                EACP.StartInfo.FileName = EAC;
                EACP.StartInfo.Arguments = "-epiclocale=en -fromfl=be -fltoken=1d2ae436h94ad05b56f91fhc -frombe";
                EACP.Start();
                foreach (ProcessThread thread in (ReadOnlyCollectionBase)EACP.Threads)
                    Win32.SuspendThread(Win32.OpenThread(2, false, thread.Id));
                Console.WriteLine("[LOG] Suspended EasyAntiCheat process"); Log("Suspended EasyAntiCheat process");
                Fortnite.Start();
                Console.WriteLine("[LOG] LAUNCHED FORTNITE, THIS MAY TAKE SOME MINUTES"); Log("Started Hybrid Fortnite");
                Fortnite.WaitForExit();
                try
                {
                    FNLP.Close();
                    EACP.Close();
                    Fortnite.Close();
                    File.Delete(RedirFilePath);
                }
                catch
                {

                }
            } 
            catch
            {

            }
            Console.Clear();
            ActualProgram();
        }
    }
}

// If you see this, leave a star and have a great day!
