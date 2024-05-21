using System;

namespace ggsLauncher
{
    public class Settings
    {
        // Settings to be changed by the user
        public static bool UseBackendWhenHybrid;
        public static bool UseBackendWhenPrivate;
        public static bool HeadlessWhenPrivate;
        public static bool InjectGameServer;

        // Booleans to apply stuff inside the code
        public static bool IsUsingHybridBackend;
        public static bool IsUsingPrivateBackend;
        public static bool IsUsingHeadlessGame;
        public static bool IsUsingGameServer;

        // UI
        private static string IsEnabled = "[x]";
        private static string IsNotEnabled = "[ ]";
        private static string[] settingsDescriptions = new string[]
        {
        " 1. Enable built-in backend for Private (Port 3551)",
        " 2. Enable built-in backend for Hybrid (Port 3551) ",
        " 3. Use Headless game launch                       ",
        " 4. Inject GameServer                              "
        };
        private static bool[] settingsStates = new bool[4];

        public static void SettingsScreen()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Settings - FortniteLauncher");

                Console.WriteLine(" ");
                for (int i = 0; i < settingsDescriptions.Length; i++)
                {
                    PrintSetting(i);
                }
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("PLEASE NOTE THAT NONE OF THESE SETTINGS ARE IMPLEMENTED YET TO THE CODE, THIS IS PURE UI TESTING");
                Console.WriteLine("Use numbers (1 - 4) to change settings, enter 69 to go back.");
                Console.WriteLine(" ");
                Console.Write("Setting: ");

                string input = Console.ReadLine();
                if (int.TryParse(input, out int n))
                {
                    if (n == 69)
                    {
                        exit = true;
                        Console.Clear();
                        ggsLauncher.Program.ActualProgram();
                    }
                    else if (n >= 1 && n <= 4)
                    {
                        ToggleSetting(n - 1);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 4, or 69 to go back.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    Console.ReadKey();
                }
            }
        }

        private static void PrintSetting(int index)
        {
            Console.SetCursorPosition(0, index + 2);
            Console.WriteLine($"{settingsDescriptions[index]} {(settingsStates[index] ? IsEnabled : IsNotEnabled)}");
        }

        public static void ToggleSetting(int index)
        {
            if (index >= 0 && index < settingsStates.Length)
            {
                settingsStates[index] = !settingsStates[index];
                if (index == 1)
                {
                    if (UseBackendWhenPrivate == false)
                    {
                        UseBackendWhenPrivate = true;
                        IsUsingPrivateBackend = true;
                        PrintSetting(index);

                    } else if (UseBackendWhenPrivate == true)
                    {
                        UseBackendWhenPrivate = false;
                        IsUsingPrivateBackend = false;
                        PrintSetting(index);
                    }
                    
                } else if (index == 2)
                {
                    if (UseBackendWhenHybrid == false)
                    {
                        UseBackendWhenHybrid = true;
                        IsUsingHybridBackend = true;
                        PrintSetting(index);

                    } else if (UseBackendWhenHybrid == true)
                    {
                        UseBackendWhenHybrid = false;
                        IsUsingHybridBackend = false;
                        PrintSetting(index);
                    }

                } else if (index == 3)
                {
                    if (HeadlessWhenPrivate == false)
                    {
                        HeadlessWhenPrivate = true;
                        IsUsingHeadlessGame = true;
                        PrintSetting(index);

                    } else if (HeadlessWhenPrivate == true)
                    {
                        HeadlessWhenPrivate = false;
                        IsUsingHeadlessGame = false;
                        PrintSetting(index);
                    }

                } else if (index == 4)
                {
                    if (InjectGameServer == false)
                    {
                        InjectGameServer = true;
                        IsUsingGameServer = true;
                        PrintSetting(index);

                    } else if (InjectGameServer == true)
                    {
                        InjectGameServer = false;
                        IsUsingGameServer = false;
                        PrintSetting(index);
                    }
                }
                // PrintSetting(index);
            }
        }
    }

}


