using System;
using System.Collections.Generic;

namespace Lab4
{
    // Абстрактний базовий клас, який виділяє загальні контракти для "4-елементних" контейнерів
    public abstract class Container4
    {
        public const int DIMENSION = 4;

        // Конструктор базового класу
        protected Container4()
        {
            Console.WriteLine($"[Container4] Створено екземпляр типу {GetType().Name}");
        }

        // Деструктор (фіналізатор)
        ~Container4()
        {
            Console.WriteLine($"[Container4] Фіналізатор викликано для {GetType().Name}");
        }

        // Контракти, що мають реалізувати похідні класи
        public abstract void SetElements();
        public abstract void Display();
        public abstract double FindMax();
    }

    // Базовий клас одновимірного вектора розмірності 4
    public class Vector4D : Container4
    {
        protected double[] _elements; // Масив елементів вектора

        // Конструктор
        public Vector4D() : base()
        {
            _elements = new double[Container4.DIMENSION];
            Console.WriteLine("[Vector4D] Конструктор виконано.");
        }

        // Деструктор
        ~Vector4D()
        {
            Console.WriteLine("[Vector4D] Деструктор (фіналізатор) викликано.");
        }

        // Віртуальний метод для задання елементів вектора
        public override void SetElements()
        {
            Console.WriteLine($"Введіть {Container4.DIMENSION} елементи вектора:");
            for (int i = 0; i < Container4.DIMENSION; i++)
            {
                Console.Write($"Елемент [{i}]: ");
                string? input = Console.ReadLine();
                while (input is null || !double.TryParse(input, out _elements[i]))
                {
                    if (input is null)
                    {
                        Console.WriteLine("Ввід завершено (EOF). Переривання вводу.");
                        throw new OperationCanceledException("Ввід завершено користувачем.");
                    }
                    Console.Write("Некоректне значення. Введіть число: ");
                    input = Console.ReadLine();
                }
            }
        }

        // Метод для задання елементів з масиву (для тестування)
        public void SetElements(double[] values)
        {
            if (values is null)
                throw new ArgumentNullException(nameof(values));
            if (values.Length != Container4.DIMENSION)
                throw new ArgumentException($"Очікується масив довжини {Container4.DIMENSION}.", nameof(values));
            for (int i = 0; i < Container4.DIMENSION; i++)
            {
                _elements[i] = values[i];
            }
        }

        // Віртуальний метод для виведення вектора на екран
        public override void Display()
        {
            Console.Write("Вектор: [");
            for (int i = 0; i < Container4.DIMENSION; i++)
            {
                Console.Write($"{_elements[i]:F2}");
                if (i < Container4.DIMENSION - 1) Console.Write(", ");
            }
            Console.WriteLine("]");
        }

        // Віртуальний метод для знаходження максимального елемента
        public override double FindMax()
        {
            double max = _elements[0];
            for (int i = 1; i < Container4.DIMENSION; i++)
            {
                if (_elements[i] > max)
                    max = _elements[i];
            }
            return max;
        }
    }

    // Похідний клас матриці 4x4
    public class Matrix : Vector4D
    {
        private double[,] _matrix; // Двовимірний масив для матриці

        // Конструктор
        public Matrix() : base()
        {
            _matrix = new double[Container4.DIMENSION, Container4.DIMENSION];
            Console.WriteLine("[Matrix] Конструктор виконано.");
        }

        // Деструктор
        ~Matrix()
        {
            Console.WriteLine("[Matrix] Деструктор (фіналізатор) викликано.");
        }

        // Перевантажений метод для задання елементів матриці
        public override void SetElements()
        {
            Console.WriteLine($"Введіть елементи матриці {Container4.DIMENSION}x{Container4.DIMENSION}:");
            for (int i = 0; i < Container4.DIMENSION; i++)
            {
                for (int j = 0; j < Container4.DIMENSION; j++)
                {
                    Console.Write($"Елемент [{i},{j}]: ");
                    string? input = Console.ReadLine();
                    while (input is null || !double.TryParse(input, out _matrix[i, j]))
                    {
                        if (input is null)
                        {
                            Console.WriteLine("Ввід завершено (EOF). Переривання вводу.");
                            throw new OperationCanceledException("Ввід завершено користувачем.");
                        }
                        Console.Write("Некоректне значення. Введіть число: ");
                        input = Console.ReadLine();
                    }
                }
            }
        }

        // Метод для задання елементів з двовимірного масиву (для тестування)
        public void SetElements(double[,] values)
        {
            if (values is null)
                throw new ArgumentNullException(nameof(values));
            if (values.GetLength(0) != Container4.DIMENSION || values.GetLength(1) != Container4.DIMENSION)
                throw new ArgumentException($"Очікується матриця розміру {Container4.DIMENSION}x{Container4.DIMENSION}.", nameof(values));
            for (int i = 0; i < Container4.DIMENSION; i++)
            {
                for (int j = 0; j < Container4.DIMENSION; j++)
                {
                    _matrix[i, j] = values[i, j];
                }
            }
        }

        // Перевантажений метод для виведення матриці на екран
        public override void Display()
        {
            Console.WriteLine($"Матриця {Container4.DIMENSION}x{Container4.DIMENSION}:");
            for (int i = 0; i < Container4.DIMENSION; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < Container4.DIMENSION; j++)
                {
                    Console.Write($"{_matrix[i, j]:F2} ");
                }
                Console.WriteLine("|");
            }
        }

        // Перевантажений метод для знаходження максимального елемента матриці
        public override double FindMax()
        {
            double max = _matrix[0, 0];
            for (int i = 0; i < Container4.DIMENSION; i++)
            {
                for (int j = 0; j < Container4.DIMENSION; j++)
                {
                    if (_matrix[i, j] > max)
                        max = _matrix[i, j];
                }
            }
            return max;
        }
    }
    // Головний клас програми
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  Лабораторна робота 6: Абстрактні класи                      ║");
            Console.WriteLine("║  Демонстрація абстрактних класів та поліморфізму             ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝\n");
            try
            {
                // Демонстрація динамічного створення об'єктів
                DemonstrateDynamicPolymorphism();
                Console.WriteLine("\n" + new string('═', 65) + "\n");
                // Інтерактивний режим з динамічним вибором типу
                RunDynamicMode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            try
            {
                Console.ReadKey();
            }
            catch (InvalidOperationException)
            {
                // Ігноруємо помилку при перенаправленні вводу
            }
        }
        // Метод для демонстрації динамічного поліморфізму
        static void DemonstrateDynamicPolymorphism()
        {
            Console.WriteLine("📌 ДЕМОНСТРАЦІЯ ДИНАМІЧНОГО ПОЛІМОРФІЗМУ\n");
            Console.WriteLine("Створюємо масив покажчиків базового типу Vector4D,");
            Console.WriteLine("але фактичний тип об'єкта визначається динамічно!\n");
            // Масив покажчиків на базовий клас (тип невідомий на етапі компіляції)
            const int DEMO_COUNT = 4;
            Vector4D[] objects = new Vector4D[DEMO_COUNT];
            // Динамічне створення різних типів об'єктів
            Console.WriteLine("Створюємо об'єкти динамічно (тип визначається під час виконання):\n");
            // Об'єкт 1: Vector4D
            objects[0] = new Vector4D();
            objects[0].SetElements(new double[] { 1.5, 8.3, 3.7, 5.2 });
            Console.WriteLine("✓ Створено об'єкт типу Vector4D");
            // Об'єкт 2: Matrix
            objects[1] = new Matrix();
            ((Matrix)objects[1]).SetElements(new double[,] {
                { 2.1, 4.5, 1.8, 3.3 },
                { 7.2, 9.6, 2.4, 5.7 },
                { 1.1, 3.8, 12.5, 4.2 },
                { 6.3, 2.9, 8.1, 1.7 }
            });
            Console.WriteLine("✓ Створено об'єкт типу Matrix");
            // Об'єкт 3: Vector4D
            objects[2] = new Vector4D();
            objects[2].SetElements(new double[] { 10.5, 2.1, 15.8, 7.3 });
            Console.WriteLine("✓ Створено об'єкт типу Vector4D");
            // Об'єкт 4: Matrix
            objects[3] = new Matrix();
            ((Matrix)objects[3]).SetElements(new double[,] {
                { 5.5, 3.2, 8.8, 1.1 },
                { 9.9, 6.6, 4.4, 2.2 },
                { 11.1, 7.7, 13.3, 10.0 },
                { 3.3, 8.8, 5.5, 14.4 }
            });
            Console.WriteLine("✓ Створено об'єкт типу Matrix");
            Console.WriteLine("\n" + new string('-', 65));
            Console.WriteLine("УВАГА! Тип об'єкта невідомий на етапі компіляції!");
            Console.WriteLine("Віртуальні методи викликаються динамічно під час виконання.");
            Console.WriteLine(new string('-', 65) + "\n");
            // Обробка об'єктів через базовий клас (поліморфізм!)
            for (int i = 0; i < objects.Length; i++)
            {
                Console.WriteLine($"\n▶ Об'єкт #{i + 1} (фактичний тип: {objects[i].GetType().Name}):");
                Console.WriteLine(new string('-', 65));
                // Виклик віртуального методу Display() - тип визначається динамічно!
                objects[i].Display();
                // Виклик віртуального методу FindMax() - тип визначається динамічно!
                double max = objects[i].FindMax();
                Console.WriteLine($"Максимальний елемент: {max}");
                Console.WriteLine($"→ Викликано метод з класу: {objects[i].GetType().Name}");
            }
            Console.WriteLine("\n" + new string('═', 65));
            Console.WriteLine(" ВИСНОВОК: Віртуальні методи дозволяють викликати правильну");
            Console.WriteLine("   версію методу в залежності від фактичного типу об'єкта,");
            Console.WriteLine("   навіть якщо ми працюємо через покажчик базового класу!");
            Console.WriteLine(new string('═', 65));
        }
        // Інтерактивний режим з динамічним вибором типу об'єкта
        static void RunDynamicMode()
        {
            Console.WriteLine(" ІНТЕРАКТИВНИЙ РЕЖИМ З ДИНАМІЧНИМ ВИБОРОМ ТИПУ\n");
            List<Vector4D> dynamicObjects = new List<Vector4D>();
            bool continueAdding = true;
            while (continueAdding)
            {
                Console.WriteLine("\nВиберіть тип об'єкта для створення:");
                Console.WriteLine("1 - Вектор 4D");
                Console.WriteLine("2 - Матриця 4x4");
                Console.WriteLine("0 - Завершити додавання");
                Console.Write("Ваш вибір: ");
                string? choice = Console.ReadLine();
                // Динамічне створення об'єкта на основі вибору користувача
                Vector4D? newObject = null;
                switch (choice)
                {
                    case "1":
 Console.WriteLine("\n→ Динамічно створюємо об'єкт типу Vector4D...");
 newObject = CreateVector();
 Console.WriteLine("✓ Об'єкт Vector4D створено!");
                        break;
                    case "2":
Console.WriteLine("\n→ Динамічно створюємо об'єкт типу Matrix...");
                        newObject = CreateMatrix();
  Console.WriteLine("✓ Об'єкт Matrix створено!");
                        break;
                    case "0":
                        continueAdding = false;
                        break;
                    default:
                        Console.WriteLine("❌ Некоректний вибір!");
                        continue;
                }
                if (newObject != null)
                {
                    dynamicObjects.Add(newObject);
  Console.WriteLine($"Об'єкт додано до колекції (всього об'єктів: {dynamicObjects.Count})");
                }
            }
            if (dynamicObjects.Count > 0)
            {
  Console.WriteLine("\n" + new string('═', 65));
 Console.WriteLine($"ОБРОБКА {dynamicObjects.Count} ДИНАМІЧНО СТВОРЕНИХ ОБ'ЄКТІВ:");
                Console.WriteLine(new string('═', 65));
                double globalMax = double.MinValue;
                string maxObjectType = "";
                int maxObjectIndex = -1;
                // Обробка через базовий клас (поліморфізм!)
                for (int i = 0; i < dynamicObjects.Count; i++)
                {
 Console.WriteLine($"\n▶ Об'єкт #{i + 1} (тип: {dynamicObjects[i].GetType().Name}):");
                    Console.WriteLine(new string('-', 65));
                    // Виклик віртуальних методів
                    dynamicObjects[i].Display();
                    double max = dynamicObjects[i].FindMax();
                    Console.WriteLine($"Максимальний елемент: {max}");
                    if (max > globalMax)
                    {
                        globalMax = max;
          maxObjectType = dynamicObjects[i].GetType().Name;
                        maxObjectIndex = i + 1;
                    }
                }
                Console.WriteLine("\n" + new string('═', 65));
                Console.WriteLine("ГЛОБАЛЬНИЙ ПІДСУМОК:");
  Console.WriteLine($"Максимальний елемент серед усіх об'єктів: {globalMax}");
 Console.WriteLine($"Знайдено в об'єкті #{maxObjectIndex} (тип: {maxObjectType})");
                Console.WriteLine(new string('═', 65));
            }
            else
            {
   Console.WriteLine("\n Жодного об'єкта не було створено.");
            }
        }
        // Метод для динамічного створення вектора
        static Vector4D CreateVector()
        {
            Vector4D vector = new Vector4D();
            Console.WriteLine("Виберіть спосіб введення:");
            Console.WriteLine("1 - Ввести вручну");
            Console.WriteLine("2 - Використати випадкові значення");
            Console.Write("Ваш вибір: ");
            string? choice = Console.ReadLine();
            if (choice == "1")
            {
                vector.SetElements();
            }
            else
            {
                Random rand = new Random();
                double[] values = new double[Vector4D.DIMENSION];
                for (int i = 0; i < Vector4D.DIMENSION; i++)
                {
                    values[i] = Math.Round(rand.NextDouble() * 20, 2);
                }
                vector.SetElements(values);
                Console.WriteLine("Згенеровано випадкові значення.");
            }


            return vector;
        }
        // Метод для динамічного створення матриці
        static Matrix CreateMatrix()
        {
            Matrix matrix = new Matrix();
            Console.WriteLine("Виберіть спосіб введення:");
            Console.WriteLine("1 - Ввести вручну");
            Console.WriteLine("2 - Використати випадкові значення");
            Console.Write("Ваш вибір: ");
            string? choice = Console.ReadLine();
            if (choice == "1")
            {
                matrix.SetElements();
            }
            else
            {
                Random rand = new Random();
   double[,] values = new double[Vector4D.DIMENSION, Vector4D.DIMENSION];
                for (int i = 0; i < Vector4D.DIMENSION; i++)
                {
                    for (int j = 0; j < Vector4D.DIMENSION; j++)
                    {
           values[i, j] = Math.Round(rand.NextDouble() * 20, 2);
                    }
                }
                matrix.SetElements(values);
                Console.WriteLine("Згенеровано випадкові значення.");
            }
            return matrix;
        }
    }
}
