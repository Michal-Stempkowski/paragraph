using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Entity : Form
    {
        public Entity()
        {
            InitializeComponent();
        }

        public Form MainMenuForm { get; set; }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Entity_Load(object sender, EventArgs e)
        {

        }

        private void _onClosing(object sender, FormClosingEventArgs e)
        {
            MainMenuForm.Show();
        }
    }
}
