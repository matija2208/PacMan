
namespace pacman
{
    public partial class Form1 : Form
    {
        int height;
        int width;
        string[] maze;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int speed = 1;
            //Bitmap pac = new Bitmap(Image.FromFile(@"C:/Users/matij/source/repos/pacman/pacman/pacman1.png"), new Size(96,96));
            
            Graphics g = this.CreateGraphics();
            
            if(e.KeyCode == Keys.Up)
            {
                Crtaj();
            }
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            height=this.Height;
            width=this.Width;
            Console.WriteLine(height + " " + width);
            Load_From_File();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            height = this.Height;
            width = this.Width;
            Console.WriteLine("resize: "+height + " " + width);
            Crtaj();
        }

        private void Load_From_File()
        {
            maze = System.IO.File.ReadAllLines(@"C:\Users\matij\source\repos\pacman\pacman\maze1.txt");
            foreach(string i in maze)
                Console.WriteLine(i);
        }
        private void Crtaj()
        {
            int dim = ((height-39) / maze.Length > width / maze[0].Length)? width / maze[0].Length: (height-39) / maze.Length;
            int xMove = (width - dim * maze[0].Length)/2;
            int yMove = (height - dim * maze.Length - 39)/2;
            Graphics g = this.CreateGraphics();
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, width, height);
            try
            {
                Bitmap walltest = new Bitmap(Image.FromFile(@"C:/Users/matij/source/repos/pacman/pacman/images/test_wall.png"), new Size(dim, dim));
                Console.WriteLine("CharDimmens: " + maze.Length + " x " + maze[0].Length + "\t" + dim);
                for (int i = 0; i < maze.Length; i++)
                {
                    for (int j = 0; j < maze[i].Length; j++)
                    {
                        if (maze[i][j] == '#')
                        {
                            g.DrawImage(walltest, j * dim + xMove, i * dim + yMove);
                        }
                    }
                }
            }
            catch(Exception e)
            {

            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine(1);
        }
    }
}