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

namespace CodeCheck
{
    public partial class Form1 : Form
    {
        List<char> symbols = new List<char>() { ' ', '+', '/', '*', '-', '{', '}', '\\', '\n', '\t', '\r', '(', ')', '\"', '\'', '=', ';' };
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            const int number = 566;
            const float number1 = 255.55f;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
                richTextBox1.Text = File.ReadAllText(textBox1.Text);
                string clearCode = richTextBox1.Text.Replace("\\\"","");
                while (clearCode.Contains("\"") && clearCode.IndexOf("\"")-1!='\'')
                {
                    int index = clearCode.IndexOf("\"");
                    clearCode = clearCode.Remove(index, 1);
                    clearCode = clearCode.Remove(index, (clearCode.IndexOf('\"') + 1 - index)<0?0: clearCode.IndexOf('\"') + 1 - index);
                }
                while (clearCode.Contains("/*"))
                {
                    clearCode = clearCode.Remove(clearCode.IndexOf("/*"), clearCode.IndexOf("*/") + 2 - clearCode.IndexOf("/*"));
                }
                while (clearCode.Contains("//"))
                {
                        clearCode = clearCode.Remove(clearCode.IndexOf("//"), 
                            clearCode.Substring(clearCode.IndexOf("//")).IndexOf('\n'));
                }
                string[] lines = clearCode.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("const"))
                    {
                        dataGridView1.Rows.Add(i.ToString());
                    }
                }

                string[] keywords = clearCode.Split(symbols.ToArray());
                int a = 0;
                for(int i=0; i < keywords.Length; i++)
                {
                    if (keywords[i] == "const")
                    {
                        i++;
                        while (keywords[i].Length == 0)
                        {
                            i++;
                        }
                        dataGridView1.Rows[a].Cells[1].Value= keywords[i];

                        i++;
                        while (keywords[i].Length == 0)
                        {
                            i++;
                        }
                        dataGridView1.Rows[a].Cells[2].Value = keywords[i];

                        i++;
                        while (keywords[i].Length == 0)
                        {
                            i++;
                        }
                        dataGridView1.Rows[a].Cells[3].Value = keywords[i];
                        a++;
                    }
                }
            }
        }
    }
}
