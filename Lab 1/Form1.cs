using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lab_1
{
    public partial class Form1 : Form
    {
        Bitmap InitialImage, TransformedImage;
        List<Color> conturP;
        private Image loadedImage;

        public int structureI {get;set;}
        public int structureJ {get;set;}
        public int Received { get; set; }

        public Form1()
        {
            InitializeComponent();
            
            saveFileDialog.Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            saveFileDialog.DefaultExt = "png";

            conturP = new List<Color>();
        }

        public void Dilation(Bitmap ImageToTransform, Bitmap B, int segnPixelI, int segnPixelJ, bool show = true)
        {
            TransformedImage = new Bitmap(ImageToTransform);
            int ns = B.Height;
            int ms = B.Width;
            int cfor = ImageToTransform.Width - ms;
            int c2for = ImageToTransform.Height - ns;
            for(int i = 0; i < cfor; i++)
            {
                for (int j = 0; j < c2for; j++)
                {
                    int maxim = 0;
                    for (int ii = 0; ii < ms; ii++)
                    {
                        for (int jj = 0; jj < ns; jj++)
                        {
                            int c = ImageToTransform.GetPixel(i + ii, j + jj).R;
                            int r = B.GetPixel(ii, jj).R;
                            if (r == 0 && maxim < c)
                            {
                                maxim = c;
                            }
                            
                        }
                    }
                   
                    TransformedImage.SetPixel(i + segnPixelI, j + segnPixelJ, Color.FromArgb(maxim, maxim, maxim));
                
                }
            }

            if (show)
            {
                transformedPictureBox.Image = TransformedImage;
                transformedPictureBox.Refresh();
            }
        }

        public void Erosion(Bitmap ImageToTransform, Bitmap B, int segnPixelI, int segnPixelJ, bool show = true)
        {
            TransformedImage = new Bitmap(ImageToTransform);
            int ns = B.Height;
            int ms = B.Width;
            int cfor = ImageToTransform.Width - ms;
            int c2for = ImageToTransform.Height - ns;
            for (int i = 0; i < cfor; i++)
            {
                for (int j = 0; j < c2for; j++)
                {
                    int minim = 255;
                    for (int ii = 0; ii < ms; ii++)
                    {
                        for (int jj = 0; jj < ns; jj++)
                        {
                            int c = ImageToTransform.GetPixel(i + ii, j + jj).R;
                            int r = B.GetPixel(ii, jj).R;
                            if (r == 0 && minim > c)
                            {
                                minim = c;
                            }

                        }
                    }

                    TransformedImage.SetPixel(i + segnPixelI, j + segnPixelJ, Color.FromArgb(minim, minim, minim));

                }
            }

            if (show)
            {
                transformedPictureBox.Image = TransformedImage;
                transformedPictureBox.Refresh();
            }
        }

        private void makeInitial()
        {
            InitialImage = TransformedImage;
            initialPictureBox.Image = InitialImage;
            initialPictureBox.Refresh();
            transformedPictureBox.Image = null;
            transformedPictureBox.Refresh();
        }

        private void openImage()
        {
            openFileDialog.ShowDialog();
            loadedImage = Image.FromFile(openFileDialog.FileName);
            InitialImage = new Bitmap(loadedImage);
            initialPictureBox.Image = InitialImage;
            initialPictureBox.Refresh();
        }
        
        private void saveImage()
        {
            saveFileDialog.ShowDialog();
            TransformedImage.Save(saveFileDialog.FileName);
        }

        private void buttonOpenImage_Click(object sender, EventArgs e)
        {
            openImage();
        }

        private void buttonDeterminareContur_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            Image loadedB = Image.FromFile(openFileDialog.FileName);
            Bitmap B = new Bitmap(loadedB);

            Received = -1;
            using (Form2 form = new Form2(this, B.Width - 1, B.Height - 1))
            {
                form.ShowDialog();
                while (Received == -1)
                {
                    int i = 1;
                }
                
            }
            Dilation(InitialImage, B, structureI, structureJ);
        }

        private void buttonDeterminareSchelet_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            Image loadedB = Image.FromFile(openFileDialog.FileName);
            Bitmap B = new Bitmap(loadedB);

            Received = -1;
            using (Form2 form = new Form2(this, B.Width - 1, B.Height - 1))
            {
                form.ShowDialog();
                while (Received == -1)
                {
                    int i = 1;
                }

            }
            Erosion(InitialImage, B, structureI, structureJ);
        }

        private void buttonSaveImage_Click(object sender, EventArgs e)
        {
            saveImage();
        }

        private void buttonSubtiere_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            Image loadedB = Image.FromFile(openFileDialog.FileName);
            Bitmap B = new Bitmap(loadedB);

            Received = -1;
            using (Form2 form = new Form2(this, B.Width - 1, B.Height - 1))
            {
                form.ShowDialog();
                while (Received == -1)
                {
                    int i = 1;
                }

            }
            //aplic eroziune, iar pe rezultat aplic dilatare
            Erosion(InitialImage, B, structureI, structureJ, false);
            Dilation(TransformedImage, B, structureI, structureJ);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            Image loadedB = Image.FromFile(openFileDialog.FileName);
            Bitmap B = new Bitmap(loadedB);

            Received = -1;
            using (Form2 form = new Form2(this, B.Width - 1, B.Height - 1))
            {
                form.ShowDialog();
                while (Received == -1)
                {
                    int i = 1;
                }

            }
            //aplic dilatare, iar pe rezultat aplic eroziune
            Dilation(InitialImage, B, structureI, structureJ, false);
            Erosion(TransformedImage, B, structureI, structureJ);
        }

        private void buttonMakeInitial_Click(object sender, EventArgs e)
        {
            makeInitial();
        }
        
    }
}
