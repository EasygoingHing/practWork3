using LibMatrix;
using System;

namespace Lib_1
{
    public static class ExtensionMatrix
    {
        /// <summary>
        /// Заполнение двумерного массива
        /// </summary>
        /// <param name="numbers">Двумерный массив</param>
        /// <param name="rows">Строки</param>
        /// <param name="column">Столбцы</param>
        /// <param name="min">Минимальное значение</param>
        /// <param name="max">Максимальное значение</param>
        public static void CreateMatrix(this Matrix<int> numbers, int rows, int column, int min = 0, int max = 10)
        {
            int[,] array = new int[rows, column];
            Random rnd = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    array[i, j] = rnd.Next(min, max);
                }
            }
            numbers.Add(array);
        }
        /// <summary>
        /// Метод для подсчета суммы чисел больше 5 в двумерном массиве
        /// </summary>
        /// <param name="array">массив</param>
        /// <returns>Целое число</returns>
        public static int Sum5(this Matrix<int> numbers)
        {
            int sum = 0;

            for (int i = 0; i < numbers.Rows; i++)
            {
                for (int j = 0; j < numbers.Columns; j++)
                {
                    if (numbers[i, j] > 5)
                    {
                        sum += numbers[i, j];
                    }                    
                }
            }
            return sum;
        }


    }   
}
