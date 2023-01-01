using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ChessBoardModel {
    public class Cell {
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public bool IsCurrentlyOccupied { get; set; }
        public bool IsLegalMove { get; set; }

        public Cell(int x, int y) {
            RowNumber = x;
            ColumnNumber = y;
        }
    }
    public class ButtonTag {
        public string Team { get; set; }
        public Point Position { get; set; }
    }

    public static class ButtonTagExtension {
        public static ButtonTag PieceTag(this Button btn) {
            return btn.Tag as ButtonTag;
        }
    }

    public class PieceImg {
        public Image WhitePawn { get; set; } = Image.FromFile(@"C:\Users\natha\Documents\.GDocuments\Coding\ChessGame\ChessGame\img\WhitePawn.png");
        //public Image WhiteRook { get; } = Image.FromFile(@"..\img\WhiteRook.png");
        //public Image WhiteKnight { get; } = Image.FromFile(@"..\img\WhiteKnight.png");
        //public Image WhiteBishop { get; } = Image.FromFile(@"..\img\WhiteBishop.png");
        //public Image WhiteQueen { get; } = Image.FromFile(@"..\img\WhiteQueen.png");
        //public Image WhiteKing { get; } = Image.FromFile(@"..\img\WhiteKing.png");

        //public Image BlackPawn { get; } = Image.FromFile(@"..\img\WhitePawn.png");
        //public Image BlackRook { get; } = Image.FromFile(@"..\img\BlackRook.png");
        //public Image BlackKnight { get; } = Image.FromFile(@"..\img\BlackKnight.png");
        //public Image BlackBishop { get; } = Image.FromFile(@"..\img\BlackBishop.png");
        //public Image BlackQueen { get; } = Image.FromFile(@"..\img\BlackQueen.png");
        //public Image BlackKing { get; } = Image.FromFile(@"..\img\BlackKing.png");
    }
}
