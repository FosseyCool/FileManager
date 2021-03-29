using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class PanelPoints
    {
        public int x;
        public int y;
        public char sym;

        public PanelPoints()
        {

        }
        public PanelPoints(int _x,int _y,char _sym)
        {
            x = _x;
            y = _y;
            sym = _sym;
        }
        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(sym);
        }
    }
}
