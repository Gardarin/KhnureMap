using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Map
{
    public partial class CreatePointForm : Form
    {
        public Model.Node NewNode { get; set; }
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Floor { get; set; }

        public CreatePointForm()
        {
            InitializeComponent();
            
        }

        public void SetCoordinates()
        {
            label1.Text = "" + X + "   " + Y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewNode = new Model.Node(Id,textBox1.Text,Floor, X, Y);
            this.Close();
        }
    }
}
