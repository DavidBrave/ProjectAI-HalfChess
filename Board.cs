using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_AI_Half_Chess
{
    public class Board
    {

        //piece = piece yang ada di petak chess
        //kalo kosong == null
        public Piece Piece;

        //posis x, petak
        public int X;
        //coordinat yang ditampilkan kalo perlu
        public string ShownX;
        //posis y, petak
        public int Y;
        //coordinat yang ditampilkan kalo perlu
        public string ShownY;

        //tanpa warna = show base color : white/black
        //valid moves = green
        //threaten = red
        //None, Red, Green
        public string ShownColor;
        //WHite, Black
        public string BaseColor;

        public Board()
        {
            Piece = null;
            X = -1;
            ShownX = "No X";
            Y = -1;
            ShownY = "No Y";

            ShownColor = "None";
            BaseColor = "White";
        }

    }
}
