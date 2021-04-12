using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Text.Json;

namespace FileManager
{
    class FileTree
    {
        public string jsonPath;
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
            
            Home();
            
        }


        /// <summary>
        /// Изменение текущей папки
        /// </summary>
        /// <param name="path">Папка в которую надо перейти</param>
        /// <returns>true-если удалось перейти в папку</returns>
        public bool ChangePath(string path)
        {
            if (path == null)
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

        public void Home()
        {
            if (File.Exists("path.json"))
            {
                jsonPath = File.ReadAllText("path.json");

                Selected.MainPath = JsonSerializer.Deserialize<string>(jsonPath);

                FillItems(Selected.MainPath);

            }
            else 
            {
                FillDisk();
                First();

            }



        }
        /// <summary>
        /// Запускает процесс выбранного элемента
        /// </summary>
        public void StartProcess()
        {
            if (Selected!=null&&!Items.First().Equals(Selected))
            {
                try
                {
                    if (Selected.Size.HasValue)
                    {
                        var fileToOpen = Selected.MainPath;
                        var process = new Process();
                        process.StartInfo = new ProcessStartInfo()
                        {
                            UseShellExecute = true,
                            FileName = fileToOpen
                        };
                        process.Start();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// удаление выбранной папки или файла
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            if (Selected!=null&&!Items.First().Equals(Selected))
            {
                try
                {
                    // удаляем физические файлы или папки
                    if (Selected.Size.HasValue)
                    {
                        File.Delete(Selected.MainPath);
                    }
            else
            {
                        Directory.Delete(Selected.MainPath, true);
                        //удаляем элементы из списка
                        Items.Remove(Selected);
                        //выбираем другой элемент
                        Previous();
                        return true;
                    }

                }
                catch (Exception)
                {

                    throw;
                }


            }
                return false;
            }
        /// <summary>
        /// Копирует выбранный  элемент,в папку которую указывает пользователь
        /// </summary>
        public void Copy()
        {
            if (Selected!=null&&!Items.First().Equals(Selected))
            {
                try
                {
                    if (Selected.Size.HasValue)
                    {


                        Console.SetCursorPosition(4,46);
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Введите путь куда хотите скопировать файл.");
                        Console.SetCursorPosition(4, 47);
                        Console.WriteLine("Eсли указанной папки не существует, она создатся автоматически.");

                        Console.SetCursorPosition(8, 58);
                        string targetPath = Console.ReadLine();

                        string sourceFile = Path.Combine(Selected.MainPath );
                        string destFile = Path.Combine(targetPath, Selected.Name);
                        
                        if (!File.Exists(targetPath))
                        {   
                            var directory = Directory.CreateDirectory(targetPath);
                            File.Copy(Selected.MainPath, destFile, true);

                        }
                        else
                        {

                            File.Copy(sourceFile, destFile, true);
                        }
                        

                    }
                    else
                    {
                        Console.SetCursorPosition(4, 46);
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Введите путь куда хотите скопировать файл.");
                        Console.SetCursorPosition(4, 47);
                        Console.WriteLine("Eсли указанной папки не существует, она создатся автоматически.");

                        Console.SetCursorPosition(8, 58);
                        string targetpath = Console.ReadLine();

                        string sourcePath = Path.Combine(Selected.MainPath);
                        string destFile = Path.Combine(targetpath, Selected.Name);

                        if (!Directory.Exists(targetpath))
                        {
                            var directory = Directory.CreateDirectory(targetpath);
                            string[] files = Directory.GetFiles(sourcePath);
                            foreach (string s in files)
                            {
                                Selected.Name = Path.GetFileName(s);
                                destFile = Path.Combine(targetpath, Selected.Name);
                                File.Copy(s, destFile, true);
                            }
                        }
                        else
                        {
                            string[] files = Directory.GetFiles(sourcePath);
                            foreach (string s in files)
                            {
                                Selected.Name = Path.GetFileName(s);
                                destFile = Path.Combine(targetpath, Selected.Name);
                                File.Copy(s, destFile, true);
                            }
                        }

                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
            
        }

        public void PrintSize()
        {
            string path = Selected.MainPath;
            double catalogSize = 0;
            catalogSize = sizeInfo(path, ref catalogSize);
            if (catalogSize != 0)
            {
                Console.SetCursorPosition(20, 50);
                Console.WriteLine("Размер каталога {0} составляет {1} ГБ", path, catalogSize);
            }
            else
            {
                Console.WriteLine("Каталог {0} пуст.", path);
            }

        }
        public double sizeInfo(string path, ref double size)
        {
            DirectoryInfo currentDirectory = new DirectoryInfo(path);
            DirectoryInfo[] directoriesInCurrentDirectory = currentDirectory.GetDirectories();
            FileInfo[] filesInCurrentDirectory = currentDirectory.GetFiles();

            foreach (FileInfo item in filesInCurrentDirectory)
            {
                size += item.Length;
            }
            foreach (DirectoryInfo item in directoriesInCurrentDirectory)
            {
                sizeInfo(item.FullName, ref size);
            }
            return Math.Round((double)(size / 1024 / 1024 / 1024), 1);
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

        public void jso()
        {

            jsonPath = JsonSerializer.Serialize(Selected.MainPath);
            File.WriteAllText("path.json", jsonPath);
        }

    }
}
