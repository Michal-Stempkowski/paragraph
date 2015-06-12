using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer.Top;

namespace GUI
{
    public partial class Entity : Form
    {
        private readonly EntityMenu _entityMenu;
        private readonly Main _mainMenuForm;

        public Entity(EntityMenu entityMenu, Main mainMenuForm)
        {
            _entityMenu = entityMenu;
            _mainMenuForm = mainMenuForm;
            InitializeComponent();
        }

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
            _mainMenuForm.Show();
        }
    }
}
