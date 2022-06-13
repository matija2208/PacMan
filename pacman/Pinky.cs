using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman
{
    internal class Pinky : Ghost
    {
        public Pinky(String[] maze, String name) : base(name, maze)
        {

        }

        public override void algorithm(Point pacman_location, int pacman_smer, String[] maze)
        {
            StringBuilder[] sb = new StringBuilder[maze.Length];
            for (int i = 0; i < maze.Length; i++)
            {
                sb[i] = new StringBuilder(maze[i]);
            }

            if (stanje == 0)
            {
                if(pacman_smer == 1)
                {
                    target.X = pacman_location.X + 3;
                    target.Y = pacman_location.Y + 4;
                }
                else if (pacman_smer == 2)
                {
                    target.X = pacman_location.X;
                    target.Y = pacman_location.Y + 4;
                }
                else if (pacman_smer == 3)
                {
                    target.X = pacman_location.X - 4;
                    target.Y = pacman_location.Y;
                }
                else if (pacman_smer == 4)
                {
                    target.X = pacman_location.X + 4;
                    target.Y = pacman_location.Y;
                }
                else
                {
                    target = pacman_location;
                }
                
            }
            else if (stanje == 1)
            {
                target = new Point(0, 0);
            }
            else if (stanje == 2)
            {
                target = pocetna_lokacija;
            }
            else if (stanje == 3)
            {
                Point[] sk = new Point[4];
                sk[0] = new Point(location.X, location.Y - 1);
                sk[1] = new Point(location.X, location.Y + 1);
                sk[2] = new Point(location.X - 1, location.Y);
                sk[3] = new Point(location.X + 1, location.Y);
            begin:

                Random rnd = new Random();
                int test = rnd.Next(1, 5);
                if (test != ((trenutni_smer % 2 == 0) ? trenutni_smer - 1 : trenutni_smer + 1) && provera(maze, sk[test - 1].X, sk[test - 1].Y))
                {
                    trenutni_smer = test;
                    if (prethodno_polje == '*' || prethodno_polje == '&')
                        sb[location.Y][location.X] = prethodno_polje;
                    else
                        sb[location.Y][location.X] = ' ';
                    location = sk[test - 1];

                    if (location.X == -1)
                        location.X = maze[0].Length - 1;
                    else if (location.X == maze[0].Length)
                        location.X = 0;

                    sb[location.Y][location.X] = name[0];
                    prethodno_polje = maze[location.Y][location.X];
                    goto end;
                }
                else
                {
                    goto begin;
                }
            }
            Point min;
            int minsmer;

            Point[] s = new Point[4];
            s[0] = new Point(location.X, location.Y - 1);
            s[1] = new Point(location.X, location.Y + 1);
            s[2] = new Point(location.X - 1, location.Y);
            s[3] = new Point(location.X + 1, location.Y);

            min = s[0];
            minsmer = 5;

            for (int i = 1; i < 5; i++)
            {
                if (i == ((trenutni_smer % 2 == 0) ? trenutni_smer - 1 : trenutni_smer + 1) || !provera(maze, s[i - 1].X, s[i - 1].Y))
                {
                    continue;
                }

                if (minsmer == 5)
                {
                    min = s[i - 1];
                    minsmer = i;
                }

                if (distance(s[i - 1]) < distance(min))
                {
                    min = s[i - 1];
                    minsmer = i;
                }

            }

            if (minsmer != 5)
            {
                trenutni_smer = minsmer;
                if (prethodno_polje == '*' || prethodno_polje == '&')
                    sb[location.Y][location.X] = prethodno_polje;
                else
                    sb[location.Y][location.X] = ' ';
                location = min;

                if (location.X == -1)
                    location.X = maze[0].Length - 1;
                else if (location.X == maze[0].Length)
                    location.X = 0;

                sb[location.Y][location.X] = name[0];
                prethodno_polje = maze[location.Y][location.X];
            }
            else
            {
                trenutni_smer = 0;
            }

        end:
            odrediSliku();
            for (int i = 0; i < maze.Length; i++)
            {
                maze[i] = sb[i].ToString();
            }
        }
    }
}
