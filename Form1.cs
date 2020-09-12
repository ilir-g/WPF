using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace IlirGashi_Wpf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Delete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1044, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(2, 73);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 40;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1184, 475);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.DataSource = null;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 25);
            this.textBox1.Margin = new System.Windows.Forms.Padding(5);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(663, 38);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Delete
            // 
            this.Delete.BackColor = System.Drawing.Color.DarkRed;
            this.Delete.Location = new System.Drawing.Point(763, 25);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(148, 38);
            this.Delete.TabIndex = 3;
            this.Delete.Text = "Delete";
            this.Delete.UseVisualStyleBackColor = false;
            this.Delete.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1271, 560);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
            BindData(textBox1.Text);
        }

        public void RefreshGrid()
        {
            if (dataGridView1.Rows != null)
            {                             
                dataGridView1.Rows.Clear();
            }
        }
        List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();
       
        private void BindData(string filePath)
        {
            Dictionary<int, double> valuesPrice = new Dictionary<int, double>();
            RefreshGrid();
       
            string[] lines = System.IO.File.ReadAllLines(filePath);
            if (lines.Length > 0)
            {
                //first line to create header
                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(';');
                var c = 0;
                foreach (string headerWord in headerLabels)
                {

                    if (headerWord.ToLower() == "binding")
                    {
                        DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
                        combo.Name = headerWord;
                        combo.HeaderText = headerWord;
                        combo.DataPropertyName = headerWord;
                        combo.Width = 100;

                        dataGridView1.Columns.Insert(c, combo);
                    }
                    else if (headerWord.ToLower() == "description")
                    {
                        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                        btn.Name = headerWord;
                        btn.HeaderText = headerWord;
                        btn.DataPropertyName = headerWord;
                        btn.Width = 100;
                        btn.UseColumnTextForButtonValue = true;
                      
                        dataGridView1.Columns.Insert(c, btn);
                    }
                    else if (headerWord.ToLower() == "in stock")
                    {                       
                        DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                        chk.Name = headerWord;
                        chk.HeaderText = headerWord;
                        chk.DataPropertyName = headerWord;
                        chk.Width = 100;
                        dataGridView1.Columns.Insert(c, chk);
                    }
                    else
                    {
                        DataGridViewTextBoxColumn text = new DataGridViewTextBoxColumn();
                        text.Name = headerWord;
                        text.HeaderText = headerWord;
                        text.DataPropertyName = headerWord;
                        text.Width = 100;
                        dataGridView1.Columns.Insert(c, text);
                    }
                                    
                    c++;
                }
                //For Data
               
                for (int i = 1,j=0; i < lines.Length; i++,j++)
                {
                    string[] dataWords = lines[i].Split(';');
                   
                    DataGridViewRow dgr = new DataGridViewRow();
                    int columnIndex = 0;
                    int rowIndex = 0;
                    
                    this.dataGridView1.Rows.Add();
                    
                    foreach (string headerWord in headerLabels)
                    {
                        var word = dataWords[columnIndex++];
                        if (headerWord.ToLower() == "description")
                        {
                            DataGridViewButtonCell btn = new DataGridViewButtonCell();
                            //btn.Value = word;
                            btn.ToolTipText = word;
                            btn.UseColumnTextForButtonValue = true;
                            this.dataGridView1.Rows[j].Cells[rowIndex] = btn;
                        }
                        if (headerWord.ToLower() == "binding")
                        { 
                            DataGridViewComboBoxCell combo = new DataGridViewComboBoxCell();                                                      
                            combo.Items.Add(word);
                            combo.Value = word;
                            this.dataGridView1.Rows[j].Cells[rowIndex] = combo;
                        }
                        else if (headerWord.ToLower() == "in stock")
                        {
                            DataGridViewCheckBoxCell check = new DataGridViewCheckBoxCell();
                           
                            if (word == "yes") { check.Value = true; }
                            this.dataGridView1.Rows[j].Cells[rowIndex] = check;
                            if (word == "no")
                            {
                                this.dataGridView1.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                                rowsToRemove.Add(this.dataGridView1.Rows[j]);
                            }
                        }
                        else
                        {
                            this.dataGridView1.Rows[j].Cells[rowIndex].Value = word;
                            if (headerWord.ToLower() == "price")
                            {
                                string value = word.Replace(",", ".");
                                valuesPrice.Add(i, double.Parse(value));
                            }
                        }
                        rowIndex++;
                        
                    }
                   
                }
            }



            var columnList = dataGridView1.Columns.Cast<DataGridViewColumn>().ToList();
            int indexPrice = columnList.FindIndex(c => c.HeaderText == "Price");
            
            var sortedvaluesPrice = (from entry in valuesPrice orderby entry.Value ascending select entry);
            int val = 0;
            foreach (var item in sortedvaluesPrice)
            {
               
                dataGridView1.Rows[item.Key].Cells[indexPrice].Style.ForeColor = Color.FromArgb(255,val, val, val);
                val += 30;
            }
            dataGridView1.CellClick += dataGridView1_CellClick;
           
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in rowsToRemove)
            {              
                dataGridView1.Rows.Remove(row);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                int rowIndex = e.RowIndex;
                int colIndex = e.ColumnIndex;
                var val=dataGridView1.Rows[rowIndex].Cells[colIndex];
                MessageBox.Show(val.ToolTipText.ToString());
            }
        }
    }
}
