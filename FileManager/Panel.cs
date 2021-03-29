using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class Panel
    {
        //Отрисовка интерфейса
        public static void DrowPanel()
        {
           

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.SetBufferSize(100, 80);
            
            HorizontalLine uperLeft = new HorizontalLine(0, 0, 0, '╔');
            uperLeft.Drow();

            HorizontalLine upLine = new HorizontalLine(1, 98, 0, '═');
            upLine.Drow();

            HorizontalLine uperRight = new HorizontalLine(99, 99, 0, '╗');
            uperRight.Drow();

            VerticalLine leftLine = new VerticalLine(1, 60, 0, '║');
            leftLine.Drow();

            VerticalLine rightLine = new VerticalLine(1, 60, 99, '║');
            rightLine.Drow();

            HorizontalLine downLeft = new HorizontalLine(0, 0, 60, '╚');
            downLeft.Drow();

            HorizontalLine downLine = new HorizontalLine(1, 98, 60, '═');
            downLine.Drow();

            HorizontalLine midLine = new HorizontalLine(1, 98, 40, '═');
            midLine.Drow();

            HorizontalLine midEndLeft = new HorizontalLine(0, 0, 40, '╠');
            midEndLeft.Drow();

            HorizontalLine midEndRight = new HorizontalLine(99, 99, 40, '╣');
            midEndRight.Drow();


            HorizontalLine midLine_2 = new HorizontalLine(1, 98, 55, '═');
            midLine_2.Drow();

            HorizontalLine midEndLeft_2 = new HorizontalLine(0, 0, 55, '╠');
            midEndLeft_2.Drow();

            HorizontalLine midEndRight_2 = new HorizontalLine(99, 99, 55, '╣');
            midEndRight_2.Drow();

            HorizontalLine downRight = new HorizontalLine(99, 99, 60, '╝');
            downRight.Drow();

        }
    }
}
