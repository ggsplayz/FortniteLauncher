﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using ggsLauncher;
//...

namespace ggsLauncher
{
    public class Program
    {
        public static string ruta;
        public static string mail;
        public static string pass;
        public static string downloadpath = Path.GetTempPath();


        public static void Main()
        {
            ActualProgram();
        }

        public static void ActualProgram()
        {
            Console.WriteLine($"\r\n                 _                           _               \r\n  __ _  __ _ ___| |    __ _ _   _ _ __   ___| |__   ___ _ __ \r\n / _` |/ _` / __| |   / _` | | | | '_ \\ / __| '_ \\ / _ \\ '__|\r\n| (_| | (_| \\__ \\ |__| (_| | |_| | | | | (__| | | |  __/ |   \r\n \\__, |\\__, |___/_____\\__,_|\\__,_|_| |_|\\___|_| |_|\\___|_|   \r\n |___/ |___/      ");
            Console.WriteLine(" ");    
            Console.WriteLine(" ");
            Console.WriteLine("Enter your Fortnite Path (Folder with Engine and FortniteGame)");
            Console.WriteLine(" ");
            ruta = Console.ReadLine();
            Console.WriteLine(" ");
            Console.WriteLine("Enter the name/email that you use");
            Console.WriteLine(" ");
            mail = Console.ReadLine();
            Console.WriteLine(" ");
            Console.WriteLine("Enter the password, put a random one if not needed");
            Console.WriteLine(" ");
            pass = Console.ReadLine();
            Console.WriteLine(" ");
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
            3551 dll: https://cdn.discordapp.com/attachments/1187061667759136858/1187109875109613598/3551.dll
            */
            Console.Clear();
            Console.WriteLine("[LOG] USING 3551.dll");
            string FortniteEXE = Path.Combine(ruta, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe");
            string EasyACEXE = Path.Combine(ruta, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_EAC.exe");
            string FortniteBEEXE = Path.Combine(ruta, "FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe");

            WebClient n1 = new WebClient();
            try
            {
                n1.DownloadFile("https://cdn.discordapp.com/attachments/1187061667759136858/1187109875109613598/3551.dll", Path.Combine(ruta, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"));
                Console.WriteLine("[LOG] PATCHED FILES");
            }
            catch
            {
                Console.WriteLine("[LOG] FAILED DOWNLOADING & PATCHING FILES, THE FILES MAYBE ARE DELETED");
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
            Fortnite.Start();
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
            8008 dll: https://cdn.discordapp.com/attachments/1187061667759136858/1187111378864046191/8008.dll
            */
            Console.Clear();
            Console.WriteLine("[LOG] USING 8008.dll");
            string FortniteEXE = Path.Combine(ruta, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe");
            string EasyACEXE = Path.Combine(ruta, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_EAC.exe");
            string FortniteBEEXE = Path.Combine(ruta, "FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe");

            WebClient n1 = new WebClient();
            try
            {
                n1.DownloadFile("https://cdn.discordapp.com/attachments/1187061667759136858/1187111378864046191/8008.dll", Path.Combine(ruta, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"));
                Console.WriteLine("[LOG] PATCHED FILES");
            }
            catch
            {
                Console.WriteLine("[LOG] FAILED DOWNLOADING & PATCHING FILES, THE FILES MAYBE ARE DELETED");
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
            Fortnite.Start();
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
            7777 dll: https://cdn.discordapp.com/attachments/1187061667759136858/1187111301126815854/7777.dll
            */
            Console.Clear();
            Console.WriteLine("[LOG] USING 7777.dll");
            string FortniteEXE = Path.Combine(ruta, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe");
            string EasyACEXE = Path.Combine(ruta, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_EAC.exe");
            string FortniteBEEXE = Path.Combine(ruta, "FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe");

            WebClient n1 = new WebClient();
            try
            {
                n1.DownloadFile("https://cdn.discordapp.com/attachments/1187061667759136858/1187111301126815854/7777.dll", Path.Combine(ruta, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "GFSDK_Aftermath_Lib.x64.dll"));
                Console.WriteLine("[LOG] PATCHED FILES");
            }
            catch
            {
                Console.WriteLine("[LOG] FAILED DOWNLOADING & PATCHING FILES, THE FILES MAYBE ARE DELETED");
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
            Fortnite.Start();
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