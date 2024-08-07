using Newtonsoft.Json; // Remember to check if all the packages are installed!
using System.Collections;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Net;

namespace ggsLauncher
{
    public class Program
    {
        public static string path;
        public static string mail;
        public static string pass;
        public static string RedirFilePath;
        public static string RedirFilePath32; // Older Builds
        public static string CustomLink;
        public static string FortniteBuild; // Specify fortnite client build (ex; 1.7.2, 4.1, 11.31 etc)
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
                Console.WriteLine("Do you want to load existing configurations? Y/N");
                Console.WriteLine(" ");
                Console.Write("Selection: ");
                string IsLoadingConfig = Console.ReadLine();

                if (IsLoadingConfig == "Yes" || IsLoadingConfig == "Y" || IsLoadingConfig == "y" || IsLoadingConfig == "yes")
                {
                    Utils.LoadConfig();
                    PortSelection();
                } 
                else if (IsLoadingConfig == "No" || IsLoadingConfig == "N" || IsLoadingConfig == "n" || IsLoadingConfig == "no")
                {
                    Console.Clear();
                    ActualProgram();
                }
            }
            else
            {
                ActualProgram();
            }
            
        }

        public static void ActualProgram()
        {
            Utils.Log("The launcher has been run without any previous configuration");
            Console.WriteLine(@"
______         _         _ _       _                            _               
|  ___|       | |       (_) |     | |                          | |              
| |_ ___  _ __| |_ _ __  _| |_ ___| |     __ _ _   _ _ __   ___| |__   ___ _ __ 
|  _/ _ \| '__| __| '_ \| | __/ _ \ |    / _` | | | | '_ \ / __| '_ \ / _ \ '__|
| || (_) | |  | |_| | | | | ||  __/ |___| (_| | |_| | | | | (__| | | |  __/ |   
\_| \___/|_|   \__|_| |_|_|\__\___\_____/\__,_|\__,_|_| |_|\___|_| |_|\___|_|"); // Little ugly in the code
            Console.WriteLine(" ");    
            Console.WriteLine("Please, select an option and then press ENTER");
            Console.WriteLine(" ");
            Console.WriteLine(" * [1] Private");
            Console.WriteLine(" * [2] Hybrid");
            Console.WriteLine(" ");
            Console.WriteLine(" * [9] Settings");
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
            else if (HybridOrNot == "9")
            {
                Settings.SettingsScreen();
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
            Utils.Log($"Local variable \"path\" was changed to {path}");

            Console.WriteLine(" ");
            Console.WriteLine("Enter the name/email that you use");
            Console.WriteLine(" ");
            mail = Console.ReadLine();
            Utils.Log($"Local variable \"mail\" was changed to {mail}");

            Console.WriteLine(" ");
            Console.WriteLine("Enter the password, put a random one if not needed");
            Console.WriteLine(" ");
            pass = Console.ReadLine();
            Utils.Log($"Local variable \"pass\" was changed to {pass}");

            Console.WriteLine(" ");
            Console.WriteLine("Enter your Fortnite Build (ex; 1.7.2, 4.1, 11.31...)"); // Will have automatic detection in future
            Console.WriteLine(" ");
            FortniteBuild = Console.ReadLine();
            Utils.Log($"Local variable \"FortniteBuild\" was changed to {FortniteBuild}");

            Console.WriteLine(" ");
            try
            {
                Utils.SaveConfig();
                Console.WriteLine($"Saved Configuration to \"\\config.json\"");
                Utils.Log("Configuration saved successfully");
                Thread.Sleep(1000);
            }
            catch 
            {
                Console.WriteLine("Configuration not saved! Continuing... (Check if launcher was run as administrator)");
                Utils.Log("Configuration was not saved successfully (Try running as administrator)");
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
            Console.WriteLine("4. 5555 ");
            Console.WriteLine("5. 1111 ");
            Console.WriteLine("6. 8888 ");
            Console.WriteLine(" ");
            Console.WriteLine("99. Custom...");
            Console.WriteLine(" ");
            Console.WriteLine("0. Main Screen ");
            Console.WriteLine(" ");
            Console.Write("Selection: ");
            int port;
            if (int.TryParse(Console.ReadLine(), out port))
            {
                switch (port)
                {
                    case 0:
                        ActualProgram();
                        break;
                    case 1:
                        Launch(3551);
                        break;
                    case 2:
                        Launch(8008);
                        break;
                    case 3:
                        Launch(7777);
                        break;
                    case 4:
                        Launch(5555);
                        break;
                    case 5:
                        Launch(1111);
                        break;
                    case 6:
                        Launch(8888);
                        break;
                    case 99:
                        Utils.CustomLaunch();
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

        // Void for launching Fortnite and suspending EAC 

        public static void Launch(int PORT)
        {
            //If download link stops working, means that I deleted the dll files. You can compile other https://github.com/Milxnor/Cobalt

            if (PORT == 9999)
            {
                Console.Clear();
                Console.WriteLine($"[LOG] USING CUSTOM DLL");
                Utils.Log($"Launch process started using a custom dll ({CustomLink})");

                string FortniteEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe");
                string EasyACEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_EAC.exe");
                string FortniteBEEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe");

                WebClient n1 = new WebClient();
                try
                {
                    n1.DownloadFile($"{CustomLink}", Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"));
                    n1.DownloadFile($"{CustomLink}", Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.dll"));
                    Console.WriteLine("[LOG] PATCHED FILES"); Utils.Log("Successfully patched game files");
                    RedirFilePath = Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll");
                    RedirFilePath32 = Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.dll");
                }
                catch
                {
                    Console.WriteLine("[LOG] FAILED DOWNLOADING & PATCHING FILES, CHECK IF THE LINK IS PUBLIC?"); Utils.Log("Failed patching game files, check if the link is public");
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
                Fortnite.Start(); Utils.Log("Fortnite was launched successfully");
                Console.WriteLine("[LOG] LAUNCHED FORTNITE, THIS MAY TAKE SOME MINUTES");
                Fortnite.WaitForExit();
                File.Delete(RedirFilePath);

                Console.WriteLine("Press any key to go back to the main screen...");
                Console.ReadKey();
                Console.Clear();
                ActualProgram();
            } 
            else
            {
                Console.Clear();
                Console.WriteLine($"[LOG] USING {PORT.ToString()}.dll");
                Utils.Log($"Launch process started using {PORT.ToString()}.dll");
                string FortniteEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe");
                string EasyACEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_EAC.exe");
                string FortniteBEEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe");

                WebClient n1 = new WebClient();
                try
                {
                    n1.DownloadFile($"https://github.com/ggsplayz/FortniteLauncher/raw/main/DLLs/{PORT.ToString()}.dll", Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"));
                    n1.DownloadFile($"https://github.com/ggsplayz/FortniteLauncher/raw/main/DLLs/{PORT.ToString()}.dll", Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.dll")); // Older Builds
                    Console.WriteLine("[LOG] PATCHED FILES"); Utils.Log("Successfully patched game files");
                    RedirFilePath = Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll");
                    RedirFilePath32 = Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.dll");

                }
                catch
                {
                    Console.WriteLine("[LOG] FAILED DOWNLOADING & PATCHING FILES, THE FILES MAYBE ARE DELETED"); Utils.Log("Failed patching game files, maybe the files were removed from server?");
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

                //FortniteClient-Win64-Shipping_EAC.exe
                Process EasyAntiCheet = new Process();
                EasyAntiCheet.StartInfo.FileName = EasyACEXE;


                if (FortniteBuild == "1.7.2" || FortniteBuild == "0.6.5") // Case sensitive, for example, 1.7.2 usually comes without FortniteLauncher exe
                {
                    //Start Fortnite
                    Fortnite.Start(); Utils.Log("Fortnite was launched successfully");

                    Console.WriteLine("[LOG] LAUNCHED FORTNITE, THIS MAY TAKE SOME MINUTES");
                    Fortnite.WaitForExit();
                    File.Delete(RedirFilePath);
                    File.Delete(RedirFilePath32);

                    Console.WriteLine("Press any key to go back to the main screen...");
                    Console.ReadKey();
                    Console.Clear();
                    ActualProgram();
                } 
                else
                {
                    Bee.Start();
                    foreach (ProcessThread thread in Bee.Threads)
                        Win32.SuspendThread(Win32.OpenThread(2, false, thread.Id));
                    EasyAntiCheet.Start();
                    foreach (ProcessThread thread in EasyAntiCheet.Threads)
                        Win32.SuspendThread(Win32.OpenThread(2, false, thread.Id));
                    //Start Fortnite
                    Fortnite.Start(); Utils.Log("Fortnite was launched successfully");

                    Console.WriteLine("[LOG] LAUNCHED FORTNITE, THIS MAY TAKE SOME MINUTES");
                    Fortnite.WaitForExit();
                    File.Delete(RedirFilePath);
                    File.Delete(RedirFilePath32);

                    Console.WriteLine("Press any key to go back to the main screen...");
                    Console.ReadKey();
                    Console.Clear();
                    ActualProgram();
                }
            }  
        }

        // S13 Hybrid

        public static string token;
        public static string exchange;
        public static string username;

        public static void Hybrid()
        {
            Console.Clear();
            
            
            Utils.Log("Using S13 Hybrid");
            

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
            Console.WriteLine($"Logged in as: {username}"); Utils.Log($"Logged to an Epic Games Account, id {username}");
            Console.WriteLine(" ");
            Console.WriteLine("Enter your Fortnite Path (Folder with Engine and FortniteGame)");
            Console.WriteLine(" ");
            path = Console.ReadLine();
            Utils.Log($"Local variable ´path´ was changed to {path}");

            exchange = HybridUtils.GetExchange(token);

            string FortniteEXE = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe");
            string EAC = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_EAC.exe");
            string FNLauncher = Path.Combine(path, "FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe");

            try
            {
                WebClient n1 = new WebClient();

                // Hybrid (Redir. to port 3551, hybrid backend recommended)
                n1.DownloadFile("https://github.com/ggsplayz/FortniteLauncher/raw/main/DLLs/S13.dll", Path.Combine(path, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"));
                Utils.Log("Patched game files for S13 Hybrid");
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
                Console.WriteLine("[LOG] Suspended FortniteLauncher process"); Utils.Log("Suspended FortniteLauncher process");
                Process EACP = new Process();
                EACP.StartInfo.FileName = EAC;
                EACP.StartInfo.Arguments = "-epiclocale=en -fromfl=be -fltoken=1d2ae436h94ad05b56f91fhc -frombe";
                EACP.Start();
                foreach (ProcessThread thread in (ReadOnlyCollectionBase)EACP.Threads)
                    Win32.SuspendThread(Win32.OpenThread(2, false, thread.Id));
                Console.WriteLine("[LOG] Suspended EasyAntiCheat process"); Utils.Log("Suspended EasyAntiCheat process");
                Fortnite.Start();
                Console.WriteLine("[LOG] LAUNCHED FORTNITE, THIS MAY TAKE SOME MINUTES"); Utils.Log("Started Hybrid Fortnite");
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
