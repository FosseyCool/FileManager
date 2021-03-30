using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FileManager
{
    class DriveItem
    {
        public DriveInfo[] drives;

        public static void PrintDrives()
        {
            DriveInfo[]  drive = DriveInfo.GetDrives();

            foreach  (DriveInfo item in drive)
            {
                Console.WriteLine(item);
            }
        }

    }
}
