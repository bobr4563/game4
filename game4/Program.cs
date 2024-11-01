namespace game4;
using System;
using System.Threading;

class Program
{
    static void Main()
    {
        int position = 10; // Позиция самолета
        int obstaclePosition = 20; // Позиция препятствия
        int score = 0;
        Random random = new Random();
        bool gameRunning = true;

        Console.WriteLine("Добро пожаловать в игру 'Полёт на самолёте'!");
        Console.WriteLine("Управляйте самолётом с помощью клавиш W (вверх) и S (вниз).");
        Console.WriteLine("Избегайте препятствий! Нажмите 'Q' для выхода.");

        Thread inputThread = new Thread(() =>
        {
            while (gameRunning)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.W && position > 0) position--;
                    if (key == ConsoleKey.S && position < 20) position++;
                    if (key == ConsoleKey.Q) gameRunning = false;
                }
            }
        });

        inputThread.Start();

        while (gameRunning)
        {
            Console.Clear();

            // Генерируем препятствие
            obstaclePosition = random.Next(0, 20);

            for (int i = 0; i < 20; i++)
            {
                if (i == position)
                    Console.Write("✈"); // Символ самолета
                else if (i == obstaclePosition)
                    Console.Write("X"); // Символ препятствия
                else
                    Console.Write(" ");
            }

            Console.WriteLine($"\nСчет: {score}");
            score++;

            // Проверяем на столкновение
            if (position == obstaclePosition)
            {
                Console.Clear();
                Console.WriteLine("Вы столкнулись с препятствием! Игра окончена.");
                Console.WriteLine($"Ваш финальный счет: {score}");
                gameRunning = false;
            }

            Thread.Sleep(1000); // Задержка между итерациями
        }
    }
}
