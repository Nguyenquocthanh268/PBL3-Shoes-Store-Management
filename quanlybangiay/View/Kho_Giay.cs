﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quanlybangiay.BLL.BLL_AD;
namespace quanlybangiay.form
{
    public partial class Kho_Giay : Form
    {
        //private Button CurrentButton;
        public Kho_Giay()
        {
            InitializeComponent();
            cbb_sort.Items.AddRange(BLL_KhoGiay.Instance.CBBsort().ToArray());
            dataGridView1.RowTemplate.Height = 40;
            foreach (string i in BLL_KhoGiay.Instance.SapxepSize(BLL_KhoGiay.Instance.CBBsize()).Distinct())
            {
                cbb_size.Items.Add(i);
            }
            foreach (string i in BLL_KhoGiay.Instance.CBBhang().Distinct())
            {
                cbb_hang.Items.Add(i);
            }

            show();
        }

        public void show()
        {
            dataGridView1.DataSource = BLL_KhoGiay.Instance.getAllGiay();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string id = txt_id.Text;
            string ten = txt_tengiay.Text;

            int index1 = cbb_size.SelectedIndex;
            int index2 = cbb_hang.SelectedIndex;

            if (index1 >= 0 && index2 < 0)
            {
                int size = Convert.ToInt32(cbb_size.SelectedItem.ToString());

                dataGridView1.DataSource = BLL_KhoGiay.Instance.searh_Size(ten, size);
            }

            else if (index2 >= 0 && index1 < 0)
            {
                string hang = cbb_hang.SelectedItem.ToString();
                dataGridView1.DataSource = BLL_KhoGiay.Instance.search_Hang(ten, hang);
            }
            else if (id == "" && ten == "" && index2 < 0 && index1 < 0)
            {
                show();
            }

            else if (id == "")
            {
                if (index1 < 0 && index2 < 0)
                {
                    dataGridView1.DataSource = BLL_KhoGiay.Instance.search_Size_Hang(ten, "", 0);
                }
                else
                {
                    int size = Convert.ToInt32(cbb_size.SelectedItem.ToString());
                    string hang = cbb_hang.SelectedItem.ToString();
                    dataGridView1.DataSource = BLL_KhoGiay.Instance.search_Size_Hang(ten, hang, size);
                }
            }
            else
            {
                if (index1 < 0 && index2 < 0)
                {
                    dataGridView1.DataSource = BLL_KhoGiay.Instance.search(id, "", "", 0);
                }

            }
           

        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn giày muốn xem!");
            }
            else
            {
                int i = 1;
                string ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                DetailKhoGiay f = new DetailKhoGiay(ID, i);
                f.Show();
            }

        }

        private void btnAddnew_Click_1(object sender, EventArgs e)
        {
            DetailKhoGiay f = new DetailKhoGiay("", 2);
            f.d = new DetailKhoGiay.Mydel(show);
            f.d2 = new DetailKhoGiay.Mydel2(ResetCombo);
            
            f.Show();
   
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                DetailKhoGiay f = new DetailKhoGiay(ID, 3);
                f.d = new DetailKhoGiay.Mydel(show);
                f.d2 = new DetailKhoGiay.Mydel2(ResetCombo);
                f.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn giày cần chỉnh sửa !");

            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                String s = "Bạn có muốn xóa mặt hàng ??";
                String s1 = "Delete";
                MessageBoxButtons ok = MessageBoxButtons.OKCancel;
                DialogResult d = MessageBox.Show(s, s1, ok);
                if (d == DialogResult.OK)
                {
                    foreach (DataGridViewRow i in dataGridView1.SelectedRows)
                    {
                        string ID = i.Cells["ID_Giay"].Value.ToString();
                        BLL_KhoGiay.Instance.Delete(ID);
                    }
                   
                    show();
                    MessageBox.Show("Xóa thành công!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn giày muốn xoá!");
            }
            
          
    }

    private void btnInput_Click_1(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count == 1)
        {
            string ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            Nhap_kho f = new Nhap_kho(ID);
            f.Show();
        }
        else
        {
            MessageBox.Show("Vui lòng chọn giày cần nhập kho !");

        }
    }

    private void btnSort_Click_1(object sender, EventArgs e)
    {
        int index = cbb_sort.SelectedIndex;
        if (index >= 0)
        {
            dataGridView1.DataSource = BLL_KhoGiay.Instance.sort(index);
        }
        else
        {
            MessageBox.Show("Vui lòng chọn thuộc tính sắp xếp !");
        }

    }
     public void ResetCombo()
        {
            cbb_hang.Items.Clear();
            cbb_size.Items.Clear();
            foreach (string i in BLL_KhoGiay.Instance.SapxepSize(BLL_KhoGiay.Instance.CBBsize()).Distinct())
            {
                cbb_size.Items.Add(i);
            }
            foreach (string i in BLL_KhoGiay.Instance.CBBhang().Distinct())
            {
                cbb_hang.Items.Add(i);
            }
        }

}
}
