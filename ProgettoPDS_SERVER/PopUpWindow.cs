using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ProgettoPDS_SERVER
{
    /// <summary>
    /// A simple popup window that can host any System.Windows.Forms.Control
    /// </summary>
    public class PopUpWindow : System.Windows.Forms.ToolStripDropDown
    {
        private System.Windows.Forms.Control _content;
        private System.Windows.Forms.ToolStripControlHost _host;
        public const int border = 15;

        public PopUpWindow(System.Windows.Forms.Control content, User u)
        {
            int width = 0;
            int height = 0;
            Point location = new Point(0, 0);
            Color color = Color.Red;

            width = Screen.PrimaryScreen.WorkingArea.Width/2-width/2;
            height = Screen.PrimaryScreen.WorkingArea.Height/2-height/2;

            //Basic setup...
            this.AutoSize = false;
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            this._content = content;
            this._host = new System.Windows.Forms.ToolStripControlHost(content);

            //Positioning and Sizing
            this.MinimumSize = content.MinimumSize;
            //this.MaximumSize = content.Size;
            this.Height = height;
            this.Width = width; ;
            this.BackColor = color;
            content.Location = location;

            //Add the host to the list
            this.Items.Add(this._host);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
}
