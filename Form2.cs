using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Playr
{
    public partial class Form2 : Form
    {

        String imagePath;
        int currentImage;
        int totalImages;
        Form1 form1;
        Bitmap currentImageBitmap;
        System.Linq.IOrderedEnumerable<System.IO.FileInfo> files;

        public Form2(Form1 mainForm)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            form1 = mainForm;
        }

        public void clearPreview()
        {
            pictureBox1.Image = null;
        }

        public void setImagePath(String newImagePath)
        {
            currentImage = 0;
            imagePath = newImagePath;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(imagePath);
            totalImages = dir.GetFiles().Length;
            form1.setNumberOfLayers(totalImages);
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.BackgroundImageLayout = ImageLayout.Center;
        }

        public String filePathAtIndex(int index)
        {
            String indexString = "";
            String zeros = "";
            if (index < 10) zeros = "000";
            else if (index < 100) zeros = "00";
            else if (index < 1000) zeros = "0";

            indexString = zeros + Convert.ToString(index);
            return imagePath + "\\out" + indexString + ".png";
        }

        public void nextFrame()
        {
            pictureBox1.Image = Image.FromFile(filePathAtIndex(currentImage));
            currentImage++;
            Console.Write("next called");
        }

        public void previousFrame()
        {
            currentImage--;
            Bitmap image = (Bitmap)Image.FromFile(filePathAtIndex(currentImage));
            pictureBox1.Image = image;
        }

    }
}
