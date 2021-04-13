using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FileManager
{


    class Program
    {

        
        static void Main(string[] args)
        {

            Console.CursorVisible = false;

            var manager = new FileTree();
            
            ShowList(manager.Items, manager.Selected);

           

            while (true)
            {
                
                var inputKey = Console.ReadKey(false);
                switch (inputKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        manager.Previous();
                        break;

                    case ConsoleKey.DownArrow:
                        manager.Next();
                        break;

                    case ConsoleKey.Enter:
                        manager.SelectOpen(); 
                        break;
                    case ConsoleKey.F2:
                        if (DeleteRequest(manager.Selected))
                        {
                            manager.Delete();
                        }
                        break;
                    case ConsoleKey.F3:
                        manager.StartProcess();
                        break;
                    case ConsoleKey.F1:
                        manager.Copy();
                        break;
    
                      
                }
                ShowList(manager.Items, manager.Selected);
                
            }
            

        }

       
        static bool DeleteRequest(MainItem item)
        {
            Console.ResetColor();
            Console.Clear();
            Panel.DrowPanel();
            Console.WriteLine(item.MainPath);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Удалить выбранный элемент?");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Если хотите удалить элемент нажмите Enter");
            Console.ResetColor();
            var key = Console.ReadKey();
            return key.Key == ConsoleKey.Enter;

        }

       

        /// <summary>
        /// Перерисовка экрана для отображения списка
        /// </summary>
        /// <param name="items">Список текущих элементов</param>
        /// <param name="selected">Выбранный элемент списка</param>
        static void ShowList(IEnumerable<MainItem> items,MainItem selected = null)
        {
            Console.ResetColor();
            Console.Clear();

            Panel.DrowPanel();
           
            int count = 0;
            foreach (var item in items)
            {
                

                if (item.Equals(selected))
                {
                    
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {

                    // Console.BackgroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                count++;
                Console.SetCursorPosition(2, count);
                PrintItem(item);
               

               
               
            }
            Console.ResetColor();
          
        }


        /// <summary>
        /// Вывод на экран строки элемента 
        /// </summary>
        /// <param name="item"></param>
        static void PrintItem(MainItem item)
        {
           
            var name = item.Name.Length <= 50 ? item.Name : $"{item.Name.Substring(0, 47)}...";

            var itemType = item.Size.HasValue ? string.Empty : "dir";

            var size = item.Size.HasValue ? BytesSizeFormat(item.Size.Value) : string.Empty;
            Console.WriteLine($"{name,-50}{itemType,3}{size,15}");
        }

      

        /// <summary>
        /// Форматирование размера
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        static string BytesSizeFormat(long size)
        {
            string[] suffixes = { "B", "KB", "MB", "TB" };
            string suffix = suffixes[0];

            for (int i = 0; i < 4; i++)
            {
                suffix = suffixes[i];
                if (size > 1024)
                {
                    size /= 1024;
                }
                else
                    break;
            }
            return $"{size:N1} {suffix}";
        }
    }
   
}
