using Grasshopper;
using Grasshopper.Kernel.Data;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GH_DataView_Component
{
    public partial class DataForm1 : Form
    {
        private void DataGridView2Excel_cvs(DataGridView dgv)
        {
            CancelSelect();
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Excel files (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "DataForm_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm");

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Stream myStream;
                myStream = dlg.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.Default);
                try
                {
                    /*
                    //写入列标题
                    string columnTitle = "";
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        if (i > 0)
                        {
                            columnTitle += "\t";
                        }
                        columnTitle += dgv.Columns[i].HeaderText;
                    }
                    sw.WriteLine(columnTitle);
                    */
                    //写入内容
                    for (int j = 0; j < dgv.Rows.Count; j++)
                    {
                        string columnValue = "";
                        for (int k = 0; k < dgv.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                columnValue += "\t";
                            }
                            if (dgv.Rows[j].Cells[k].Value == null)
                            {
                                columnValue += "";
                            }
                            else
                            {
                                columnValue += dgv.Rows[j].Cells[k].Value.ToString().Trim();
                            }
                        }
                        sw.WriteLine(columnValue);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
            }
        }
        DataView parent;
        public DataForm1(DataView p)
        {
            this.KeyDown += new KeyEventHandler(this.GH_NotesEditor_KeyDown);
            InitializeComponent();
            parent = p;
            this.dataGridView1.RowCount = parent.table_Height;
            this.dataGridView1.ColumnCount = parent.table_Width;
            for (int r = 0; r < dataGridView1.RowCount; r++)
            {
                dataGridView1.Rows[r].HeaderCell.Value = "{" + r.ToString() + "}";
            }
            for (int c = 0; c < dataGridView1.ColumnCount; c++)
            {
                dataGridView1.Columns[c].HeaderText = c.ToString();
            }
            if (parent.ShowGridHead)
            {
                this.dataGridView1.RowHeadersVisible = true;
                this.dataGridView1.ColumnHeadersVisible = true;
            }
            else
            {
                this.dataGridView1.RowHeadersVisible = false;
                this.dataGridView1.ColumnHeadersVisible = false;
            }

            dataGridView1.RowTemplate.Height = (int)parent.CellHeight;
            //this.FormBorderStyle = FormBorderStyle.None;         
            RectangleF rf = parent.Attributes.Bounds;
            int Height_1 = (int)(parent.CellHeight * dataGridView1.RowCount + this.dataGridView1.Location.Y + 35);
            if (Height_1 > 800) Height_1 = 800;
            this.Size = new Size((int)rf.Width + 35, Height_1);

           dataGridView1.MouseClick += new MouseEventHandler(dataGridView1_MouseClick);
            this.menuStrip1.MouseClick += new MouseEventHandler(dataGridView1_MouseClick);
            this.Select();
        }
        public void FillDataTable()
        {
            CancelSelect();
            for (int r = 0; r < dataGridView1.RowCount; r++)
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    string CellData = "";
                    if (dataGridView1.Rows[r].Cells[i].Value != null)
                    {
                        CellData = dataGridView1.Rows[r].Cells[i].Value.ToString();
                    }
                    parent.table0[i, r] = CellData;
                }
            }
            parent.ExpireSolution(true);
        }
        public void ReceiveDataTable()
        {
            for (int r = 0; r < dataGridView1.RowCount; r++)
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    string CellData = "";
                    if (parent.table0[i, r] != null)
                    {
                        CellData = parent.table0[i, r];
                    }
                    dataGridView1.Rows[r].Cells[i].Value = CellData;
                    dataGridView1.Rows[r].Cells[i].Style.BackColor = Color.White;
                }
            }
        }
        private void okToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parent.table0 = new string[dataGridView1.ColumnCount, dataGridView1.RowCount];
            FillDataTable();
            this.Close();
        }
        private void DataFrom_Load(object sender, EventArgs e)
        {
            ReceiveDataTable();
        }
        private void DataForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.isEdit = false;
        }
        private void GH_NotesEditor_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < dataGridView1.RowCount; r++)
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    string CellData = "";
                    dataGridView1.Rows[r].Cells[i].Value = CellData;
                }
            }
        }
        public void CancelSelect()
        {
            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            this.dataGridView1.CurrentCell = null;
            /*
          for(int i=0;i<  dataGridView1.SelectedCells.Count;i++)
            {
                dataGridView1.SelectedCells[i].Selected = false;
            }*/
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView2Excel_cvs(this.dataGridView1);
        }
        private void ToNumber(string Flag)
        {
            CancelSelect();
            for (int r = 0; r < dataGridView1.RowCount; r++)
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    try
                    {
                        string CellData = "";
                        if (dataGridView1.Rows[r].Cells[i].Value != null)
                        { CellData = dataGridView1.Rows[r].Cells[i].Value.ToString(); }
                        if (CellData != null && CellData != "")
                        {
                            if (Flag == "Int32")
                            {
                                if (CellData[0] == '0' && CellData[1] == 'x')
                                {
                                    CellData.Split(new char[2] { '0', 'x' });
                                    CellData = Convert.ToInt32(CellData, 16).ToString();
                                }
                            }
                            else if (Flag == "uint16_t")
                            {
                                ushort data_1 = Convert.ToUInt16(CellData);
                                CellData = "0x" + string.Format("{0:X}", data_1);
                            }
                            else if (Flag == "uint8_t")
                            {
                                byte data_1 = Convert.ToByte(CellData);
                                CellData = "0x" + string.Format("{0:X}", data_1);
                            }
                           

                            dataGridView1.Rows[r].Cells[i].Value = CellData;
                        }
                    }
                    catch { continue; }
                }
            }
        }
       
        private void toByteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToNumber("uint8_t");
        }
        private void toInt16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToNumber("uint16_t");
        }      
        private void convertToIntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToNumber("Int32");
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                parent.table0 = new string[dataGridView1.ColumnCount, dataGridView1.RowCount];
                FillDataTable();
                this.Close();
            }
        }
    }
}
