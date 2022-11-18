using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaScriptInterpreter
{
    public class Parser
    {
        /// <summary>
        /// Parse the file gived.
        /// If the return object is null, the program runned sucessfully
        /// </summary>
        /// <param name="file">The file to parse</param>
        /// <returns>The exception occued (if there is one)</returns>
        public static Exception Parse(string file)
        {
            string[] lines = File.ReadAllLines(file);
            int currentLine = 0;

            try
            {
                for (int lineInt = 0; lineInt < lines.Length; lineInt++)
                {
                    string line = lines[lineInt];
                    currentLine = lineInt;

                    if (line.StartsWith("#"))
                    {
                        // don't care
                        continue;
                    }
                    else if (line.StartsWith("PauseProgram"))
                    {
                        int milliSeconds = int.Parse(line.Split(' ')[1]);
                        Thread.Sleep(milliSeconds);
                    }
                    else if (line.StartsWith("Start"))
                    {
                        string[] splittedLine = line.Split(' ');
                        string ptl = "";
                        for (int i = 1; i < splittedLine.Length; i++)
                        {
                            ptl += splittedLine[i] + " ";
                        }
                        Process.Start(ptl);
                    }
                    else if (line.StartsWith("ThrowException"))
                    {
                        string[] splittedLine = line.Split(' ');
                        string exmsg = "";
                        for (int i = 1; i < splittedLine.Length; i++)
                        {
                            exmsg += splittedLine[i] + " ";
                        }
                        throw new Exception(exmsg);
                    }
                    else if (line.StartsWith("WriteLine"))
                    {
                        string[] lineToWrite = line.Split(' ');
                        string ltw = "";
                        for (int i = 1; i < lineToWrite.Length; i++)
                        {
                            ltw += lineToWrite[i] + " ";
                        }
                        Console.WriteLine(ltw);
                    }
                    else if (line.StartsWith("Write"))
                    {
                        string[] lineToWrite = line.Split(' ');
                        string ltw = "";
                        for (int i = 1; i < lineToWrite.Length; i++)
                        {
                            ltw += lineToWrite[i] + " ";
                        }
                        Console.Write(ltw);
                    }
                    else if (line.StartsWith("SetForeground"))
                    {
                        int foreColor = int.Parse(line.Split(' ')[1]);
                        Console.ForegroundColor = (ConsoleColor)foreColor;
                    }
                    else if (line.StartsWith("SetBackground"))
                    {
                        int backColor = int.Parse(line.Split(' ')[1]);
                        Console.BackgroundColor = (ConsoleColor)backColor;
                    }
                    else if (line.StartsWith("Clear"))
                    {
                        Console.Clear();
                    }
                    else if (line.StartsWith("Exit"))
                    {
                        int exitCode = int.Parse(line.Split(' ')[1]);

                        Console.ResetColor();

                        Console.WriteLine();
                        Console.WriteLine("Program exited with code " + exitCode + " (press any key to exit interpreter)");
                        Console.ReadKey();
                        Environment.Exit(exitCode);
                    }
                }

                Console.ResetColor();
            }
            catch (Exception ex)
            {

                Console.WriteLine("An unhandled exception has occured!");
                Console.WriteLine(ex.GetType().FullName + " : " + ex.Message);

                var st = ex.StackTrace; 

                Console.WriteLine("Stack trace : \n" + st);
                Console.WriteLine($"   at {file}: line {currentLine}");
                Console.WriteLine("Program exited with code -1 (press any key to exit interpreter)");
                Console.ReadKey();
            }

            return null;
        }
    }
}
