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

        public bool isKingCheck = false;

        public void CheckLegalMoves(Cell currentCell, string chessPiece, string pieceTeam) {
            int boardSize = 8;

            //Clear moves
            for (int x = 0; x < Size; x++) {
                for (int y = 0; y < Size; y++) {
                    Grid[y, x].IsLegalMove = false;
                    //Grid[y, x].IsCheckPiece = false;
                    //Grid[y, x].IsCheckPath = false;
                }
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

                            if (isKingCheck) {
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

                // Attack Black
                if (currentCell.RowNumber + 1 >= 0
                    && currentCell.RowNumber + 1 < boardSize) { 
                    bool isCheckPath = false;

                    // Diagonal attack
                    if (Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsCurrentlyOccupied) {
                        Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsLegalMove = true;

                        if (Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].Piece == "King"
                            && Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].Team != Grid[currentCell.RowNumber, currentCell.ColumnNumber].Team) {
                            isKingCheck = true;
                            isCheckPath = true;
                        }

                        if (isCheckPath) {
                            if (Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsLegalMove) {
                                Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsCheckPath = true;
                            }
                        }

                        if (isKingCheck) {
                            if (Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsCheckPiece) {
                                Grid[currentCell.RowNumber + row + 1, currentCell.ColumnNumber + column].IsLegalMove = true;
                            }
                        }
                    }
                }

                // Up (White)
                if (row == 0 && column == -1) {
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + 0 < boardSize
                        && currentCell.ColumnNumber + -i < boardSize
                        && currentCell.RowNumber + 0 >= 0
                        && currentCell.ColumnNumber + -i >= 0) {
                            Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove = true;

                            if (isKingCheck) {
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

                // Attack White
                if (currentCell.RowNumber + -1 >= 0
                    && currentCell.RowNumber + -1 < boardSize) {
                    bool isCheckPath = false;

                    // Diagonal attack
                    if (Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsCurrentlyOccupied) {
                        Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsLegalMove = true;

                        if (Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].Piece == "King"
                            && Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].Team != Grid[currentCell.RowNumber, currentCell.ColumnNumber].Team) {
                            isKingCheck = true;
                            isCheckPath = true;
                        }

                        if (isCheckPath) {
                            if (Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsLegalMove) {
                                Grid[currentCell.RowNumber + row + -1, currentCell.ColumnNumber + column].IsCheckPath = true;
                            }
                        }

                        if (isKingCheck) {
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

                    if (Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].Piece == "King"
                        && Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].Team != Grid[currentCell.RowNumber, currentCell.ColumnNumber].Team) {
                        isKingCheck = true;
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
                    if (isKingCheck) {
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

            void isValidCell(int row, int column, int range) {
                // Right
                if (row == +1 && column == 0) {
                    bool isCheckPath = false;
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + i < boardSize
                        && currentCell.ColumnNumber + 0 < boardSize
                        && currentCell.RowNumber + i >= 0
                        && currentCell.ColumnNumber + 0 >= 0) {
                            // Set all cells in direction to be a legal move
                            Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsLegalMove = true;

                            //Checks to see if legal moves intercept the King piece of other team
                            if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].Piece == "King"
                                && Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsLegalMove
                                && Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].Team != Grid[currentCell.RowNumber, currentCell.ColumnNumber].Team) {

                                Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCheckPiece = true;

                                isKingCheck = true;
                                isCheckPath = true;
                            }

                            // Set .IsCheckPath property of cell to true => used for visually showing when king is in check
                            if (isCheckPath) {
                                for (int j = 1; j <= range; j++) {
                                    if (currentCell.RowNumber + j < boardSize
                                    && currentCell.ColumnNumber + 0 < boardSize
                                    && currentCell.RowNumber + j >= 0
                                    && currentCell.ColumnNumber + 0 >= 0
                                    && Grid[currentCell.RowNumber + j, currentCell.ColumnNumber + 0].IsLegalMove) {
                                        Grid[currentCell.RowNumber + j, currentCell.ColumnNumber + 0].IsCheckPath = true;

                                        if (Grid[currentCell.RowNumber + j, currentCell.ColumnNumber + 0].IsCurrentlyOccupied) {
                                            goto CheckLoopEnd;
                                        }
                                    }
                                }
                            }
                            CheckLoopEnd:;

                            // Only moves that intercept the check line are legal when king is in check
                            if (isKingCheck) {
                                if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsCheckPath) {
                                    Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsLegalMove = true;
                                }
                                if (!Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsCheckPath) {
                                    Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsLegalMove = false;
                                }
                                if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsCheckPiece) {
                                    Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsLegalMove = true;
                                }
                                if (Grid[currentCell.RowNumber, currentCell.ColumnNumber].Piece == "King") {
                                    if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsCheckPath) {
                                        Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsLegalMove = false;
                                    }
                                    if (!Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsCheckPath) {
                                        Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsLegalMove = true;
                                    }
                                    if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsCheckPiece) {
                                        Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsLegalMove = true;
                                    }
                                }
                            }

                            if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsCurrentlyOccupied) {
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }
                // Left
                if (row == -1 && column == 0) {
                    bool isCheckPath = false;
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + -i < boardSize
                        && currentCell.ColumnNumber + 0 < boardSize
                        && currentCell.RowNumber + -i >= 0
                        && currentCell.ColumnNumber + 0 >= 0) {
                            Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsLegalMove = true;

                            if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].Piece == "King"
                                && Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsLegalMove
                                && Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].Team != Grid[currentCell.RowNumber, currentCell.ColumnNumber].Team) {

                                Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCheckPiece = true;

                                isKingCheck = true;
                                isCheckPath = true;
                            }

                            if (isCheckPath) {
                                for (int j = 1; j <= range; j++) {
                                    if (currentCell.RowNumber + -j < boardSize
                                    && currentCell.ColumnNumber + 0 < boardSize
                                    && currentCell.RowNumber + -j >= 0
                                    && currentCell.ColumnNumber + 0 >= 0
                                    && Grid[currentCell.RowNumber + -j, currentCell.ColumnNumber + 0].IsLegalMove) {
                                        Grid[currentCell.RowNumber + -j, currentCell.ColumnNumber + 0].IsCheckPath = true;

                                        if (Grid[currentCell.RowNumber + -j, currentCell.ColumnNumber + 0].IsCurrentlyOccupied) {
                                            goto CheckLoopEnd;
                                        }
                                    }
                                }
                            }
                            CheckLoopEnd:;

                            if (isKingCheck) {
                                if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsCheckPath) {
                                    Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsLegalMove = true;
                                }
                                if (!Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsCheckPath) {
                                    Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsLegalMove = false;
                                }
                                if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsCheckPiece) {
                                    Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsLegalMove = true;
                                }
                                if (Grid[currentCell.RowNumber, currentCell.ColumnNumber].Piece == "King") {
                                    if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsCheckPath) {
                                        Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsLegalMove = false;
                                    }
                                    if (!Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsCheckPath) {
                                        Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsLegalMove = true;
                                    }
                                    if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsCheckPiece) {
                                        Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsLegalMove = true;
                                    }
                                }
                            }

                            if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsCurrentlyOccupied) {
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }
                // Down
                if (row == 0 && column == +1) {
                    bool isCheckPath = false;
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + 0 < boardSize
                        && currentCell.ColumnNumber + i < boardSize
                        && currentCell.RowNumber + 0 >= 0
                        && currentCell.ColumnNumber + i >= 0) {
                            Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsLegalMove = true;

                            if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].Piece == "King"
                                && Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsLegalMove
                                && Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].Team != Grid[currentCell.RowNumber, currentCell.ColumnNumber].Team) {

                                Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCheckPiece = true;

                                isKingCheck = true;
                                isCheckPath = true;
                            }

                            if (isCheckPath) {
                                for (int j = 1; j <= range; j++) {
                                    if (currentCell.RowNumber + 0 < boardSize
                                    && currentCell.ColumnNumber + j < boardSize
                                    && currentCell.RowNumber + 0 >= 0
                                    && currentCell.ColumnNumber + j >= 0
                                    && Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + j].IsLegalMove) {
                                        Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + j].IsCheckPath = true;

                                        if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + j].IsCurrentlyOccupied) {
                                            goto CheckLoopEnd;
                                        }
                                    }
                                }
                            }
                            CheckLoopEnd:;

                            if (isKingCheck) {
                                if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsLegalMove = true;
                                }
                                if (!Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsLegalMove = false;
                                }
                                if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsCheckPiece) {
                                    Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsLegalMove = true;
                                }
                                if (Grid[currentCell.RowNumber, currentCell.ColumnNumber].Piece == "King") {
                                    if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsCheckPath) {
                                        Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsLegalMove = false;
                                    }
                                    if (!Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsCheckPath) {
                                        Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsLegalMove = true;
                                    }
                                    if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsCheckPiece) {
                                        Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsLegalMove = true;
                                    }
                                }
                            }

                            if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsCurrentlyOccupied) {
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }
                // Up
                if (row == 0 && column == -1) {
                    bool isCheckPath = false;
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + 0 < boardSize
                        && currentCell.ColumnNumber + -i < boardSize
                        && currentCell.RowNumber + 0 >= 0
                        && currentCell.ColumnNumber + -i >= 0) {
                            Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove = true;

                            if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].Piece == "King"
                                && Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove
                                && Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].Team != Grid[currentCell.RowNumber, currentCell.ColumnNumber].Team) {

                                Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCheckPiece = true;

                                isKingCheck = true;
                                isCheckPath = true;
                            }

                            if (isCheckPath) {
                                for (int j = 1; j <= range; j++) {
                                    if (currentCell.RowNumber + 0 < boardSize
                                    && currentCell.ColumnNumber + -j < boardSize
                                    && currentCell.RowNumber + 0 >= 0
                                    && currentCell.ColumnNumber + -j >= 0
                                    && Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -j].IsLegalMove) {
                                        Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -j].IsCheckPath = true;

                                        if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -j].IsCurrentlyOccupied) {
                                            goto CheckLoopEnd;
                                        }
                                    }
                                }
                            }
                            CheckLoopEnd:;

                            if (isKingCheck) {
                                if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove = true;
                                }
                                if (!Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove = false;
                                }
                                if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsCheckPiece) {
                                    Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove = true;
                                }
                                if (Grid[currentCell.RowNumber, currentCell.ColumnNumber].Piece == "King") {
                                    if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsCheckPath) {
                                        Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove = false;
                                    }
                                    if (!Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsCheckPath) {
                                        Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove = true;
                                    }
                                    if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsCheckPiece) {
                                        Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove = true;
                                    }
                                }
                            }

                            if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsCurrentlyOccupied) {
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }
                // Down-Right
                if (row == +1 && column == +1) {
                    bool isCheckPath = false;
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + i < boardSize
                        && currentCell.ColumnNumber + i < boardSize
                        && currentCell.RowNumber + i >= 0
                        && currentCell.ColumnNumber + i >= 0) {
                            Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsLegalMove = true;

                            if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].Piece == "King"
                                && Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsLegalMove
                                && Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].Team != Grid[currentCell.RowNumber, currentCell.ColumnNumber].Team) {

                                Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCheckPiece = true;

                                isKingCheck = true;
                                isCheckPath = true;
                            }

                            if (isCheckPath) {
                                for (int j = 1; j <= range; j++) {
                                    if (currentCell.RowNumber + j < boardSize
                                    && currentCell.ColumnNumber + j < boardSize
                                    && currentCell.RowNumber + j >= 0
                                    && currentCell.ColumnNumber + j >= 0
                                    && Grid[currentCell.RowNumber + j, currentCell.ColumnNumber + j].IsLegalMove) {
                                        Grid[currentCell.RowNumber + j, currentCell.ColumnNumber + j].IsCheckPath = true;

                                        if (Grid[currentCell.RowNumber + j, currentCell.ColumnNumber + j].IsCurrentlyOccupied) {
                                            goto CheckLoopEnd;
                                        }
                                    }
                                }
                            }
                        CheckLoopEnd:;

                            if (isKingCheck) {
                                if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsLegalMove = true;
                                }
                                if (!Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsLegalMove = false;
                                }
                                if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsCheckPiece) {
                                    Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsLegalMove = true;
                                }
                                if (Grid[currentCell.RowNumber, currentCell.ColumnNumber].Piece == "King") {
                                    if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsCheckPath) {
                                        Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsLegalMove = false;
                                    }
                                    if (!Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsCheckPath) {
                                        Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsLegalMove = true;
                                    }
                                    if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsCheckPiece) {
                                        Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsLegalMove = true;
                                    }
                                }
                            }

                            if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsCurrentlyOccupied) {
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }
                // Up-Left
                if (row == -1 && column == -1) {
                    bool isCheckPath = false;
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + -i < boardSize
                        && currentCell.ColumnNumber + -i < boardSize
                        && currentCell.RowNumber + -i >= 0
                        && currentCell.ColumnNumber + -i >= 0) {
                            Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsLegalMove = true;

                            if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].Piece == "King"
                                && Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsLegalMove
                                && Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].Team != Grid[currentCell.RowNumber, currentCell.ColumnNumber].Team) {

                                Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCheckPiece = true;

                                isKingCheck = true;
                                isCheckPath = true;
                            }

                            if (isCheckPath) {
                                for (int j = 1; j <= range; j++) {
                                    if (currentCell.RowNumber + -j < boardSize
                                    && currentCell.ColumnNumber + -j < boardSize
                                    && currentCell.RowNumber + -j >= 0
                                    && currentCell.ColumnNumber + j >= 0
                                    && Grid[currentCell.RowNumber + -j, currentCell.ColumnNumber + -j].IsLegalMove) {
                                        Grid[currentCell.RowNumber + -j, currentCell.ColumnNumber + -j].IsCheckPath = true;

                                        if (Grid[currentCell.RowNumber + -j, currentCell.ColumnNumber + -j].IsCurrentlyOccupied) {
                                            goto CheckLoopEnd;
                                        }
                                    }
                                }
                            }
                            CheckLoopEnd:;

                            if (isKingCheck) {
                                if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsLegalMove = true;
                                }
                                if (!Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsLegalMove = false;
                                }
                                if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsCheckPiece) {
                                    Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsLegalMove = true;
                                }
                                if (Grid[currentCell.RowNumber, currentCell.ColumnNumber].Piece == "King") {
                                    if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsCheckPath) {
                                        Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsLegalMove = false;
                                    }
                                    if (!Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsCheckPath) {
                                        Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsLegalMove = true;
                                    }
                                    if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsCheckPiece) {
                                        Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsLegalMove = true;
                                    }
                                }
                            }

                            if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsCurrentlyOccupied) {
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }
                // Up-Right
                if (row == +1 && column == -1) {
                    bool isCheckPath = false;
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + i < boardSize
                        && currentCell.ColumnNumber + -i < boardSize
                        && currentCell.RowNumber + i >= 0
                        && currentCell.ColumnNumber + -i >= 0) {
                            Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsLegalMove = true;

                            if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].Piece == "King"
                                && Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsLegalMove
                                && Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].Team != Grid[currentCell.RowNumber, currentCell.ColumnNumber].Team) {

                                Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCheckPiece = true;

                                isKingCheck = true;
                                isCheckPath = true;
                            }

                            if (isCheckPath) {
                                for (int j = 1; j <= range; j++) {
                                    if (currentCell.RowNumber + j < boardSize
                                    && currentCell.ColumnNumber + -j < boardSize
                                    && currentCell.RowNumber + j >= 0
                                    && currentCell.ColumnNumber + -j >= 0
                                    && Grid[currentCell.RowNumber + j, currentCell.ColumnNumber + -j].IsLegalMove
                                    ) {
                                        Grid[currentCell.RowNumber + j, currentCell.ColumnNumber + -j].IsCheckPath = true;

                                        if (Grid[currentCell.RowNumber + j, currentCell.ColumnNumber + -j].IsCurrentlyOccupied) {
                                            goto CheckLoopEnd;
                                        }
                                    }
                                }
                            }
                            CheckLoopEnd:;

                            if (isKingCheck) {
                                if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsLegalMove = true;
                                }
                                if (!Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsLegalMove = false;
                                }
                                if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsCheckPiece) {
                                    Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsLegalMove = true;
                                }
                                if (Grid[currentCell.RowNumber, currentCell.ColumnNumber].Piece == "King") {
                                    if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsCheckPath) {
                                        Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsLegalMove = false;
                                    }
                                    if (!Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsCheckPath) {
                                        Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsLegalMove = true;
                                    }
                                    if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsCheckPiece) {
                                        Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsLegalMove = true;
                                    }
                                }
                            }

                            if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsCurrentlyOccupied) {
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }
                // Down-Left
                if (row == -1 && column == +1) {
                    bool isCheckPath = false;
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + -i < boardSize
                        && currentCell.ColumnNumber + i < boardSize
                        && currentCell.RowNumber + -i >= 0
                        && currentCell.ColumnNumber + i >= 0) {
                            Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsLegalMove = true;

                            if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].Piece == "King"
                                && Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsLegalMove
                                && Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].Team != Grid[currentCell.RowNumber, currentCell.ColumnNumber].Team) {

                                Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCheckPiece = true;

                                isKingCheck = true;
                                isCheckPath = true;
                            }

                            if (isCheckPath) {
                                for (int j = 1; j <= range; j++) {
                                    if (currentCell.RowNumber + -j < boardSize
                                    && currentCell.ColumnNumber + j < boardSize
                                    && currentCell.RowNumber + -j >= 0
                                    && currentCell.ColumnNumber + j >= 0
                                    && Grid[currentCell.RowNumber + -j, currentCell.ColumnNumber + j].IsLegalMove) {
                                        Grid[currentCell.RowNumber + -j, currentCell.ColumnNumber + j].IsCheckPath = true;

                                        if (Grid[currentCell.RowNumber + -j, currentCell.ColumnNumber + j].IsCurrentlyOccupied) {
                                            goto CheckLoopEnd;
                                        }
                                    }
                                }
                            }
                            CheckLoopEnd:;

                            if (isKingCheck) {
                                if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsLegalMove = true;
                                }
                                if (!Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsCheckPath) {
                                    Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsLegalMove = false;
                                }
                                if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsCheckPiece) {
                                    Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsLegalMove = true;
                                }
                                if (Grid[currentCell.RowNumber, currentCell.ColumnNumber].Piece == "King") {
                                    if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsCheckPath) {
                                        Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsLegalMove = false;
                                    }
                                    if (!Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsCheckPath) {
                                        Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsLegalMove = true;
                                    }
                                    if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsCheckPiece) {
                                        Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsLegalMove = true;
                                    }
                                }
                            }

                            if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsCurrentlyOccupied) {
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }
            }

            // Define all legal moves for piece
            switch (chessPiece) {
                case "King":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

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
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

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
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

                    isValidCell(0, -1, 7); // Up
                    isValidCell(0, +1, 7); // Down
                    isValidCell(-1, 0, 7); // Left
                    isValidCell(+1, 0, 7); // Right

                    break;

                case "Bishop":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

                    isValidCell(-1, -1, 7); // Up-Left
                    isValidCell(+1, -1, 7); // Up-Right
                    isValidCell(-1, +1, 7); // Down-Left
                    isValidCell(+1, +1, 7); // Down-Right

                    break;

                case "Knight":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

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
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

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