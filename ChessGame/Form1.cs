﻿using ChessBoardModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ChessGame {

    public partial class Form1 : Form {

        // Reference to the class Board
        private static Board chessBoard = new Board(8);

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
            for (int x = 0; x < chessBoard.Size; x++) {
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

                    //chessBoard.Grid[x, y].Tag = new Point(x, y);
                    btnGrid[x, y].Tag = new ButtonTag() {
                        ButtonPosition = new Point(x, y)
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
            string[] pieces = { "Rook", "Knight", "Bishop", "Queen", "King", "Bishop", "Knight", "Rook" };

            // Initial setup of pieces on the board
            for (int i = 0; i < 8; i++) {
                chessBoard.Grid[i, 6].Piece = "Pawn";
                chessBoard.Grid[i, 6].Team = "White";
                chessBoard.Grid[i, 1].Piece = "Pawn";
                chessBoard.Grid[i, 1].Team = "Black";
                chessBoard.Grid[i, 7].Piece = pieces[i];
                chessBoard.Grid[i, 7].Team = "White";
                chessBoard.Grid[i, 0].Piece = pieces[i];
                chessBoard.Grid[i, 0].Team = "Black";

                //chessBoard.Grid[4, 3].Piece = "Queen";
                //chessBoard.Grid[4, 3].Team = "Black";
                //chessBoard.Grid[6, 5].Piece = "King";
                //chessBoard.Grid[6, 5].Team = "White";
                //chessBoard.Grid[5, 4].Piece = "Rook";
                //chessBoard.Grid[5, 4].Team = "White";
            }

            DrawPieces();
        }

        public void DrawPieces() {
            IsKingCheck();
            for (int x = 0; x < chessBoard.Size; x++) {
                for (int y = 0; y < chessBoard.Size; y++) {
                    if (chessBoard.Grid[x, y].Piece == null) {
                        btnGrid[x, y].Image = null;
                    }

                    if (chessBoard.Grid[x, y].Team == "White") {
                        switch (chessBoard.Grid[x, y].Piece) {
                            case "Pawn":
                                btnGrid[x, y].Image = Image.FromFile(@"..\..\img\WhitePawn.png");
                                break;

                            case "Rook":
                                btnGrid[x, y].Image = Image.FromFile(@"..\..\img\WhiteRook.png");
                                break;

                            case "Knight":
                                btnGrid[x, y].Image = Image.FromFile(@"..\..\img\WhiteKnight.png");
                                break;

                            case "Bishop":
                                btnGrid[x, y].Image = Image.FromFile(@"..\..\img\WhiteBishop.png");
                                break;

                            case "Queen":
                                btnGrid[x, y].Image = Image.FromFile(@"..\..\img\WhiteQueen.png");
                                break;

                            case "King":
                                btnGrid[x, y].Image = Image.FromFile(@"..\..\img\WhiteKing.png");
                                break;
                        }
                    }

                    if (chessBoard.Grid[x, y].Team == "Black") {
                        switch (chessBoard.Grid[x, y].Piece) {
                            case "Pawn":
                                btnGrid[x, y].Image = Image.FromFile(@"..\..\img\BlackPawn.png");
                                break;

                            case "Rook":
                                btnGrid[x, y].Image = Image.FromFile(@"..\..\img\BlackRook.png");
                                break;

                            case "Knight":
                                btnGrid[x, y].Image = Image.FromFile(@"..\..\img\BlackKnight.png");
                                break;

                            case "Bishop":
                                btnGrid[x, y].Image = Image.FromFile(@"..\..\img\BlackBishop.png");
                                break;

                            case "Queen":
                                btnGrid[x, y].Image = Image.FromFile(@"..\..\img\BlackQueen.png");
                                break;

                            case "King":
                                btnGrid[x, y].Image = Image.FromFile(@"..\..\img\BlackKing.png");
                                break;
                        }
                    }
                    btnGrid[x, y].ImageAlign = ContentAlignment.MiddleCenter;

                    //if (chessBoard.Grid[x, y].Piece != null) {
                    //    Cell currentCell = chessBoard.Grid[x, y];
                    //    string chessPiece = chessBoard.Grid[x, y].Piece;
                    //    string pieceTeam = chessBoard.Grid[x, y].Team;

                    //    if (pieceTeam == "White") {
                    //        chessBoard.CheckLegalMoves(currentCell, chessPiece, pieceTeam);
                    //    }
                    //}

                    if (chessBoard.Grid[x, y].IsCheckPath) {
                        if ((x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0)) {
                            btnGrid[x, y].BackColor = Color.LightSalmon;
                        }
                        else {
                            btnGrid[x, y].BackColor = Color.Salmon;
                        }
                    }

                    //if (chessBoard.Grid[x, y].IsAttackPathBlack) {
                    //    if ((x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0)) {
                    //        btnGrid[x, y].BackColor = Color.DarkBlue;
                    //    }
                    //    else {
                    //        btnGrid[x, y].BackColor = Color.LightBlue;
                    //    }
                    //}

                    //if (chessBoard.Grid[y, x].Piece == null) {
                    //    //Console.Write("|      ");
                    //    //Console.Write(chessBoard.Grid[y, x].IsAttackPathBlack);
                    //    Console.Write(" | " + chessBoard.Grid[y, x].IsAttackPathBlack);

                    //}
                    //else if (chessBoard.Grid[y, x].Piece != null) {
                    //    Console.Write(" | " + chessBoard.Grid[y, x].IsAttackPathBlack);
                    //}
                }
                //Console.WriteLine();
                //Console.WriteLine("---------------------------------------------------------------");
            }
        }

        // Chequered board
        public void SetBoardColor() {
            for (int x = 0; x < chessBoard.Size; x++) {
                for (int y = 0; y < chessBoard.Size; y++) {
                    if ((x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0)) {
                        btnGrid[x, y].BackColor = Color.White;
                    }
                    else {
                        btnGrid[x, y].BackColor = Color.BurlyWood;
                    }
                }
            }
        }

        private string lastPiece;
        private string lastPieceTeam;
        private int moveCounter = 0;

        //Show legal moves of a piece when clicked
        public void ShowLegalMoves(Point location) {
            // Reset board grid colours
            SetBoardColor();

            int _x = location.X;
            int _y = location.Y;

            Cell currentCell = chessBoard.Grid[_x, _y];

            // Change background color on each button
            for (int x = 0; x < chessBoard.Size; x++) {
                for (int y = 0; y < chessBoard.Size; y++) {
                    if (moveCounter > 1) {
                        SetBoardColor();
                        break;
                    }

                    // Piece movement - updates stored piece
                    if (chessBoard.Grid[x, y].IsLegalMove) {
                        lastPiece = currentCell.Piece;
                        lastPieceTeam = currentCell.Team;
                    }

                    // Show legal moves on board
                    if (chessBoard.Grid[x, y].IsLegalMove) {
                        if ((x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0)) {
                            btnGrid[x, y].BackColor = Color.LightGreen;
                        }
                        else {
                            btnGrid[x, y].BackColor = Color.DarkGreen;
                        }
                    }

                }
            }
        }


        // Check all legal moves on the board to see if any moves intercept with the king (need to add team check to prevent checking own king)
        public void IsKingCheck() {

            chessBoard.legalMovesCounterWhite = 0;
            chessBoard.legalMovesCounterBlack = 0;

            // Clear check path before reviewing if king is in check
            for (int x = 0; x < chessBoard.Size; x++) {
                for (int y = 0; y < chessBoard.Size; y++) {
                    chessBoard.Grid[x, y].IsCheckPath = false;
                    chessBoard.Grid[x, y].IsLegalMove = false;
                    chessBoard.Grid[x, y].IsCheckPiece = false;
                    chessBoard.Grid[x, y].IsAttackPiece = false;
                    chessBoard.Grid[y, x].IsAttackPathWhite = false;
                    chessBoard.Grid[y, x].IsAttackPathBlack = false;
                    chessBoard.Grid[y, x].LegalMovesCounter = 0;

                    chessBoard.isKingCheckWhite = false;
                    chessBoard.isKingCheckBlack = false;

                    if (chessBoard.Grid[x, y].Piece != null) {
                        chessBoard.Grid[x, y].IsCurrentlyOccupied = true;
                    }
                    else {
                        chessBoard.Grid[x, y].IsCurrentlyOccupied = false;
                    }
                }
            }

            // Check for any legal moves that intercept with the king
            for (int x = 0; x < chessBoard.Size; x++) {
                for (int y = 0; y < chessBoard.Size; y++) {
                    if (chessBoard.Grid[x, y].Piece != null) {
                        Cell currentCell = chessBoard.Grid[x, y];
                        string chessPiece = chessBoard.Grid[x, y].Piece;
                        string pieceTeam = chessBoard.Grid[x, y].Team;

                        if (pieceTeam == "White") {
                            chessBoard.legalMovesCounter = 0;
                            chessBoard.CheckLegalMoves(currentCell, chessPiece, pieceTeam);
                            chessBoard.legalMovesCounterWhite += chessBoard.legalMovesCounter;
                            //chessBoard.legalMovesCounterWhite += chessBoard.Grid[x, y].LegalMovesCounter;
                        }
                        //if (pieceTeam == "Black") {
                        //    chessBoard.legalMovesCounter = 0;
                        //    chessBoard.CheckLegalMoves(currentCell, chessPiece, pieceTeam);
                        //    chessBoard.legalMovesCounterBlack += chessBoard.legalMovesCounter;
                        //}
                    }
                }
            }

            // PROBLEM: If both teams are in the same for loop a8 rook is selected as lastX,lastY coordinate and removed when h2 pawn is moved first
            for (int x = 0; x < chessBoard.Size; x++) {
                for (int y = 0; y < chessBoard.Size; y++) {
                    if (chessBoard.Grid[x, y].Piece != null) {
                        Cell currentCell = chessBoard.Grid[x, y];
                        string chessPiece = chessBoard.Grid[x, y].Piece;
                        string pieceTeam = chessBoard.Grid[x, y].Team;

                        //if (pieceTeam == "White") {
                        //    chessBoard.legalMovesCounter = 0;
                        //    chessBoard.CheckLegalMoves(currentCell, chessPiece, pieceTeam);
                        //    chessBoard.legalMovesCounterWhite += chessBoard.legalMovesCounter;
                        //}

                        if (pieceTeam == "Black") {
                            chessBoard.legalMovesCounter = 0;
                            chessBoard.CheckLegalMoves(currentCell, chessPiece, pieceTeam);
                            chessBoard.legalMovesCounterBlack += chessBoard.legalMovesCounter;
                        }
                    }
                }
            }

            Console.WriteLine("White legal moves: " + chessBoard.legalMovesCounterWhite);
            Console.WriteLine("Black legal moves: " + chessBoard.legalMovesCounterBlack);
        }

        private int lastX;
        private int lastY;
        private List<string> capturedWhite = new List<string>();
        private List<string> capturedBlack = new List<string>();

        private void Grid_Button_Click(object sender, EventArgs e) {
            if (isMoving != true) { return; }

            // Get the row and column number of button clicked - sender is the obj that is clicked
            Button clickedButton = (Button)sender;

            Point location = clickedButton.PieceTag().ButtonPosition;

            int _x = location.X;
            int _y = location.Y;

            Cell currentCell = chessBoard.Grid[_x, _y];

            // Turn system - only correct team pieces can be moved on their turn
            if (iswhiteMoving && currentCell.Team != "White" && moveCounter != 1) { return; }
            else if ((!iswhiteMoving && currentCell.Team != "Black" && moveCounter != 1)) { return; }

            string pieceTeam = currentCell.Team;
            bool isLegalMove = currentCell.IsLegalMove;

            // lastX/Y < 8 checks that value is in range of board - used for allowing one move at a time
            if (isLegalMove && lastX < 8 && lastY < 8) {
                chessBoard.Grid[lastX, lastY].Piece = null;
                chessBoard.Grid[lastX, lastY].Team = null;
            }

            lastX = _x;
            lastY = _y;

            moveCounter++;

            chessPiece = currentCell.Piece;

            // Check if cell is occupied => used to prevent jumping pieces (CheckLegalMoves)
            for (int x = 0; x < chessBoard.Size; x++) {
                for (int y = 0; y < chessBoard.Size; y++) {
                    if (chessBoard.Grid[x, y].Piece != null) {
                        chessBoard.Grid[x, y].IsCurrentlyOccupied = true;
                    }
                    else {
                        chessBoard.Grid[x, y].IsCurrentlyOccupied = false;
                    }
                }
            }

            // Determine next legal moves
            chessBoard.CheckLegalMoves(currentCell, chessPiece, pieceTeam);
            ShowLegalMoves(location);

            // Move piece on click of second button (2nd button is where piece should be moved to)
            if (isLegalMove && moveCounter == 2) {
                // Add captured pieces to List<String>
                switch (chessBoard.Grid[lastX, lastY].Team) {
                    case "White":
                        capturedWhite.Add(chessBoard.Grid[lastX, lastY].Piece);
                        break;

                    case "Black":
                        capturedBlack.Add(chessBoard.Grid[lastX, lastY].Piece);
                        break;
                }
                currentCell.Piece = lastPiece;
                currentCell.Team = lastPieceTeam;
                isMoving = false;

                DrawPieces();
            }

            // Only allows one move to be made at a time
            if (moveCounter > 1) {
                moveCounter = 0;
                lastPiece = null;
                lastX = 30;
                lastY = 30;
                MoveButtonColor();
            }

            populateCapturedPieces();

            Console.Write("Captured White: ");
            foreach (var i in capturedWhite) { Console.Write(i.ToString() + ", "); };
            Console.WriteLine();
            Console.Write("Captured Black: ");
            foreach (var i in capturedBlack) { Console.Write(i.ToString() + ", "); }
            Console.WriteLine();
            Console.WriteLine("---------------");

            if (chessBoard.legalMovesCounterWhite == 0) {
                Console.WriteLine("Black Wins via checkmate!");
            }
            if (chessBoard.legalMovesCounterBlack == 0) {
                Console.WriteLine("White Wins via checkmate!");
            }
        }

        // Changes color of play button depending on whether player is making a move
        private void MoveButtonColor() {
            if (isMoving) {
                btn_play.BackColor = Color.Green;
            }
            else {
                btn_play.BackColor = Color.Salmon;
            }
        }

        // Used to determine when a move can be made + turn system
        private bool isMoving = false;

        private bool iswhiteMoving = false;

        // Toggles ability to make a move + turn system
        private void btn_play_Click(object sender, EventArgs e) {
            isMoving ^= true;
            iswhiteMoving ^= true;
            if (iswhiteMoving) {
                lbl_turn.Text = "White Moving...";
            }
            else {
                lbl_turn.Text = "Black Moving...";
            }
            MoveButtonColor();
        }

        private void populateCapturedPieces() {

            flp_white.Controls.Clear();
            flp_black.Controls.Clear();

            // Gets the index of the piece image from the image list
            void imgListIndex(Button button){

                button.Height = panel1.Width / (chessBoard.Size * 2);
                button.Width = panel1.Width / (chessBoard.Size * 2);
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;

                switch (button.Tag) {
                    case "Pawn":
                        button.ImageIndex = 0;
                        break;

                    case "Rook":
                        button.ImageIndex = 1;
                        break;

                    case "Knight":
                        button.ImageIndex = 2;
                        break;

                    case "Bishop":
                        button.ImageIndex = 3;
                        break;

                    case "Queen":
                        button.ImageIndex = 4;
                        break;

                    case "King":
                        button.ImageIndex = 5;
                        break;
                }
            }

            // Draws the white captured pieces
            if (capturedWhite != null && capturedWhite.Any()) {
                foreach (var i in capturedWhite) {
                    Button button = new Button();
                    button.Tag = i;
                    button.ImageList = imgList_white;

                    imgListIndex(button);

                    flp_white.Controls.Add(button);
                };
            }

            if (capturedBlack != null && capturedBlack.Any()) {
                foreach (var i in capturedBlack) {
                    Button button = new Button();
                    button.Tag = i;
                    button.ImageList = imgList_black;

                    imgListIndex(button);

                    flp_black.Controls.Add(button);
                };
            }
        }
    }
}