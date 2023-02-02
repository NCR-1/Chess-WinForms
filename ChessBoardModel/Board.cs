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
        public int legalMovesCounterWhite = 0;   
        public int legalMovesCounterBlack = 0;
        public int legalMovesCounter = 0;

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
                                goto LoopEnd;
                            }
                        }
                    }
                }
            LoopEnd:;
            }

            void CheckAttackPath(int row, int column, int range, bool isKingAttackLineWhite, bool isKingAttackLineBlack) {
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

                        if (isKingAttackLineWhite) {
                            targetCell.IsAttackPathWhite = true;

                            if (targetCell.Piece == "King") {
                                goto LoopEnd;
                            }
                        }

                        if (currentCell.Team == "White"
                            && currentCell.IsAttackPathBlack
                            && !targetCell.IsAttackPathBlack) {
                            targetCell.IsLegalMove = false;
                        }

                        if (isKingAttackLineBlack) {
                            targetCell.IsAttackPathBlack = true;

                            if (targetCell.Piece == "King") {
                                goto LoopEnd;
                            }
                        }

                        if (currentCell.Team == "Black" 
                            && currentCell.IsAttackPathWhite
                            && !targetCell.IsAttackPathWhite) {
                            targetCell.IsLegalMove = false;
                        }
                    }
                }
            LoopEnd:;
            }

            void AttackPath(int row, int column, int range) {

                bool isKingAttackLineWhite = false;
                bool isKingAttackLineBlack = false;

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

                        if (currentCell.Team == "White"
                            && targetCell.Piece == "King"
                            && targetCell.Team != currentCell.Team) {

                            isKingAttackLineWhite = true;
                            currentCell.IsAttackPiece = true;
                        }

                        if (currentCell.Team == "Black"
                            && targetCell.Piece == "King"
                            && targetCell.Team != currentCell.Team) {

                            isKingAttackLineBlack = true;
                            currentCell.IsAttackPiece = true;
                        }
                        // Checks if the path of the piece intercepts with the King but is blocked therfore not placing it in check
                        CheckAttackPath(row, column, range, isKingAttackLineWhite, isKingAttackLineBlack);
                    }
                }
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

                        // Prevent being able to move to space occupied by own team
                        if (currentCell.Team == targetCell.Team) {
                            targetCell.IsLegalMove = false;
                        }

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
                                if (targetCell.IsCheckPath
                                    && targetCell.Team != currentCell.Team) {
                                    targetCell.IsLegalMove = true;
                                }
                                if (!targetCell.IsCheckPath 
                                    && targetCell.Team != currentCell.Team) {

                                    targetCell.IsLegalMove = false;
                                }
                                if (targetCell.IsCheckPiece 
                                    && targetCell.Team != currentCell.Team) {

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
                                if (targetCell.IsCheckPath
                                    && targetCell.Team != currentCell.Team) {
                                    targetCell.IsLegalMove = true;
                                }
                                if (!targetCell.IsCheckPath 
                                    && targetCell.Team != currentCell.Team) {

                                    targetCell.IsLegalMove = false;
                                }
                                if (targetCell.IsCheckPiece 
                                    && targetCell.Team != currentCell.Team) {

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

                        if (targetCell.IsLegalMove) {
                            currentCell.LegalMovesCounter++;
                        }

                        if (targetCell.IsCurrentlyOccupied
                            && targetCell.Piece != "King") {
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

                        // Prevent being able to move to space occupied by own team
                        if (currentCell.Team == Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].Team) {
                            Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsLegalMove = false;
                        }

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

                        if (currentCell.Team == "White"
                            && currentCell.IsAttackPathBlack
                            && !Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsAttackPathBlack) {
                            Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsLegalMove = false;
                        }

                        if (currentCell.Team == "Black"
                            && currentCell.IsAttackPathWhite
                            && !Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsAttackPathWhite) {
                            Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsLegalMove = false;
                        }

                        if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsLegalMove) {
                            currentCell.LegalMovesCounter++;
                        }

                        if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsCurrentlyOccupied) {
                            Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + colNum].IsLegalMove = false;
                            goto LoopEnd;
                        }

                    }
                }
                LoopEnd:;

                //Pawn take piece(attack) - diagnonal move
                void pawnAttack(int direction) {
                    if (currentCell.RowNumber + direction >= 0
                    && currentCell.RowNumber + direction < boardSize) {
                        bool isCheckPath = false;

                        // Diagonal attack
                        if (Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsCurrentlyOccupied) {
                            Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsLegalMove = true;

                            // Prevent being able to move to space occupied by own team
                            if (currentCell.Team == Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].Team) {
                                Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsLegalMove = false;
                            }

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

                            if (currentCell.Team == "White"
                                && currentCell.IsAttackPathBlack
                                && !Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsAttackPathBlack) {
                                Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsLegalMove = false;

                                if (Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsAttackPiece) {
                                    Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsLegalMove = true;
                                }
                            }

                            if (currentCell.Team == "Black"
                                && currentCell.IsAttackPathWhite
                                && !Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsAttackPathWhite) {
                                Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsLegalMove = false;

                                if (Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsAttackPiece) {
                                    Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsLegalMove = true;
                                }
                            }
                        }
                        
                        if (Grid[currentCell.RowNumber + row + direction, currentCell.ColumnNumber + column].IsLegalMove) {
                            currentCell.LegalMovesCounter++;
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

                    // Prevent being able to move to space occupied by own team
                    if (currentCell.Team == targetCell.Team) {
                        targetCell.IsLegalMove = false;
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
                            goto LoopEnd;
                        }
                    }
                LoopEnd:;

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

                    if (targetCell.IsLegalMove) {
                        currentCell.LegalMovesCounter++;
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

                    AttackPath(0, -1, 1); // Up
                    AttackPath(0, +1, 1); // Down
                    AttackPath(-1, 0, 1); // Left
                    AttackPath(+1, 0, 1); // Right
                    AttackPath(-1, -1, 1); // Up-Left
                    AttackPath(+1, -1, 1); // Up-Right
                    AttackPath(-1, +1, 1); // Down-Left
                    AttackPath(+1, +1, 1); // Down-Right

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

                    AttackPath(0, -1, 7); // Up
                    AttackPath(0, +1, 7); // Down
                    AttackPath(-1, 0, 7); // Left
                    AttackPath(+1, 0, 7); // Right
                    AttackPath(-1, -1, 7); // Up-Left
                    AttackPath(+1, -1, 7); // Up-Right
                    AttackPath(-1, +1, 7); // Down-Left
                    AttackPath(+1, +1, 7); // Down-Right

                    break;

                case "Rook":
                    try { currentCell.IsCurrentlyOccupied = true; } catch { }

                    isValidCell(0, -1, 7); // Up
                    isValidCell(0, +1, 7); // Down
                    isValidCell(-1, 0, 7); // Left
                    isValidCell(+1, 0, 7); // Right

                    AttackPath(0, -1, 7); // Up
                    AttackPath(0, +1, 7); // Down
                    AttackPath(-1, 0, 7); // Left
                    AttackPath(+1, 0, 7); // Right

                    break;

                case "Bishop":
                    try { currentCell.IsCurrentlyOccupied = true; } catch { }

                    isValidCell(-1, -1, 7); // Up-Left
                    isValidCell(+1, -1, 7); // Up-Right
                    isValidCell(-1, +1, 7); // Down-Left
                    isValidCell(+1, +1, 7); // Down-Right

                    AttackPath(-1, -1, 7); // Up-Left
                    AttackPath(+1, -1, 7); // Up-Right
                    AttackPath(-1, +1, 7); // Down-Left
                    AttackPath(+1, +1, 7); // Down-Right

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

            for (int x = 0; x < Size; x++) {
                for (int y = 0; y < Size; y++) {
                    if (Grid[y, x].IsLegalMove) {
                        legalMovesCounter++;
                    }
                }
            }
        }
    }
}