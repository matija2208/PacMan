using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman
{

    internal class PacMan
    {
        int x, y;
        int px, py;
        int lifes;
        float speed;
        Bitmap slika;
        int trenutni_smer = 0;
        int sledeci_smer = 0;
        // up    =1
        // down  =2
        // left  =3
        // right =4

        public PacMan(String[] maze)
        {
            speed = 0.1f;
            lifes = 3;
            for(int i = 0; i < maze.Length; i++)
            {
                for(int j = 0; j < maze[i].Length; j++)
                {
                    if (maze[i][j]=='@')
                    {
                        x = j;
                        y = i;
                        px = x;
                        py = y;
                    }
                }
            }
            slika = new Bitmap(Image.FromFile("./images/pacman4.png"));
        }

        public int pocetak(ref String[] maze)
        {
            StringBuilder[] sb = new StringBuilder[maze.Length];
            for (int i = 0; i < maze.Length; i++)
            {
                sb[i] = new StringBuilder(maze[i]);
            }

            sb[y][x] = ' ';

            for (int i = 0; i < maze.Length; i++)
            {
                maze[i] = sb[i].ToString();
            }

            x = px;
            y = py;
            lifes--;
            if (lifes == 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public void keyPress(KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Up)
            {
                sledeci_smer = 1;
            }
            else if(e.KeyCode==Keys.Down)
            {
                sledeci_smer = 2;
            }
            else if(e.KeyCode == Keys.Left)
            {
                sledeci_smer = 3;
            }
            else if(e.KeyCode==Keys.Right)
            {
                sledeci_smer = 4;
            }
        }

        bool provera(String[] maze, int x, int y)
        {
            try
            {
                if (maze[y][x] == ' ' || maze[y][x] == '*' || maze[y][x] == '&')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                return true;
            }
            
        }

        void revert_photo(int smer)
        {
            if (smer == 1)
            {
                slika.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else if (smer == 2)
            {
                slika.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            else if (smer == 3)
            {
                slika.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }
        }
        public int movePacMan(ref String[] maze)
        {
            
            StringBuilder[] sb = new StringBuilder[maze.Length];
            for(int i=0;i<maze.Length;i++)
            {
                sb[i] = new StringBuilder(maze[i]);
            }
            sb[y][x] = ' ';
            
            if(sledeci_smer != 0)
            {
                if (sledeci_smer == 1 && provera(maze, x, y - 1))
                {
                    revert_photo(trenutni_smer);
                    trenutni_smer = 1;
                    sledeci_smer = 0;
                    slika.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                else if(sledeci_smer == 2 && provera(maze, x, y + 1))
                {
                    revert_photo(trenutni_smer);
                    trenutni_smer = 2;
                    sledeci_smer = 0;
                    slika.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                else if(sledeci_smer == 3 && provera(maze, x - 1, y))
                {
                    revert_photo(trenutni_smer);
                    trenutni_smer = 3;
                    sledeci_smer = 0;
                    slika.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
                else if(sledeci_smer == 4 && provera(maze, x + 1, y))
                {
                    revert_photo(trenutni_smer);
                    trenutni_smer = 4;
                    sledeci_smer = 0;
                }
            }
            
            if(trenutni_smer == 1 && provera(maze, x, y - 1))
            {
                y--;
            }
            else if(trenutni_smer == 2 && provera(maze, x, y + 1))
            {
                y++;
            }
            else if(trenutni_smer == 3 && provera(maze, x - 1, y))
            {
                x--;
                if(x == -1)
                {
                    x = maze[0].Length-1;
                }
            }
            else if(trenutni_smer == 4 && provera(maze, x + 1, y))
            {
                x++;
                if(x == maze[0].Length)
                {
                    x = 0;
                }
            }
            else
            {
                trenutni_smer = 0;
            }
            
            sb[y][x] = '@';
            if (maze[y][x] == '&')
            {
                for (int i = 0; i < maze.Length; i++)
                {
                    maze[i] = sb[i].ToString();
                }
                return 1;
            }
            else if(maze[y][x] == '*')
            {
                for (int i = 0; i < maze.Length; i++)
                {
                    maze[i] = sb[i].ToString();
                }
                return 2;
            }
            else
            {
                for (int i = 0; i < maze.Length; i++)
                {
                    maze[i] = sb[i].ToString();
                }
                return 0;
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
                if (trenutni_smer == 1)
                {
                    slika.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                else if (trenutni_smer == 2)
                {
                    slika.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                else if (trenutni_smer == 3)
                {
                    slika.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
            }
        }

        

        public float Xcalc(int dim, int tick)
        {
            if(tick == 8 && trenutni_smer == 3)
            {
                return x * dim + dim;
            }
            else if (tick == 8 && trenutni_smer == 4)
            {
                return x * dim - dim;
            }


            if (trenutni_smer == 3)
            {
                return x * dim + (8.0f - tick) * (dim / 8.0f);
            }
            else if(trenutni_smer == 4)
            {
                return x * dim - (8.0f - tick) * (dim / 8.0f);
            }
            else
            {
                return x * dim;
            }
        }
        
        public float Ycalc(int dim, int tick)
        {
            if (tick == 8 && trenutni_smer == 1)
            {
                return y * dim + dim;
            }
            else if (tick == 8 && trenutni_smer == 2)
            {
                return y * dim - dim;
            }


            if (trenutni_smer == 1)
            {
                return y * dim + (8.0f - tick) * (dim / 8.0f);
            }
            else if (trenutni_smer == 2)
            {
                return y * dim - (8.0f - tick) * (dim / 8.0f);
            }
            else
            {
                return y * dim;
            }
        }

        public int X
        {
            get
            {
                return x;
            }
        }
        public int Y
        {
            get
            {
                return y;
            }
        }
        public int Trenutni_smer
        {
            get
            {
                return trenutni_smer;
            }
        }
    }
}
