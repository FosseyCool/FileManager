using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class VerticalLine
    {
        List<PanelPoints> pointList=new List<PanelPoints>();

        public VerticalLine(int yDown,int yUp,int x, char sym)
        {
            for (int y = yDown; y < yUp; y++)
            {
                PanelPoints p = new PanelPoints(x, y, sym);
                pointList.Add(p);
            }
        }

        public void Drow()
        {
            foreach (PanelPoints p in pointList)
            {
                p.Draw();
            }
        }
      
    }
}
