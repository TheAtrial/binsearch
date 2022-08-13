using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        System.IO.FileInfo fi;
        OpenFileDialog Fd = new OpenFileDialog();

        public Form1()
        {
            InitializeComponent();

            Fd.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            //saveOpenFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }

        static int BinarySearch(string[] array, string searchedValue, int first, int last)
        {
            //границы сошлись
            if (first > last)
            {
                //элемент не найден
                return -1;
            }

            //средний индекс подмассива
            int middle = (first + last) / 2;
            //значение в средине подмассива
            string middleValue = array[middle];
            int q = String.Compare(middleValue, searchedValue);
            if (q == 0)
            {
                return middle;
            }
            else
            {
                if (q > 0)
                {
                    //рекурсивный вызов поиска для левого подмассива
                    return BinarySearch(array, searchedValue, first, middle - 1);
                }
                else
                {
                    //рекурсивный вызов поиска для правого подмассива
                    return BinarySearch(array, searchedValue, middle + 1, last);
                }
            }
        }

        void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<string> text1 = Mas();

            string []text = text1.ToArray();
            Array.Sort(text);

            if (text.Length == 0)
            {
                MessageBox.Show("Файл пуст");
            }
            else
            {
                for (int i = 0; i < text.Length; i++)
                {
                    listBox1.Items.Add(text[i]);
                }
                MessageBox.Show("Файл открыт");
            }


        }

        public List<string> Mas()
        {
            List<string> text = new List<string>();
            if (Fd.ShowDialog() == DialogResult.Cancel)
            {
                return null;
            }
            // получаем выбранный файл
            string filename = Fd.FileName;
            // читаем файл в строку
            string[] fileText = System.IO.File.ReadAllLines(filename, Encoding.GetEncoding(1251));
            for (int i = 0; i <fileText.Length; i++)
            {
                text.Add(fileText[i]); 
            }
            return text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] text;
            text = new string[listBox1.Items.Count];
            
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                text[i] = listBox1.Items[i].ToString();
            }

            while (true)
            {
                
                var k = Convert.ToString(textBox2.Text);

                if (textBox2.Text == String.Empty)
                {
                    MessageBox.Show("Ошибка! Вы не ввели элемент для поиска");
                    break;
                }

                var searchResult = BinarySearch(text, k, 0, text.Length - 1);
                if (searchResult < 0)
                {
                    MessageBox.Show("Элемент не найден!");
                    break;
                }
                else
                {
                    MessageBox.Show("Элемент найден. Индекс элемента со значением " + searchResult + " равен " + k);
                    break;
                }
            }
        }
    }
}
