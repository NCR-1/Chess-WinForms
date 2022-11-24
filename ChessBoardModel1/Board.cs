using System;
using System.Collections.Generic;
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

        public void ShowLegalMoves(Cell currentCell, string chessPiece) {

            int boardSize = 8;

            // Clear moves
            for (int x = 0; x < Size; x++) {
                for (int y = 0; y < Size; y++) {
                    Grid[x, y].IsLegalMove = false;
                    Grid[x, y].IsCurrentlyOccupied = false;
                }
            }

            // Checks if index is in bounds to avoid IndexOutOfBounds error setting cell as valid move
            void isValidCell(int row, int column) {
                if (currentCell.RowNumber + row < boardSize && currentCell.ColumnNumber + column < boardSize && currentCell.RowNumber + row >= 0 && currentCell.ColumnNumber + column >= 0) {
                    Grid[currentCell.RowNumber + row , currentCell.ColumnNumber + column].IsLegalMove = true;
                }
            }

            // Find all legal moves for piece
            switch (chessPiece) {
                case "King":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

                    isValidCell(+1, 0);
                    isValidCell(-1, 0);
                    isValidCell(0, +1);
                    isValidCell(0, -1);
                    isValidCell(+1, +1);
                    isValidCell(-1, -1);
                    isValidCell(+1, -1);
                    isValidCell(-1, +1);
                    break;

                case "Queen":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

                    isValidCell(+1, 0);
                    isValidCell(+2, 0);
                    isValidCell(+3, 0);
                    isValidCell(+4, 0);
                    isValidCell(+5, 0);
                    isValidCell(+6, 0);
                    isValidCell(+7, 0);
                    isValidCell(-1, 0);
                    isValidCell(-2, 0);
                    isValidCell(-3, 0);
                    isValidCell(-4, 0);
                    isValidCell(-5, 0);
                    isValidCell(-6, 0);
                    isValidCell(-7, 0);
                    isValidCell(0, -1);
                    isValidCell(0, -2);
                    isValidCell(0, -3);
                    isValidCell(0, -4);
                    isValidCell(0, -5);
                    isValidCell(0, -6);
                    isValidCell(0, -7);
                    isValidCell(0, +1);
                    isValidCell(0, +2);
                    isValidCell(0, +3);
                    isValidCell(0, +4);
                    isValidCell(0, +5);
                    isValidCell(0, +6);
                    isValidCell(0, +7);

                    isValidCell(+1, +1);
                    isValidCell(+2, +2);
                    isValidCell(+3, +3);
                    isValidCell(+4, +4);
                    isValidCell(+5, +5);
                    isValidCell(+6, +6);
                    isValidCell(+7, +7);
                    isValidCell(-1, -1);
                    isValidCell(-2, -2);
                    isValidCell(-3, -3);
                    isValidCell(-4, -4);
                    isValidCell(-5, -5);
                    isValidCell(-6, -6);
                    isValidCell(-7, -7);
                    isValidCell(-1, +1);
                    isValidCell(-2, +2);
                    isValidCell(-3, +3);
                    isValidCell(-4, +4);
                    isValidCell(-5, +5);
                    isValidCell(-6, +6);
                    isValidCell(-7, +7);
                    isValidCell(+1, -1);
                    isValidCell(+2, -2);
                    isValidCell(+3, -3);
                    isValidCell(+4, -4);
                    isValidCell(+5, -5);
                    isValidCell(+6, -6);
                    isValidCell(+7, -7);
                    break;

                case "Rook":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

                    isValidCell(+1, 0);
                    isValidCell(+2, 0);
                    isValidCell(+3, 0);
                    isValidCell(+4, 0);
                    isValidCell(+5, 0);
                    isValidCell(+6, 0);
                    isValidCell(+7, 0);
                    isValidCell(-1, 0);
                    isValidCell(-2, 0);
                    isValidCell(-3, 0);
                    isValidCell(-4, 0);
                    isValidCell(-5, 0);
                    isValidCell(-6, 0);
                    isValidCell(-7, 0);
                    isValidCell(0, -1);
                    isValidCell(0, -2);
                    isValidCell(0, -3);
                    isValidCell(0, -4);
                    isValidCell(0, -5);
                    isValidCell(0, -6);
                    isValidCell(0, -7);
                    isValidCell(0, +1);
                    isValidCell(0, +2);
                    isValidCell(0, +3);
                    isValidCell(0, +4);
                    isValidCell(0, +5);
                    isValidCell(0, +6);
                    isValidCell(0, +7);
                    break;

                case "Bishop":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

                    isValidCell(+1, +1);
                    isValidCell(+2, +2);
                    isValidCell(+3, +3);
                    isValidCell(+4, +4);
                    isValidCell(+5, +5);
                    isValidCell(+6, +6);
                    isValidCell(+7, +7);
                    isValidCell(-1, -1);
                    isValidCell(-2, -2);
                    isValidCell(-3, -3);
                    isValidCell(-4, -4);
                    isValidCell(-5, -5);
                    isValidCell(-6, -6);
                    isValidCell(-7, -7);
                    isValidCell(-1, +1);
                    isValidCell(-2, +2);
                    isValidCell(-3, +3);
                    isValidCell(-4, +4);
                    isValidCell(-5, +5);
                    isValidCell(-6, +6);
                    isValidCell(-7, +7);
                    isValidCell(+1, -1);
                    isValidCell(+2, -2);
                    isValidCell(+3, -3);
                    isValidCell(+4, -4);
                    isValidCell(+5, -5);
                    isValidCell(+6, -6);
                    isValidCell(+7, -7);
                    break;

                case "Knight":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

                    isValidCell(+2, +1);
                    isValidCell(-2, -1);
                    isValidCell(+2, -1);
                    isValidCell(-2, +1);
                    isValidCell(+1, +2);
                    isValidCell(-1, -2);
                    isValidCell(+1, -2);
                    isValidCell(-1, +2);
                    break;

                case "Pawn W":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

                    isValidCell(0, -1);

                    if (currentCell.ColumnNumber == 6) {
                        isValidCell(0, -1);
                        isValidCell(0, -2);
                    }
                    break;

                case "Pawn B":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }

                    isValidCell(0, +1);

                    if (currentCell.ColumnNumber == 1) {
                        isValidCell(0, +1);
                        isValidCell(0, +2);
                    }
                    break;
            }
        }
    }
}
