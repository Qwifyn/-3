
using System;

class Program
{
    static void Main()
    {
        char[,] map = new char[10, 10];

        string[] layout =
        {
            "##########",
            "#...K....#",
            "#.#.##...#",
            "#H.....###",
            "#..###.D.#",
            "#G...#...#",
            "####.#...#",
            "#G...G...#",
            "#EG..H...#",
            "##########"
        };

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                map[i, j] = layout[i][j];
            }
        }

        int x = 1;
        int y = 1;

        int health = 20;
        int maxHealth = 60;
        int turns = 0;
        int maxTurns = 30;

        bool hasKey = false;
        bool hasDagger = false;
        bool gameOver = false;
        bool won = false;

        while (gameOver == false)
        {
            Console.WriteLine("=== ПОБЕГ ИЗ ТЮРЬМЫ ===");
            Console.WriteLine("Здоровье: " + health + "/" + maxHealth);
            Console.WriteLine("Ходы: " + turns + "/" + maxTurns);
            Console.WriteLine("Ключ: " + (hasKey ? "Есть" : "Нет"));
            Console.WriteLine("Заточка: " + (hasDagger ? "Есть" : "Нет"));
            Console.WriteLine();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == y && j == x)
                    {
                        Console.Write("P ");
                    }
                    else
                    {
                        Console.Write(map[i, j] + " ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("W - вверх");
            Console.WriteLine("S - вниз");
            Console.WriteLine("A - влево");
            Console.WriteLine("D - вправо");
            Console.WriteLine("Q - выход");
            Console.Write("Твой ход: ");
            string command = Console.ReadLine();

            if (command == null)
            {
                command = "";
            }

            command = command.ToLower();

            if (command == "q" || command == "exit")
            {
                gameOver = true;
                break;
            }

            int newX = x;
            int newY = y;

            switch (command)
            {
                case "w":
                    newY--;
                    break;

                case "s":
                    newY++;
                    break;

                case "a":
                    newX--;
                    break;

                case "d":
                    newX++;
                    break;

                default:
                    Console.WriteLine("Неправильная команда");
                    Console.ReadKey();
                    continue;
            }

            if (newX < 0 || newX >= 10)
            {
                Console.WriteLine("Нельзя выйти за границы карты");
                Console.ReadKey();
                continue;
            }

            if (newY < 0 || newY >= 10)
            {
                Console.WriteLine("Нельзя выйти за границы карты");
                Console.ReadKey();
                continue;
            }

            if (map[newY, newX] == '#')
            {
                Console.WriteLine("Здесь стена");
                Console.ReadKey();
                continue;
            }

            if (map[newY, newX] == 'E' && hasKey == false)
            {
                Console.WriteLine("Сначала найди ключ");
                Console.ReadKey();
                continue;
            }

            if (map[newY, newX] == 'G')
            {
                if (hasDagger == true)
                {
                    Console.WriteLine("Ты победил охранника!");
                    map[newY, newX] = '.';
                    x = newX;
                    y = newY;
                }
                else
                {
                    health = health - 20;

                    if (health < 0)
                    {
                        health = 0;
                    }

                    Console.WriteLine("У тебя нет заточки");
                    Console.WriteLine("Охранник оттолкнул тебя назад");

                    turns++;

                    if (health <= 0)
                    {
                        gameOver = true;
                    }

                    if (turns >= maxTurns)
                    {
                        gameOver = true;
                    }

                    Console.ReadKey();
                    continue;
                }
            }
            else
            {
                x = newX;
                y = newY;
            }

            turns++;

            if (map[y, x] == 'K')
            {
                hasKey = true;
                map[y, x] = '.';
                Console.WriteLine("Ты нашел ключ");
            }
            else if (map[y, x] == 'D')
            {
                hasDagger = true;
                map[y, x] = '.';
                Console.WriteLine("Ты нашел заточку");
            }
            else if (map[y, x] == 'H')
            {
                health = health + 20;

                if (health > maxHealth)
                {
                    health = maxHealth;
                }

                map[y, x] = '.';
                Console.WriteLine("Ты нашел аптечку");
            }
            else
            {
                Console.WriteLine("Ты продолжаешь искать выход");
            }

            if (health <= 0)
            {
                health = 0;
                gameOver = true;
            }
            else if (health <= 5)
            {
                Console.WriteLine("Осторожно! Мало здоровья");
            }
            else
            {
                Console.WriteLine("Ты еще держишься");
            }

            if (map[y, x] == 'E' && hasKey == true)
            {
                won = true;
                gameOver = true;
            }

            if (turns >= maxTurns && won == false)
            {
                gameOver = true;
            }

            if (gameOver == false)
            {
                Console.ReadKey();
            }
        }

        Console.Clear();

        if (won == true)
        {
            Console.WriteLine("Победа! Ты сбежал из тюрьмы");
        }
        else if (health <= 0)
        {
            Console.WriteLine("Ты проиграл! Здоровье закончилось");
        }
        else if (turns >= maxTurns)
        {
            Console.WriteLine("Время вышло! Ты не успел сбежать");
        }
        else
        {
            Console.WriteLine("Ты вышел из игры");
        }

        Console.WriteLine("Здоровье: " + health);
        Console.WriteLine("Количество ходов: " + turns);
        Console.WriteLine("Игра окончена!");
    }
}
