using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Advanced_Tactics
{
    public class Player
    {
        //protected Unit unit;
        //protected List<Unit> unitofplayer;
        //protected int score;
        //protected Cell[,] carte;

        //public List<Unit> UnitOfPlayer { get { return unitofplayer; } set { unitofplayer = value; } }
        //public Color PlayerColor { get; set; }

        //public Player(Cell[,] Carte)
        //{
        //    unitofplayer = new List<Unit>();
        //    carte = Carte;
        //}

        //protected void Random()
        //{
        //    unit = new Unit("plane", "fou", carte, 1, 5, unitofplayer);

        //    string[] arrayrang = new string[] { "aa", "com", "doc", "hq", "ing", "plane", "pvt", "tank", "truck" };
        //    string[] arrayclasse = new string[] { "roi", "dame", "tour", "fou", "cavalier", "pion" };


        //    // Fonction anonyme qui permet de faire ce que ferait une methode void sans utiliser de methode, et c'est justement l'avantage
        //    // http://msdn.microsoft.com/en-us/library/dd267613(v=vs.110).aspx
        //    // Cette fonction cree tous simplements plusieurs unitees
        //    Func<string, string, Map, int, int, List<Unit>, Unit, Unit> Rdunit = (r, c, m, x, y, l, u) => new Unit(r, c, m.Carte, x, y, l);
        //    Random rrd = new Random();

        //    // Et ici j'appelle en boucle la dite fonction n fois, n etant le nombre d'unitees voulus
        //    for (int i = 0; i < rrd.Next(100, 200); i++)
        //        Rdunit(arrayrang[rrd.Next(arrayrang.Count())], arrayclasse[rrd.Next(arrayclasse.Count())], map, rrd.Next(0, data.WidthMap), rrd.Next(0, data.HeightMap), ListToDraw, unit);
        //}
    }
}
