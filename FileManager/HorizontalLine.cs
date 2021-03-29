using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class HorizontalLine
    {
        List<PanelPoints> pointsList = new List<PanelPoints>();

        public HorizontalLine(int xLeft,int xRight,int y,char sym)
        {
            for (int x=xLeft;x <=xRight;x++)
            {
                PanelPoints p = new PanelPoints(x, y, sym);
                pointsList.Add(p);
            }
        }

        public void Drow()
        {
            foreach (PanelPoints  p in pointsList)
            {
                p.Draw();
            }
        }
    }
}
