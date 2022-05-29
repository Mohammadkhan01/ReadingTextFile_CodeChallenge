using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ReadingTextFiles
{
    public partial class FileContent : Form
    {
        private string filePath;
        private string fileContent;

        public FileContent()
        {
            InitializeComponent();
            textBox1.Multiline = true;
            textBox1.ScrollBars = ScrollBars.Both;
            //Setup events that listens on keypress
            textBox1.KeyDown += TextBox1_KeyDown;
        }


        // Handle the KeyDown event to print the type of character entered into the control.
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                LoadResult(textBox1.Text);                
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();
                    using (StreamReader reader = new StreamReader(fileStream))                    
                        fileContent = reader.ReadToEnd();                    
                }
            }
            MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
            textBox1.Text = fileContent;
            if(textBox1.Text != "")
            LoadResult(fileContent);

        }

        
        public int CountWords(string test)
        {
            var noOfWords = Regex.Replace(test, "[^a-zA-Z_]+", " ")
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Length;
            return noOfWords;
        }

        public int countLetters(string test)
        {
            int totalLetterChar = 0;
            char[] strArray = test.ToCharArray();
            foreach (var item in strArray)
            {
                if (char.IsLetter(item))
                    totalLetterChar++;
            }
            return totalLetterChar;
        }

        public void LoadResult(string fileContent)
        {
            lblWords.Text = (CountWords(fileContent)).ToString();
            lblLetters.Text = (countLetters(fileContent)).ToString();
            var x = Convert.ToInt32(lblWords.Text.Trim());
            var y = Convert.ToInt32(lblLetters.Text.Trim());
            var z = y / x;
            lblAvg.Text = z.ToString();
        }

        private void FileContent_Load(object sender, EventArgs e)
        {
            
            string a1 = Environment.UserName;
                label1.Text = a1.ToString();
            string a2 = (Environment.Is64BitOperatingSystem) ? "64 bit Operating System" : "32 bit Operating System";
            label3.Text = a2.ToString();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            lblWords.Text = "0";
            lblLetters.Text = "0";
            lblAvg.Text = "0";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        
    }
}
