using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_AI_Half_Chess
{
    public class Move
    {
        public int AddX;
        public int AddY;


        public Move()
        {
            AddX = 0;
            AddY = 0;
        }
        public Move(int x, int y)
        {
            AddX = x;
            AddY = y;
        }
    }
}
