using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4KNativeEditor
{
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
        }
        
         string LittleEndian(string num)
        {
            int number = Convert.ToInt32(num, 16);
            byte[] bytes = BitConverter.GetBytes(number);
            string retval = "";
            foreach (byte b in bytes)
            { retval += b.ToString("X2"); }

            retval = retval.Substring(0, 4);
            return retval;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            int Height = Int32.Parse(txtHeight.Text);
            int Width = Int32.Parse(txtWidth.Text);
            // MessageBox.Show("Resolution: " + screenWidth + "x" + screenHeight);
            string WidthHex = LittleEndian(Width.ToString("X"));
            string HeightHex = LittleEndian(Height.ToString("X"));
            //MessageBox.Show("Resolution: " +(WidthHex) + "x" + (HeightHex));


            int WidthpartOne = int.Parse(WidthHex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
          
           int WidthpartTwo = int.Parse(WidthHex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);

            int HeightpartOne = int.Parse(HeightHex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);

            int HeightpartTwo = int.Parse(HeightHex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
           
            using (FileStream sr = File.OpenWrite(@"MarySkelter2.exe"))
            {
                sr.Seek(0x70447C, SeekOrigin.Begin);
                sr.WriteByte((byte)WidthpartOne);
                sr.Seek(0x70447D, SeekOrigin.Begin);
                sr.WriteByte((byte)WidthpartTwo);
               




                sr.Seek(0x704480, SeekOrigin.Begin);
                sr.WriteByte((byte)HeightpartOne);
                sr.Seek(0x704481, SeekOrigin.Begin);
                sr.WriteByte((byte)HeightpartTwo);
               

                sr.Close();

                MessageBox.Show("¡¡New Internal Resolution, applied!!");

            }


        }
    
        private void Form1_Load(object sender, EventArgs e)
        {
            string screenWidth = Screen.PrimaryScreen.Bounds.Width.ToString();
            txtWidth.Text = screenWidth;
            string screenHeight = Screen.PrimaryScreen.Bounds.Height.ToString();
            txtHeight.Text = screenHeight;

            string curFile = @"MarySkelter2.exe";
          
            if (File.Exists(curFile))
            {
            }
            else
            { MessageBox.Show("Please put this application in the game executable directory.");
                System.Environment.Exit(1);
            }


        }
    }
}
