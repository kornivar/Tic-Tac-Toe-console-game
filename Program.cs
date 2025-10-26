char[,] board = new char[3, 3];
int moves = 0;
char player = 'X';
bool gameOver = false;

for (int i = 0; i < 3; i++)
{
    for (int j = 0; j < 3; j++)
    {
        board[i, j] = ' ';
    }
}

while (!gameOver)
{
    Console.WriteLine("\nCurrent board:");
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            Console.Write(board[i, j]);
            if (j < 2) Console.Write("|");
        }
        Console.WriteLine();
        if (i < 2) Console.WriteLine("-+-+-");
    }

    Console.WriteLine("\nPlayer " + player + ", enter your move (row and column 1-3):");
    int row = int.Parse(Console.ReadLine()) - 1;
    int col = int.Parse(Console.ReadLine()) - 1;

    if (row < 0 || row > 2 || col < 0 || col > 2 || board[row, col] != ' ')
    {
        Console.WriteLine("Invalid move! Try again.");
        continue;
    }

    board[row, col] = player;
    moves++;

    bool win = false;

    for (int i = 0; i < 3; i++)
    {
        if ((board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) ||
            (board[0, i] == player && board[1, i] == player && board[2, i] == player))
        {
            win = true;
            break;
        }
    }

    if (!win)
    {
        if ((board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
            (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player))
        {
            win = true;
        }
    }

    if (win)
    {
        Console.WriteLine("\nPlayer " + player + " wins!");
        gameOver = true;
    }
    else if (moves >= 9)
    {
        Console.WriteLine("\nIt's a draw!");
        gameOver = true;
    }
    else
    {
        player = (player == 'X') ? 'O' : 'X';
    }
}
