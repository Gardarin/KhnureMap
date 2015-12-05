using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Map.Model;

namespace Map
{
    public partial class Form1 : Form
    {
        Model.Model Mod;
        System.Drawing.Graphics Graph;
        System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
        int MouseState = 0;
        int MouseX;
        int MouseY;

        public Form1()
        {
            InitializeComponent();
            Graph = pictureBox1.CreateGraphics();


            int[,] ar=new int[18,18];
            
            Mod = new Model.Model(ar);
            SetNod();
            //Mod.SetStartFinish(Mod.AllNodes[0], Mod.AllNodes[6]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreatePath();
        }

        private void CreatePoint()
        {
            CreatePointForm form = new CreatePointForm();
            form.X = MouseX;
            form.Y = MouseY;
            form.ShowDialog();
            if (form.NewNode != null)
            {
                Mod.AllNodes.Add(form.NewNode);
            }
            RedrawPicture();
        }

        private void CreatePath()
        {
            RedrawPicture();
            Mod.SetStartFinish(Mod.AllNodes[Convert.ToInt32(textBox1.Text)], Mod.AllNodes[Convert.ToInt32(textBox2.Text)]);
            List<Node> Res = Mod.GetRout();
            for (int i = 0; i < Res.Count - 1; ++i)
            {
                Graph.DrawLine(myPen, Res[i].X, Res[i].Y, Res[i + 1].X, Res[i + 1].Y);
            }
        }

        private void RedrawPicture()
        {
            PictureBox p = new PictureBox();
            p.Image = pictureBox1.Image;
            Graph.DrawImage(p.Image, 0, 0);

            foreach (Node n in Mod.AllNodes)
            {
                Graph.DrawEllipse(myPen, n.X, n.Y, 4, 4);
            }
        }

        private void SetNod()
        {
            int[,] ar = new int[18, 18];
            ar[0, 7] = 1;
            ar[1, 8] = 1;
            ar[2, 11] = 1;
            ar[3, 13] = 1;
            ar[4, 17] = 1;
            ar[5, 15] = 1;
            ar[6, 10] = 1;
            ar[7, 8] = 3;
            ar[8, 9] = 5;
            ar[9, 10] = 2;
            ar[10, 11] = 1;
            ar[11, 12] = 1;
            ar[12, 13] = 2;
            ar[13, 14] = 1;
            ar[14, 15] = 2;
            ar[15, 16] = 2;
            ar[16, 17] = 1;
            ar[17, 9] = 1;

            ar[7, 0] = 1;
            ar[8, 1] = 1;
            ar[11, 2] = 1;
            ar[13, 3] = 1;
            ar[17, 4] = 1;
            ar[15, 5] = 1;
            ar[10, 6] = 1;
            ar[8, 7] = 3;
            ar[9, 8] = 5;
            ar[10, 9] = 2;
            ar[11, 10] = 1;
            ar[12, 11] = 1;
            ar[13, 12] = 2;
            ar[14, 13] = 1;
            ar[15, 14] = 2;
            ar[16, 15] = 2;
            ar[17, 16] = 1;
            ar[9, 17] = 1;

            Mod.Array = ar;

            Mod.AllNodes.Add(new Model.Node("0", 87, 180));
            Mod.AllNodes.Add(new Model.Node("1", 184, 105));
            Mod.AllNodes.Add(new Model.Node("2", 494, 105));
            Mod.AllNodes.Add(new Model.Node("3", 602, 287));
            Mod.AllNodes.Add(new Model.Node("4", 322, 251));
            Mod.AllNodes.Add(new Model.Node("5", 461, 317));
            Mod.AllNodes.Add(new Model.Node("6", 469, 178));
            Mod.AllNodes.Add(new Model.Node("7", 93, 139));
            Mod.AllNodes.Add(new Model.Node("8", 182, 141));
            Mod.AllNodes.Add(new Model.Node("9", 357, 145));
            Mod.AllNodes.Add(new Model.Node("10", 470, 142));
            Mod.AllNodes.Add(new Model.Node("11", 495, 142));
            Mod.AllNodes.Add(new Model.Node("12", 568, 142));
            Mod.AllNodes.Add(new Model.Node("13", 568, 288));
            Mod.AllNodes.Add(new Model.Node("14", 568, 344));
            Mod.AllNodes.Add(new Model.Node("15", 462, 347));
            Mod.AllNodes.Add(new Model.Node("16", 356, 347));
            Mod.AllNodes.Add(new Model.Node("17", 356, 257));
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            //label1.Text=""+
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseX = e.X;
            MouseY = e.Y;
            label1.Text = "" + e.X + "   " + e.Y;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MouseState == 1)
            {
                MouseState = 0;
            }
            else
            {
                MouseState = 1;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MouseState == 0)
            {
                CreatePoint();
            }
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
