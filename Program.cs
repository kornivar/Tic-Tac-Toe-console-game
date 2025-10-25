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
}
