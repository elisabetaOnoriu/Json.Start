﻿namespace SpreadsheetConsole
{
    class Program
    {
        static int selectedRow = 1;
        static int selectedCol = 1;
        static bool isEditing = false;
        const int defaultSize = 11;
        const string defaultCell = "|________";
        static string[,] table = new string[defaultSize, defaultSize];
        
        static void Main(string[] args)
        {
            BuildTable();
            NavigateThroughCells();
        }

        static void BuildTable ()
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

                Console.WriteLine("\n");
            }
        }

        static void NavigateThroughCells()
        {
            ConsoleKeyInfo keyInfo;          
            do
            {
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
                        Console.Write(table[selectedRow, selectedCol]);
                        break;
                }
            } 
            while (keyInfo.Key != ConsoleKey.Escape);
        }

        private static void SetBackgroundAndForegroundColor(int i, int j)
        {
            if (CellHasToBeHighlighted(i, j) )
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static bool CellHasToBeHighlighted(int i, int j) => IsSelectedCell(i, j) || IsHeader(i, j);

        private static bool IsSelectedCell(int i, int j) => i == selectedRow && j == selectedCol;

        private static bool IsHeader(int i, int j) => i == 0 || j == 0;

        static void SetHeaders(int i)
        {
            table[i, 0] = $"{i}".PadRight(5);
            table[0, i] = $"    {(char)('A' + i - 1)}    ";
            table[i, defaultSize - 1] = "|_______|";
        }

        private static bool EditSelectedCell(int i, int j)
        {
            if (isEditing && IsSelectedCell(i, j))
            {
                table[i, j] = "|" + Console.ReadLine().PadRight(8);
                Console.SetCursorPosition(j * 8 + j - 4, i * 2);                                              
                Console.Write(table[i, j]);
                isEditing = false;
                return true;
            }

            return false;
        }
    }
}
