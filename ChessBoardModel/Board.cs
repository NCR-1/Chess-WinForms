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

        public bool isKingCheckWhite = false;
        public bool isKingCheckBlack = false;
        public bool isKingAttackLineWhite = false;
        public bool isKingAttackLineBlack = false;
        public int totalLegalMovesWhite;
        public int totalLegalMovesBlack;

        public void CheckLegalMoves(Cell currentCell, string chessPiece, string pieceTeam) {
            int boardSize = 8;

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
                                //if (targetCell.Piece != "King") {
                                    goto CheckLoopEnd;
                                //}
                            }
                        }
                    }
                }
            CheckLoopEnd:;
            }

            void AttackPath(int row, int column, int range) {

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

                        if (targetCell.IsCurrentlyOccupied) {
                            if (targetCell.Piece != "King") {
                                goto LoopEnd;
                            }
                        }

                        // Stores the attacking line of the piece - used for preventing King moving into attacking line
                        if (currentCell.Team == "White") {

                            targetCell.IsAttackPathWhite = true;
                            if (targetCell.Piece == "King"
                                && targetCell.Team != currentCell.Team) {
                                isKingAttackLineWhite = true;
                            }

                            if (isKingAttackLineBlack
                                && currentCell.IsAttackPathBlack
                                && !targetCell.IsAttackPathBlack) {
                                targetCell.IsLegalMove = false;
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

                            targetCell.IsAttackPathBlack = true;
                            if (targetCell.Piece == "King"
                                && targetCell.Team != currentCell.Team) {
                                isKingAttackLineBlack = true;
                            }

                            if (isKingAttackLineWhite
                                && currentCell.IsAttackPathWhite
                                && !targetCell.IsAttackPathWhite) {
                                targetCell.IsLegalMove = false;
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

                        if (targetCell.IsCurrentlyOccupied) {
                            if (targetCell.Piece != "King") {
                                goto LoopEnd;
                            }
                        }
                    }
                }
            LoopEnd:;
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
                        }

                        CheckPath(row, column, range, isCheckPath);
                        AttackPath(row, column, range);

                        if (targetCell.IsCurrentlyOccupied) {
                            goto LoopEnd;
                        }
                    }
                }
            LoopEnd:;
            }

            void isValidCellPawn(int row, int column, int range) {

                // Pawn forward movement
                for (int i = 1; i <= range; i++) {
                    int colNum = 0;

                    if (column == 1) {
                        colNum = i;
                    }
                    if (column == -1) {
                        colNum = i * -1;
                    }

                    if (currentCell.RowNumber + 0 < boardSize
                    && currentCell.ColumnNumber + colNum < boardSize
                    && currentCell.RowNumber + 0 >= 0
                    && currentCell.ColumnNumber + colNum >= 0) {

                        Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsLegalMove = true;

                        // If king is in check Pawn can only move to position that intercepts check line
                        if (isKingCheckBlack
                            && currentCell.Team == "Black") {
                            if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsCheckPath) {
                                Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsLegalMove = true;
                            }
                            if (!Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsCheckPath) {
                                Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsLegalMove = false;
                            }
                        }
                        if (isKingCheckWhite
                            && currentCell.Team == "White") {
                            if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsCheckPath) {
                                Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsLegalMove = true;
                            }
                            if (!Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsCheckPath) {
                                Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsLegalMove = false;
                            }
                        }
                        if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsCurrentlyOccupied) {
                            Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsLegalMove = false;
                            goto LoopEnd;
                        }

                    }
                }
                LoopEnd:;

                // Pawn take piece (attack) - diagnonal move
                void pawnAttack(int direction) {
                    if (currentCell.RowNumber + direction >= 0
                    && currentCell.RowNumber + direction < boardSize) {
                        bool isCheckPath = false;

                        // Stores the attacking line of the piece - used for preventing King moving into attacking line
                        if (currentCell.Team == "White"
                            && currentCell.Team != Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].Team) {
                            Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsAttackPathWhite = true;
                        }
                        // Stores the attacking line of the piece - used for preventing King moving into attacking line
                        if (currentCell.Team == "Black"
                            && currentCell.Team != Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].Team) {
                            Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsAttackPathBlack = true;
                        }
                        // Diagonal attack
                        if (Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsCurrentlyOccupied) {
                            Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsLegalMove = true;

                            // Place the king in check
                            if (Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].Piece == "King"
                                && Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].Team != currentCell.Team) {
                                if (Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].Team == "White") {
                                    isKingCheckWhite = true;
                                }
                                if (Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].Team == "Black") {
                                    isKingCheckBlack = true;
                                }
                                isCheckPath = true;
                            }

                            if (isCheckPath
                                && Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsLegalMove) {
                                Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsCheckPath = true;
                            }

                            if (isKingCheckWhite
                                && currentCell.Team == "White") {
                                if (Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsCheckPiece) {
                                    Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsLegalMove = true;
                                }
                                else if (!Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsCheckPiece) {
                                    Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsLegalMove = false;
                                }
                            }
                            if (isKingCheckBlack
                                && currentCell.Team == "Black") {
                                if (Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsCheckPiece) {
                                    Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsLegalMove = true;
                                }
                                else if (!Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsCheckPiece) {
                                    Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsLegalMove = false;
                                }
                            }
                        }
                    }
                }

                pawnAttack(1);
                pawnAttack(-1);
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