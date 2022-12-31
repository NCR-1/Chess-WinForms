using ChessBoardModel;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

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

                    //btnGrid[x, y].Tag = new Point(x, y);
                    btnGrid[x, y].Tag = new ButtonTag() {
                        Team = "",
                        Position = new Point(x, y)
                    };
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
                ((dynamic)btnGrid[i, 6].Tag).Team = "White";
                btnGrid[i, 1].Text = "Pawn B";
                ((dynamic)btnGrid[i, 1].Tag).Team = "Black";
                btnGrid[i, 7].Text = pieces[i];
                ((dynamic)btnGrid[i, 7].Tag).Team = "White";
                btnGrid[i, 0].Text = pieces[i];
                ((dynamic)btnGrid[i, 0].Tag).Team = "Black";
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
        string lastPieceTeam;
        int moveCounter = 0;

        //Show legal moves of a piece when clicked
        public void ShowLegalMoves(Button clickedButton) {

            // Reset board grid colours
            SetBoardColor();

            // Change background color on each button
            for (int x = 0; x < chessBoard.Size; x++) {
                for (int y = 0; y < chessBoard.Size; y++) {

                    if (moveCounter > 1) {
                        SetBoardColor();
                        break;
                    }

                    // Prevent being able to move to space occupied by own team
                    if (((dynamic)btnGrid[x, y].Tag).Team == ((dynamic)clickedButton.Tag).Team) {
                        chessBoard.Grid[x, y].IsLegalMove = false;
                    }

                    //if (btnGrid[x, y].Text != "") {
                    //    chessBoard.Grid[x, y].IsCurrentlyOccupied = true;
                    //}

                    if (chessBoard.Grid[x, y].IsLegalMove == true) {
                        lastPiece = clickedButton.Text;
                        lastPieceTeam = ((dynamic)clickedButton.Tag).Team;

                        if ((x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0)) {
                            btnGrid[x, y].BackColor = Color.LightGreen;
                        }
                        else {
                            btnGrid[x, y].BackColor = Color.DarkGreen;
                        }
                    }
                    else if (chessBoard.Grid[x, y].IsLegalMove == false) {
                        //btnGrid[x, y].BackColor = Color.Red;
                    }
                    if (chessBoard.Grid[x, y].IsCurrentlyOccupied == true) {
                        //btnGrid[x, y].BackColor = Color.Red;
                    }
                }
            }
        }

        int lastX;
        int lastY;
        List<string> capturedWhite = new List<string>();
        List<string> capturedBlack = new List<string>();

        private void Grid_Button_Click(object sender, EventArgs e) {

            if (isMoving == true) {

                // Get the row and column number of button clicked - sender is the obj that is clicked
                Button clickedButton = (Button)sender;
                Point location = ((dynamic)clickedButton.Tag).Position;

                int _x = location.X;
                int _y = location.Y;

                string pieceTeam = ((dynamic)clickedButton.Tag).Team;

                bool isLegalMove = chessBoard.Grid[_x, _y].IsLegalMove;

                // lastX/Y < 8 checks that value is in range of board - used for allowing one move at a time
                if (isLegalMove == true && lastX < 8 && lastY < 8) {
                    btnGrid[lastX, lastY].Text = "";
                    ((dynamic)btnGrid[lastX, lastY].Tag).Team = "";
                }

                lastX = _x;
                lastY = _y;

                Cell currentCell = chessBoard.Grid[_x, _y];

                moveCounter++;

                chessPiece = (sender as Button).Text;

                // Check if cell is occupied => used to prevent jumping pieces (CheckLegalMoves)
                for (int x = 0; x < chessBoard.Size; x++) {
                    for (int y = 0; y < chessBoard.Size; y++) {
                        if (btnGrid[x, y].Text != "") {
                            chessBoard.Grid[x, y].IsCurrentlyOccupied = true;
                        }
                        else {
                            chessBoard.Grid[x, y].IsCurrentlyOccupied = false;
                        }
                    }
                }

                // Determine next legal moves
                chessBoard.CheckLegalMoves(currentCell, chessPiece);
                ShowLegalMoves(clickedButton); 


                // Move piece on click of second button (2nd button is where piece should be moved to)
                if (isLegalMove == true && moveCounter == 2) {
                    // Add captured pieces to List<String>
                    switch (((dynamic)btnGrid[lastX, lastY].Tag).Team) {
                        case "White":
                            capturedWhite.Add(btnGrid[lastX, lastY].Text);
                            break;
                        case "Black":
                            capturedBlack.Add(btnGrid[lastX, lastY].Text);
                            break;
                    }
                    clickedButton.Text = lastPiece;
                    Console.WriteLine("clicked button team: "+ pieceTeam);
                    Console.WriteLine("last piece team: " + lastPieceTeam);

                    ((dynamic)clickedButton.Tag).Team = lastPieceTeam;
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
                Console.WriteLine("location: " + location);
                Console.WriteLine("Team: " + ((dynamic)clickedButton.Tag).Team);
                Console.Write("Captured White: ");
                foreach(var i in capturedWhite) { Console.Write(i.ToString() + ", "); };
                Console.WriteLine();
                Console.Write("Captured Black: ");
                foreach (var i in capturedBlack) { Console.Write(i.ToString() + ", "); }
                Console.WriteLine();
                Console.WriteLine("---------------");
            }
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
