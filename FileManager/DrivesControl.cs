using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FileManager
{
    class DrivesControl
    {
        private DriveInfo [] items;

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

        public DrivesControl(DriveInfo [] items)
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
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            for (int i = 0; i < allDrives.Length; i++)
            {
                Console.SetCursorPosition(2, i + 1);
                Console.WriteLine(allDrives[i]);
            }


            DriveInfo[] drives = DriveInfo.GetDrives();

            DrivesControl lv = new DrivesControl(drives);

            ConsoleKeyInfo cki;
            do
            {

                cki = Console.ReadKey(true);
                switch (cki.Key)
                {
                    case ConsoleKey.UpArrow:
                        lv.SelectedIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        lv.SelectedIndex++;
                        break;
                    default:
                        break;
                }
            } while (cki.Key != ConsoleKey.Enter);


            Console.Clear();

            Foo(lv.SelectedItem);


            Console.ReadKey();
            
        }

        static void Foo(object o)
        {

            Console.WriteLine($"Выбрана диск {o}");

            string[] dirs = Directory.GetDirectories(o.ToString());

            foreach (var item in dirs)
            {
                Console.WriteLine(item);
            }

            DirectoriesControl.controlCursor();
        }



    }
}
