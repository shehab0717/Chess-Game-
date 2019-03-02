using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_v1._0
{
    class refr
    {
        public Piece a, b;
        public int d, dpic;
        public refr()
        {

        }
        public refr(Piece A,Piece B)
        {
            this.a = new Piece();
            this.b = new Piece();
            a.x = A.x; a.y = A.y;
            b.x = B.x; b.y = B.y;
            a.kind = A.kind; a.pic = A.pic; a.color = A.color;
            b.kind = B.kind; b.pic = B.pic; b.color = B.color;
            d = b.color;
            dpic = -1;
        }
    }
}
