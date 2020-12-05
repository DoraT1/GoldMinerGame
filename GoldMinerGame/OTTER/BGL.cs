using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OTTER
{
    /// <summary>
    /// -
    /// </summary>
    public partial class BGL : Form
    {

        /* ------------------- */
        #region Environment Variables

        List<Func<int>> GreenFlagScripts = new List<Func<int>>();

        /// <summary>
        /// Uvjet izvršavanja igre. Ako je <c>START == true</c> igra će se izvršavati.
        /// </summary>
        /// <example><c>START</c> se često koristi za beskonačnu petlju. Primjer metode/skripte:
        /// <code>
        /// private int MojaMetoda()
        /// {
        ///     while(START)
        ///     {
        ///       //ovdje ide kod
        ///     }
        ///     return 0;
        /// }</code>
        /// </example>
        public static bool START = true;

        //sprites
        /// <summary>
        /// Broj likova.
        /// </summary>
        public static int spriteCount = 0, soundCount = 0;

        /// <summary>
        /// Lista svih likova.
        /// </summary>
        //public static List<Sprite> allSprites = new List<Sprite>();
        public static SpriteList<Sprite> allSprites = new SpriteList<Sprite>();

        //sensing
        int mouseX, mouseY;
        Sensing sensing = new Sensing();

        //background
        List<string> backgroundImages = new List<string>();
        int backgroundImageIndex = 0;
        string ISPIS = "";

        SoundPlayer[] sounds = new SoundPlayer[1000];
        TextReader[] readFiles = new StreamReader[1000];
        TextWriter[] writeFiles = new StreamWriter[1000];
        bool showSync = false;
        int loopcount;
        DateTime dt = new DateTime();
        String time;
        double lastTime, thisTime, diff;

        #endregion
        /* ------------------- */
        #region Events

        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            try
            {                
                foreach (Sprite sprite in allSprites)
                {                    
                    if (sprite != null)
                        if (sprite.Show == true)
                        {
                            g.DrawImage(sprite.CurrentCostume, new Rectangle(sprite.X, sprite.Y, sprite.Width, sprite.Heigth));
                        }
                    if (allSprites.Change)
                        break;
                }
                if (allSprites.Change)
                    allSprites.Change = false;
            }
            catch
            {
                //ako se doda sprite dok crta onda se mijenja allSprites
                //MessageBox.Show("Greška!");
                //return;
            }
        }

        private void startTimer(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            Init();
        }

        private void updateFrameRate(object sender, EventArgs e)
        {
            updateSyncRate();
        }

        /// <summary>
        /// Crta tekst po pozornici.
        /// </summary>
        /// <param name="sender">-</param>
        /// <param name="e">-</param>
        public void DrawTextOnScreen(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var brush = new SolidBrush(Color.WhiteSmoke);
            string text = ISPIS;

            SizeF stringSize = new SizeF();
            Font stringFont = new Font("Arial", 14);
            stringSize = e.Graphics.MeasureString(text, stringFont);

            using (Font font1 = stringFont)
            {
                RectangleF rectF1 = new RectangleF(0, 0, stringSize.Width, stringSize.Height);
                e.Graphics.FillRectangle(brush, Rectangle.Round(rectF1));
                e.Graphics.DrawString(text, font1, Brushes.Black, rectF1);
            }
        }

        private void mouseClicked(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;            
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = false;
            sensing.MouseDown = false;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;

            //sensing.MouseX = e.X;
            //sensing.MouseY = e.Y;
            //Sensing.Mouse.x = e.X;
            //Sensing.Mouse.y = e.Y;
            sensing.Mouse.X = e.X;
            sensing.Mouse.Y = e.Y;

        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            sensing.Key = e.KeyCode.ToString();
            sensing.KeyPressedTest = true;
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            sensing.Key = "";
            sensing.KeyPressedTest = false;
        }

        private void Update(object sender, EventArgs e)
        {
            if (sensing.KeyPressed(Keys.Escape))
            {
                START = false;
            }

            if (START)
            {
                this.Refresh();
            }
        }

        #endregion
        /* ------------------- */
        #region Start of Game Methods

        //my
        #region my

        //private void StartScriptAndWait(Func<int> scriptName)
        //{
        //    Task t = Task.Factory.StartNew(scriptName);
        //    t.Wait();
        //}

        //private void StartScript(Func<int> scriptName)
        //{
        //    Task t;
        //    t = Task.Factory.StartNew(scriptName);
        //}

        private int AnimateBackground(int intervalMS)
        {
            while (START)
            {
                setBackgroundPicture(backgroundImages[backgroundImageIndex]);
                Game.WaitMS(intervalMS);
                backgroundImageIndex++;
                if (backgroundImageIndex == 3)
                    backgroundImageIndex = 0;
            }
            return 0;
        }

        private void KlikNaZastavicu()
        {
            foreach (Func<int> f in GreenFlagScripts)
            {
                Task.Factory.StartNew(f);
            }
        }

        #endregion

        /// <summary>
        /// BGL
        /// </summary>

        string imeIgraca;
        int level;
        int bodovi;
        int brojzivota;
        public BGL(string ime,int L, int B, int BZ)
        {
            InitializeComponent();
            imeIgraca = ime;
            level = L;
            bodovi = B;
            brojzivota = BZ;
        }

        /// <summary>
        /// Pričekaj (pauza) u sekundama.
        /// </summary>
        /// <example>Pričekaj pola sekunde: <code>Wait(0.5);</code></example>
        /// <param name="sekunde">Realan broj.</param>
        public void Wait(double sekunde)
        {
            int ms = (int)(sekunde * 1000);
            Thread.Sleep(ms);
        }

        //private int SlucajanBroj(int min, int max)
        //{
        //    Random r = new Random();
        //    int br = r.Next(min, max + 1);
        //    return br;
        //}

        /// <summary>
        /// -
        /// </summary>
        public void Init()
        {
            if (dt == null) time = dt.TimeOfDay.ToString();
            loopcount++;
            //Load resources and level here
            this.Paint += new PaintEventHandler(DrawTextOnScreen);
            SetupGame();
        }

        /// <summary>
        /// -
        /// </summary>
        /// <param name="val">-</param>
        public void showSyncRate(bool val)
        {
            showSync = val;
            if (val == true) syncRate.Show();
            if (val == false) syncRate.Hide();
        }

        /// <summary>
        /// -
        /// </summary>
        public void updateSyncRate()
        {
            if (showSync == true)
            {
                thisTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
                diff = thisTime - lastTime;
                lastTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;

                double fr = (1000 / diff) / 1000;

                int fr2 = Convert.ToInt32(fr);

                syncRate.Text = fr2.ToString();
            }

        }

        //stage
        #region Stage

        /// <summary>
        /// Postavi naslov pozornice.
        /// </summary>
        /// <param name="title">tekst koji će se ispisati na vrhu (naslovnoj traci).</param>
        public void SetStageTitle(string title)
        {
            this.Text = title;
        }

        /// <summary>
        /// Postavi boju pozadine.
        /// </summary>
        /// <param name="r">r</param>
        /// <param name="g">g</param>
        /// <param name="b">b</param>
        public void setBackgroundColor(int r, int g, int b)
        {
            this.BackColor = Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Postavi boju pozornice. <c>Color</c> je ugrađeni tip.
        /// </summary>
        /// <param name="color"></param>
        public void setBackgroundColor(Color color)
        {
            this.BackColor = color;
        }

        /// <summary>
        /// Postavi sliku pozornice.
        /// </summary>
        /// <param name="backgroundImage">Naziv (putanja) slike.</param>
        public void setBackgroundPicture(string backgroundImage)
        {
            this.BackgroundImage = new Bitmap(backgroundImage);
        }

        /// <summary>
        /// Izgled slike.
        /// </summary>
        /// <param name="layout">none, tile, stretch, center, zoom</param>
        public void setPictureLayout(string layout)
        {
            if (layout.ToLower() == "none") this.BackgroundImageLayout = ImageLayout.None;
            if (layout.ToLower() == "tile") this.BackgroundImageLayout = ImageLayout.Tile;
            if (layout.ToLower() == "stretch") this.BackgroundImageLayout = ImageLayout.Stretch;
            if (layout.ToLower() == "center") this.BackgroundImageLayout = ImageLayout.Center;
            if (layout.ToLower() == "zoom") this.BackgroundImageLayout = ImageLayout.Zoom;
        }

        #endregion

        //sound
        #region sound methods

        /// <summary>
        /// Učitaj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        /// <param name="file">-</param>
        public void loadSound(int soundNum, string file)
        {
            soundCount++;
            sounds[soundNum] = new SoundPlayer(file);
        }

        /// <summary>
        /// Sviraj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        public void playSound(int soundNum)
        {
            sounds[soundNum].Play();
        }

        /// <summary>
        /// loopSound
        /// </summary>
        /// <param name="soundNum">-</param>
        public void loopSound(int soundNum)
        {
            sounds[soundNum].PlayLooping();
        }

        /// <summary>
        /// Zaustavi zvuk.
        /// </summary>
        /// <param name="soundNum">broj</param>
        public void stopSound(int soundNum)
        {
            sounds[soundNum].Stop();
        }

        #endregion

        //file
        #region file methods

        /// <summary>
        /// Otvori datoteku za čitanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToRead(string fileName, int fileNum)
        {
            readFiles[fileNum] = new StreamReader(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToRead(int fileNum)
        {
            readFiles[fileNum].Close();
        }

        /// <summary>
        /// Otvori datoteku za pisanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToWrite(string fileName, int fileNum)
        {
            writeFiles[fileNum] = new StreamWriter(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToWrite(int fileNum)
        {
            writeFiles[fileNum].Close();
        }

        /// <summary>
        /// Zapiši liniju u datoteku.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <param name="line">linija</param>
        public void writeLine(int fileNum, string line)
        {
            writeFiles[fileNum].WriteLine(line);
        }

        /// <summary>
        /// Pročitaj liniju iz datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća pročitanu liniju</returns>
        public string readLine(int fileNum)
        {
            return readFiles[fileNum].ReadLine();
        }

        /// <summary>
        /// Čita sadržaj datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća sadržaj</returns>
        public string readFile(int fileNum)
        {
            return readFiles[fileNum].ReadToEnd();
        }

        #endregion

        //mouse & keys
        #region mouse methods

        /// <summary>
        /// Sakrij strelicu miša.
        /// </summary>
        public void hideMouse()
        {
            Cursor.Hide();
        }

        /// <summary>
        /// Pokaži strelicu miša.
        /// </summary>
        public void showMouse()
        {
            Cursor.Show();
        }

        /// <summary>
        /// Provjerava je li miš pritisnut.
        /// </summary>
        /// <returns>true/false</returns>
        public bool isMousePressed()
        {
            //return sensing.MouseDown;
            return sensing.MouseDown;
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">naziv tipke</param>
        /// <returns></returns>
        public bool isKeyPressed(string key)
        {
            if (sensing.Key == key)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">tipka</param>
        /// <returns>true/false</returns>
        public bool isKeyPressed(Keys key)
        {
            if (sensing.Key == key.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #endregion
        /* ------------------- */

        /* ------------ GAME CODE START ------------ */

        /* Game variables */


        /* Initialization */

        public delegate void TouchHandler();
        public static event TouchHandler DodirRuba;
        //Metoda za event
        private void Dodir()
        {
            if (mis.X>=GameOptions.RightEdge)
            {
                mis.X = 0;
            }
            if (bomba.X <= GameOptions.LeftEdge)
            {
                bomba.X = GameOptions.RightEdge;
            }
        }

        Prepreke bomba;
        Prepreke mis;
        Blago veciNovcic;
        Blago manjiNovcic;
        Blago poluga;
        Alat lopata;

        Label lblVrijeme;
        int timeLeft = 30;
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                if (brojzivota <= 0)
                {
                    timer3.Stop();
                    Prijelazi(brojzivota, bodovi, imeIgraca);
                }
                else
                {
                    timeLeft--;
                    lblVrijeme.Invoke((MethodInvoker)(() => lblVrijeme.Text = timeLeft.ToString()));
                }
                
            }
            else if (timeLeft <= 0)
            {
                timer3.Stop();
                Prijelazi(brojzivota, bodovi, imeIgraca);
            }

        }

        private void IspisRezultata(int bodovi, int zivoti, int level)
        {
            ISPIS = String.Format("Igrač: {0} Bodovi: {1} Životi: {2}\nLevel: {3}", imeIgraca, bodovi, zivoti,level);
        }
        

        Random r = new Random();

        private void SetupGame()
        {
            //1. setup stage
            SetStageTitle("PMF");
            //setBackgroundColor(Color.WhiteSmoke);            
            setBackgroundPicture("backgrounds\\level1.png");
            //none, tile, stretch, center, zoom
            setPictureLayout("stretch");

            //2. add sprites
            poluga = new Blago("sprites\\poluga.png", r.Next(0,GameOptions.RightEdge-50), r.Next(200,GameOptions.DownEdge-50), 2, 10);
            poluga.SetSize(10);

            manjiNovcic = new Blago("sprites\\novcic.png", r.Next(0, GameOptions.RightEdge-50), r.Next(200, GameOptions.DownEdge-50), 2, 1);
            manjiNovcic.SetSize(8);

            veciNovcic = new Blago("sprites\\novcic.png", r.Next(0, GameOptions.RightEdge-50), r.Next(200, GameOptions.DownEdge-50), 2, 5);
            veciNovcic.SetSize(10);

            bomba = new Prepreke("sprites\\bomb.png",GameOptions.RightEdge,r.Next(300, 450), 3, 1);
            bomba.SetSize(15);

            mis = new Prepreke("sprites\\mouse.png",0, r.Next(200, 350), 3, 2);
            mis.SetSize(8);
            
            lopata = new Alat("sprites\\lopata.png", 0, 150, 0);
            lopata.SetSize(40);
            lopata.SetX((GameOptions.RightEdge - lopata.Width)/ 2);
            lopata.RotationStyle = "LeftRight";

            lblVrijeme = new Label();  //ispis preostalog vremena
            lblVrijeme.Location = new Point(2, 50);
            lblVrijeme.AutoSize = true;
            lblVrijeme.Text = "30";
            this.Controls.Add(lblVrijeme);

            //3. scripts that start
            //povezivanja dogadaja i metode

            DodirRuba += Dodir;
            Game.StartScript(Provjera);
            
        }

        /* Scripts */

        private int Provjera()
        {
            while (START) //ili neki drugi uvjet
            {
                if (level == 1)
                {
                    PrviLevel();
                }

                if (level == 2)
                {
                    DrugiLevel();
                }
            }
            return 0;
        }

        private void PrviLevel()
        {
            allSprites.Clear();
            Game.AddSprite(lopata);
            Game.AddSprite(poluga);
            Game.AddSprite(manjiNovcic);
            Game.AddSprite(veciNovcic);
            Game.AddSprite(bomba);
            OkretanjeLopate();
        }

        private void DrugiLevel()
        {
            allSprites.Clear();
            Game.AddSprite(lopata);
            Game.AddSprite(poluga);
            Game.AddSprite(manjiNovcic);
            Game.AddSprite(veciNovcic);
            Game.AddSprite(bomba);
            Game.AddSprite(mis);
            OkretanjeLopate2();
        }

        private int OkretanjeLopate()
        {
            while (START)
            {
                bomba.X -= 5;
                
                Wait(0.1);

                IspisRezultata(bodovi,brojzivota,level);

                lopata.PointToMouse(sensing.Mouse);
                Wait(0.01);

                if (sensing.MouseDown)
                {
                    if (lopata.spreman)
                    {
                        lopata.spreman = false;
                        Game.StartScript(Kopanje);
                        lopata.spreman= false;
                    }
                }
            }
            return 0;
        }

        private int OkretanjeLopate2()
        {
            while (START)
            {
                bomba.X -= 5;
                mis.X += 7;
                Wait(0.1);

                IspisRezultata(bodovi, brojzivota,level);

                lopata.PointToMouse(sensing.Mouse);
                Wait(0.01);

                if (sensing.MouseDown)
                {
                    if (lopata.spreman)
                    {
                        lopata.spreman = false;
                        Game.StartScript(Kopanje);
                        lopata.spreman = false;
                    }
                }
            }
            return 0;
        }

        private int Kopanje()
        {
            lopata.SetDirection(lopata.GetHeading());
            while (lopata.spreman == false && brojzivota>0) //kopa i nedostupan je za upravljanje
            {
                lopata.MoveSteps(lopata.Brzina);
                Wait(0.1);
                if (lopata.TouchingEdge())
                {
                    lopata.Y -= lopata.Brzina;
                    lopata.spreman = true;
                    lopata.SetX((GameOptions.RightEdge - lopata.Width) / 2);
                    lopata.SetY(150);
                }
                if (lopata.TouchingSprite(bomba))
                {
                    lopata.spreman= true;
                    brojzivota = brojzivota - bomba.GubljenjeZivota;
                    IspisRezultata(bodovi,brojzivota,level);
                    bomba.GotoXY(r.Next(0, 650), r.Next(200, 450));
                    lopata.SetX((GameOptions.RightEdge - lopata.Width) / 2);
                    lopata.SetY(150);
                }

                if (lopata.TouchingSprite(mis))
                {
                    brojzivota = brojzivota - mis.GubljenjeZivota;
                    lopata.spreman = true;
                    IspisRezultata(bodovi,brojzivota,level);
                    mis.GotoXY(r.Next(0, GameOptions.RightEdge-50), r.Next(200, GameOptions.DownEdge-50));
                    lopata.SetX((GameOptions.RightEdge - lopata.Width) / 2);
                    lopata.SetY(150);
                }
                if (lopata.TouchingSprite(manjiNovcic))
                {
                    bodovi = bodovi + manjiNovcic.BrojBodova;
                    manjiNovcic.GotoXY(-150, -200);
                    lopata.spreman = true;
                    IspisRezultata(bodovi, brojzivota,level);
                    manjiNovcic.GotoXY(r.Next(0, GameOptions.RightEdge-50), r.Next(200, GameOptions.DownEdge-50));
                    lopata.SetX((GameOptions.RightEdge - lopata.Width) / 2);
                    lopata.SetY(150);
                }
                if (lopata.TouchingSprite(veciNovcic))
                {
                    bodovi = bodovi + veciNovcic.BrojBodova;
                    veciNovcic.GotoXY(-150, -200);
                    lopata.spreman = true;
                    IspisRezultata(bodovi, brojzivota,level);
                    veciNovcic.GotoXY(r.Next(0, GameOptions.RightEdge-50), r.Next(200, GameOptions.DownEdge-50));
                    lopata.SetX((GameOptions.RightEdge - lopata.Width) / 2);
                    lopata.SetY(150);
                }
                if (lopata.TouchingSprite(poluga))
                {
                    bodovi = bodovi + poluga.BrojBodova;
                    poluga.GotoXY(-150, -200);
                    lopata.spreman = true;
                    IspisRezultata(bodovi, brojzivota,level);
                    poluga.GotoXY(r.Next(0, GameOptions.RightEdge-50), r.Next(200, GameOptions.DownEdge-50));
                    lopata.SetX((GameOptions.RightEdge - lopata.Width) / 2);
                    lopata.SetY(150);
                }
                
            }
            return 0;
        }


        public void Prijelazi(int brZ, int b, string ime)
        {
            if ((level == 1 || level == 2) && brZ <= 0) //ako su izgubljeni svi zivoti
            {
                DatotekaUpis(ime, b);
                Form3 zavrsna = new Form3(ime, b);
                START = false;

                //wait da pričeka dok se sve pogasi što treba na start
                Wait(0.5);
                //hide, da se ne zatvori jer nakon Close se ništa više neće izvršiti
                this.Hide();
                //bgl nije zatvorena te čeka dok završna ne obavi što treba
                zavrsna.ShowDialog();
                //nakon što se završna zatvori, ili će se zatvoriti ili će prije djelovati Restart
                this.Close();

            }

            if (brZ > 0 && level == 1 && timeLeft==0) //prijelaz na drugi level
            {
                if (brZ == 6)
                {
                    b += 10;
                }
                START = false;
                START = true;
                MessageBox.Show("CESTITATMO! IDETE NA DRUGI LEVEL!\n\nVasi trenutni bodovi: "+b+"\nPreostali zivoti: " + brZ);
                BGL novi = new BGL(imeIgraca,2, b, brZ);
                this.Hide();
                novi.ShowDialog();
                this.Close();
            }

            if (brZ > 0 && level == 2 && timeLeft==0) //kraj
            {
                if (brZ == 6)
                {
                    b += 10;
                }
                DatotekaUpis(ime, b);
                MessageBox.Show("BRAVO\nDošli ste do kraja igre :)\n\nBodovi: "+b);

                Form3 zavrsna = new Form3(ime,b);
                this.Hide();
                zavrsna.ShowDialog();
                START = false;
                allSprites.Clear();
            }
        }

        public void DatotekaUpis(string im, int bod)
        {
            using (StreamWriter sw = File.AppendText("ranglista.txt"))
            {
                sw.WriteLine(im + " " + bod);
            }
        }
        

        /* ------------ GAME CODE END ------------ */


    }
}
