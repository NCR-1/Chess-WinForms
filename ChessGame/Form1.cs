using ChessBoardModel;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChessGame {
    public partial class Form1 : Form {

        // Reference to the class Board
        static Board chessBoard = new Board(8);

        // 2D array of buttons => values determined by chessBoard
        public Button[,] btnGrid = new Button[chessBoard.Size, chessBoard.Size];
        public string chessPiece;

        public Form1() {
            InitializeComponent();
            populateGrid();
            //selectPiece.SelectedIndex = 0;
            chessPiece = selectPiece.GetItemText(selectPiece.SelectedItem);
        }

        private void populateGrid() {
           
            // Create buttons and place them into the panel
            int buttonSize = panel1.Width / chessBoard.Size;
            panel1.Height = panel1.Width;

            // Nested loops => create buttons and add them to the screen
            for (int x = 0; x< chessBoard.Size; x++) {
                for (int y = 0; y < chessBoard.Size; y++) {
                    btnGrid[x, y] = new Button();
                    btnGrid[x, y].Height = buttonSize;
                    btnGrid[x, y].Width = buttonSize;
                    btnGrid[x, y].FlatStyle = FlatStyle.Flat;
                    btnGrid[x, y].FlatAppearance.BorderSize = 0;

                    // Move button appearance
                    btn_play.FlatStyle = FlatStyle.Flat;
                    btn_play.FlatAppearance.BorderSize = 0;
                    btn_play.BackColor = Color.Salmon;

                    // Set the location of the button
                    btnGrid[x, y].Location = new Point(x * buttonSize, y * buttonSize);

                    btnGrid[x, y].Tag = new Point(x, y);
                    //btnGrid[x, y].Text = x + "," + y;

                    // Add click event for the button
                    btnGrid[x, y].Click += Grid_Button_Click;

                    // Add button to panel
                    panel1.Controls.Add(btnGrid[x, y]);

                }
            }

            SetBoardColor();

            // Correct order of pieces on back rows
            string[] pieces = { "Rook", "Knight", "Bishop", "Queen", "King", "Bishop", "Knight", "Rook"};

            // Initial setup of pieces on the board
            for (int i = 0; i < 8; i++) {
                btnGrid[i, 6].Text = "Pawn W";
                btnGrid[i, 1].Text = "Pawn B";
                btnGrid[i, 0].Text = pieces[i];
                btnGrid[i, 7].Text = pieces[i];
            }
        }

        // Chequered board
        public void SetBoardColor() {
            for (int x = 0; x < chessBoard.Size; x++) {
                for (int y = 0; y < chessBoard.Size; y++) {
                    if ((x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0)) {
                        btnGrid[x, y].BackColor = Color.White;
                        btnGrid[x, y].ForeColor = Color.Black;
                    }
                    else {
                        btnGrid[x, y].BackColor = Color.Black;
                        btnGrid[x, y].ForeColor = Color.White;
                    }
                }
            }
        }

        string lastPiece;
        int moveCounter = 0;

        //Show legal moves of a piece when clicked
        public void ShowLegalMoves(Button clickedButton) {

            // Reset board grid colours
            SetBoardColor();

            // Change background color on each button
            for (int x = 0; x < chessBoard.Size; x++) {
                for (int y = 0; y < chessBoard.Size; y++) {

                    //Set colours of legal move cells and currently occupied cell
                    if (moveCounter > 1) {
                        SetBoardColor();
                        break;
                    }
                    if (chessBoard.Grid[x, y].IsLegalMove == true) {
                        lastPiece = clickedButton.Text;
                        if ((x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0)) {
                            btnGrid[x, y].BackColor = Color.LightGreen;
                        }
                        else {
                            btnGrid[x, y].BackColor = Color.DarkGreen;
                        }
                    }
                    else if (chessBoard.Grid[x, y].IsLegalMove == false) {
                    }
                    else if (chessBoard.Grid[x, y].IsCurrentlyOccupied == true) {
                        btnGrid[x, y].BackColor = Color.Red;
                        // Used to set value to that of combobox for development
                        btnGrid[x, y].Text = chessPiece;
                    }
                }
            }
        }

        int lastX;
        int lastY;

        private void Grid_Button_Click(object sender, EventArgs e) {

            if (isMoving == true) {

                // Get the row and column number of button clicked - sender is the obj that is clicked
                Button clickedButton = (Button)sender;
                Point location = (Point)clickedButton.Tag;

                int _x = location.X;
                int _y = location.Y;

                bool isLegalMove = chessBoard.Grid[_x, _y].IsLegalMove;

                if (isLegalMove == true && lastX < 8 && lastY < 8) {
                    btnGrid[lastX, lastY].Text = "";
                }

                lastX = _x;
                lastY = _y;

                Cell currentCell = chessBoard.Grid[_x, _y];

                moveCounter++;

                // Selecting piece using ComboBox will override pice choice (for development)
                if (selectPiece.GetItemText(selectPiece.SelectedItem) == "") {
                    chessPiece = (sender as Button).Text;
                }

                // Determine next legal moves
                chessBoard.CheckLegalMoves(currentCell, chessPiece);
                ShowLegalMoves(clickedButton);

                // Move piece on click of second button (2nd button is where piece should be moved to)
                if (isLegalMove == true && moveCounter == 2) {
                    clickedButton.Text = lastPiece;
                }

                if (moveCounter == 2) {
                    Console.WriteLine("isLegalMove: " + isLegalMove);
                    Console.WriteLine("moveCounter: " + moveCounter);
                    Console.WriteLine("---------------");
                }

                // Only allows one move to be made at a time
                if (moveCounter > 1) {
                    moveCounter = 0;
                    lastPiece = null;
                    lastX = 30;
                    lastY = 30;
                    isMoving = false;
                    //isLegalMove = false;
                    MoveButtonColor();
                }

                Console.WriteLine("isMoving: " + isMoving);
                Console.WriteLine("moveCounter: " + moveCounter);
                Console.WriteLine("lastPiece: " + lastPiece);
                Console.WriteLine("---------------");

            }
        }

        private void selectPiece_SelectionChangeCommitted(object sender, EventArgs e) {
            chessPiece = selectPiece.GetItemText(selectPiece.SelectedItem);
        }

        // Changes color of play button depending on whether player is making a move
        void MoveButtonColor() {
            if (isMoving == true) {
                btn_play.BackColor = Color.Green;
            }
            else {
                btn_play.BackColor = Color.Salmon;
            }
        }

        // Used to determine when a move can be made
        bool isMoving = false;

        // Toggles ability to make a move
        private void btn_play_Click(object sender, EventArgs e) {
            isMoving ^= true;
            MoveButtonColor();
            Console.WriteLine("isMoving: " + isMoving);
            Console.WriteLine("---------------");
        }
    }
}
