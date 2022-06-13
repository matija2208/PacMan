using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman
{
    internal abstract class Ghost
    {
        protected String name;
        protected Point location;
        protected Point target;
        protected Point pocetna_lokacija;
        protected Bitmap slika;
        protected int trenutni_smer;
        protected int stanje;
        protected char prethodno_polje;

        public Ghost(string name, String[] maze)
        {
            this.name = name;
            for (int i = 0; i < maze.Length; i++)
            {
                for (int j = 0; j < maze[i].Length; j++)
                {
                    if (maze[i][j] == name[0])
                    {
                        location = new Point(j, i);
                        pocetna_lokacija = location;
                    }
                }
            }

            this.slika = new Bitmap(Image.FromFile("./images/"+name+"4.png"));
            
            trenutni_smer = 0;
            stanje = 1;
            prethodno_polje = ' ';
        }

        protected double distance(Point location)
        {
            return Math.Sqrt(Math.Pow(location.X - target.X, 2) + Math.Pow(location.Y - target.Y, 2));
        }
        protected bool provera(String[] maze, int x, int y)
        {
            try
            {
                if (maze[y][x] == ' ' || maze[y][x] == '*' || maze[y][x] == '&' || maze[y][x] == '@')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return true;
            }

        }
        public abstract void algorithm(Point pacman_location, int pacman_smer, String[] maze);

        public Bitmap Slika
        {
            get
            {
                return slika;
            }
            set
            {
                slika = value;
            }

        }

        public void odrediSliku()
        {
            try
            {
                if (stanje == 0 || stanje == 1)
                    Slika = new Bitmap(Image.FromFile("./images/" + name + trenutni_smer + ".png"));
                else if (stanje == 2)
                    Slika = new Bitmap(Image.FromFile("./images/eyes" + trenutni_smer + ".png"));
                else if (stanje == 3)
                    Slika = new Bitmap(Image.FromFile("./images/aghost1.png"));
            }
            catch(Exception e)
            {
                Slika = new Bitmap(Image.FromFile("./images/" + name +"1.png"));
            }
            
        }

        public float Xcalc(int dim, int tick)
        {
            if (tick == 8 && trenutni_smer == 3)
            {
                return location.X * dim + dim;
            }
            else if (tick == 8 && trenutni_smer == 4)
            {
                return location.X * dim - dim;
            }


            if (trenutni_smer == 3)
            {
                return location.X * dim + (8.0f - tick) * (dim / 8.0f);
            }
            else if (trenutni_smer == 4)
            {
                return location.X * dim - (8.0f - tick) * (dim / 8.0f);
            }
            else
            {
                return location.X * dim;
            }
        }

        public float Ycalc(int dim, int tick)
        {
            if (tick == 8 && trenutni_smer == 1)
            {
                return location.Y * dim + dim;
            }
            else if (tick == 8 && trenutni_smer == 2)
            {
                return location.Y * dim - dim;
            }


            if (trenutni_smer == 1)
            {
                return location.Y * dim + (8.0f - tick) * (dim / 8.0f);
            }
            else if (trenutni_smer == 2)
            {
                return location.Y * dim - (8.0f - tick) * (dim / 8.0f);
            }
            else
            {
                return location.Y * dim;
            }
        }

        public int Stanje
        {
            get
            {
                return stanje;
            }
            set
            {
                stanje = value;
            }
        }

        public Point Target
        {
            get { return target; }
        }

        public Point Location
        {
            get { return location; }
        }

        public bool provera_lokacije()
        {
            return (location == pocetna_lokacija && stanje == 2);
        }
    }
}
