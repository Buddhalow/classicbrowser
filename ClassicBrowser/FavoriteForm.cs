using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassicBrowser
{
    public partial class FavoriteForm : Form
    {
        public FavoriteForm()
        {
            InitializeComponent();
        }
        public Favorite Favorite
        {
            get
            {
                return new Favorite()
                {
                    Url = this.Url,
                    Name = this.Title
                };
            }
        }
        public string Title
        {
            get
            {
                return tbTitle.Text;
            }
            set
            {
                tbTitle.Text = value;
            }
        }
        public string Url
        {
            get
            {
                return tbURL.Text;
            }
            set
            {
                tbURL.Text = value;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FavoriteForm_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
