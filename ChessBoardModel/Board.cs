using System;
namespace ChessBoardModel {

    public class Board {

        // Default size is 8x8 (A-H, 1-8)
        public int Size { get; set; }

        // 2D array of type cell
        public Cell[,] Grid { get; set; }

        // Board constructor
        public Board(int size) {
            Size = size;

            // Create new 2d array
            Grid = new Cell[Size, Size];

            // Fill 2d array with new cells, each has unique coordinate
            for (int x = 0; x < Size; x++) {
                for (int y = 0; y < Size; y++) {
                    Grid[x, y] = new Cell(x, y);

                    //Console.Write("|" + y + "," + x);
                }
                //Console.WriteLine();
                //Console.WriteLine("--------------------------------");
            }
        }

        public void CheckLegalMoves(Cell currentCell, string chessPiece, string pieceTeam) {
            int boardSize = 8;

            bool isKingCheckWhite = false;
            bool isKingCheckBlack = false;

            //Clear legal moves
            for (int x = 0; x < Size; x++) {
                for (int y = 0; y < Size; y++) {
                    Grid[y, x].IsLegalMove = false;
                }
            }

            void CheckPath(int row, int column, int range, bool isCheckPath) {
                for (int i = 1; i <= range; i++) {
                    int rowNum = 0;
                    int colNum = 0;

                    if (row == 1) {
                        rowNum = i;
                    }
                    if (row == 0) {
                        rowNum = i * 0;
                    }
                    if (row == -1) {
                        rowNum = i * -1;
                    }

                    if (column == 1) {
                        colNum = i;
                    }
                    if (column == 0) {
                        colNum = i * 0;
                    }
                    if (column == -1) {
                        colNum = i * -1;
                    }

                    if (currentCell.RowNumber + rowNum < boardSize
                    && currentCell.ColumnNumber + colNum < boardSize
                    && currentCell.RowNumber + rowNum >= 0
                    && currentCell.ColumnNumber + colNum >= 0
                    && Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove) {

                        Cell targetCell = Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum];


                        if (isCheckPath) {
                            targetCell.IsCheckPath = true;
                            
                            if (targetCell.IsCurrentlyOccupied) {
                                goto CheckLoopEnd;
                            }
                        }

                        if (!isCheckPath) {
                            if (targetCell.IsCurrentlyOccupied) {
                                goto CheckLoopEnd;
                            }

                            if (currentCell.Team == "White") {
                                targetCell.IsAttackPathWhite = true;
                            }
                            if (currentCell.Team == "Black") {
                                targetCell.IsAttackPathBlack = true;
                            }
                        }

                    }
                }
            CheckLoopEnd:;
            }

            void isValidCell(int row, int column, int range) {
                bool isCheckPath = false;

                for (int i = 1; i <= range; i++) {
                    int rowNum = 0;
                    int colNum = 0;

                    if (row == 1) {
                        rowNum = i;
                    }
                    if (row == 0) {
                        rowNum = i * 0;
                    }
                    if (row == -1) {
                        rowNum = i * -1;
                    }

                    if (column == 1) {
                        colNum = i;
                    }
                    if (column == 0) {
                        colNum = i * 0;
                    }
                    if (column == -1) {
                        colNum = i * -1;
                    }

                    if (currentCell.RowNumber + rowNum < boardSize
                    && currentCell.ColumnNumber + colNum < boardSize
                    && currentCell.RowNumber + rowNum >= 0
                    && currentCell.ColumnNumber + colNum >= 0) {

                        Cell targetCell = Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum];

                        // Set all cells in direction to be a legal move
                        targetCell.IsLegalMove = true;

                        // Stores the attacking line of the piece - used for preventing King moving into attacking line
                        if (currentCell.Team == "White") {

                            //Checks to see if legal moves intercept the King piece of other team
                            if (targetCell.Piece == "King"
                                && targetCell.IsLegalMove
                                && targetCell.Team != currentCell.Team) {
                                
                                currentCell.IsCheckPiece = true;
                                isKingCheckBlack = true;
                                isCheckPath = true;
                            }

                            //Only moves that intercept the check line are legal when king is in check
                            if (isKingCheckWhite) {
                                if (targetCell.IsCheckPath) {
                                    targetCell.IsLegalMove = true;
                                }
                                if (!targetCell.IsCheckPath) {
                                    targetCell.IsLegalMove = false;
                                }
                                if (targetCell.IsCheckPiece) {
                                    targetCell.IsLegalMove = true;
                                }
                            }

                            // Prevent King entering attacking line of enemy piece
                            if (currentCell.Piece == "King") {
                                if (targetCell.IsAttackPathBlack) {
                                    targetCell.IsLegalMove = false;
                                }

                                if (isKingCheckWhite) {
                                    if (targetCell.IsAttackPathBlack) {
                                        targetCell.IsLegalMove = false;
                                    }
                                    if (!targetCell.IsAttackPathBlack) {
                                        targetCell.IsLegalMove = true;
                                    }
                                    if (targetCell.IsCheckPiece) {
                                        targetCell.IsLegalMove = true;
                                    }
                                }
                            }
                        }

                        if (currentCell.Team == "Black") {

                            //Checks to see if legal moves intercept the King piece of other team
                            if (targetCell.Piece == "King"
                                && targetCell.IsLegalMove
                                && targetCell.Team != currentCell.Team) {

                                currentCell.IsCheckPiece = true;
                                isKingCheckWhite = true;
                                isCheckPath = true;
                            }

                            //Only moves that intercept the check line are legal when king is in check
                            if (isKingCheckBlack) {
                                if (targetCell.IsCheckPath) {
                                    targetCell.IsLegalMove = true;
                                }
                                if (!targetCell.IsCheckPath) {
                                    targetCell.IsLegalMove = false;
                                }
                                if (targetCell.IsCheckPiece) {
                                    targetCell.IsLegalMove = true;
                                }
                            }

                            // Prevent King entering attacking line of enemy piece
                            if (currentCell.Piece == "King") {
                                if (targetCell.IsAttackPathWhite) {
                                    targetCell.IsLegalMove = false;
                                }

                                if (isKingCheckBlack) {
                                    if (targetCell.IsAttackPathWhite) {
                                        targetCell.IsLegalMove = false;
                                    }
                                    if (!targetCell.IsAttackPathWhite) {
                                        targetCell.IsLegalMove = true;
                                    }
                                    if (targetCell.IsCheckPiece) {
                                        targetCell.IsLegalMove = true;
                                    }
                                }
                            }
                        }

                        CheckPath(row, column, range, isCheckPath);

                        if (targetCell.IsCurrentlyOccupied) {
                            goto LoopEnd;
                        }
                    }
                }
            LoopEnd:;
            }

            void isValidCellPawn(int row, int column, int range) {
                // Down (Black)
                if (row == 0 && column == +1) {
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + 0 < boardSize
                        && currentCell.ColumnNumber + i < boardSize
                        && currentCell.RowNumber + 0 >= 0
                        && currentCell.ColumnNumber + i >= 0) {
                            Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsLegalMove = true;

                            // If king is in check Pawn can only move to position that intercepts check line
                            if (isKingCheckBlack) {
                                if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsLegalMove = true;
                                }
                                if (!Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsLegalMove = false;
                                }
                            }

                            if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsCurrentlyOccupied) {
                                Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsLegalMove = false;
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }

                // Up (White)
                if (row == 0 && column == -1) {
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + 0 < boardSize
                        && currentCell.ColumnNumber + -i < boardSize
                        && currentCell.RowNumber + 0 >= 0
                        && currentCell.ColumnNumber + -i >= 0) {
                            Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove = true;

                            // If king is in check Pawn can only move to position that intercepts check line
                            if (isKingCheckWhite) {
                                if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove = true;
                                }
                                if (!Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove = false;
                                }
                            }
                            // Prevent pove moving forward into occupied space
                            if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsCurrentlyOccupied) {
                                Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove = false;
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }

                // Attack Right Diagonal
                if (currentCell.RowNumber + 1 >= 0
                    && currentCell.RowNumber + 1 < boardSize) {
                    bool isCheckPath = false;

                    // Stores the attacking line of the piece - used for preventing King moving into attacking line
                    if (currentCell.Team == "White"
                        && currentCell.Team != Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].Team) {
                        Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsAttackPathWhite = true;
                    }
                    // Stores the attacking line of the piece - used for preventing King moving into attacking line
                    if (currentCell.Team == "Black"
                        && currentCell.Team != Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].Team) {
                        Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsAttackPathBlack = true;
                    }
                    // Diagonal attack
                    if (Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsCurrentlyOccupied) {
                        Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsLegalMove = true;

                        // Place the king in check
                        if (Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].Piece == "King"
                            && Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].Team != currentCell.Team) {
                            if (Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].Team == "White") {
                                isKingCheckWhite = true;
                            }
                            if (Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].Team == "Black") {
                                isKingCheckBlack = true;
                            }
                            isCheckPath = true;
                        }

                        if (isCheckPath
                            && Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsLegalMove) {
                                Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsCheckPath = true;
                        }

                        if (isKingCheckWhite 
                            && currentCell.Team == "White") {
                            if (Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsCheckPiece) {
                                Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsLegalMove = true;
                            }
                            else if (!Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsCheckPiece) {
                                Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsLegalMove = false;
                            }
                        }
                        if (isKingCheckBlack
                            && currentCell.Team == "Black") {
                            if (Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsCheckPiece) {
                                Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsLegalMove = true;
                            }
                            else if (!Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsCheckPiece) {
                                Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsLegalMove = false;
                            }
                        }
                    }
                }

                // Attack Left  Diagonal
                if (currentCell.RowNumber + -1 >= 0
                    && currentCell.RowNumber + -1 < boardSize) {
                    bool isCheckPath = false;

                    // Stores the attacking line of the piece - used for preventing King moving into attacking line
                    if (currentCell.Team == "White"
                        && currentCell.Team != Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].Team) {
                        Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsAttackPathWhite = true;
                    }
                    // Stores the attacking line of the piece - used for preventing King moving into attacking line
                    if (currentCell.Team == "Black"
                        && currentCell.Team != Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].Team) {
                        Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsAttackPathBlack = true;
                    }
                    // Diagonal attack
                    if (Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsCurrentlyOccupied) {
                        Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsLegalMove = true;

                        // Place the king in check
                        if (Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].Piece == "King"
                            && Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].Team != currentCell.Team) {
                            if (Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].Team == "White") {
                                isKingCheckWhite = true;
                            }
                            if (Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].Team == "Black") {
                                isKingCheckBlack = true;
                            }
                            isCheckPath = true;
                        }

                        if (isCheckPath
                            && Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsLegalMove) {
                                Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsCheckPath = true;
                        }

                        if (isKingCheckWhite 
                            && currentCell.Team == "White"
                            && Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsCheckPiece) {
                                Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsLegalMove = true;
                        }
                        if (isKingCheckBlack 
                            && currentCell.Team == "Black"
                            && Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsCheckPiece) {
                                Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsLegalMove = true;
                        }
                    }
                }
            }

            void isValidCellKnight(int row, int column) {
                bool isCheckPath = false;
                if (currentCell.RowNumber + row < boardSize
                    && currentCell.ColumnNumber + column < boardSize
                    && currentCell.RowNumber + row >= 0
                    && currentCell.ColumnNumber + column >= 0) {

                    Cell targetCell = Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column];

                    targetCell.IsLegalMove = true;

                    // Stores the attacking line of the piece - used for preventing King moving into attacking line
                    if (currentCell.Team == "White"
                        && targetCell.Team != currentCell.Team) {
                        targetCell.IsAttackPathWhite = true;
                    }
                    if (currentCell.Team == "Black"
                        && targetCell.Team != currentCell.Team) {
                        targetCell.IsAttackPathBlack = true;
                    }

                    // Check the king
                    if (targetCell.Piece == "King"
                        && targetCell.Team != currentCell.Team) {
                        if (Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].Team == "White") {
                            isKingCheckWhite = true;
                        }
                        if (Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].Team == "Black") {
                            isKingCheckBlack = true;
                        }
                        isCheckPath = true;
                    }

                    // Set the checkline
                    if (isCheckPath && targetCell.IsLegalMove) {
                        targetCell.IsCheckPath = true;

                        if (targetCell.IsCurrentlyOccupied) {
                            goto CheckLoopEnd;
                        }
                    }
                CheckLoopEnd:;

                    // Only moves that intercept the check line are legal when king is in check
                    if (isKingCheckWhite && currentCell.Team == "White") {
                        if (targetCell.IsCheckPath) {
                            targetCell.IsLegalMove = true;
                        }
                        if (!targetCell.IsCheckPath) {
                            targetCell.IsLegalMove = false;
                        }
                        if (targetCell.IsCheckPiece) {
                            targetCell.IsLegalMove = true;
                        }
                    }
                    if (isKingCheckBlack && currentCell.Team == "Black") {
                        if (targetCell.IsCheckPath) {
                            targetCell.IsLegalMove = true;
                        }
                        if (!targetCell.IsCheckPath) {
                            targetCell.IsLegalMove = false;
                        }
                        if (targetCell.IsCheckPiece) {
                            targetCell.IsLegalMove = true;
                        }
                    }
                }
            }

            // Define all legal moves for piece
            switch (chessPiece) {
                case "King":
                    try { currentCell.IsCurrentlyOccupied = true; } catch { }

                    isValidCell(0, -1, 1); // Up
                    isValidCell(0, +1, 1); // Down
                    isValidCell(-1, 0, 1); // Left
                    isValidCell(+1, 0, 1); // Right
                    isValidCell(-1, -1, 1); // Up-Left
                    isValidCell(+1, -1, 1); // Up-Right
                    isValidCell(-1, +1, 1); // Down-Left
                    isValidCell(+1, +1, 1); // Down-Right

                    break;

                case "Queen":
                    try { currentCell.IsCurrentlyOccupied = true; } catch { }

                    isValidCell(0, -1, 7); // Up
                    isValidCell(0, +1, 7); // Down
                    isValidCell(-1, 0, 7); // Left
                    isValidCell(+1, 0, 7); // Right
                    isValidCell(-1, -1, 7); // Up-Left
                    isValidCell(+1, -1, 7); // Up-Right
                    isValidCell(-1, +1, 7); // Down-Left
                    isValidCell(+1, +1, 7); // Down-Right

                    break;

                case "Rook":
                    try { currentCell.IsCurrentlyOccupied = true; } catch { }

                    isValidCell(0, -1, 7); // Up
                    isValidCell(0, +1, 7); // Down
                    isValidCell(-1, 0, 7); // Left
                    isValidCell(+1, 0, 7); // Right

                    break;

                case "Bishop":
                    try { currentCell.IsCurrentlyOccupied = true; } catch { }

                    isValidCell(-1, -1, 7); // Up-Left
                    isValidCell(+1, -1, 7); // Up-Right
                    isValidCell(-1, +1, 7); // Down-Left
                    isValidCell(+1, +1, 7); // Down-Right

                    break;

                case "Knight":
                    try { currentCell.IsCurrentlyOccupied = true; } catch { }

                    isValidCellKnight(+2, +1);
                    isValidCellKnight(-2, -1);
                    isValidCellKnight(+2, -1);
                    isValidCellKnight(-2, +1);
                    isValidCellKnight(+1, +2);
                    isValidCellKnight(-1, -2);
                    isValidCellKnight(+1, -2);
                    isValidCellKnight(-1, +2);

                    break;

                case "Pawn":
                    try { currentCell.IsCurrentlyOccupied = true; } catch { }

                    if (pieceTeam == "White") {
                        isValidCellPawn(0, -1, 1);
                        //Initial move
                        if (currentCell.ColumnNumber == 6) {
                            isValidCellPawn(0, -1, 2);
                        }
                    }

                    if (pieceTeam == "Black") {
                        isValidCellPawn(0, +1, 1);
                        //Initial move
                        if (currentCell.ColumnNumber == 1) {
                            isValidCellPawn(0, +1, 2);
                        }
                    }

                    break;
            }
        }
    }
}