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

        public void CheckLegalMoves(Cell currentCell, string chessPiece, string pieceTeam) {
            int boardSize = 8;

            //Clear legal moves
            for (int x = 0; x < Size; x++) {
                for (int y = 0; y < Size; y++) {
                    Grid[y, x].IsLegalMove = false;
                }
            }

            void CheckPath(int rowCheck, int columnCheck, int rangeCheck, bool isCheckPath) {
                if (isCheckPath) {
                    for (int j = 1; j <= rangeCheck; j++) {
                        int rowNum = 0;
                        int colNum = 0;

                        if (rowCheck == 1) {
                            rowNum = j;
                        }
                        if (rowCheck == 0) {
                            rowNum = j * 0;
                        }
                        if (rowCheck == -1) {
                            rowNum = j * -1;
                        }

                        if (columnCheck == 1) {
                            colNum = j;
                        }
                        if (columnCheck == 0) {
                            colNum = j * 0;
                        }
                        if (columnCheck == -1) {
                            colNum = j * -1;
                        }

                        if (currentCell.RowNumber + rowNum < boardSize
                        && currentCell.ColumnNumber + colNum < boardSize
                        && currentCell.RowNumber + rowNum >= 0
                        && currentCell.ColumnNumber + colNum >= 0
                        && Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove) {
                            Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsCheckPath = true;

                            if (Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsCurrentlyOccupied) {
                                goto CheckLoopEnd;
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
                        // Set all cells in direction to be a legal move
                        Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove = true;

                        // Stores the attacking line of the piece - used for preventing King moving into attacking line
                        if (currentCell.Team == "White") {
                            Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsAttackPathWhite = true;
                        }
                        if (currentCell.Team == "Black") {
                            Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsAttackPathBlack = true;
                        }

                        //Checks to see if legal moves intercept the King piece of other team
                        if (Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].Piece == "King"
                            && Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove
                            && Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].Team != currentCell.Team) {
                            currentCell.IsCheckPiece = true;

                            if (Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].Team == "White") {
                                isKingCheckWhite = true;
                            }
                            if (Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].Team == "Black") {
                                isKingCheckBlack = true;
                            }
                            isCheckPath = true;
                        }

                        CheckPath(row, column, range, isCheckPath);

                        // Only moves that intercept the check line are legal when king is in check
                        if (isKingCheckWhite && currentCell.Team == "White") {
                            if (Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsCheckPath) {
                                Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove = true;
                            }
                            if (!Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsCheckPath) {
                                Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove = false;
                            }
                            if (Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsCheckPiece) {
                                Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove = true;
                            }
                        }
                        // Only moves that intercept the check line are legal when king is in check
                        if (isKingCheckBlack && currentCell.Team == "Black") {
                            if (Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsCheckPath) {
                                Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove = true;
                            }
                            if (!Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsCheckPath) {
                                Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove = false;
                            }
                            if (Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsCheckPiece) {
                                Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove = true;
                            }
                        }
                        // Prevent King entering attacking line of enemy piece
                        if (currentCell.Piece == "King") {
                            if (currentCell.Team == "White"
                                && Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsAttackPathBlack) {
                                Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove = false;
                            }
                            if (currentCell.Team == "Black"
                                && Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsAttackPathWhite) {
                                Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove = false;
                            }

                            if (isKingCheckWhite && currentCell.Team == "White") {
                                if (Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsAttackPathBlack) {
                                    Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove = false;
                                }
                                if (!Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsAttackPathBlack) {
                                    Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove = true;
                                }
                                if (Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsCheckPiece) {
                                    Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove = true;
                                }
                            }
                            if (isKingCheckBlack && currentCell.Team == "Black") {
                                if (Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsAttackPathWhite) {
                                    Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove = false;
                                }
                                if (!Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsAttackPathWhite) {
                                    Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove = true;
                                }
                                if (Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsCheckPiece) {
                                    Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsLegalMove = true;
                                }
                            }
                        }

                        if (Grid[currentCell.RowNumber + rowNum, currentCell.ColumnNumber + colNum].IsCurrentlyOccupied) {
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

                            if (isKingCheckWhite) {
                                if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove = true;
                                }
                                if (!Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove = false;
                                }
                            }

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
                    if (currentCell.Team == "White") {
                        Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsAttackPathWhite = true;
                    }
                    // Stores the attacking line of the piece - used for preventing King moving into attacking line
                    if (currentCell.Team == "Black") {
                        Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsAttackPathBlack = true;
                    }

                    // Diagonal attack
                    if (Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsCurrentlyOccupied) {
                        Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsLegalMove = true;

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

                        if (isCheckPath) {
                            if (Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsLegalMove) {
                                Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsCheckPath = true;
                            }
                        }

                        if (isKingCheckWhite && currentCell.Team == "White") {
                            if (Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsCheckPiece) {
                                Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsLegalMove = true;
                            }
                        }
                        if (isKingCheckBlack && currentCell.Team == "Black") {
                            if (Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsCheckPiece) {
                                Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsLegalMove = true;
                            }
                        }
                    }
                }

                // Attack Left  Diagonal
                if (currentCell.RowNumber + -1 >= 0
                    && currentCell.RowNumber + -1 < boardSize) {
                    bool isCheckPath = false;

                    // Stores the attacking line of the piece - used for preventing King moving into attacking line
                    if (currentCell.Team == "White") {
                        Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsAttackPathWhite = true;
                    }
                    // Stores the attacking line of the piece - used for preventing King moving into attacking line
                    if (currentCell.Team == "Black") {
                        Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsAttackPathBlack = true;
                    }

                    // Diagonal attack
                    if (Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsCurrentlyOccupied) {
                        Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsLegalMove = true;

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

                        if (isCheckPath) {
                            if (Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsLegalMove) {
                                Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsCheckPath = true;
                            }
                        }

                        if (isKingCheckWhite && currentCell.Team == "White") {
                            if (Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsCheckPiece) {
                                Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsLegalMove = true;
                            }
                        }
                        if (isKingCheckBlack && currentCell.Team == "Black") {
                            if (Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsCheckPiece) {
                                Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsLegalMove = true;
                            }
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
                    Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsLegalMove = true;

                    // Stores the attacking line of the piece - used for preventing King moving into attacking line
                    if (currentCell.Team == "White") {
                        Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsAttackPathWhite = true;
                    }
                    // Stores the attacking line of the piece - used for preventing King moving into attacking line
                    if (currentCell.Team == "Black") {
                        Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsAttackPathBlack = true;
                    }

                    if (Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].Piece == "King"
                        && Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].Team != currentCell.Team) {
                        if (Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].Team == "White") {
                            isKingCheckWhite = true;
                        }
                        if (Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].Team == "Black") {
                            isKingCheckBlack = true;
                        }
                        isCheckPath = true;
                    }

                    if (isCheckPath) {
                        if (Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsLegalMove) {
                            Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsCheckPath = true;

                            if (Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsCurrentlyOccupied) {
                                goto CheckLoopEnd;
                            }
                        }
                    }
                CheckLoopEnd:;

                    // Only moves that intercept the check line are legal when king is in check
                    if (isKingCheckWhite && currentCell.Team == "White") {
                        if (Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsCheckPath) {
                            Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsLegalMove = true;
                        }
                        if (!Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsCheckPath) {
                            Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsLegalMove = false;
                        }
                        if (Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsCheckPiece) {
                            Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsLegalMove = true;
                        }
                    }
                    if (isKingCheckBlack && currentCell.Team == "Black") {
                        if (Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsCheckPath) {
                            Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsLegalMove = true;
                        }
                        if (!Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsCheckPath) {
                            Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsLegalMove = false;
                        }
                        if (Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsCheckPiece) {
                            Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsLegalMove = true;
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