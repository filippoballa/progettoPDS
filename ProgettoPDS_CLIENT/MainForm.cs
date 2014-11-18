using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgettoPDS_CLIENT
{
    public partial class MainForm : Form
    {
        private User user;

        public MainForm( User aux)
        {
            InitializeComponent();
            this.user = aux;
            this.Text += " - " + aux.Name;
			MessageBox.Show("Hello!!");
        }
    }
}
