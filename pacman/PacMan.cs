using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman
{

    internal class PacMan
    {
        float x, y;
        float speed;
        Bitmap slika;

        bool isLeft, isRight, isUp, isDown;

        static int round(float a, bool c, bool d)
        {
            int b = (int)a;
            if(c)
                return (a > b) ? b + 1 : b;
            else
                return b;
        }

        public PacMan(String[] maze)
        {
            isDown = false;
            isLeft = false;
            isRight = false;
            isUp = false;

            speed = 0.1f;

            for(int i = 0; i < maze.Length; i++)
            {
                for(int j = 0; j < maze[i].Length; j++)
                {
                    if (maze[i][j]=='@')
                    {
                        x = j;
                        y = i;
                    }
                }
            }
            slika = new Bitmap(Image.FromFile("./images/pacman4.png"));
        }
        public void keyPress(KeyEventArgs e)
        {
            if(isUp)
            {
                slika.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else if(isDown)
            {
                slika.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            else if(isLeft)
            {
                slika.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }

            isUp= false;
            isDown= false;
            isLeft= false;
            isRight= false;

            if(e.KeyCode==Keys.Up)
            {
                isUp = true;
                slika.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            else if(e.KeyCode==Keys.Down)
            {
                isDown = true;
                slika.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else if(e.KeyCode == Keys.Left)
            {
                isLeft = true;
                slika.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }
            else if(e.KeyCode==Keys.Right)
            {
                isRight = true;
            }
        }
        public void movePacMan(ref String[] maze)
        {
            
            StringBuilder[] sb = new StringBuilder[maze.Length];
            for(int i=0;i<maze.Length;i++)
            {
                sb[i] = new StringBuilder(maze[i]);
            }
            sb[PacMan.round(y,isUp,isDown)][round(x,isLeft,isRight)] = ' ';
            if (isUp && (maze[round(y,isUp,isDown) - 1][round(x,isLeft,isRight)] == ' '|| maze[round(y,isUp,isDown) - 1][round(x,isLeft,isRight)] == '*' || maze[round(y,isUp,isDown) - 1][round(x,isLeft,isRight)] == '&'))
            {
                y -= speed;
            }
            else if (isDown && (maze[round(y,isUp,isDown) + 1][round(x,isLeft,isRight)] == ' ' || maze[round(y,isUp,isDown) + 1][round(x,isLeft,isRight)] == '*' || maze[round(y,isUp,isDown) + 1][round(x,isLeft,isRight)] == '&'))
            {
                y += speed;
            }
            else if (isLeft && (maze[round(y,isUp,isDown)][round(x,isLeft,isRight) - 1] == ' ' || maze[round(y,isUp,isDown)][round(x,isLeft,isRight) - 1] == '*' || maze[round(y,isUp,isDown)][round(x,isLeft,isRight) - 1] == '&'))
            {
                x -= speed;
            }
            else if(isRight && (maze[round(y,isUp,isDown)][round(x,isLeft,isRight) + 1] == ' ' || maze[round(y,isUp,isDown)][round(x,isLeft,isRight) + 1] == '*' || maze[round(y,isUp,isDown)][round(x,isLeft,isRight) + 1] == '&'))
            {
                x += speed;
            }
            sb[round(y,isUp,isDown)][round(x,isLeft,isRight)] = '@';
            for (int i = 0; i < maze.Length; i++)
            {
                maze[i] = sb[i].ToString();
            }
        }

        public Bitmap Slika
        {
            get
            {
                return slika;
            }
            set
            {
                slika = value;
                if (isUp)
                {
                    slika.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                else if (isDown)
                {
                    slika.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                else if (isLeft)
                {
                    slika.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
            }
        }

        public float X
        {
            get
            {
                return x;
            }
        }
        public float Y
        {
            get
            {
                return y;
            }
        }
    }
}
