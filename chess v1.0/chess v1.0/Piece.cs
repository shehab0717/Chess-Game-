using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_v1._0
{
    class Piece:PictureBox
    {
        public int color;
        public char kind;
        public int pic;
        public int x, y;

        public Piece()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
        }
        public Piece(char k,int c,int pic)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.color = c;
            this.kind = k;
            this.pic = pic;
        }

    }
}
