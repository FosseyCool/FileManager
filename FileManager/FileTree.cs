using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace FileManager
{
    class FileTree
    {
        /// <summary>
        /// Выбранный элемент
        /// </summary>
        public MainItem Selected { get; protected set; }
        /// <summary>
        /// Список элементов текущей папки
        /// </summary>
        public List<MainItem> Items { get; private set; }
        /// <summary>
        /// Инициализация менеджера файлов с начальной папкой корня текущего диска
        /// </summary>
        public FileTree()
            :this(Path.GetPathRoot(Directory.GetCurrentDirectory()))

        { }

        public FileTree(string initPath)
        {
            if (!ChangePath(initPath))
            {
                ChangePath(Path.GetPathRoot(Directory.GetCurrentDirectory()));
            }
        }

        /// <summary>
        /// Изменение текущей папки
        /// </summary>
        /// <param name="path">Папка в которую надо перейти</param>
        /// <returns>true-если удалось перейти в папку</returns>
        public bool ChangePath(string path)
        {
            if (path==null)
            {
                FillDisk();
                First();
            }
            else if (Directory.Exists(path))
            {
                FillItems(path);
                First();
                return true;
            }
            return false;

        }
        /// <summary>
        /// Выбор первого элемента текущего списка
        /// </summary>
        public void First()
        {
            Selected = Items [0];
        }
        /// <summary>
        /// Выбор последнего элемента текущего списка
        /// </summary>
        public void Last()
        {
            Selected = Items[Items.Count - 1];
        }
        /// <summary>
        /// Выбор предыдущего элемента
        /// </summary>
        public void Previous()
        {
            var index = Items.FindIndex(o => o.Equals(Selected));
            if (index<=0)
            {
                Selected = Items[0];
            }
            else
            {
                Selected = Items[index - 1];
            }
           
        }
        /// <summary>
        /// Выбор следующего элемента
        /// </summary>
        public void Next()
        {
            var index = Items.FindIndex(o => o.Equals(Selected));
            if (index >= Items.Count - 1)
            {
                Selected = Items[Items.Count - 1];
            }
            else
            {
                Selected = Items[index + 1];
            }
        }

        public bool SelectOpen()
        {
            if (Selected!=null)
            {
                return ChangePath(Selected.MainPath);
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Заполнение списка элементами выбранной папки
        /// </summary>
        /// <param name="path"></param>
        public void FillItems(string path)
        {
            List<MainItem> list = new List<MainItem>();
            try
            {   
                // перебираем элементы текущий папки и наполняем список элементами
                foreach (var entry in Directory.GetFileSystemEntries(path))
                {
                    try
                    {
                        var item = new MainItem()
                        {
                            MainPath = entry,
                            Name = Path.GetFileName(entry),
                            Size=File.Exists(entry)?new FileInfo(entry).Length:(long?)null
                        };
                        list.Add(item);
                       
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            Items = list.OrderBy(o => o.Size.HasValue).ThenBy(o => o.Name).ToList();

            //поиск предыдущей папки
            var itemParent = new MainItem() { Name = "..." };
            var parent = Directory.GetParent(path);
            if (parent!=null&&!parent.FullName.Equals(path))
                itemParent.MainPath = parent.FullName;

            Items.Insert(0, itemParent); 
        }

        /// <summary>
        /// Заполнение элемента списка дисками
        /// </summary>
        /// <param name="path"></param>
        private void FillDisk()
        {
            List<MainItem> list = new List<MainItem>();
            try
            {
                foreach (var entry in Environment.GetLogicalDrives())
                {
                    try
                    {
                        var item = new MainItem()
                        {
                            MainPath = entry,
                            Name = entry
                        };
                        list.Add(item);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            // сортировка по типу (папка/файл), а потом по имени
            Items = list.OrderBy(o => o.Name).ToList();
        }

    }
}
