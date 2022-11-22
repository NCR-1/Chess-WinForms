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
                    try {  Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }
                    try {  Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try {  Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try {  Grid[currentCell.RowNumber, currentCell.ColumnNumber + 1].IsLegalMove = true; } catch { }
                    try {  Grid[currentCell.RowNumber, currentCell.ColumnNumber - 1].IsLegalMove = true; } catch { }
                    try {  Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].IsLegalMove = true; } catch { }
                    try {  Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].IsLegalMove = true; } catch { }
                    try {  Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].IsLegalMove = true; } catch { }
                    try {  Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].IsLegalMove = true; } catch { }
                    break;

                case "Queen":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }
                    try { Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 2, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 3, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 4, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 5, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 6, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 7, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 2, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 3, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 4, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 5, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 6, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 7, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber + 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber + 2].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber + 3].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber + 4].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber + 5].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber + 6].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber + 7].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 2].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 3].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 4].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 5].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 6].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 7].IsLegalMove = true; } catch { }

                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }
                    try { Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 2, currentCell.ColumnNumber + 2].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 3, currentCell.ColumnNumber + 3].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 4, currentCell.ColumnNumber + 4].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 5, currentCell.ColumnNumber + 5].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 6, currentCell.ColumnNumber + 6].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 7, currentCell.ColumnNumber + 7].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 2, currentCell.ColumnNumber - 2].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 3, currentCell.ColumnNumber - 3].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 4, currentCell.ColumnNumber - 4].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 5, currentCell.ColumnNumber - 5].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 6, currentCell.ColumnNumber - 6].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 7, currentCell.ColumnNumber - 7].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 2, currentCell.ColumnNumber + 2].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 3, currentCell.ColumnNumber + 3].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 4, currentCell.ColumnNumber + 4].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 5, currentCell.ColumnNumber + 5].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 6, currentCell.ColumnNumber + 6].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 7, currentCell.ColumnNumber + 7].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 2, currentCell.ColumnNumber - 2].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 3, currentCell.ColumnNumber - 3].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 4, currentCell.ColumnNumber - 4].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 5, currentCell.ColumnNumber - 5].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 6, currentCell.ColumnNumber - 6].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 7, currentCell.ColumnNumber - 7].IsLegalMove = true; } catch { }
                    break;

                case "Rook":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }
                    try { Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 2, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 3, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 4, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 5, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 6, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 7, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 2, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 3, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 4, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 5, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 6, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 7, currentCell.ColumnNumber].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber + 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber + 2].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber + 3].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber + 4].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber + 5].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber + 6].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber + 7].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 2].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 3].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 4].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 5].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 6].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 7].IsLegalMove = true; } catch { }
                    break;

                case "Bishop":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }
                    try { Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 2, currentCell.ColumnNumber + 2].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 3, currentCell.ColumnNumber + 3].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 4, currentCell.ColumnNumber + 4].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 5, currentCell.ColumnNumber + 5].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 6, currentCell.ColumnNumber + 6].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 7, currentCell.ColumnNumber + 7].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 2, currentCell.ColumnNumber - 2].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 3, currentCell.ColumnNumber - 3].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 4, currentCell.ColumnNumber - 4].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 5, currentCell.ColumnNumber - 5].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 6, currentCell.ColumnNumber - 6].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 7, currentCell.ColumnNumber - 7].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 2, currentCell.ColumnNumber + 2].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 3, currentCell.ColumnNumber + 3].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 4, currentCell.ColumnNumber + 4].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 5, currentCell.ColumnNumber + 5].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 6, currentCell.ColumnNumber + 6].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 7, currentCell.ColumnNumber + 7].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 2, currentCell.ColumnNumber - 2].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 3, currentCell.ColumnNumber - 3].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 4, currentCell.ColumnNumber - 4].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 5, currentCell.ColumnNumber - 5].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 6, currentCell.ColumnNumber - 6].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 7, currentCell.ColumnNumber - 7].IsLegalMove = true; } catch { }
                    break;

                case "Knight":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }
                    try { Grid[currentCell.RowNumber + 2, currentCell.ColumnNumber + 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 2, currentCell.ColumnNumber - 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 2, currentCell.ColumnNumber - 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 2, currentCell.ColumnNumber + 1].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber + 2].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber - 2].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber - 2].IsLegalMove = true; } catch { }
                    try { Grid[currentCell.RowNumber - 1, currentCell.ColumnNumber + 2].IsLegalMove = true; } catch { }
                    break;

                case "Pawn":
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber].IsCurrentlyOccupied = true; } catch { }
                    try { Grid[currentCell.RowNumber, currentCell.ColumnNumber - 1].IsLegalMove = true; } catch { }
                    try { 
                        if (currentCell.ColumnNumber == 6) {
                            Grid[currentCell.RowNumber, currentCell.ColumnNumber - 1].IsLegalMove = true;
                            Grid[currentCell.RowNumber, currentCell.ColumnNumber - 2].IsLegalMove = true;
                        }
                    } catch { }
                    break;
            }
        }
    }
}
