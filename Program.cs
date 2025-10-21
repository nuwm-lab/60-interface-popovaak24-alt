using System;
using System.Collections.Generic;
namespace Lab4
{
    // Інтерфейс для математичних операцій
    public interface IMathOperations
    {
        double FindMin();           // Знайти мінімальний елемент
        double FindMax();           // Знайти максимальний елемент
        double CalculateAverage();  // Обчислити середнє значення
        void MultiplyByScalar(double scalar);  // Помножити на скаляр
        void Normalize();           // Нормалізація (ділення на максимум)
        string GetStatistics();     // Отримати статистичну інформацію
    }
    
    // Абстрактний базовий клас для 4-вимірних геометричних об'єктів
    public abstract class Shape4D
    {
        public const int DIMENSION = 4; // Константа для розмірності
        protected string _name; // Назва об'єкта
        protected static int _objectCount = 0; // Лічильник створених об'єктів
        
        // Конструктор за замовчуванням
        protected Shape4D()
        {
            _name = "Unnamed Shape";
            _objectCount++;
            Console.WriteLine($"[Конструктор Shape4D] Створено абстрактний об'єкт '{_name}'. Всього об'єктів: {_objectCount}");
        }
        
        // Параметризований конструктор
        protected Shape4D(string name)
        {
            _name = name ?? "Unnamed Shape";
            _objectCount++;
            Console.WriteLine($"[Конструктор Shape4D] Створено абстрактний об'єкт '{_name}'. Всього об'єктів: {_objectCount}");
        }
        
        // Деструктор (фіналізатор)
        ~Shape4D()
        {
            _objectCount--;
            Console.WriteLine($"[Деструктор Shape4D] Знищено об'єкт '{_name}'. Залишилось об'єктів: {_objectCount}");
        }
        
        // Властивість для отримання назви
        public string Name
        {
            get { return _name; }
            set { _name = value ?? "Unnamed Shape"; }
        }
        
        // Статична властивість для отримання кількості об'єктів
        public static int ObjectCount
        {
            get { return _objectCount; }
        }
        
        // Абстрактні методи (обов'язкові для реалізації в похідних класах)
        public abstract void SetElements();
        public abstract void Display();
        public abstract double FindMax();
        public abstract double CalculateSum();
        public abstract string GetInfo();
    }
    
    // Клас одновимірного вектора розмірності 4
    public class Vector4D : Shape4D, IMathOperations
    {
        protected double[] _elements; // Масив елементів вектора
       
        // Конструктор за замовчуванням
        public Vector4D() : base("Vector4D")
        {
            _elements = new double[DIMENSION];
            Console.WriteLine($"[Конструктор Vector4D] Створено вектор з {DIMENSION} нульовими елементами");
        }
        
        // Параметризований конструктор (ініціалізація масивом)
        public Vector4D(double[] values) : base("Vector4D")
        {
            if (values == null || values.Length != DIMENSION)
                throw new ArgumentException($"Очікується масив довжини {DIMENSION}");
            
            _elements = new double[DIMENSION];
            Array.Copy(values, _elements, DIMENSION);
            Console.WriteLine($"[Конструктор Vector4D] Створено вектор з переданих значень: [{string.Join(", ", values)}]");
        }
        
        // Параметризований конструктор (ініціалізація одним значенням)
        public Vector4D(double value) : base("Vector4D")
        {
            _elements = new double[DIMENSION];
            for (int i = 0; i < DIMENSION; i++)
            {
                _elements[i] = value;
            }
            Console.WriteLine($"[Конструктор Vector4D] Створено вектор з однаковими елементами: {value}");
        }
        
        // Конструктор копіювання
        public Vector4D(Vector4D other) : base($"Copy of {other._name}")
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            
            _elements = new double[DIMENSION];
            Array.Copy(other._elements, _elements, DIMENSION);
            Console.WriteLine($"[Конструктор Vector4D] Створено копію вектора");
        }
        
        // Деструктор (фіналізатор)
        ~Vector4D()
        {
            Console.WriteLine($"[Деструктор Vector4D] Очищення вектора '{_name}'. Звільнення пам'яті для {DIMENSION} елементів");
        }
        // Реалізація абстрактного методу для задання елементів вектора
        public override void SetElements()
        {
            Console.WriteLine($"Введіть {DIMENSION} елементи вектора:");
            for (int i = 0; i < DIMENSION; i++)
            {
                Console.Write($"Елемент [{i}]: ");
                while (!double.TryParse(Console.ReadLine(), out _elements[i]))
                {
                    Console.Write("Некоректне значення. Введіть число: ");
                }
            }
        }
        // Метод для задання елементів з масиву (для тестування)
        public virtual void SetElements(double[] values)
        {
            if (values is null)
                throw new ArgumentNullException(nameof(values));
            if (values.Length != DIMENSION)
                throw new ArgumentException($"Очікується масив довжини {DIMENSION}.", nameof(values));
            for (int i = 0; i < DIMENSION; i++)
            {
                _elements[i] = values[i];
            }
        }
        // Реалізація абстрактного методу для виведення вектора на екран
        public override void Display()
        {
            Console.Write("Вектор: [");
            for (int i = 0; i < DIMENSION; i++)
            {
                Console.Write(_elements[i]);
                if (i < DIMENSION - 1) Console.Write(", ");
            }
            Console.WriteLine("]");
        }
        // Реалізація абстрактного методу для знаходження максимального елемента
        public override double FindMax()
        {
            double max = _elements[0];
            for (int i = 1; i < DIMENSION; i++)
            {
                if (_elements[i] > max)
                    max = _elements[i];
            }
            return max;
        }
        
        // Реалізація абстрактного методу для обчислення суми елементів
        public override double CalculateSum()
        {
            double sum = 0;
            for (int i = 0; i < DIMENSION; i++)
            {
                sum += _elements[i];
            }
            return sum;
        }
        
        // Реалізація абстрактного методу для отримання інформації про об'єкт
        public override string GetInfo()
        {
            return $"Тип: {GetType().Name}, Назва: {_name}, Розмірність: {DIMENSION}, Елементів: {DIMENSION}";
        }
        
        // ============= Реалізація інтерфейсу IMathOperations =============
        
        // Знаходження мінімального елемента
        public double FindMin()
        {
            double min = _elements[0];
            for (int i = 1; i < DIMENSION; i++)
            {
                if (_elements[i] < min)
                    min = _elements[i];
            }
            return min;
        }
        
        // Обчислення середнього значення
        public double CalculateAverage()
        {
            return CalculateSum() / DIMENSION;
        }
        
        // Множення всіх елементів на скаляр
        public void MultiplyByScalar(double scalar)
        {
            for (int i = 0; i < DIMENSION; i++)
            {
                _elements[i] *= scalar;
            }
            Console.WriteLine($"[Vector4D] Вектор помножено на скаляр {scalar}");
        }
        
        // Нормалізація (ділення всіх елементів на максимум)
        public void Normalize()
        {
            double max = FindMax();
            if (max != 0)
            {
                for (int i = 0; i < DIMENSION; i++)
                {
                    _elements[i] /= max;
                }
                Console.WriteLine($"[Vector4D] Вектор нормалізовано (поділено на максимум: {max})");
            }
            else
            {
                Console.WriteLine("[Vector4D] Неможливо нормалізувати: максимум = 0");
            }
        }
        
        // Отримання статистичної інформації
        public string GetStatistics()
        {
            return $"📊 Статистика вектора:\n" +
                   $"   • Мінімум: {FindMin():F2}\n" +
                   $"   • Максимум: {FindMax():F2}\n" +
                   $"   • Сума: {CalculateSum():F2}\n" +
                   $"   • Середнє: {CalculateAverage():F2}\n" +
                   $"   • Кількість елементів: {DIMENSION}";
        }
    }
    // Похідний клас матриці 4x4
    public class Matrix : Vector4D, IMathOperations
    {
        private double[,] _matrix; // Двовимірний масив для матриці
        
        // Конструктор за замовчуванням
        public Matrix() : base()
        {
            _matrix = new double[DIMENSION, DIMENSION];
            _name = "Matrix4x4";
            Console.WriteLine($"[Конструктор Matrix] Створено матрицю {DIMENSION}x{DIMENSION} з нульовими елементами");
        }
        
        // Параметризований конструктор (ініціалізація двовимірним масивом)
        public Matrix(double[,] values) : base()
        {
            if (values == null || values.GetLength(0) != DIMENSION || values.GetLength(1) != DIMENSION)
                throw new ArgumentException($"Очікується матриця розміру {DIMENSION}x{DIMENSION}");
            
            _matrix = new double[DIMENSION, DIMENSION];
            Array.Copy(values, _matrix, DIMENSION * DIMENSION);
            _name = "Matrix4x4";
            Console.WriteLine($"[Конструктор Matrix] Створено матрицю {DIMENSION}x{DIMENSION} з переданих значень");
        }
        
        // Параметризований конструктор (діагональна матриця)
        public Matrix(double diagonalValue) : base()
        {
            _matrix = new double[DIMENSION, DIMENSION];
            for (int i = 0; i < DIMENSION; i++)
            {
                _matrix[i, i] = diagonalValue;
            }
            _name = "Diagonal Matrix";
            Console.WriteLine($"[Конструктор Matrix] Створено діагональну матрицю з значенням: {diagonalValue}");
        }
        
        // Конструктор копіювання
        public Matrix(Matrix other) : base()
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            
            _matrix = new double[DIMENSION, DIMENSION];
            Array.Copy(other._matrix, _matrix, DIMENSION * DIMENSION);
            _name = $"Copy of {other._name}";
            Console.WriteLine($"[Конструктор Matrix] Створено копію матриці");
        }
        
        // Деструктор (фіналізатор)
        ~Matrix()
        {
            Console.WriteLine($"[Деструктор Matrix] Очищення матриці '{_name}'. Звільнення пам'яті для {DIMENSION * DIMENSION} елементів");
        }
        // Перевантажений метод для задання елементів матриці
        public override void SetElements()
        {
            Console.WriteLine($"Введіть елементи матриці {DIMENSION}x{DIMENSION}:");
            for (int i = 0; i < DIMENSION; i++)
            {
                for (int j = 0; j < DIMENSION; j++)
                {
                    Console.Write($"Елемент [{i},{j}]: ");
                    while (!double.TryParse(Console.ReadLine(), out _matrix[i, j]))
                    {
                        Console.Write("Некоректне значення. Введіть число: ");
                    }
                }
            }
        }
        // Метод для задання елементів з двовимірного масиву (для тестування)
        public void SetElements(double[,] values)
        {
            if (values is null)
                throw new ArgumentNullException(nameof(values));
            if (values.GetLength(0) != DIMENSION || values.GetLength(1) != DIMENSION)
                throw new ArgumentException($"Очікується матриця розміру {DIMENSION}x{DIMENSION}.", nameof(values));
            for (int i = 0; i < DIMENSION; i++)
            {
                for (int j = 0; j < DIMENSION; j++)
                {
                    _matrix[i, j] = values[i, j];
                }
            }
        }
        // Перевантажений метод для виведення матриці на екран
        public override void Display()
        {
            Console.WriteLine($"Матриця {DIMENSION}x{DIMENSION}:");
            for (int i = 0; i < DIMENSION; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < DIMENSION; j++)
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
            for (int i = 0; i < DIMENSION; i++)
            {
                for (int j = 0; j < DIMENSION; j++)
                {
                    if (_matrix[i, j] > max)
                        max = _matrix[i, j];
                }
            }
            return max;
        }
        
        // Перевизначення методу для обчислення суми всіх елементів матриці
        public override double CalculateSum()
        {
            double sum = 0;
            for (int i = 0; i < DIMENSION; i++)
            {
                for (int j = 0; j < DIMENSION; j++)
                {
                    sum += _matrix[i, j];
                }
            }
            return sum;
        }
        
        // Перевизначення методу для отримання інформації про матрицю
        public override string GetInfo()
        {
            return $"Тип: {GetType().Name}, Назва: {_name}, Розмірність: {DIMENSION}x{DIMENSION}, Елементів: {DIMENSION * DIMENSION}";
        }
        
        // ============= Реалізація інтерфейсу IMathOperations =============
        
        // Знаходження мінімального елемента матриці
        public new double FindMin()
        {
            double min = _matrix[0, 0];
            for (int i = 0; i < DIMENSION; i++)
            {
                for (int j = 0; j < DIMENSION; j++)
                {
                    if (_matrix[i, j] < min)
                        min = _matrix[i, j];
                }
            }
            return min;
        }
        
        // Обчислення середнього значення елементів матриці
        public new double CalculateAverage()
        {
            return CalculateSum() / (DIMENSION * DIMENSION);
        }
        
        // Множення всіх елементів матриці на скаляр
        public new void MultiplyByScalar(double scalar)
        {
            for (int i = 0; i < DIMENSION; i++)
            {
                for (int j = 0; j < DIMENSION; j++)
                {
                    _matrix[i, j] *= scalar;
                }
            }
            Console.WriteLine($"[Matrix] Матриця помножена на скаляр {scalar}");
        }
        
        // Нормалізація матриці (ділення на максимум)
        public new void Normalize()
        {
            double max = FindMax();
            if (max != 0)
            {
                for (int i = 0; i < DIMENSION; i++)
                {
                    for (int j = 0; j < DIMENSION; j++)
                    {
                        _matrix[i, j] /= max;
                    }
                }
                Console.WriteLine($"[Matrix] Матрицю нормалізовано (поділено на максимум: {max})");
            }
            else
            {
                Console.WriteLine("[Matrix] Неможливо нормалізувати: максимум = 0");
            }
        }
        
        // Отримання статистичної інформації про матрицю
        public new string GetStatistics()
        {
            return $"📊 Статистика матриці:\n" +
                   $"   • Мінімум: {FindMin():F2}\n" +
                   $"   • Максимум: {FindMax():F2}\n" +
                   $"   • Сума: {CalculateSum():F2}\n" +
                   $"   • Середнє: {CalculateAverage():F2}\n" +
                   $"   • Розмір: {DIMENSION}x{DIMENSION}\n" +
                   $"   • Кількість елементів: {DIMENSION * DIMENSION}";
        }
    }
    // Головний клас програми
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  Лабораторна робота 6: Абстрактні класи та інтерфейси       ║");
            Console.WriteLine("║  Демонстрація конструкторів, деструкторів та інтерфейсів    ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝\n");
            try
            {
                // Демонстрація роботи з інтерфейсом
                DemonstrateInterfaceUsage();
                
                Console.WriteLine("\n" + new string('═', 65) + "\n");
                
                // Демонстрація конструкторів та деструкторів
                DemonstrateConstructorsAndDestructors();
                
                Console.WriteLine("\n" + new string('═', 65) + "\n");
                
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
        
        // Метод для демонстрації роботи з інтерфейсом IMathOperations
        static void DemonstrateInterfaceUsage()
        {
            Console.WriteLine("📌 ДЕМОНСТРАЦІЯ РОБОТИ З ІНТЕРФЕЙСОМ IMathOperations\n");
            Console.WriteLine(new string('═', 65));
            
            // Створення вектора
            Console.WriteLine("\n🔹 РОБОТА З ВЕКТОРОМ:");
            Console.WriteLine(new string('-', 65));
            Vector4D vector = new Vector4D(new double[] { 2.5, 8.0, 3.5, 6.0 });
            vector.Display();
            
            // Використання методів інтерфейсу через змінну типу Vector4D
            Console.WriteLine("\n📊 Статистичні дані:");
            Console.WriteLine(vector.GetStatistics());
            
            // Множення на скаляр
            Console.WriteLine("\n🔢 Множення вектора на скаляр 2.0:");
            vector.MultiplyByScalar(2.0);
            vector.Display();
            
            // Нормалізація
            Console.WriteLine("\n📐 Нормалізація вектора:");
            vector.Normalize();
            vector.Display();
            Console.WriteLine($"Перевірка: максимум після нормалізації = {vector.FindMax():F2}");
            
            // Створення матриці
            Console.WriteLine("\n" + new string('═', 65));
            Console.WriteLine("\n🔹 РОБОТА З МАТРИЦЕЮ:");
            Console.WriteLine(new string('-', 65));
            Matrix matrix = new Matrix(new double[,] {
                { 1.0, 4.0, 2.0, 3.0 },
                { 8.0, 5.0, 6.0, 7.0 },
                { 9.0, 12.0, 10.0, 11.0 },
                { 13.0, 16.0, 14.0, 15.0 }
            });
            matrix.Display();
            
            // Використання методів інтерфейсу через змінну типу Matrix
            Console.WriteLine("\n📊 Статистичні дані:");
            Console.WriteLine(matrix.GetStatistics());
            
            // Множення на скаляр
            Console.WriteLine("\n🔢 Множення матриці на скаляр 0.5:");
            matrix.MultiplyByScalar(0.5);
            matrix.Display();
            
            // Нормалізація
            Console.WriteLine("\n📐 Нормалізація матриці:");
            matrix.Normalize();
            matrix.Display();
            Console.WriteLine($"Перевірка: максимум після нормалізації = {matrix.FindMax():F2}");
            
            // Демонстрація поліморфізму через інтерфейс
            Console.WriteLine("\n" + new string('═', 65));
            Console.WriteLine("\n🔸 ПОЛІМОРФІЗМ ЧЕРЕЗ ІНТЕРФЕЙС:");
            Console.WriteLine(new string('-', 65));
            
            // Масив інтерфейсів
            IMathOperations[] operations = new IMathOperations[2];
            operations[0] = new Vector4D(new double[] { 10, 20, 30, 40 });
            operations[1] = new Matrix(5.0); // Діагональна матриця
            
            Console.WriteLine("\nОбробка різних об'єктів через інтерфейс IMathOperations:\n");
            
            for (int i = 0; i < operations.Length; i++)
            {
                Console.WriteLine($"▶ Об'єкт #{i + 1} (тип: {operations[i].GetType().Name}):");
                Console.WriteLine(new string('-', 65));
                
                // Виклик методів через інтерфейс
                Console.WriteLine($"Мінімум: {operations[i].FindMin():F2}");
                Console.WriteLine($"Максимум: {operations[i].FindMax():F2}");
                Console.WriteLine($"Середнє: {operations[i].CalculateAverage():F2}");
                Console.WriteLine();
            }
            
            Console.WriteLine(new string('═', 65));
            Console.WriteLine("✅ ВИСНОВОК: Інтерфейс IMathOperations дозволяє працювати з");
            Console.WriteLine("   різними типами об'єктів через єдиний інтерфейс!");
            Console.WriteLine(new string('═', 65));
        }
        
        // Метод для демонстрації конструкторів та деструкторів
        static void DemonstrateConstructorsAndDestructors()
        {
            Console.WriteLine("📌 ДЕМОНСТРАЦІЯ КОНСТРУКТОРІВ ТА ДЕСТРУКТОРІВ\n");
            Console.WriteLine(new string('═', 65));
            
            // Демонстрація різних конструкторів Vector4D
            Console.WriteLine("\n🔹 ДЕМОНСТРАЦІЯ КОНСТРУКТОРІВ КЛАСУ Vector4D:");
            Console.WriteLine(new string('-', 65));
            
            Console.WriteLine("\n1️⃣ Конструктор за замовчуванням:");
            Vector4D v1 = new Vector4D();
            v1.Display();
            Console.WriteLine(v1.GetInfo());
            
            Console.WriteLine("\n2️⃣ Параметризований конструктор (з масивом):");
            Vector4D v2 = new Vector4D(new double[] { 1.5, 2.3, 4.7, 3.9 });
            v2.Display();
            Console.WriteLine($"Сума елементів: {v2.CalculateSum():F2}");
            Console.WriteLine($"Максимальний елемент: {v2.FindMax():F2}");
            
            Console.WriteLine("\n3️⃣ Параметризований конструктор (одне значення):");
            Vector4D v3 = new Vector4D(5.0);
            v3.Display();
            Console.WriteLine(v3.GetInfo());
            
            Console.WriteLine("\n4️⃣ Конструктор копіювання:");
            Vector4D v4 = new Vector4D(v2);
            v4.Display();
            Console.WriteLine($"Це копія вектора v2");
            
            // Демонстрація різних конструкторів Matrix
            Console.WriteLine("\n" + new string('═', 65));
            Console.WriteLine("\n🔹 ДЕМОНСТРАЦІЯ КОНСТРУКТОРІВ КЛАСУ Matrix:");
            Console.WriteLine(new string('-', 65));
            
            Console.WriteLine("\n1️⃣ Конструктор за замовчуванням:");
            Matrix m1 = new Matrix();
            m1.Display();
            Console.WriteLine(m1.GetInfo());
            
            Console.WriteLine("\n2️⃣ Параметризований конструктор (з двовимірним масивом):");
            Matrix m2 = new Matrix(new double[,] {
                { 1.5, 2.0, 3.5, 4.0 },
                { 5.5, 6.0, 7.5, 8.0 },
                { 9.5, 10.0, 11.5, 12.0 },
                { 13.5, 14.0, 15.5, 16.0 }
            });
            m2.Display();
            Console.WriteLine($"Сума елементів: {m2.CalculateSum():F2}");
            Console.WriteLine($"Максимальний елемент: {m2.FindMax():F2}");
            
            Console.WriteLine("\n3️⃣ Параметризований конструктор (діагональна матриця):");
            Matrix m3 = new Matrix(7.0);
            m3.Display();
            Console.WriteLine(m3.GetInfo());
            
            Console.WriteLine("\n4️⃣ Конструктор копіювання:");
            Matrix m4 = new Matrix(m2);
            m4.Display();
            Console.WriteLine($"Це копія матриці m2");
            
            // Демонстрація статичного лічильника об'єктів
            Console.WriteLine("\n" + new string('═', 65));
            Console.WriteLine($"📊 СТАТИСТИКА: Всього створено об'єктів Shape4D: {Shape4D.ObjectCount}");
            Console.WriteLine(new string('═', 65));
            
            Console.WriteLine("\n⚠️ Деструктори будуть викликані автоматично при завершенні програми");
            Console.WriteLine("    або коли об'єкти будуть видалені збирачем сміття (Garbage Collector)\n");
            
            // Примусово викликаємо збирач сміття для демонстрації деструкторів
            Console.WriteLine("🗑️ Викликаємо збирач сміття для демонстрації деструкторів...");
            v1 = null!;
            v2 = null!;
            v3 = null!;
            v4 = null!;
            m1 = null!;
            m2 = null!;
            m3 = null!;
            m4 = null!;
            
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            Console.WriteLine("\n✅ Збирач сміття завершив роботу");
            Console.WriteLine($"📊 Об'єктів залишилось: {Shape4D.ObjectCount}");
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
