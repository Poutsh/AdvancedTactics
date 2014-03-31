using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Advanced_Tactics_Propre
{
    class Resources
    {
        public static Texture2D OptionReso, OptionReso2, OptionReso3,
                                OptionScreen, OptionScreen2,
                                OptionVolB, OptionVolM, OptionsVolumeB2, OptionsVolumeB3, OptionsVolumeM2, OptionsVolumeM3,
                                TitreJouer, TitreOptions, TitreQuitter, OptionRetour;

        public static Texture2D souris, viseur;

        public static Texture2D casepossible, caseinterdite;

        public static Texture2D map;

        public static Texture2D tank, tanksheet;

        public static SoundEffect sonsouris;
        public static Song musiquemenu;
        

        public static void LoadContent(ContentManager content)
        {
            //Menu
            OptionReso = content.Load<Texture2D>("Menu/OptionReso");
            OptionReso2 = content.Load<Texture2D>("Menu/OptionReso2");
            OptionReso3 = content.Load<Texture2D>("Menu/OptionReso3");
            OptionScreen = content.Load<Texture2D>("Menu/OptionScreen");
            OptionScreen2 = content.Load<Texture2D>("Menu/OptionScreen2");
            OptionVolB = content.Load<Texture2D>("Menu/OptionVolB");
            OptionVolM = content.Load<Texture2D>("Menu/OptionVolM");
            OptionsVolumeB2 = content.Load<Texture2D>("Menu/OptionsVolumeB2");
            OptionsVolumeB3 = content.Load<Texture2D>("Menu/OptionsVolumeB3");
            OptionsVolumeM2 = content.Load<Texture2D>("Menu/OptionsVolumeM2");
            OptionsVolumeM3 = content.Load<Texture2D>("Menu/OptionsVolumeM3");
            TitreJouer = content.Load<Texture2D>("Menu/TitreJouer");
            TitreOptions = content.Load<Texture2D>("Menu/TitreOptions");
            TitreQuitter = content.Load<Texture2D>("Menu/TitreQuitter");
            OptionRetour = content.Load<Texture2D>("Menu/OptionRetour");

            //Curseur, Souris
            souris = content.Load<Texture2D>("Curseur/cursortransp");
            viseur = content.Load<Texture2D>("Curseur/viseur");

            //Case
            casepossible = content.Load<Texture2D>("Case/bleu");
            caseinterdite = content.Load<Texture2D>("Case/rouge");

            //Map
            map = content.Load<Texture2D>("Map/map1");

            //Unitees
            tank = content.Load<Texture2D>("Unitees/minitanktrans");
            tanksheet = content.Load<Texture2D>("Unitees/tanksheet");

            //Musique, Son
            musiquemenu = content.Load<Song>("Son/Russian Red Army Choir");
            sonsouris = content.Load<SoundEffect>("Son/click1");

        }
    }
}
