
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
        int[][] timer = new int[][] {
            new int[]{ 7, 20, 7, 20, 5, 20, 5},
            new int[]{ 7, 20, 7, 20, 5},
            new int[]{ 5, 20, 5, 20, 5},
        };

        int counter = 0;
        int isStarted = 0;
        bool isRunning = true;
        bool b = true;
        int points = 0;
        int counter_pojedenih = 0;
        bool LEVEL_UP = false;
        int level = 0;
        PacMan p;
        Blinky blink;
        Pinky pink;
        Clyde clyd;
        Inky ink;

        public Form1()
        {
            InitializeComponent();
            Load_From_File();
            p = new PacMan(maze);
            blink = new Blinky(maze, "blinky");
            pink = new Pinky(maze, "pinky");
            clyd = new Clyde(maze, "clyde");
            ink = new Inky(maze, "inky");
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Console.WriteLine();
            //Console.WriteLine(e.KeyCode);
            //Console.WriteLine();
            p.keyPress(e);
            if(e.KeyCode == Keys.Space)
            {
                if(isStarted == 0)
                {
                    isStarted = 1;

                    Crtaj();
                    
                }
                else if(isStarted == 1)
                {
                    isStarted = 2;
                    timer1.Start();
                    timer2.Start();
                }
                else if(!isRunning && isStarted == 2)
                {
                    points = 0;
                    Load_From_File();
                    p = new PacMan(maze);
                    blink = new Blinky(maze, "blinky");
                    pink = new Pinky(maze, "pinky");
                    clyd = new Clyde(maze, "clyde");
                    ink = new Inky(maze, "inky");
                    height = this.Height;
                    width = this.Width;
                    tick = 0;
                    tick2 = 0;
                    counter = 0;
                    isRunning = true;
                    b = true;
                    points = 0;
                    counter_pojedenih = 0;
                    LEVEL_UP = false;
                    level = 0;

                    timer1.Start();
                    timer2.Start();
                }
                //else
                //{
                //    if (b)
                //    {
                //        b = false;
                //        timer1.Stop();
                //        timer2.Stop();
                //    }
                //    else
                //    {
                //        b = true;
                //        timer1.Start();
                //        timer2.Start();
                //    }
                //}
            }
            
        }

        

        private void Form1_Resize(object sender, EventArgs e)
        {
            height = this.Height;
            width = this.Width;
            Graphics g = this.CreateGraphics();
            g.FillRectangle(new SolidBrush(Color.Black), 0,0,width, height);
            

            Crtaj();
        }

        private void Load_From_File()
        {
            Random r = new Random();
            try
            {
                maze = System.IO.File.ReadAllLines("maze"+ r.Next(1,4)+".txt");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            foreach (string i in maze)
            {
                //Console.WriteLine(i);
            }
        }
        private void Crtaj()
        {
            int dim = ((height-39) / maze.Length > width / (maze[0].Length + 7))? width / (maze[0].Length + 7): (height-39) / maze.Length;
            int xMove = ((width - dim * maze[0].Length)/2>(dim*7)) ? (width - dim * maze[0].Length) / 2 : dim*7;
            int yMove = (height - dim * maze.Length - 39)/2;
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);


            
            if(isStarted == 0)
            {
                Graphics gx = this.CreateGraphics();
                int dimension = (width > height - 39) ? height - 39 : width;
                Bitmap title_screen = new Bitmap(Image.FromFile("./images/title.png"), dimension, dimension);
                gx.DrawImage(title_screen, (width - dimension) / 2, (height - dimension) / 2);
                //Console.WriteLine(isStarted);
            }
            else if(isStarted == 1)
            {
                isStarted = 2;
                Crtaj();
                isStarted = 1;
            }
            else if(isRunning)
            {
                try
                {
                    Console.WriteLine();
                    Bitmap walltest = new Bitmap(Image.FromFile(@"./images/test_wall.png"), new Size(dim, dim));
                    Bitmap dot = new Bitmap(Image.FromFile("./images/dot.png"), new Size(dim, dim));
                    Bitmap superdot = new Bitmap(Image.FromFile("./images/superdot.png"), new Size(dim, dim));

                    g.FillRectangle(new SolidBrush(Color.Black), 0, 0, width, height);
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
                        //Console.WriteLine(maze[i]);
                    }

                    g.DrawImage(p.Slika, p.Xcalc(dim, tick) + xMove, p.Ycalc(dim, tick) + yMove, dim, dim);
                    g.DrawImage(blink.Slika, blink.Xcalc(dim, tick) + xMove, blink.Ycalc(dim, tick) + yMove, dim, dim);
                    g.DrawImage(pink.Slika, pink.Xcalc(dim, tick) + xMove, pink.Ycalc(dim, tick) + yMove, dim, dim);
                    g.DrawImage(clyd.Slika, clyd.Xcalc(dim, tick) + xMove, clyd.Ycalc(dim, tick) + yMove, dim, dim);
                    g.DrawImage(ink.Slika, ink.Xcalc(dim, tick) + xMove, ink.Ycalc(dim, tick) + yMove, dim, dim);

                    

                    Bitmap life = new Bitmap(Image.FromFile("./images/pacman1.png"), dim, dim);

                    for(int i=0;i<p.Lifes;i++)
                    {
                        g.DrawImage(life, i * (dim + dim/3), yMove);
                    }

                    String text = "lvl: " + (level + 1);
                    Font f = new Font("Consolas", (float)dim, FontStyle.Regular, GraphicsUnit.Pixel);
                    g.DrawString(text, f, new SolidBrush(Color.White), new Point(0, (4 * dim)/3 + yMove));
                    text = "scr:\n" + points;
                    g.DrawString(text, f, new SolidBrush(Color.White), new Point(0, (8 * dim) / 3 + yMove));
                    text = "hi-scr:\n" + File.ReadAllText("highscore.txt");
                    g.DrawString(text, f, new SolidBrush(Color.White), new Point(0, (16 * dim) / 3 + yMove));
                    //Console.WriteLine(blink.Target.X + " " + blink.Target.Y);
                    //Console.WriteLine(blink.Location.X + " " + blink.Location.Y + " " + blink.Stanje);

                    Graphics gx = this.CreateGraphics();
                    gx.DrawImage(bmp, 0, 0);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            else if(!isRunning)
            {
                try
                {
                    isRunning = true;
                    Crtaj();
                    isRunning = false;
                    Graphics gx = this.CreateGraphics();

                    Font f = new Font("Consolas", 2.0f*(float)dim, FontStyle.Bold, GraphicsUnit.Pixel);
                    gx.DrawString("GAME OVER", f, new SolidBrush(Color.Yellow), new Point((width - 9 * dim) / 2, (height - dim) / 2 - 39));
                }
                catch(Exception e)
                {
                    isRunning = false;
                }
            }
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            height = this.Height;
            width = this.Width;
            timer4.Start();
        }
        void gh_pos_test( Ghost g)
        {
            if (new Point(p.X, p.Y) == g.Location)
            {
                if (blink.Stanje < 2)
                {
                    if (p.pocetak(ref maze) == 1)
                    {
                        timer1.Stop();
                        timer2.Stop();
                        isRunning = false;
                        try
                        {
                            if (points > int.Parse((File.ReadAllText("highscore.txt") != null) ? File.ReadAllText("highscore.txt") : "0"))
                            {
                                StreamWriter f = new StreamWriter("highscore.txt");
                                f.Write(points.ToString());
                                f.Close();
                            }
                        }
                        catch(Exception e)
                        {
                            StreamWriter f = new StreamWriter("highscore.txt");
                            f.Write(points.ToString());
                            f.Close();
                        }
                        
                    }
                }
                else if (g.Stanje == 3)
                {
                    g.Stanje = 2;
                    g.odrediSliku();
                    counter_pojedenih++;
                    points += counter_pojedenih * 200;
                }
            }
        }

        bool test_zvezdice()
        {
            bool t = true;
            for(int i = 0; i < maze.Length; i++)
            {
                for(int j = 0; j < maze[i].Length; j++)
                {
                    if(maze[i][j] == '*')
                    {
                        t = false;
                        goto end;
                    }
                }
            }
            end:
            return t;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tick++;
            if (tick == 5)
            {
                tick = 1;
            }
            
            p.Slika = new Bitmap(Image.FromFile("./Images/pacman" + ((tick%4)+1) + ".png"));

            if (tick%4==0)
            {
                if (LEVEL_UP)
                {
                    tick = 0;
                    Load_From_File();
                    p = new PacMan(maze);
                    blink = new Blinky(maze, "blinky");
                    pink = new Pinky(maze, "pinky");
                    clyd = new Clyde(maze, "clyde");
                    ink = new Inky(maze, "inky");
                    height = this.Height;
                    width = this.Width;

                    LEVEL_UP = false;
                    level++;
                    counter = 0;
                }

                int t = p.movePacMan(ref maze);
                blink.algorithm(new Point(p.X, p.Y), p.Trenutni_smer, blink.Location, maze);
                pink.algorithm(new Point(p.X, p.Y), p.Trenutni_smer, blink.Location, maze);
                clyd.algorithm(new Point(p.X, p.Y), p.Trenutni_smer, blink.Location, maze);
                ink.algorithm(new Point(p.X, p.Y), p.Trenutni_smer, blink.Location, maze);

                if(t == 1)
                {
                    Random rnd = new Random();
                    stanje3 = rnd.Next(5, 11);
                    blink.Stanje = 3;
                    pink.Stanje = 3;
                    clyd.Stanje = 3;
                    ink.Stanje = 3;

                    blink.odrediSliku();
                    pink.odrediSliku();
                    clyd.odrediSliku();
                    ink.odrediSliku();

                    points += 50;

                    timer3.Start();
                }
                else if(t == 2)
                {
                    points += 10;
                }
                if(test_zvezdice())
                {
                    LEVEL_UP = true;
                    
                }
                gh_pos_test(((Ghost)blink));
                gh_pos_test((Ghost)pink);
                gh_pos_test((Ghost)clyd);
                gh_pos_test((Ghost)ink);

                if (blink.provera_lokacije())
                {
                    blink.Stanje = 0;
                    blink.odrediSliku();
                }

                if (pink.provera_lokacije())
                {
                    pink.Stanje = 0;
                    pink.odrediSliku();
                }

                if (clyd.provera_lokacije())
                {
                    clyd.Stanje = 0;
                    clyd.odrediSliku();
                }

                if (ink.provera_lokacije())
                {
                    ink.Stanje = 0;
                    ink.odrediSliku();
                }
            }
            
            Crtaj();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            tick2++;
            try
            {
                if(tick2 == timer[(level==0)?0:(level<4)?1:2][counter])
                {
                    
                    if(counter%2==0 && blink.Stanje==1)
                    {
                        blink.Stanje = 0;
                        pink.Stanje = 0;
                        clyd.Stanje = 0;
                        ink.Stanje = 0;
                    }
                    else if(counter %2==1 && blink.Stanje==0)
                    {
                        blink.Stanje = 1;
                        pink.Stanje = 1;
                        clyd.Stanje = 1;
                        ink.Stanje = 1;
                    }
                    counter++;
                    tick2 = 0;
                }
            }
            catch(Exception ex)
            {
                if(blink.Stanje<2)
                {
                    blink.Stanje = 0;
                    pink.Stanje = 0;
                    clyd.Stanje = 0;
                    ink.Stanje = 0;
                }
                
                timer2.Stop();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            stanje3--;
            if(stanje3 == 0)
            {
                counter_pojedenih = 0;
                if(blink.Stanje==3)
                {
                    blink.Stanje = 0;
                    blink.odrediSliku();
                }
                if (pink.Stanje == 3)
                {
                    pink.Stanje = 0;
                    pink.odrediSliku();
                }
                if (clyd.Stanje == 3)
                {
                    clyd.Stanje = 0;
                    clyd.odrediSliku();
                }
                if (ink.Stanje == 3)
                {
                    ink.Stanje = 0;
                    ink.odrediSliku();
                }

                timer3.Stop();
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            Crtaj();
            timer4.Stop();
        }
    }
}