using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_AI_Half_Chess
{
    public class Piece
    {
        public List<Move> Moves;

        //Black, White
        public string Color;

        //Knight, Bishop, Rook, Queen, King
        public string Type;

        //Point for heuristic function
        public int Point;


    }
}
