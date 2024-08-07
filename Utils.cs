using Newtonsoft.Json;

namespace ggsLauncher
{
    public class Utils
    {
        public static void Log(string message)
        {
            try
            {
                string finalLog = $"[{DateTime.Now}] - {message}";
                using (StreamWriter sw = File.AppendText(Program.logFile))
                {
                    sw.WriteLine(finalLog);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void LoadConfig()
        {
            if (File.Exists(Program.configFile))
            {
                string json = File.ReadAllText(Program.configFile);
                dynamic config = JsonConvert.DeserializeObject(json);
                Program.path = config.path;
                Program.mail = config.mail;
                Program.pass = config.pass;
            }
            else
            {
                File.Create(Program.configFile);
                Program.path = "";
                Program.mail = "";
                Program.pass = "";
            }
        }

        public static void SaveConfig()
        {
            dynamic config = new
            {
                Program.path,
                Program.mail,
                Program.pass
            };
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(Program.configFile, json);
        }

        public static void CustomLaunch()
        {
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine("Please, enter your redirection DLL download link.");
            Console.Write("LINK: ");
            Program.CustomLink = Console.ReadLine();
            Console.WriteLine(" ");
            Console.WriteLine("Press any key to launch Fortnite...");
            Console.ReadKey();

            Program.Launch(9999);
        }
    }
}
