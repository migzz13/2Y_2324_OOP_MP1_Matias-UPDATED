using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Y_2324_OOP_MP1_Matias
{
    internal class Program
    {
        static char[,] board;
        static int _gameCount = 0;
        static int _playerScore = 0;
        static int _computerScore = 0;
        static int _consecutiveDraws = 0;
        static Random _rand = new Random();

        static void Main(string[] args)
        {
            do
            {
                InitializeBoard();
                DisplayBoard();

                bool gameWon = false;
                bool draw = false;
                bool playerTurn = (_gameCount % 2 == 0);

                do
                {
                    if (playerTurn)
                    {
                        MakeMove('X');
                    }
                    else
                    {
                        if (!draw)
                        {
                            MakeComputerMove();
                        }
                    }

                    DisplayBoard();
                    gameWon = CheckWin();
                    draw = CheckDraw();

                    if (gameWon)
                    {
                        Console.WriteLine(playerTurn ? "Player wins!" : "Computer wins!");
                        if (playerTurn)
                        {
                            _playerScore++;
                        }
                        else
                        {
                            _computerScore++;
                        }
                    }
                    else if (draw)
                    {
                        Console.WriteLine("It's a draw!");
                        _consecutiveDraws++;
                    }

                    playerTurn = !playerTurn;

                } while (!gameWon && !draw);

                _gameCount++;

                if (_playerScore == 3 || _computerScore == 3 || _consecutiveDraws == 5)
                {
                    break;
                }

                Console.WriteLine("Press any key to continue to the next game...");
                Console.ReadKey();

            } while (true);

            Console.WriteLine("Game Over!");
            Console.WriteLine($"Player Score: {_playerScore} | Computer Score: {_computerScore}");
            Console.ReadKey();
        }

        static void MakeMove(char symbol)
        {
            bool validMove = false;
            int row = 0;
            int col = 0;

            do
            {
                string input = Console.ReadLine();

                if (IsValidMoveFormat(input))
                {
                    string[] coordinates = input.Split('-');
                    if (int.TryParse(coordinates[0], out row) && int.TryParse(coordinates[1], out col) && row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == ' ')
                    {
                        board[row, col] = symbol;
                        validMove = true;
                    }
                }

                if (!validMove)
                {
                    Console.WriteLine("Invalid move. Please try again.");
                }
            } while (!validMove);
        }

        static void MakeComputerMove()
        {
            int row, col;

            do
            {
                row = _rand.Next(3);
                col = _rand.Next(3);
            } while (board[row, col] != ' ');

            board[row, col] = 'O';
        }

        static void InitializeBoard()
        {
            board = new char[3, 3];

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }


        static void DisplayBoard()
        {
            Console.Clear();
            Console.WriteLine("     0   1   2");
            Console.WriteLine("   -------------");
            for (int row = 0; row < 3; row++)
            {
                Console.Write($" {row} |");
                for (int col = 0; col < 3; col++)
                {
                    Console.Write($" {board[row, col]} |");
                }
                Console.WriteLine("\n   -------------");
            }

            Console.WriteLine($"Player Score: {_playerScore} | Computer Score: {_computerScore}");
            Console.WriteLine("Please enter valid coordinates using the following format X-Y");
            Console.WriteLine("X is the column number (0-2)");
            Console.WriteLine("Y is the row number (0-2)");
        }

        static void PlayGame()
        {
            bool gameWon = false;
            bool draw = false;
            bool playerTurn = (_gameCount % 2 == 0);

            do
            {
                if (playerTurn)
                {
                    GetUserMove();
                }
                else
                {
                    MakeComputerMove();
                }

                DisplayBoard();
                gameWon = CheckWin();
                draw = CheckDraw();

                if (gameWon)
                {
                    Console.WriteLine(playerTurn ? "Player wins!" : "Computer wins!");
                    if (playerTurn)
                    {
                        _playerScore++;
                    }
                    else
                    {
                        _computerScore++;
                    }
                }
                else if (draw)
                {
                    Console.WriteLine("It's a draw!");
                }

                playerTurn = !playerTurn;

            } while (!gameWon && !draw);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void GetUserMove()
        {
            bool validMove = false;
            int row, col;

            do
            {
                Console.Write("Player, enter your move (X-Y): ");
                string input = Console.ReadLine();

                if (IsValidMoveFormat(input))
                {
                    string[] coordinates = input.Split('-');
                    if (int.TryParse(coordinates[0], out row) && int.TryParse(coordinates[1], out col) && row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == ' ')
                    {
                        board[row, col] = 'X';
                        validMove = true;
                    }
                }

                if (!validMove)
                {
                    Console.WriteLine("Invalid move. Please try again.");
                }
            } while (!validMove);
        }

        static bool IsValidMoveFormat(string input)
        {
            string[] coordinates = input.Split('-');
            return coordinates.Length == 2 && int.TryParse(coordinates[0], out int row) && int.TryParse(coordinates[1], out int col);
        }
        static bool CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != ' ' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    return true;
                }

                if (board[0, i] != ' ' && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                {
                    return true;
                }
            }

            if (board[0, 0] != ' ' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                return true;
            }

            if (board[0, 2] != ' ' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                return true;
            }

            return false;
        }

        static bool CheckDraw()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == ' ')
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
