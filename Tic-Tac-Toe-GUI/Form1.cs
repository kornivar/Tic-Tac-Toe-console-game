namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private char[,] board = new char[3, 3];
        private int moves = 0;
        char player = 'X';
        private bool win = false;

        TableLayoutPanel table = new TableLayoutPanel();
        Label label = new Label();
        Button button_again = new Button();

        public Form1()
        {
            InitializeComponent();

            this.Text = "Tic-Tac-Toe";
            this.Width = 600;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightBlue;

            this.create_interface();
            this.initial_board();
        }

        void CenterLabel()
        {
            this.label.Top = table.Bottom + 10;
            this.label.Left = (this.ClientSize.Width - this.label.Width) / 2;
        }

        private void initial_board()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        private void create_interface()
        {
            initial_board();
            this.player = 'X';
            this.moves = 0;
            this.win = false;

            this.table.RowCount = 3;
            this.table.ColumnCount = 3;
            this.table.Size = new Size(300, 300);
            this.table.Anchor = AnchorStyles.Top;
            this.table.Location = new Point((this.ClientSize.Width - this.table.Width) / 2, 30);

            for (int i = 0; i < 3; i++)
            {
                this.table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f));
                this.table.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f));
            }

            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(200, 200);
                   
                    btn.Tag = (r, c);
                    btn.Click += ButtonClick;

                    this.table.Controls.Add(btn, c, r);
                }
            }

            this.table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            this.table.BackColor = Color.MistyRose;
            this.Controls.Add(this.table);


            this.label.Font = new Font("Cascadia Mono", 14);
            this.label.Text = "Player X makes a move";
            this.label.AutoSize = true;
            this.label.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(this.label);

            CenterLabel();

            this.button_again.Font = new Font("Cascadia Mono", 14);
            this.button_again.Text = "Play again";
            this.button_again.TextAlign = ContentAlignment.MiddleCenter;
            this.button_again.AutoSize = true;
            this.button_again.BackColor = Color.MistyRose;

            this.Controls.Add(this.button_again);

            this.button_again.Top = label.Bottom + 20;
            this.button_again.Left = (this.ClientSize.Width - this.button_again.Width) / 2;
            this.button_again.Click += Button_again_Click;

            this.Resize += (s, e) =>
            {
                this.table.Left = (this.ClientSize.Width - this.table.Width) / 2;
                this.button_again.Left = (this.ClientSize.Width - this.button_again.Width) / 2;
                CenterLabel();
            };

        }

        private void check_condition(char[,] board, char player)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
                {
                    this.win = true;
                    HighlightWinner((i, 0), (i, 1), (i, 2));
                }
                else if(board[0, i] == player && board[1, i] == player && board[2, i] == player)
                {
                    this.win = true;
                    HighlightWinner((0, i), (1, i), (2, i));
                }
            }

            if (!this.win)
            {
                if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
                {
                    this.win = true;
                    HighlightWinner((0, 0), (1, 1), (2, 2));
                }
                else if(board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
                {
                    this.win = true;
                    HighlightWinner((0, 2), (1, 1), (2, 0));
                }
            }

            if (this.win)
            {
                this.label.Text = $"Player {player} wins!";
                CenterLabel();
                MessageBox.Show($"Player {player} wins!");
                DisableAllButtons();
            }
            else if (this.moves >= 9)
            {
                this.label.Text = "It's a draw!";
                CenterLabel();
                MessageBox.Show("It's a draw!");
                DisableAllButtons();
            }
            else
            {
                this.player = (player == 'X') ? 'O' : 'X';
            }

        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            var (r, c) = ((int, int))btn.Tag;

            if (this.board[r, c] == ' ')
            {
                btn.Font = new Font("Cascadia Mono", 25); 
                if (this.player == 'X')
                {
                    btn.Text = "X";
                    board[r, c] = 'X';
                    this.moves++;
                }
                else{
                    btn.Text = "O";
                    board[r, c] = 'O';
                    this.moves++;
                }
            }
            this.check_condition(this.board, this.player);
        }

        private void DisableAllButtons()
        {
            foreach (Control control in table.Controls)
            {
                if (control is Button btn)
                {
                    btn.Enabled = false;
                }
            }
        }

        private void HighlightWinner((int, int) a, (int, int) b, (int, int) c)
        {
            GetButton(a).BackColor = Color.LightGreen;
            GetButton(b).BackColor = Color.LightGreen;
            GetButton(c).BackColor = Color.LightGreen;
        }

        private Button GetButton((int r, int c) pos)
        {
            return (Button)table.GetControlFromPosition(pos.c, pos.r);
        }

        private void restart_game()
        {
            initial_board();

            this.moves = 0;
            this.win = false;
            this.player = 'X';

            foreach (Control control in table.Controls)
            {
                if (control is Button btn)
                {
                    btn.Text = "";
                    btn.Enabled = true;
                    btn.BackColor = Color.MistyRose;
                }
            }

            this.label.Text = $"Player {this.player} makes a move";
            CenterLabel();
        }

        private void Button_again_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            restart_game();
        }
    }
}
