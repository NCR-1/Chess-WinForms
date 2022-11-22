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
            selectPiece.SelectedIndex = 0;
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

                    // Set the location of the button
                    btnGrid[x, y].Location = new Point(x * buttonSize, y * buttonSize);

                    btnGrid[x, y].Tag = new Point(x, y);
                    btnGrid[x, y].Text = x + "|" + y;

                    // Add click event for the button
                    btnGrid[x, y].Click += Grid_Button_Click;

                    // Add button to panel
                    panel1.Controls.Add(btnGrid[x, y]);

                    SetBoardColor(x,y);

                }
            }
        }

        public void SetBoardColor(int x, int y) {
            if ((x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0)) {
                btnGrid[x, y].BackColor = Color.White;
                btnGrid[x, y].ForeColor = Color.Black;
            }
            else {
                btnGrid[x, y].BackColor = Color.Black;
                btnGrid[x, y].ForeColor = Color.White;
            }
        }

        private void Grid_Button_Click(object sender, EventArgs e) {
            // Get the row and column number of button clicked - sender is the obj that is clicked
            Button clickedButton = (Button)sender;
            Point location = (Point)clickedButton.Tag;

            int _x = location.X;
            int _y = location.Y;

            Cell currentCell = chessBoard.Grid[_x, _y];

            // Determine next legal moves
            chessBoard.ShowLegalMoves(currentCell, chessPiece);

            // Change background color on each button
            for (int x = 0; x < chessBoard.Size; x++) {
                for (int y = 0; y < chessBoard.Size; y++) {
                    SetBoardColor(x, y);
                    btnGrid[x, y].Text = "";
                    if (chessBoard.Grid[x, y].IsLegalMove == true) {
                        if ((x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0)) {
                            btnGrid[x, y].BackColor = Color.LightGreen;
                        }
                        else {
                            btnGrid[x, y].BackColor = Color.DarkGreen;
                        }
                    }
                    else if (chessBoard.Grid[x, y].IsCurrentlyOccupied == true) {
                        btnGrid[x, y].BackColor = Color.Red;
                        btnGrid[x, y].Text = chessPiece;
                    }

                }
            }
        }

        private void selectPiece_SelectionChangeCommitted(object sender, EventArgs e) {
            chessPiece = selectPiece.GetItemText(selectPiece.SelectedItem);
        }
    }
}
