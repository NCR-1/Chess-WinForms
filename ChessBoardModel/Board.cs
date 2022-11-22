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

            // Clear moves
            for (int x = 0; x < Size; x++) {
                for (int y = 0; y < Size; y++) {
                    Grid[x, y].IsLegalMove = false;
                    Grid[x, y].IsCurrentlyOccupied = false;
                }
            }

            // Find all legal moves for piece

            switch (chessPiece) {
                case "King":
                    break;

                case "Queen":
                    break;

                case "Rook":
                    break;

                case "Bishop":
                    break;

                case "Knight":
                    Grid[currentCell.RowNumber + 2, currentCell.ColumnNumber + 1].IsLegalMove = true;
                    Grid[currentCell.RowNumber - 2, currentCell.ColumnNumber - 1].IsLegalMove = true;
                    Grid[currentCell.RowNumber + 2, currentCell.ColumnNumber - 1].IsLegalMove = true;
                    Grid[currentCell.RowNumber - 2, currentCell.ColumnNumber + 1].IsLegalMove = true;
                    Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 2].IsLegalMove = true;
                    Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 2].IsLegalMove = true;
                    Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 2].IsLegalMove = true;
                    Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 2].IsLegalMove = true;
                    break;

                case "Pawn":
                    break;
            }
        }
    }
}
