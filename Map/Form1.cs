using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Threading;
using Map.Model;

namespace Map
{
    public partial class Form1 : Form
    {
        Model.Model Mod;
        System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);


        List<Bitmap> Maps = new List<Bitmap>();
        Graphics Grapg;

        Bitmap Map;
        Bitmap bit;
        int MouseX;
        int MouseY;
        bool MoveMap = false;
        int X=0;
        int Y=0;
        int XOld = 0;
        int YOld = 0;

        public Form1()
        {
            InitializeComponent();

            SetPathLibrary.Model model = new SetPathLibrary.Model();
            Maps = model.GetMap("", "");
            List<string> rooms=model.GetRoom();
            rooms.Sort();
            comboBox1.Items.AddRange(rooms.ToArray());
            comboBox2.Items.AddRange(rooms.ToArray());
           

            int[,] ar = new int[18, 18];

            Mod = new Model.Model(ar);
            Grapg = pictureBox1.CreateGraphics();
            timer1.Start();
            XmlRead();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreatePath();
        }

        private void CreatePoint()
        {
            CreatePointForm form = new CreatePointForm();
            form.X = MouseX+Math.Abs(X);
            form.Y = MouseY+Math.Abs(Y);
            form.Id = Mod.AllNodes.Count;
            form.Floor = trackBar2.Value;
            form.SetCoordinates();
            form.ShowDialog();
            if (form.NewNode != null)
            {
                Mod.AllNodes.Add(form.NewNode);
            }
            RedrawPicture();
        }

        private void CreateLine(int i,int j)
        {
            if (i < Mod.AllNodes.Count && j < Mod.AllNodes.Count)
            {
                Mod.AllNodes[i].AddNode(Mod.AllNodes[j]);
                Mod.AllNodes[j].AddNode(Mod.AllNodes[i]);
            }
        }

        private void CreatePath()
        {
            Maps.Clear();
            SetPathLibrary.Model model = new SetPathLibrary.Model();
            Maps = null;
            Maps = model.GetMap(comboBox1.Text, comboBox2.Text);
            timer1.Start();
           
        }

        private void RedrawPicture()
        {
            
            Map = Maps[trackBar2.Value];
            bit = null;

            Size size = new Size(Map.Size.Width / (trackBar1.Value/2), Map.Size.Height / (trackBar1.Value/2));
           
            bit = new Bitmap(Map, size);
            Map=null;
            
            Grapg.DrawImage(bit, X, Y);

           
        }

        private void Scaling()
        {
            Size size = new Size(Map.Size.Width / trackBar1.Value, Map.Size.Height / trackBar1.Value);
            bit = new Bitmap(bit, size);
            //pictureBox1.Image=bit;
            Graphics g= pictureBox1.CreateGraphics();
            g.DrawImage(bit, X, Y);        
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            CreatePoint();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseX = e.X;
            MouseY = e.Y;
            label1.Text = "" + e.X + "   " + e.Y;
            if (MoveMap)
            {
                X += ((e.X -XOld));
                Y += ((e.Y -YOld));
                XOld = e.X;
                YOld = e.Y;
                if (X > 0)
                {
                    X = 0;
                }
                if (Y > 0)
                {
                    Y = 0;
                }
                if (X < -1 *(bit.Size.Width- pictureBox1.Size.Width))
                {
                    X = -1 * (bit.Size.Width - pictureBox1.Size.Width);
                }
                if (Y < -1 * (bit.Size.Height - pictureBox1.Size.Height))
                {
                    Y = -1 * (bit.Size.Height - pictureBox1.Size.Height);
                }
                timer1.Start();
            
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmlPrint();
            RedrawPicture();

            //if (MouseState == 0)
            //{
            //    MouseState = 1;
            //    button2.Text = "" + MouseState;
            //    return;
            //}
            //if (MouseState == 1)
            //{
            //    MouseState = 2;
            //    button2.Text = "" + MouseState;
            //    return;
            //}
            //if (MouseState == 2)
            //{
            //    button2.Text = "" + MouseState;
            //    MouseState = 0;
            //    return;
            //}
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
                ////XmlPrint();
                //XmlRead();
                //RedrawPicture();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MoveMap = true;
            XOld = e.X;
            YOld = e.Y;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            MoveMap = false;
            if (X > 0)
            {
                X = 0;
            }
            if (Y > 0)
            {
                Y = 0;
            }
        }

        private void SetFileAtribute(XmlDocument xd, XmlNode xn, Node node)
        {
            XmlAttribute attribute = xd.CreateAttribute("Id"); // создаём атрибут
            attribute.Value = "" + node.Id; // устанавливаем значение атрибута
            xn.Attributes.Append(attribute);

            attribute = xd.CreateAttribute("Name"); // создаём атрибут
            attribute.Value = node.Name; // устанавливаем значение атрибута
            xn.Attributes.Append(attribute);

            attribute = xd.CreateAttribute("Floor"); // создаём атрибут
            attribute.Value = ""+node.Floor; // устанавливаем значение атрибута
            xn.Attributes.Append(attribute);

            attribute = xd.CreateAttribute("X"); // создаём атрибут
            attribute.Value = "" + node.X; // устанавливаем значение атрибута
            xn.Attributes.Append(attribute);

            attribute = xd.CreateAttribute("Y"); // создаём атрибут
            attribute.Value = "" + node.Y; // устанавливаем значение атрибута
            xn.Attributes.Append(attribute);
            
        }

        private XmlDocument CreateXmlFile(string fullName)
        {
            XmlTextWriter textWritter = new XmlTextWriter(fullName, Encoding.UTF8);
            textWritter.WriteStartDocument();
            textWritter.WriteStartElement("head");
            textWritter.WriteEndElement();
            textWritter.Close();

            return null;
        }

        public void XmlPrint()
        {
            XmlDocument XmlDocument = new XmlDocument();
            XmlNode xmlNodeFile;
            XmlNode xmlNode;

            string path = "11.xml";
            CreateXmlFile(path);
            XmlDocument.Load(path);
            System.IO.FileStream file = System.IO.File.Create(path);

            foreach (Node n in Mod.AllNodes)
            {
                xmlNodeFile = XmlDocument.CreateElement("Point");
                SetFileAtribute(XmlDocument, xmlNodeFile, n);
                foreach (Node m in n.Nodes)
                {
                    xmlNode = XmlDocument.CreateElement("P");
                    XmlAttribute attribute = XmlDocument.CreateAttribute("Id"); // создаём атрибут
                    attribute.Value = ""+m.Id; // устанавливаем значение атрибута
                    xmlNode.Attributes.Append(attribute);
                    xmlNodeFile.AppendChild(xmlNode);
                }
                XmlDocument.DocumentElement.AppendChild(xmlNodeFile);
            }

            XmlDocument.Save(file);
            file.Close();
        }

        private Node FindNod(int id)
        {
            foreach (Node n in Mod.AllNodes)
            {
                if (n.Id == id)
                {
                    return n;
                }
            }
            return null;
        }

        public void XmlRead()
        {
            XmlDocument XmlDocument = new XmlDocument();

            string path = "11.xml";
           
            XmlDocument.Load(path);
            Mod.AllNodes.Clear();

            foreach (XmlNode xn in XmlDocument.DocumentElement.ChildNodes)
            {


                Mod.AllNodes.Add(new Node(Convert.ToInt32(xn.Attributes.GetNamedItem("Id").Value), xn.Attributes.GetNamedItem("Name").Value, Convert.ToInt32(xn.Attributes.GetNamedItem("Floor").Value),
                    Convert.ToInt32(xn.Attributes.GetNamedItem("X").Value), Convert.ToInt32(xn.Attributes.GetNamedItem("Y").Value)));
            }

            Node node;
            foreach (XmlNode xn in XmlDocument.DocumentElement.ChildNodes)
            {
                node = FindNod(Convert.ToInt32(xn.Attributes.GetNamedItem("Id").Value));
                foreach (XmlNode m in xn.ChildNodes)
                {
                    node.Nodes.Add(FindNod(Convert.ToInt32(m.Attributes.GetNamedItem("Id").Value)));
                }
            }

            int[,] ar = new int[Mod.AllNodes.Count, Mod.AllNodes.Count];

            foreach (Node n in Mod.AllNodes)
            {
                foreach (Node m in n.Nodes)
                {
                    ar[n.Id, m.Id] = Convert.ToInt32(Math.Sqrt((n.X - m.X)*(n.X - m.X) + (n.Y - m.Y) * (n.Y - m.Y)));
                }
            }

            Mod.Array = ar;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            RedrawPicture();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            
            timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateLine(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            //panel2.Location = new Point(pictureBox1.Size.Width - panel2.Size.Width, pictureBox1.Size.Height/2);
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            //panel2.Location = new Point(pictureBox1.Size.Width - 30, pictureBox1.Size.Height/2);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //pictureBox1.Dock = DockStyle.Fill;
            Grapg = pictureBox1.CreateGraphics();
            timer1.Start();
        }
    }
}
