using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_v1._0
{
    
    public partial class Form1 : Form
    {
        public int[] nx = { 1, 1, -1, -1, 2, 2, -2, -2 };
        public int[] ny = { 2, -2, 2, -2, 1, -1, 1, -1 };
        public int[] dx = { 0, 0, 1, -1, 1, 1, -1, -1 };
        public int[] dy = { 1, -1, 0, 0, 1, -1, 1, -1 };
        PictureBox[,] dWhite = new PictureBox[8, 2];
        PictureBox[,] dBlack = new PictureBox[8, 2];
        public int dbx,dby,dwx,dwy;
        public bool ch = false;
        public bool GameOver = false;
        int player = 1;
        public string sympols = "PPRRNNBBQQKK";                 //sympols represent type of pieces
        public int[,] grid = new int[8, 8];                     //represents the form of the grid
        private Piece[,] pieces = new Piece[8, 8];
        public bool[,] danger = new bool[8, 8];
        List<Point> all= new List<Point>();                     //store all possible positions can move to
        Stack<refr> history1 = new Stack<refr>();
        Stack<refr> history2 = new Stack<refr>();
        //picked piece position
        int px = -1, py = -1;

        //function initialize the main form of the chess grid
        //puts all pieces in the start position
        public void buildGrid()
        {
            for (int i = 0; i < 8; i++) for (int j = 0; j < 8; j++) { grid[i, j] = -1; }
            grid[0, 5] = 6; grid[0, 6] = 4; grid[0, 7] = 2;
            grid[7, 5] = 7; grid[7, 6] = 5; grid[7, 7] = 3;
            for (int i = 0; i < 8; i++)
            {
                grid[1, i] = 0;
                grid[6, i] = 1;
            }
            int v = 2;
            for (int i = 0; i < 5; i++)
            {
                grid[0, i] = v++;
                grid[7, i] = v++;
            }
        }
        public Form1()
        {
            InitializeComponent();
            buildGrid();
            panel1.Width =panel1.Height= 60 * 8;
            dbx = dby = dwx = dwy = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dead();
            px=py=-1;
            player = 1;
            panel1.Controls.Clear();
            buildGrid();
            int x = 0, y = 0, inc = 60, w = 60 * 8;
            //loop over all pieces on the grid and set the picture of it and the back color
            for(int i=0;i<8;i++)
            {
               for(int j=0;j<8;j++)
               {
                   danger[i, j] = new bool();
                   danger[i, j] = false;
                   pieces[i, j] = new Piece();
                   if (grid[i, j] == -1)//cell has no piece
                   {
                       pieces[i, j].color = -1;
                       pieces[i, j].kind = 'E';//empty
                       pieces[i, j].pic = -1;
                   }
                   else
                   {
                       pieces[i, j].color = grid[i, j] % 2;//set color (1 -> white, 0 -> black)
                       pieces[i, j].kind = sympols[grid[i, j]];
                       pieces[i, j].pic = grid[i, j];//pic index in the picture list
                       pieces[i, j].Image = piecesPic.Images[pieces[i, j].pic];//set the image
                   }
                   pieces[i, j].SetBounds(x, y, inc, inc);
                   pieces[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                   pieces[i, j].Click += pickUp;

                   //set coordinates 
                   pieces[i, j].x = i;
                   pieces[i, j].y = j;

                   //back color according to the cell position
                   if ((i + j) % 2 == 0)
                       pieces[i, j].BackColor = Color.White;
                   else
                       pieces[i, j].BackColor = Color.Brown;
                   
                   //move forward
                   x += inc;
                   y += (x / w) * inc;
                   x %= panel1.Width;
                   this.panel1.Controls.Add(pieces[i, j]);
               }
            }
        }
        public void pickUp(object sender,EventArgs e)
        {
            Piece s = sender as Piece;
            int x = s.x, y = s.y;
            //first click : set picked piece cordinates to this piece coordinates
            //then see all possible positions can move to
            if (s.color != player && s.BackColor != Color.LimeGreen && s.BackColor != Color.Green)
                return;
            if(px==-1&&py==-1)
            {
                px = s.x;
                py = s.y;
                if((px+py)%2==0)
                    pieces[px, py].BackColor = Color.LimeGreen;
                else
                    pieces[px, py].BackColor = Color.Green;
                suggest(px, py);
                //color(px, py,s.kind);
            }
            //double click on the same cell : cancel cell selection
            else if(px==s.x&&py==s.y)
            {
                if ((px + py) % 2 == 0)
                    pieces[px, py].BackColor = Color.White;
                else
                    pieces[px, py].BackColor = Color.Brown;
                cancel();
                px = py = -1;
            }
            else if (s.BackColor == Color.LimeGreen || s.BackColor == Color.Green)//Moved
            {
                eat(s);
                Piece from = pieces[px, py];
                history1.Push(new refr(from, s));
                history2.Clear();
                pieces[x,y].kind = from.kind;
                pieces[x, y].color = from.color;
                pieces[x, y].pic = from.pic;
                pieces[x, y].Image = piecesPic.Images[s.pic];
                from.kind = 'E';
                from.color = -1;
                from.pic = -1;
                if ((from.x + from.y) % 2 == 0)
                    from.BackColor = Color.White;
                else
                    from.BackColor = Color.Brown;
                from.Image = null;
                cancel();
                Attack(s.color);
                if(check((player+1)%2))
                {
                    label1.Text="Check";
                }
                else
                {
                    label1.Text = "";
                }
                player = (player + 1) % 2;

            }
        }
        public void suggest(int x,int y,bool at=false)
        {
            char kind=pieces[x,y].kind;
            if(kind=='P')//pawn
            {
                pawn(x, y,at);
            }
            if(kind=='N')
            {
                Knight(x, y,at);
            }
            if(kind=='B')
            {
                Bishob(x, y,at);
            }
            if(kind=='R')
            {
                Rook(x, y, at);
            }
            if(kind=='Q')
            {
                Rook(x, y, at);
                Bishob(x, y, at);
            }
            if(kind=='K')
            {
                King(x, y, at);
            }

        }
        public void cancel()
        {
            foreach(Point p in all)
            {
                if ((p.X + p.Y) % 2 == 0)
                    pieces[p.X, p.Y].BackColor = Color.White;
                else
                    pieces[p.X, p.Y].BackColor = Color.Brown;
            }
            px = py = -1;
        }
        public bool valid(int x,int y)
        {
            return (x >= 0 && x < 8 && y >= 0 && y < 8);
        }

        //pieces moves

        //Attack
        public void Attack(int c)
        {
            for (int i = 0; i < 8;i++)
            {
                for(int j=0;j<8;j++)
                {
                    danger[i, j] = false;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (pieces[i, j].color == c)
                        suggest(i, j, true);
                }
            }
        }
        // Pawn
        public void pawn(int x,int y,bool at)
        {
            int d = 1;
            if (pieces[x, y].color == 1)//white
            {
                d = -1;
            }
            int a = x + d, b = y + 1;
            if (at)
            {
                a = x + d; b = y + 1;
                if (valid(a, b))
                {
                    danger[a, b] = true;
                }
                b -= 2;
                if (valid(a, b))
                {
                    danger[a, b] = true;
                }
                return;

            }
            char k = pieces[x, y].kind;
            int c = pieces[x, y].color;
            if (valid(x + d, y) == true && pieces[x + d, y].kind == 'E')//forward 1 step
            {
                color(x + d, y, k);
            }
            //right corner has an enemy
            if (valid(a, b) && pieces[a, b].color > -1 && pieces[a, b].color != pieces[x, y].color)
            {
                pieces[x, y].kind = 'E';
                pieces[x, y].color = -1;
                color(a, b,k);
                pieces[x, y].kind = k;
                pieces[x, y].color = c;
            }
            b -= 2;
            //left corner has an enemy
            if (valid(a, b) && pieces[a, b].color > -1 && pieces[a, b].color != pieces[x, y].color)
            {
                pieces[x, y].kind = 'E';
                pieces[x, y].color = -1;
                color(a, b,k);

            }
            if (d == 1 && x == 1 && pieces[x + d, y].kind == 'E' && pieces[x + 2, y].kind == 'E')//first move at all
            {
                color(x + 2, y,k);
            }
            if (d == -1 && x == 6 && pieces[x + d, y].kind == 'E' && pieces[x -2, y].kind == 'E')
            {
                color(x - 2, y,k);
            }
            pieces[x, y].kind = k;
            pieces[x, y].color = c;
            
        }
        //Knight
        public void Knight(int x,int y,bool at)
        {
            char k = 'N';
            pieces[x, y].kind = 'E';
            for(int i=0;i<8;i++)
            {
                int a = x + nx[i], b = y + ny[i];
                if(valid(a,b))
                {
                    if (!at && pieces[a, b].color != pieces[x, y].color)
                        color(a, b, k);
                    else if(at)
                    {
                        danger[a, b] = true;
                    }
                }
            }
            pieces[x, y].kind = k;
        }
        //Bishob
        public void Bishob(int x,int y,bool at)
        {
            char k = pieces[x, y].kind;
            pieces[x, y].kind = 'E';
            for(int i=4;i<8;i++)
            {
                int a = dx[i], b = dy[i];
                int xx=x+a,yy=y+b;
                while(valid(xx,yy))
                {
                    if (at)
                    {
                        danger[xx, yy] = true;
                    }
                    if (pieces[xx, yy].color == pieces[x, y].color)
                        break;
                    if (!at)
                        color(xx, yy, k);
                    if (pieces[xx, yy].kind != 'E')
                        break;
                    xx += a;
                    yy += b;
                }
            }
            pieces[x, y].kind = k;
        }
        //Rook
        public void Rook(int x,int y,bool at)
        {
            char k = pieces[x, y].kind;
            pieces[x, y].kind = 'E';
            for (int i = 0; i < 4; i++)
            {
                int a = dx[i], b = dy[i];
                int xx = x + a, yy = y + b;
                while (valid(xx, yy))
                {
                    if (at)
                    {
                        danger[xx, yy] = true;
                    }
                    if (pieces[xx, yy].color == pieces[x, y].color)
                        break;
                    if (!at)
                        color(xx, yy, k);
                    if (pieces[xx, yy].kind != 'E')
                        break;
                    xx += a;
                    yy += b;
                }
            }
            pieces[x, y].kind = k;
        }
        //King
        public void King(int x,int y,bool at)
        {
            char k = pieces[x, y].kind;
            pieces[x, y].kind = 'E';
            for(int i=0;i<8;i++)
            {
                int a = x + dx[i], b = y + dy[i];
                if (valid(a, b))
                {
                    if (at)
                        danger[a, b] = true;
                    else if (!danger[a, b] && pieces[a, b].color != player)
                    {
                        color(a, b, k);
                    }
                }
            }
            pieces[x, y].kind = k;
        }
        public void color(int x, int y,char k)
        {
            Piece temp = new Piece();
            temp.kind = pieces[x, y].kind;
            temp.color = pieces[x, y].color;
            pieces[x, y].kind = k;
            pieces[x, y].color = player;
            Attack((player + 1) % 2);
            pieces[x, y].kind = temp.kind;
            pieces[x, y].color = temp.color;
            if (check(player))
            {
                return;
            }
            if((x+y)%2==0)
                pieces[x, y].BackColor = Color.LimeGreen;
            else
                pieces[x, y].BackColor = Color.Green;
            Point p = new Point(x, y);
            all.Add(p);
        }

        public bool check(int c)
        {
            for(int i=0;i<8;i++)
            {
                for(int j=0;j<8;j++)
                {
                    if (pieces[i, j].kind == 'K' && pieces[i, j].color == c && danger[i, j])
                        return true;
                }
            }
            return false;
        }
        
        public void dead()
        {
            deadBlack.Controls.Clear();
            deadWhite.Controls.Clear();
            deadWhite.Width=deadBlack.Width = 80;
            deadWhite.Height = deadBlack.Height = 40 * 8;
            for(int i=0;i<8*40;i+=40)
            {
                for(int j=0;j<2*40;j+=40)
                {
                    int a = i / 40, b = j / 40;
                    dBlack[a, b] = new PictureBox();
                    dBlack[a, b].Width = dBlack[a, b].Height = 40;
                    dBlack[a, b].SetBounds(j, i, 40, 40);
                    dBlack[a, b].SizeMode = PictureBoxSizeMode.StretchImage;
                    dBlack[a, b].Image = null;
                    deadBlack.Controls.Add(dBlack[a, b]);
                }
            }
            for (int i = 0; i < 8 * 40; i += 40)
            {
                for (int j = 0; j < 2 * 40; j += 40)
                {
                    int a = i / 40, b = j / 40;
                    dWhite[a, b] = new PictureBox();
                    dWhite[a, b].Width = dWhite[a, b].Height = 40;
                    dWhite[a, b].SetBounds(j, i, 40, 40);
                    dWhite[a, b].SizeMode = PictureBoxSizeMode.StretchImage;
                    dWhite[a, b].Image = null;
                    deadWhite.Controls.Add(dWhite[a, b]);
                }
            }

        }
        private void eat(Piece target)
        {
            if(target.pic!=-1)
            {
                if(player==1)//white
                {
                    dBlack[dbx, dby].Image = piecesPic.Images[target.pic];
                    dby++;
                    dbx += (dby / 2);
                    dby %= 2;
                }
                else
                {
                    dWhite[dwx, dwy].Image = piecesPic.Images[target.pic];
                    dwy++;
                    dwx += (dwy / 2);
                    dwy %= 2;
                }
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            if (history1.Count == 0)
                return;
            refr current = new refr();
            current = history1.Pop();
            refr h2 = new refr(pieces[current.a.x, current.a.y], pieces[current.b.x, current.b.y]);
            int x = current.a.x, y = current.a.y;
            pieces[x, y].kind = current.a.kind;
            pieces[x, y].pic = current.a.pic;
            pieces[x, y].color = current.a.color;
            pieces[x, y].Image = piecesPic.Images[current.a.pic];
            x = current.b.x;
            y = current.b.y;
            pieces[x, y].kind = current.b.kind;
            pieces[x, y].pic = current.b.pic;
            pieces[x, y].color = current.b.color;
            if (current.b.pic != -1)
                pieces[x, y].Image = piecesPic.Images[current.b.pic];
            else
                pieces[x, y].Image = null;
            if(current.d==1)//white
            {
                h2.dpic = current.b.pic;
                h2.d = 1;
                if (dwy == 0)
                {
                    dwx--;
                    dwy = 1;
                }
                else
                    dwy--;
                dWhite[dwx, dwy].Image = null;
            }
            else if(current.d==0)
            {
                h2.dpic = current.b.pic;
                h2.d = 0;
                if (dby == 0)
                {
                    dbx--;
                    dby = 1;
                }
                else
                    dby--;
                dBlack[dbx, dby].Image = null;
            }
            history2.Push(h2);
            player = (player + 1) % 2;
            
        }

        private void ForwardBtn_Click(object sender, EventArgs e)
        {
            if (history2.Count == 0)
                return;
            refr current = new refr();
            current = history2.Pop();
            history1.Push(new refr(pieces[current.a.x, current.a.y], pieces[current.b.x, current.b.y]));
            int x = current.a.x, y = current.a.y;
            pieces[x, y].kind = current.a.kind;
            pieces[x, y].pic = current.a.pic;
            pieces[x, y].color = current.a.color;
            if (current.a.pic != -1)
                pieces[x, y].Image = piecesPic.Images[current.a.pic];
            else
                pieces[x, y].Image = null;
            x = current.b.x;
            y = current.b.y;
            pieces[x, y].kind = current.b.kind;
            pieces[x, y].pic = current.b.pic;
            pieces[x, y].color = current.b.color;
            if (current.b.pic != -1)
                pieces[x, y].Image = piecesPic.Images[current.b.pic];
            else
                pieces[x, y].Image = null;
            if(current.d==1&&current.dpic!=-1)//white
            {
                dWhite[dwx, dwy].Image = null;
                if (current.dpic != -1)
                    dWhite[dwx, dwy].Image = piecesPic.Images[current.dpic];
                if (dwy == 1)
                {
                    dwx++;
                    dwy = 0;
                }
                else
                    dwy++;
            }
            else if(current.d==0&&current.dpic!=-1)
            {
                
                dBlack[dbx, dby].Image = null;
                if (current.dpic != -1)
                    dBlack[dbx, dby].Image = piecesPic.Images[current.dpic];
                if (dby == 1)
                {
                    dbx++;
                    dby = 0;
                }
                else
                    dby++;
            }
            player = (player + 1) % 2;

        }
    
    }
    
}
