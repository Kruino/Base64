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

namespace Base64
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Base64 Decoder";
            button2.Text = "Decode";
            button3.Text = "Delete Text";
            label1.Text = "Base64:";
            button5.Text = "Go to file";
            label2.Text = "File Name:";
            label3.Text = "";
            label4.Text = "Type:";            
            richTextBox2.DragDrop += new DragEventHandler(richTextBox2_DragDrop);
            richTextBox2.AllowDrop = true;
            
        }

        private void richTextBox2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData("FileDrop") is string[] Txt)
            {
                foreach (var list in from string name in Txt
                                     let list = Txt as string[]
                                     where list != null && !string.IsNullOrWhiteSpace(list[0])
                                     select list)
                {
                    richTextBox2.Clear();
                    richTextBox2.LoadFile(list[0], RichTextBoxStreamType.PlainText);
                }
            }
        }

        
        private void button2_Click_1(object sender, EventArgs e)
        {

            string Base64BinaryStr = richTextBox2.Text;
            


            string FileName = textBox1.Text;
            
            string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // finder den nuværendes brugers desktop mappe
            string type = "Type";
            progressBar1.Maximum = 100;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            
            if (string.IsNullOrEmpty(richTextBox2.Text)) //Checker om base64 input feltet er tomt, og lær dig ikke komme vidre vis det er.
            {
                label3.Text = "Please input base64 to continue.";
                label3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
            }

            
            else if (string.IsNullOrEmpty(textBox1.Text)) //Checker om navnefeltet er tomt. og lær dig ikke komme vidre vis det er.
            {
                label2.Text = "Invalid Name:";
                label2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
            }

            
            else //Ellers forsætter den med at checke om bruger inputet har har en specefik signatur i den.
            {
                
                switch (Base64BinaryStr)
            {
                    //-----------------------------------------------------------------------//
                    
                    case string Pdf when Pdf.Contains("JVBERi0"): //Den checker om texten har JVBERi0 i den. Vis den har, er det et pdf document.
                        type = ".pdf"; //laver en ny value under string type.
                        label4.Text = "Decoded: Pdf"; //Skriver i label4 at den har decoded en pdf fil.
                    break;

                    //----------------------------------------------------------------// 
                    
                    case string Png when Png.Contains("iVBORw0KGgo"):
                        type = ".png";
                        label4.Text = "Type: png";
                    break;

                    //------------------------------------------------------------------//
                    
                    case string Gif when Gif.Contains("R0lGODdh")|| Gif.Contains("R0lGODlh"):
                        type = ".gif";
                        label4.Text = "Type: gif";
                    break;

                    //----------------------------------------------------------------//
                    
                    case string Mp3 when Mp3.Contains("SUQzBAAAAAAAI1"):
                        type = ".mp3";
                        label4.Text = "Type: mp3";
                    break;

                    //--------------------------------------------------------------//
                    
                    case string Jpg when Jpg.Contains("/9j/"):
                        type = ".jpg";
                        label4.Text = "Type: jpg";
                    break;

                    //-------------------------------------------------------------//
                    
                    case string Mkv when Mkv.Contains("GkXfo6NChoEB"):
                        type = ".mkv";
                        label4.Text = "Type: mkv";
                    break;

                    //----------------------------------------------------------//
     
                    case string Mp4 when Mp4.Contains("AAAAGGZ0"):
                        type = ".mp4";
                        label4.Text = "Type: mp4";
                    break;

                    //---------------------------------------------------------//                    

                    case string Docx when Docx.Contains("UEsDBBQA"):
                        type = ".docx";
                        label4.Text = "Type: Docx";
                    break;
                }
                
                string FileLocation = FilePath + @"\" + FileName + type; // string der får sat sammen hele file stigen. For at kunne checke den og bruge den længere nede.

                if (File.Exists(FileLocation)) // Cheker om filen excistere.  
                {
                    label2.Text = "Filename Exists:"; // Hvis den ikke excistere ændre den label 2 til "Filename Exists". 
                    label2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                }
                else if (type.Contains("Type"))
                {
                    label3.Text = "Base64 invalid or not supported.";
                    label3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                }
                else
                {
                     
                    byte[] Bytes = Convert.FromBase64String(Base64BinaryStr); // tær Base64BinaryStr og convertere den fra base64.
                    

                System.IO.FileStream Stream =
                new FileStream(FileLocation, FileMode.CreateNew); //Udskriver den som den type den fandt ud af tidligere.
                    System.IO.BinaryWriter Writer =
                        new BinaryWriter(Stream);
                    Writer.Write(Bytes, 0, Bytes.Length);
                    Writer.Close();

                    progressBar1.Value = 100;

                    //vis de har fået sat et navn på ændre den label2 tilbage til Name:
                    label2.Text = "Name:";
                    //Og skifter farven tilbage til sort.
                    label2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");

                    //Vis man er kommet igennem decoding skriver den Succesful i bunden. 
                    label3.Text = "Your decoding was succesful.";
                    label3.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                    
                }
            }
       }

        
        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = ""; //vis du clicker delete text i bunden. Sletter den alt text inden i text feltet.
            label3.Text = "";
            
        }  

        private void button5_Click(object sender, EventArgs e)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //finder din skrivebordsfolder.

            System.Diagnostics.Process.Start("explorer.exe",filePath); //sender dig ud til din desktop folder.
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}