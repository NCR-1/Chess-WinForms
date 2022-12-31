using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ChessBoardModel {
    public class Board {

        // Default size is 8x8 (A-H, 1-8)
        public int Size { get; set; }

        // 2D array of type cell
        public Cell[,] Grid { get; set; }

        // Board constructor
        public Board (int size) {
            
            Size = size;

            // Create new 2d array
            Grid = new Cell[Size, Size];

            // Fill 2d array with new cells, each has unique coordinate
            for (int x = 0; x < Size; x++) {
                for (int y = 0; y < Size; y++) {
                    Grid[x, y] = new Cell(x, y);
                }
            }
        }

        public void CheckLegalMoves(Cell currentCell, string chessPiece) {

            int boardSize = 8;

            bool isBlocked = false;

            //Clear moves
            for (int x = 0; x < Size; x++) {
                for (int y = 0; y < Size; y++) {
                    Grid[x, y].IsLegalMove = false;
                }
            }

            // Checks if index is in bounds to avoid IndexOutOfBounds error setting cell as valid move
            void isValidCellSpecial(int row, int column) {
                if (isBlocked == false) {
                    if (currentCell.RowNumber + row < boardSize
                        && currentCell.ColumnNumber + column < boardSize
                        && currentCell.RowNumber + row >= 0
                        && currentCell.ColumnNumber + column >= 0) {
                        Grid[currentCell.RowNumber + row, currentCell.ColumnNumber + column].IsLegalMove = true;
                    }
                }
            }

            void isValidCell(int row, int column, int range) {
                if (row == +1 && column == 0) {
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + i < boardSize
                        && currentCell.ColumnNumber + 0 < boardSize
                        && currentCell.RowNumber + i >= 0
                        && currentCell.ColumnNumber + 0 >= 0) {
                            Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsLegalMove = true;

                            if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + 0].IsCurrentlyOccupied == true) {
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }
                if (row == -1 && column == 0) {
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + -i < boardSize
                        && currentCell.ColumnNumber + 0 < boardSize
                        && currentCell.RowNumber + -i >= 0
                        && currentCell.ColumnNumber + 0 >= 0) {
                            Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsLegalMove = true;

                            if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + 0].IsCurrentlyOccupied == true) {
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }
                if (row == 0 && column == +1) {
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + 0 < boardSize
                        && currentCell.ColumnNumber + i < boardSize
                        && currentCell.RowNumber + 0 >= 0
                        && currentCell.ColumnNumber + i >= 0) {
                            Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsLegalMove = true;

                            if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + i].IsCurrentlyOccupied == true) {
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }
                if (row == 0 && column == -1) {
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + 0 < boardSize
                        && currentCell.ColumnNumber + -i < boardSize
                        && currentCell.RowNumber + 0 >= 0
                        && currentCell.ColumnNumber + -i >= 0) {
                            Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsLegalMove = true;

                            if (Grid[currentCell.RowNumber + 0, currentCell.ColumnNumber + -i].IsCurrentlyOccupied == true) {
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }
                if (row == +1 && column == +1) {
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + i < boardSize
                        && currentCell.ColumnNumber + i < boardSize
                        && currentCell.RowNumber + i >= 0
                        && currentCell.ColumnNumber + i >= 0) {
                            Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsLegalMove = true;

                            if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + i].IsCurrentlyOccupied == true) {
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }
                if (row == -1 && column == -1) {
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + -i < boardSize
                        && currentCell.ColumnNumber + -i < boardSize
                        && currentCell.RowNumber + -i >= 0
                        && currentCell.ColumnNumber + -i >= 0) {
                            Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsLegalMove = true;

                            if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + -i].IsCurrentlyOccupied == true) {
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }
                if (row == +1 && column == -1) {
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + i < boardSize
                        && currentCell.ColumnNumber + -i < boardSize
                        && currentCell.RowNumber + i >= 0
                        && currentCell.ColumnNumber + -i >= 0) {
                            Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsLegalMove = true;

                            if (Grid[currentCell.RowNumber + i, currentCell.ColumnNumber + -i].IsCurrentlyOccupied == true) {
                                goto LoopEnd;
                            }
                        }
                    }
                LoopEnd:;
                }
                if (row == -1 && column == +1) {
                    bool isBlocked1 = false;
                    for (int i = 1; i <= range; i++) {
                        if (currentCell.RowNumber + -i < boardSize
                        && currentCell.ColumnNumber + i < boardSize
                        && currentCell.RowNumber + -i >= 0
                        && currentCell.ColumnNumber + i >= 0
                        && isBlocked1 ==  false) {
                            Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsLegalMove = true;

                            if (Grid[currentCell.RowNumber + -i, currentCell.ColumnNumber + i].IsCurrentlyOccupied == true) {
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
                    isValidCell(-1, -1, 1); //Up-Left
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
                    isValidCell(-1, -1, 7); //Up-Left
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

                    isValidCell(-1, -1, 7); //Up-Left
                    isValidCell(+1, -1, 7); // Up-Right
                    isValidCell(-1, +1, 7); // Down-Left
                    isValidCell(+1, +1, 7); // Down-Right
                    break;

                case "Knight":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

                    isValidCellSpecial(+2, +1);
                    isValidCellSpecial(-2, -1);
                    isValidCellSpecial(+2, -1);
                    isValidCellSpecial(-2, +1);
                    isValidCellSpecial(+1, +2);
                    isValidCellSpecial(-1, -2);
                    isValidCellSpecial(+1, -2);
                    isValidCellSpecial(-1, +2);
                    break;

                case "Pawn W":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

                    isValidCellSpecial(0, -1);

                    if (currentCell.ColumnNumber == 6) {
                        isValidCellSpecial(0, -1);
                        isValidCellSpecial(0, -2);
                    }
                    break;

                case "Pawn B":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

                    isValidCellSpecial(0, +1);

                    if (currentCell.ColumnNumber == 1) {
                        isValidCellSpecial(0, +1);
                        isValidCellSpecial(0, +2);
                    }
                    break;
            }
        }
    }

    public class ButtonTag {
        public string Team { get; set; }
        public Point Position { get; set; }
    }
}
