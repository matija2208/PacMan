
namespace pacman
{
    public partial class Form1 : Form
    {
        int height;
        int width;
        string[] maze;
        int tick = 0;
        int tick2 = 0;
        int stanje3;
        int[] timer = { 7, 20, 7, 20, 5, 20, 5, 20, 5 };
        int counter = 0;
        bool b = true;
        PacMan p;
        Blinky blink;
        
        public Form1()
        {
            Load_From_File();
            p = new PacMan(maze);
            blink = new Blinky(maze, "blinky");
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine(e.KeyCode);
            Console.WriteLine();
            p.keyPress(e);
            if(e.KeyCode == Keys.Space)
            {
                if(b)
                {
                    b = false;
                    timer1.Stop();
                    timer2.Stop();
                }
                else
                {
                    b = true;
                    timer1.Start();
                    timer2.Start();
                }
            }
            else if(e.KeyCode == Keys.D3)
            {
                blink.Stanje = 3;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            height =this.Height;
            width=this.Width;
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            height = this.Height;
            width = this.Width;
            Graphics g = this.CreateGraphics();
            g.FillRectangle(new SolidBrush(Color.Black), 0,0,width, height);
        }

        private void Load_From_File()
        {
            maze = System.IO.File.ReadAllLines(@"maze1.txt");
            foreach (string i in maze)
            {
                Console.WriteLine(i);
            }
        }
        private void Crtaj()
        {
            int dim = ((height-39) / maze.Length > width / maze[0].Length)? width / maze[0].Length: (height-39) / maze.Length;
            int xMove = (width - dim * maze[0].Length)/2;
            int yMove = (height - dim * maze.Length - 39)/2;
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            
            try
            {
                Console.WriteLine();
                Bitmap walltest = new Bitmap(Image.FromFile(@"./images/test_wall.png"), new Size(dim, dim));
                Bitmap dot = new Bitmap(Image.FromFile("./images/dot.png"), new Size(dim, dim));
                Bitmap superdot = new Bitmap(Image.FromFile("./images/superdot.png"), new Size(dim, dim));

                for (int i = 0; i < maze.Length; i++)
                {
                    for (int j = 0; j < maze[i].Length; j++)
                    {
                        if (maze[i][j] == '#')
                        {
                            g.DrawImage(walltest, j * dim + xMove, i * dim + yMove);
                        }
                        else if (maze[i][j] == '*')
                        {
                            g.DrawImage(dot, j * dim + xMove, i * dim + yMove);
                        }
                        else if (maze[i][j] == '&')
                        {
                            g.DrawImage(superdot, j * dim + xMove, i * dim + yMove);
                        }
                        else
                        {
                            g.FillRectangle(new SolidBrush(Color.Black), j * dim + xMove, i * dim + yMove, dim, dim);
                        }
                        
                    }
                    Console.WriteLine(maze[i]);
                }
                
                g.DrawImage(p.Slika, p.Xcalc(dim, tick) + xMove, p.Ycalc(dim, tick) + yMove, dim, dim);
                g.DrawImage(blink.Slika, blink.Xcalc(dim, tick) + xMove, blink.Ycalc(dim, tick) + yMove, dim, dim);

                g.FillRectangle(new SolidBrush(Color.Black), 0, 0, xMove, height);
                g.FillRectangle(new SolidBrush(Color.Black), width - xMove, 0, xMove, height);
                g.FillRectangle(new SolidBrush(Color.Black), 0, 0, width, yMove);
                g.FillRectangle(new SolidBrush(Color.Black), 0, width - yMove, width, yMove);

                Console.WriteLine(blink.Target.X + " "+ blink.Target.Y);
                Console.WriteLine(blink.Location.X + " " + blink.Location.Y + " " + blink.Stanje);

                Graphics gx = this.CreateGraphics();
                gx.DrawImage(bmp, 0, 0);
            }
            catch(Exception e)
            {

            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tick++;
            if (tick == 9)
            {
                tick = 1;
            }
            p.Slika = new Bitmap(Image.FromFile("./Images/pacman" + ((tick%4)+1) + ".png"));

            if (tick%8==0)
            {
                int t = p.movePacMan(ref maze);
                blink.algorithm(new Point(p.X, p.Y), maze);

                if(t == 1)
                {
                    Random rnd = new Random();
                    stanje3 = rnd.Next(5, 11);
                    blink.Stanje = 3;
                    blink.odrediSliku();
                    timer3.Start();
                }

                if (new Point(p.X, p.Y) == blink.Location)
                {
                    if (blink.Stanje < 2)
                    {       
                        if (p.pocetak(ref maze) == 1)
                        {
                            timer1.Stop();
                        }
                    }
                    else if(blink.Stanje == 3)
                    {
                        blink.Stanje = 2;
                        blink.odrediSliku();
                    }
                }
                
                if (blink.provera_lokacije())
                {
                    blink.Stanje = 0;
                    blink.odrediSliku();
                }
            }
            
            Crtaj();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            tick2++;
            try
            {
                if(tick2 == timer[counter])
                {
                    
                    if(counter%2==0 && blink.Stanje==1)
                    {
                        blink.Stanje = 0;
                    }
                    else if(counter %2==1 && blink.Stanje==0)
                    {
                        blink.Stanje = 1;
                    }
                    counter++;
                    tick2 = 0;
                }
            }
            catch(Exception ex)
            {
                if (counter % 2 == 0 && blink.Stanje == 1)
                {
                    blink.Stanje = 0;
                }
                else if (counter % 2 == 1 && blink.Stanje == 0)
                {
                    blink.Stanje = 1;
                }
                timer2.Stop();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            stanje3--;
            if(stanje3 == 0)
            {
                if(blink.Stanje==3)
                {
                    blink.Stanje = 0;
                    blink.odrediSliku();
                }
                
                timer3.Stop();
            }
        }
    }
}