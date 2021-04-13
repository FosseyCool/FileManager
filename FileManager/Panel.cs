using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FileManager
{
    class Panel
    {
        
        /// <summary>
        /// Отрисовывает интерфейс
        /// </summary>
        public static void DrowPanel()
        {
           

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            //Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.SetBufferSize(80, 61);
            
            HorizontalLine uperLeft = new HorizontalLine(0, 0, 0, '╔');
            uperLeft.Drow();

            HorizontalLine upLine = new HorizontalLine(1, 70, 0, '═');
            upLine.Drow();

            HorizontalLine uperRight = new HorizontalLine(71, 71, 0, '╗');
            uperRight.Drow();

            VerticalLine leftLine = new VerticalLine(1, 60, 0, '║');
            leftLine.Drow();

            VerticalLine rightLine = new VerticalLine(1, 60, 71, '║');
            rightLine.Drow();

            HorizontalLine downLeft = new HorizontalLine(0, 0, 60, '╚');
            downLeft.Drow();

            HorizontalLine downLine = new HorizontalLine(1, 70, 60, '═');
            downLine.Drow();

            HorizontalLine midLine = new HorizontalLine(1, 70, 40, '═');
            midLine.Drow();

            HorizontalLine midEndLeft = new HorizontalLine(0, 0, 40, '╠');
            midEndLeft.Drow();

            HorizontalLine midEndRight = new HorizontalLine(71, 71, 40, '╣');
            midEndRight.Drow();


            HorizontalLine midLine_2 = new HorizontalLine(1, 70, 55, '═');
            midLine_2.Drow();

            HorizontalLine midEndLeft_2 = new HorizontalLine(0, 0, 55, '╠');
            midEndLeft_2.Drow();

            HorizontalLine midEndRight_2 = new HorizontalLine(71, 71, 55, '╣');
            midEndRight_2.Drow();

            HorizontalLine downRight = new HorizontalLine(71, 71, 60, '╝');
            downRight.Drow();

            DrowFunctions();

        }

        
        /// <summary>
        /// Отрисовывает информацию о клавишах
        /// </summary>
        public static void DrowFunctions()
        {
            Console.SetCursorPosition(30, 41);
            Console.WriteLine("ИНФОРМАЦИЯ");

            Console.SetCursorPosition(3, 58);
            Console.WriteLine("Ввод:");

            Console.SetCursorPosition(3, 53);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("<F1> Копировать");

            Console.ResetColor();
            Console.SetCursorPosition(29, 53);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("<F2> Удалить");

            Console.ResetColor();
            Console.SetCursorPosition(52, 53);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("<F3> Открыть файл");


        }
    }
}
