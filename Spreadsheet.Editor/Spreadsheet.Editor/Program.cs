﻿namespace SpreadsheetConsole
{
    class Program
    {
        static int selectedRow = 1;
        static int selectedCol = 1;
        static bool isEditing = false;
        const int defaultSize = 21;
        const int cellSize = 9;
        const string defaultCell = "         ";
        static string[,] table = new string[defaultSize, defaultSize];

        static void Main(string[] args)
        {
            BuildTable();
            ConfigureLayout();
            NavigateThroughCells();
            Console.ResetColor();
        }

        static void ConfigureLayout()
        {
            Console.BufferWidth =  Math.Max(defaultSize * cellSize - 4, Console.WindowWidth);
        }

        static void BuildTable()
        {
            for (int i = 1; i < defaultSize; i++)
            {
                for (int j = 1; j < defaultSize; j++)
                {
                    table[i, j] = defaultCell;
                }

                SetHeaders(i);
            }

            table[0, 0] = "     ";
        }

        static void GenerateTable(string[,] table)
        {

            for (int i = 0; i < defaultSize; i++)
            {
                for (int j = 0; j < defaultSize; j++)
                {
                    SetBackgroundAndForegroundColor(i, j);
                    if (!EditSelectedCell(i, j))
                    {
                        Console.Write(table[i, j]);
                    }
                }

                Console.WriteLine();
            }
        }

        static void NavigateThroughCells()
        {
            ConsoleKeyInfo keyInfo;
            do
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Clear();
                GenerateTable(table);
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedRow > 0)
                            selectedRow--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedRow < defaultSize - 1)
                            selectedRow++;
                        break;

                    case ConsoleKey.LeftArrow:
                        if (selectedCol > 0)
                            selectedCol--;
                        break;

                    case ConsoleKey.RightArrow:
                        if (selectedCol < defaultSize - 1)
                            selectedCol++;
                        break;

                    case ConsoleKey.Enter:
                        isEditing = true;
                        Console.SetCursorPosition(selectedRow * 8 + selectedRow - 4, selectedCol);
                        Console.Write(table[selectedRow, selectedCol]);
                        break;
                }
            }
            while (keyInfo.Key != ConsoleKey.Escape);
        }

        private static void SetBackgroundAndForegroundColor(int i, int j)
        {
            if (CellHasToBeHighlighted(i, j))
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static bool CellHasToBeHighlighted(int i, int j)
        {
            bool highlight = IsHeader(i, j);
            if (highlight && i == selectedRow)
            {
                highlight = false;
            }

            return IsSelectedCell(i, j) || highlight;
        }

        private static bool IsSelectedCell(int i, int j) => i == selectedRow && j == selectedCol;

        private static bool IsHeader(int i, int j) => i == 0 || j == 0;

        static void SetHeaders(int i)
        {
            table[i, 0] = $"{i}".PadLeft(5);
            table[0, i] = $"    {(char)('A' + i - 1)}    ";
            table[i, defaultSize - 1] = "         ";
        }

        private static bool EditSelectedCell(int i, int j)
        {
            if (isEditing && IsSelectedCell(i, j))
            {
                table[i, j] = " " + Console.ReadLine().PadRight(8);
                Console.SetCursorPosition(j * 8 + j - 4, i);
                Console.Write(table[i, j]);
                isEditing = false;
                return true;
            }

            return false;
        }
    }
}