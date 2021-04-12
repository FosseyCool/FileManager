using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class MainItem
    {
        /// <summary>
        /// Полный путь к элементу
        /// </summary>
        public string MainPath { get; set; }

        /// <summary>
        /// Имя элемента
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Размер
        /// </summary>
        public long ? Size { get; set; } = null;







    }
}
