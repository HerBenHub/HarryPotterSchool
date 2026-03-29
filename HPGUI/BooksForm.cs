using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace HPGUI
{
    public partial class BooksForm : Form
    {
        private BindingList<BookDto> _books = new BindingList<BookDto>();

        private const string BooksUrl = "https://potterapi-fedeperin.vercel.app/en/books";

        public BooksForm()
        {
            InitializeComponent();

            dgvBooks.Columns.Clear();

            dgvBooks.AutoGenerateColumns = false;

            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colOriginalTitle",
                HeaderText = "Eredeti cím",
                DataPropertyName = "OriginalTitle"
            });

            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPages",
                HeaderText = "Oldalak száma",
                DataPropertyName = "Pages"
            });

            dgvBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colReleaseDate",
                HeaderText = "Kiadás dátuma",
                DataPropertyName = "ReleaseDate"
            });

            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.MultiSelect = false;
            dgvBooks.ReadOnly = true;

            dgvBooks.DataSource = _books;

            btnDownloadBooks.Click += btnDownloadBooks_Click;
            dgvBooks.SelectionChanged += dgvBooks_SelectionChanged;
        }

        private async void btnDownloadBooks_Click(object sender, EventArgs e)
        {
            btnDownloadBooks.Enabled = false;
            btnDownloadBooks.Text = "Letöltés...";

            try
            {
                var downloaded = await DownloadBooksAsync();

                _books.RaiseListChangedEvents = false;
                _books.Clear();
                foreach (var b in downloaded)
                    _books.Add(b);
                _books.RaiseListChangedEvents = true;
                _books.ResetBindings();

                lbDescription.Text = "";
                pbCover.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nem sikerült letölteni a könyveket!\n" + ex.Message);
            }
            finally
            {
                btnDownloadBooks.Enabled = true;
                btnDownloadBooks.Text = "Letöltés";
            }
        }

        private async Task<List<BookDto>> DownloadBooksAsync()
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(10);

                var json = await client.GetStringAsync(BooksUrl);

                var serializer = new JavaScriptSerializer();
                var list = serializer.Deserialize<List<BookDto>>(json);

                return list ?? new List<BookDto>();
            }
        }

        private async void dgvBooks_SelectionChanged(object sender, EventArgs e)
        {
            var book = dgvBooks.CurrentRow?.DataBoundItem as BookDto;
            if (book == null)
                return;

            lbDescription.Text = book.Description ?? "";

            pbCover.Image = Placeholder.MakePlaceholderImage();

            if (!string.IsNullOrWhiteSpace(book.Cover))
            {
                var img = await DownloadImageSafeAsync(book.Cover);
                if (img != null)
                    pbCover.Image = img;
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
                            return (Image)null;

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

        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
