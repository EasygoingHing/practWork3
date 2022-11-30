using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LibMatrix
{
    public class Matrix<T>
    {
        private T[,] _items;
        private int _rows, _columns;
                
        public Matrix(int rowCount, int columnCount, string extension = ".matrix")
        {
            _items = new T[rowCount, columnCount];
            Extension = extension;
        }

        public T this[int row, int column]
        {
            get { return _items[row, column]; }
            set { _items[row, column] = value; }
        }

        
        public string Extension { get; private set; }
        
        public void DefaultInit()
        {
            for (int i = 0; i < _items.GetLength(0); i++)
            {
                for (int j = 0; j < _items.GetLength(1); j++)
                {
                    _items[i, j] = default;
                }
            }
        }

        public void Add(T[,] items)
        {
            _rows = items.GetLength(0);
            _columns = items.GetLength(1);
            _items = new T[_rows, _columns];
            for (int i = 0; i < items.GetLength(0); i++)
            {
                for (int j = 0; j < items.GetLength(1); j++)
                {
                    _items[i, j] = items[i, j];
                }
            }
        }

        public int Rows
        {
            get => _rows; 
            private set 
            {
                if (value == _rows)
                {
                    return;
                }
                _rows = value;
            }
        }

        public int Columns
        {
            get => _columns; 
            private set
            {
                if (value == _columns)
                {
                    return;
                }
                _columns = value;
            }
        }

        /// <summary>
        /// Класс для сохранения или считывания данных
        /// </summary>
        private static readonly BinaryFormatter _formatter = new BinaryFormatter();

        /// <summary>
        ///  Сохраняет элементы двухмерного массива в файл через сериализацию данных
        /// </summary>
        /// <param name="data">Массив</param>
        /// <param name="path">Полный путь</param>
        public void Save(object data, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);            
            _formatter.Serialize(stream, data);            
        }

        /// <summary>
        /// Загружает элементы двухмерного массива из файла через десериализацию
        /// </summary>
        /// <param name="path">Полный путь</param>
        /// <returns>массив</returns>
        public object Load(string path)
        {
            FileStream stream = new FileStream(path, FileMode.Open);            
            return _formatter.Deserialize(stream);            
        }

        /// <summary>
        /// Преобразование объекта в поток байтов
        /// </summary>
        /// <param name="path">Полный путь</param>
        public void MatrixSerialize(string path)
        {
            Save(_items, path);
        }

        /// <summary>
        /// Получение из потока байтов ранее сохраненного объекта
        /// </summary>
        /// <param name="path">полный путь</param>
        public void MatrixDeserialize(string path)
        {
            _items = (T[,])Load(path);
        }

        public DataTable ToDataTable()
        {
            var res = new DataTable();
            for (int i = 0; i < _items.GetLength(1); i++)
            {
                res.Columns.Add("#" + (i + 1), typeof(T));
            }

            for (int i = 0; i < _items.GetLength(0); i++)
            {
                var row = res.NewRow();

                for (int j = 0; j < _items.GetLength(1); j++)
                {
                    row[j] = _items[i, j];
                }
                res.Rows.Add(row);
            }
            return res;
        }
    }    
}
