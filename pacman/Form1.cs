
namespace pacman
{
    public partial class Form1 : Form
    {
        int height;
        int width;
        string[] maze;
        int tick = 4;
        bool b = true;
        PacMan p;
        public Form1()
        {
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
                }
                else
                {
                    b = true;
                    timer1.Start();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            height=this.Height;
            width=this.Width;
            Load_From_File();
            p = new PacMan(maze);
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
            Graphics g = this.CreateGraphics();
            
            try
            {
                Console.WriteLine();
                Bitmap walltest = new Bitmap(Image.FromFile(@"./images/test_wall.png"), new Size(dim, dim));
                for (int i = 0; i < maze.Length; i++)
                {
                    for (int j = 0; j < maze[i].Length; j++)
                    {
                        if (maze[i][j] == '#')
                        {
                            g.DrawImage(walltest, j * dim + xMove, i * dim + yMove);
                        }
                        
                        else
                        {
                            g.FillRectangle(new SolidBrush(Color.Black), j * dim + xMove, i * dim + yMove, dim, dim);
                        }
                        
                    }
                    Console.WriteLine(maze[i]);
                }
                g.DrawImage(p.Slika, p.X * dim + xMove, p.Y * dim + yMove, dim, dim);
                Console.WriteLine(p.X+" "+p.Y);

            }
            catch(Exception e)
            {

            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tick++;
            if (tick == 5)
            {
                tick = 1;
            }
            p.Slika = new Bitmap(Image.FromFile("./Images/pacman" + tick + ".png"));
            p.movePacMan(ref maze);
            Crtaj();
        }
        
    }
}