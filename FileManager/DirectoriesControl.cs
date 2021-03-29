using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileManager
{
    class DirectoriesControl
    {

        private string[] items;

        private int selectedIndex;
        public int SelectedIndex
        {
            
            get
            { 
                return selectedIndex;
            }
            set
            {              
                value = Math.Abs(value % items.Length);
                selectedIndex = value;
                
                Draw();
            }
        }

        public object SelectedItem { get { return items[SelectedIndex]; } }//показывает выбранный вариант

        public DirectoriesControl(string [] items)
        {
            
            this.items = items;
            selectedIndex = 0;
            
        }
        private void Draw()
        {
            
            Console.Clear();

            
            Panel.DrowPanel();

           //Console.SetCursorPosition(2, 1); //верхняя позиция директории
            for (int i = 0; i < items.Length; i++)
            {
                Console.SetCursorPosition(2, i + 1);

                if (i == selectedIndex)
                {
                    var tmp = Console.BackgroundColor;
                    Console.BackgroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = tmp;
                    Console.WriteLine(items[i]);
                    Console.ForegroundColor = Console.BackgroundColor;
                    Console.BackgroundColor = tmp;
                }
                else
                {                   
                    Console.WriteLine(items[i]);                    
                }
            }          
        }
        public static void controlCursor()
        {
            Panel.DrowPanel();
            Console.SetCursorPosition(2, 1);
            string directoryPath = "D:\\";
            string[] directrories = Directory.GetDirectories(directoryPath);
            

            for (int i = 0; i < directrories.Length; i++)
            {
                Console.SetCursorPosition(2, i + 1);

                Console.WriteLine(directrories[i]);

            }
          
            DirectoriesControl driveD = new DirectoriesControl(directrories);


            ConsoleKeyInfo cki;
            do
            {
                
                cki = Console.ReadKey(true);
                switch (cki.Key)
                {
                    case ConsoleKey.UpArrow:
                        driveD.SelectedIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        driveD.SelectedIndex++;
                        break;
                    default:
                        break;
                }
            } while (cki.Key != ConsoleKey.Enter);

            Console.Clear();
         
            Foo(driveD.SelectedItem);
            Console.ReadKey();
        }
        static void Foo(object o)
        {
            Console.WriteLine($"Выбрана папка {o}");
        }

        public static void infoDrivers()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo item in allDrives)
            {
                Console.WriteLine(item);
            }

        }

    }
}
