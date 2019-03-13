using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL_AIS_Readers;
using System.IO;


namespace C_sharp_ais_manager
{       
    class Program
    {
        static void Main(string[] args)
        {
            Globals Global_vars = new Globals();
            
            if (args.Length > 0)
            {
                if (args[0] == "-gui")
                {
                    Global_vars.gui_on = true;

                    Global_vars.debug_file_path = "../../../" + Global_vars.debug_file_path;
                }                
            }
            else
            { 
                Global_vars.gui_on = false;
            }
                     
            if (!File.Exists(Global_vars.debug_file_path))
            {
                using (StreamWriter sw = File.CreateText(Global_vars.debug_file_path))
                {
                    sw.WriteLine("========================================");
                    sw.WriteLine("Debug file created time:");
                    sw.WriteLine(DateTime.Now);
                    sw.WriteLine("========================================");
                    sw.Close();
                }                

            } else
            {
                using (StreamWriter sw = File.AppendText(Global_vars.debug_file_path))
                {
                    sw.WriteLine("========================================");
                    sw.WriteLine("\nDebug file appended time:");
                    sw.WriteLine((DateTime.Now).ToString());
                    sw.WriteLine("========================================");
                }
            }
                        
            Console.WriteLine("Checking settings.ini ...");

            File.AppendAllText(Global_vars.debug_file_path, "Checking settings.ini..." + Environment.NewLine);

            ais_readers.ReadSettings(ref Global_vars);

            ais_readers.OpenAllDevicesAndReadLists(ref Global_vars);
            
            ais_readers.print_menu();          
                        
            Console.WriteLine("Press ESC to stop");

            ConsoleKeyInfo input;

            if (Global_vars.gui_on == false)
            {
                ais_readers.PrintRealTimeEventTable();

                do
                {
                    while (!Console.KeyAvailable)
                    {
                        ais_readers.AppLoop(ref Global_vars);
                    }

                    input = Console.ReadKey(true);

                    ais_readers.menu(input.Key, ref Global_vars);

                } while (input.Key != ConsoleKey.Escape);
            }
            else
            {               
                do
               {
                    Global_vars.gui_command = Console.In.ReadLine();

                    Console.WriteLine(Global_vars.gui_command);
                    
                    while (Global_vars.gui_command == "0")                    
                    {
                        
                        ais_readers.AppLoop(ref Global_vars);
                       
                    }
                    
                     ais_readers.gui_menu(Global_vars.gui_command, ref Global_vars);
                    
                } while (Global_vars.gui_command != "escape");

                    Console.WriteLine("Escape sent, process exiting");
            }

          }

        }
        
    }

