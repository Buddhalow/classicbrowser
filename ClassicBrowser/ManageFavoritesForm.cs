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
    public partial class ManageFavoritesForm : Form
    {
        public ManageFavoritesForm()
        {
            InitializeComponent();
            Render();
        }
        private FavoriteManager favoriteManager;
        public void Render()
        {
            listView1.Items.Clear();
            foreach (Favorite favorite in FavoriteManager.FavoritesManager.Favorites)
            {
                ListViewItem item = listView1.Items.Add(favorite.Name);
                item.SubItems.Add(favorite.Url);
                item.Tag = favorite;
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
        private void ManageFavoritesForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FavoriteForm favoriteForm = new FavoriteForm();
            if (favoriteForm.ShowDialog() == DialogResult.OK)
            {
                Favorite favorite = favoriteForm.Favorite;
                FavoriteManager.FavoritesManager.Favorites.Add(favorite);
                FavoriteManager.Save();
                Render();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete these favorites?", "Delete favorites", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    Favorite favorite = (Favorite)item.Tag;
                    FavoriteManager.FavoritesManager.Favorites.Remove(favorite);
                    FavoriteManager.Save();
                    Render();
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
