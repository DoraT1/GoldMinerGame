using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    public abstract class Likovi : Sprite
    {
        protected int brzinaKretanja;
        public int Brzina
        {
            get { return brzinaKretanja; }
            set
            {
                if (brzinaKretanja <= 0)
                {
                    brzinaKretanja = 1;
                }
                else
                {
                    brzinaKretanja = value;
                }
            }
        }

        public Likovi(string slika, int xcor, int ycor, int brzina) : base(slika, xcor, ycor)
        {
            this.brzinaKretanja = brzina;
        }
    }

    public class Prepreke : Likovi
    {
        protected int gubljenjeZivota;

        public int GubljenjeZivota
        {
            get { return gubljenjeZivota; }
            set { gubljenjeZivota = value; }
        }
        public Prepreke(string slika, int xcor, int ycor, int brzina, int gubljenjeZivota) : base(slika, xcor, ycor, brzina)
        {
            this.gubljenjeZivota = gubljenjeZivota;
        }

        Random r = new Random();
        public override int X  //ne dopustamo da izade van granica
        {
            get
            {
                return this.x;
            }
            set
            {
                if (value < GameOptions.LeftEdge)
                {
                    x=GameOptions.RightEdge;
                    this.y=r.Next(200, 350);
                }
                else if (value > GameOptions.RightEdge)
                {
                    x = GameOptions.LeftEdge;
                    this.y = r.Next(300, 450);
                }
                else
                {
                    x = value;
                }
            }
        }
    }

    public class Blago : Likovi
    {
        protected int brojBodova;

        public int BrojBodova
        {
            get { return brojBodova; }
            set { brojBodova = value; }
        }

        public Blago(string slika, int xcor, int ycor, int brzina, int brBodova) : base(slika, xcor, ycor, brzina)
        {
            this.brojBodova = brBodova;
        }

    }

    public class Alat : Likovi
    {
        public bool spreman; //motika na svom mjestu, spremna kopati
        public Alat(string slika, int xcor, int ycor, int brzina) : base(slika, xcor, ycor, 30)
        {
            this.spreman = true;
        }
    }




}

