using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapEditor
{
    public partial class Form1 : Form
    {
        int x;
        int y;
        string c;
        public Form1()
        {
            InitializeComponent();
        }

        private void Put_Click(object sender, EventArgs e)
        {

            PictureBox truc = new PictureBox();
            
            truc.Location = new Point(256 + 32 * x, 32 * y + 15);
            truc.Size = new Size(32, 32);
            switch (c)
            {
                case "Terre":
                    truc.Image = MapEditor.Properties.Resources.terre;
                    break;
                case "Montagne":
                    truc.Image = MapEditor.Properties.Resources.montagne;
                    break;
                case "Eau":
                    truc.Image = MapEditor.Properties.Resources.eau;
                    break;
                default: truc.Image = MapEditor.Properties.Resources.terre;
                    break;
            }
            truc.Visible = true;
            Controls.Add(truc);
        }

        private void PosX_SelectedIndexChanged(object sender, EventArgs e)
        {
            x = PosX.SelectedIndex;
        }


        private void PosY_SelectedIndexChanged(object sender, EventArgs e)
        {
            y = PosY.SelectedIndex;
        }

        private void CaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            c = (string)CaseType.SelectedItem;
        }



        /*x = int.Parse(PosX.SelectedItem.ToString());
            y = int.Parse(PosY.SelectedItem.ToString());*/



    }
}
