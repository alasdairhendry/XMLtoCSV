using System;
using System.IO;

namespace XMLtoCSV
{
    class MainClass
    {
        static string xmlFilePath = "";
        static bool xmlFilePathValidated = false;

        const string cmdConfirmPath = "val";
        const string cmdPrevDirectory = "cd-";
        const string cmdHomeDirectory = "cd";

        static ConsoleColor initialColour = ConsoleColor.White;

        public static void Main(string[] args)
        {
            Begin();
            Log.WriteLineColour("Application end", ConsoleColor.Red);
            Console.WriteLine("Bye");
            return;

            while (true)
            {
                string s = Console.ReadLine();

                if (s == "cc")
                {
                    Console.Clear();
                }

                if (s == "be")
                {
                    Begin();
                    Log.WriteLineColour("Application end", ConsoleColor.Red);
                    break;
                }
            }
            return;

            SetHomeDirectory();
            InitialText();
            GetPath();
            CheckBegin();

            while (true)
                Console.ReadLine();
        }

        private static void SetHomeDirectory()
        {
            xmlFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private static void CheckBegin()
        {
            Console.WriteLine("Begin conversion? (Y/N)");

            string s = Console.ReadLine();

            if (s.ToLower() == "y")
            {
                Console.WriteLine("begin");
                Begin();
            }
            else
            {
                Console.WriteLine("Current path directory: " + xmlFilePath);
                Console.WriteLine();
                xmlFilePathValidated = false;
                GetPath();
            }
        }

        private static void Begin()
        {
            //Convertor.Run("/Users/alasdairhendry/Desktop/test.xml");
            Convertor.Run("/Users/alasdairhendry/Desktop/Book Data.xml");
        }

        private static void InitialText()
        {
            Console.WriteLine("Please enter the XML file path, including the filename and extension.");
            Console.WriteLine("Add in increments or use '/' to add multiple directories");
            Console.WriteLine();
            Console.WriteLine(string.Format("[COMMAND] '{0}' to validate path", cmdConfirmPath));
            Console.WriteLine(string.Format("[COMMAND] '{0}' to go to Home directory", cmdHomeDirectory));
            Console.WriteLine(string.Format("[COMMAND] '{0}' to go to back a directory", cmdPrevDirectory));
            Console.WriteLine();
            Console.WriteLine("Current path directory: " + xmlFilePath);
            Console.WriteLine();
        }

        private static void GetPath()
        {
            do
            {
                string s = Console.ReadLine();

                if (s.Equals(cmdHomeDirectory))
                {
                    SetHomeDirectory();
                }
                else if (s.Equals(cmdPrevDirectory))
                {
                    xmlFilePath = Path.GetFullPath(Path.Combine(xmlFilePath, ".."));
                }
                else if (s.EndsWith(cmdConfirmPath))
                {
                    ValidatePath();

                }
                else
                {
                    xmlFilePath = Path.GetFullPath(Path.Combine(xmlFilePath, s));
                }

                Console.WriteLine("New path directory: " + xmlFilePath);
                Console.WriteLine();
            } while (!xmlFilePathValidated);
        }

        private static void ValidatePath()
        {
            bool directoryExists = false;
            bool fileExists = false;

            Console.WriteLine();

            if (!Directory.Exists(Path.GetDirectoryName(xmlFilePath)))
            {
                Log.WriteLineColour(string.Format("Directory does not exist: [{0}]", Path.GetDirectoryName(xmlFilePath)), ConsoleColor.Red);
                directoryExists = false;

                Log.WriteLineColour(string.Format("File not found: [{0}]", Path.GetFileName(xmlFilePath)), ConsoleColor.Red);
                fileExists = false;
            }
            else
            {
                Log.WriteLineColour(string.Format("Directory verified: [{0}]", Path.GetDirectoryName(xmlFilePath)), ConsoleColor.Green);
                directoryExists = true;

                if (!File.Exists(xmlFilePath))
                {
                    Log.WriteLineColour(string.Format("File not found: [{0}]", Path.GetFileName(xmlFilePath)), ConsoleColor.Red);
                    fileExists = false;
                }
                else if (string.IsNullOrWhiteSpace(Path.GetExtension(xmlFilePath)))
                {
                    Log.WriteLineColour(string.Format("Extension not found: [{0}]", Path.GetFileName(xmlFilePath)), ConsoleColor.Red);
                    fileExists = false;
                }
                else if (Path.GetExtension(xmlFilePath) != ".xml")
                {
                    Log.WriteLineColour(string.Format("Extension is invalid: [{0}]", Path.GetExtension(xmlFilePath)), ConsoleColor.Red);
                    fileExists = false;
                }
                else
                {
                    Log.WriteLineColour(string.Format("File verified: [{0}]", Path.GetFileName(xmlFilePath)), ConsoleColor.Green);
                    fileExists = true;
                }
            }

            Console.WriteLine();

            if (directoryExists && fileExists) xmlFilePathValidated = true;
            else xmlFilePathValidated = false;
        }
    }
}
