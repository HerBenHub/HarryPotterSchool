using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using HarryPotter;

namespace HPGUI
{
    public partial class Form1 : Form
    {
        private List<Character> _characters = new List<Character>();

        public Form1()
        {
            InitializeComponent();

            lvCharacters.View = View.Details;

            lvCharacters.Columns.Add("Index", 50, HorizontalAlignment.Left);
            lvCharacters.Columns.Add("Full Name", 150, HorizontalAlignment.Left);
            lvCharacters.Columns.Add("Nickname", 100, HorizontalAlignment.Left);
            lvCharacters.Columns.Add("House", 100, HorizontalAlignment.Left);
            lvCharacters.Columns.Add("Birthdate", 100, HorizontalAlignment.Left);

            lvCharacters.FullRowSelect = true;
            lvCharacters.GridLines = true;

            pbCharacterImage.SizeMode = PictureBoxSizeMode.Zoom;

            lvCharacters.SelectedIndexChanged += lvCharacters_SelectedIndexChanged;
            btnRefresh.Click += btnRefresh_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            LoadCharactersToListView();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCharactersToListView();
        }
        private void LoadCharactersToListView()
        {
            try
            {
                var data = new Data();
                _characters = data.ReadCharacters();

                lvCharacters.Items.Clear();

                foreach (var c in _characters.OrderBy(x => x.Index))
                {
                    var item = new ListViewItem(c.Index.ToString());

                    item.SubItems.Add(c.FullName);
                    item.SubItems.Add(c.NickName);
                    item.SubItems.Add(c.HogwardsHouse);

                    item.SubItems.Add(c.Birthdate == DateTime.MinValue ? "" : c.Birthdate.ToShortDateString());

                    item.Tag = c;
                    lvCharacters.Items.Add(item);
                }

                ClearDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nem sikerült betölteni a karaktereket!\n" + ex.Message);
            }
        }

        private void ClearDetails()
        {
            pbCharacterImage.Image = Placeholder.MakePlaceholderImage();
            lbKnownSpells.Items.Clear();

            lbChildren.Items.Clear();
        }

        private async void lvCharacters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvCharacters.SelectedItems.Count == 0)
                return;

            var character = lvCharacters.SelectedItems[0].Tag as Character;
            if (character == null)
                return;


            await LoadCharacterDetailsAsync(character);
        }



        private async Task LoadCharacterDetailsAsync(Character character)
        {

            lbKnownSpells.Items.Clear();
            if (character.KnownSpells != null)
            {
                foreach (var s in character.KnownSpells)
                    lbKnownSpells.Items.Add(s.Name);
            }

            lbChildren.Items.Clear();

            if (character.Children != null)
            {
                foreach (var ch in character.Children)
                    lbChildren.Items.Add(ch.FullName);
            }

            pbCharacterImage.Image = Placeholder.MakePlaceholderImage();

            if (!string.IsNullOrWhiteSpace(character.Image))
            {
                var img = await DownloadImageSafeAsync(character.Image);
                if (img != null)
                    pbCharacterImage.Image = img;
            }
        }

        private Task<Image> DownloadImageSafeAsync(string url)
        {
            return Task.Run(() =>
            {
                try
                {

                    var request = (HttpWebRequest)WebRequest.Create(url);

                    request.Timeout = 5000;
                    request.ReadWriteTimeout = 5000;

                    using (var response = (HttpWebResponse)request.GetResponse())
                    using (var stream = response.GetResponseStream())
                    {
                        if (stream == null)
                        {
                            return (Image)null;
                        }

                        using (var ms = new MemoryStream())
                        {
                            stream.CopyTo(ms);
                            ms.Position = 0;

                            return Image.FromStream(ms);
                        }
                    }
                }
                catch
                {
                    return (Image)null;
                }
            });
        }

        private void btnLoadBooks_Click(object sender, EventArgs e)
        {
            BooksForm frm = new BooksForm();

            frm.ShowDialog();
            frm.Dispose();
        }
    }
}
