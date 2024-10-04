using System;

class SudokuSolver
{
    // Size of the grid (9x9)
    private const int GRID_SIZE = 9;

    public static void Main(string[] args)
    {
        // Example Sudoku puzzle (0 represents an empty cell)
        int[,] board = new int[GRID_SIZE, GRID_SIZE]
        {
            { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
            { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
            { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
            { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
            { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
            { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
            { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
            { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
            { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
        };

        Console.WriteLine("Sudoku Puzzle:");
        PrintBoard(board);

        if (SolveSudoku(board))
        {
            Console.WriteLine("\nSolved Sudoku:");
            PrintBoard(board);
        }
        else
        {
            Console.WriteLine("No solution exists.");
        }
    }

    // Function to print the board
    private static void PrintBoard(int[,] board)
    {
        for (int row = 0; row < GRID_SIZE; row++)
        {
            for (int col = 0; col < GRID_SIZE; col++)
            {
                Console.Write(board[row, col] + " ");
            }
            Console.WriteLine();
        }
    }

    // Function to check if a number can be placed in a given position
    private static bool IsValidPlacement(int[,] board, int num, int row, int col)
    {
        // Check the row
        for (int i = 0; i < GRID_SIZE; i++)
        {
            if (board[row, i] == num)
                return false;
        }

        // Check the column
        for (int i = 0; i < GRID_SIZE; i++)
        {
            if (board[i, col] == num)
                return false;
        }

        // Check the 3x3 subgrid
        int subgridRowStart = row / 3 * 3;
        int subgridColStart = col / 3 * 3;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[subgridRowStart + i, subgridColStart + j] == num)
                    return false;
            }
        }

        return true;
    }

    // Backtracking function to solve the Sudoku puzzle
    private static bool SolveSudoku(int[,] board)
    {
        for (int row = 0; row < GRID_SIZE; row++)
        {
            for (int col = 0; col < GRID_SIZE; col++)
            {
                // Check if the current cell is empty (0)
                if (board[row, col] == 0)
                {
                    // Try placing numbers 1-9 in the empty cell
                    for (int num = 1; num <= 9; num++)
                    {
                        // Check if it's valid to place the number
                        if (IsValidPlacement(board, num, row, col))
                        {
                            board[row, col] = num;

                            // Recursively try to solve the rest of the puzzle
                            if (SolveSudoku(board))
                            {
                                return true;
                            }

                            // If placing the number doesn't lead to a solution, backtrack
                            board[row, col] = 0;
                        }
                    }

                    // If no number can be placed in this cell, return false to trigger backtracking
                    return false;
                }
            }
        }

        // If the board is filled without issues, the puzzle is solved
        return true;
    }
}
