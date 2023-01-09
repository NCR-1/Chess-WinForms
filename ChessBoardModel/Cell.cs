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

        public string Piece { get; set; }
        public string Team { get; set; }
        //public Point Position { get; set; }

        public Cell(int x, int y) {
            RowNumber = x;
            ColumnNumber = y;
        }
    }

    public class ButtonTag {
        public Point ButtonPosition { get; set; }
    }

    public static class ButtonTagExtension {
        public static ButtonTag PieceTag(this Button btn) {
            return btn.Tag as ButtonTag;
        }
    }
}
