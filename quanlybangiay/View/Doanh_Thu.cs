using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quanlybangiay.BLL.BLL_AD;
using quanlybangiay.DTO;

namespace quanlybangiay.form
{
    public partial class Doanh_Thu : Form
    {
        private Button currentButton;
        public Doanh_Thu()
        {
            InitializeComponent();
            but7days.Select();
            bt_color(but7days);
            dtStartday.Value = DateTime.Today.AddDays(-7);
            dtEndday.Value = DateTime.Now;
            LoadData();
        }
        private void bt_color(object sender)
        {
            if (sender != null)
            {
                if (currentButton != (Button)sender)
                {
                    //Button - UI
                    Disable();
                    currentButton = (Button)sender;
                    currentButton.BackColor = butThisyear.FlatAppearance.BorderColor;
                    currentButton.ForeColor = Color.White;
                }
                if (currentButton == butTuyChinh)
                {
                    dtStartday.Enabled = true;
                    dtEndday.Enabled = true;
                    butOK.Enabled = true;
                }
                else
                {
                    //dtStartday.Enabled = false;
                    //dtEndday.Enabled = false;
                    butOK.Enabled = false;
                }
            }
        }
        private void Disable()
        {
            if (currentButton != null)
            {
                currentButton.BackColor = Color.FromArgb(31, 35, 70);
                currentButton.ForeColor = Color.FromArgb(103, 128, 144);
            }
        }
        private void LoadData()
        {
            var refreshData = BLL_DoanhThu.Instance.LoadData(dtStartday.Value, dtEndday.Value);
            if (refreshData == true)
            {
                lbTongDonHang.Text = BLL_DoanhThu.Instance.TongHoaDon.ToString();
                lbTongDoanhThu.Text = BLL_DoanhThu.Instance.TongDoanhThu.ToString();
                lbTongLoiNhuan.Text = BLL_DoanhThu.Instance.TongLoiNhuan.ToString();
                lbTongKhachHang.Text = BLL_DoanhThu.Instance.TongKhachHang.ToString();
                lbTongGiayBan.Text = BLL_DoanhThu.Instance.TongGiayDaBan.ToString();
                lbTongGiayTonKho.Text = BLL_DoanhThu.Instance.TongGiayTonKho.ToString();


                chartDoanhThu.DataSource = BLL_DoanhThu.Instance.GrossRevenueList;
                chartDoanhThu.Series[0].XValueMember = "Date";  
                chartDoanhThu.Series[0].YValueMembers = "TotalAmount";
                chartDoanhThu.DataBind();

            }
            else Console.WriteLine("View not loaded, same query");
        }

        private void butToday_Click(object sender, EventArgs e)
        {
            bt_color(sender);
            dtStartday.Value = DateTime.Today;
            dtEndday.Value = DateTime.Now;
            LoadData();

        }
        private void but7days_Click(object sender, EventArgs e)
        {
            bt_color(sender);
            dtStartday.Value = DateTime.Today.AddDays(-7);
            dtEndday.Value = DateTime.Now;
            LoadData();

        }
        private void butThismonth_Click(object sender, EventArgs e)
        {
            bt_color(sender);
            dtStartday.Value = DateTime.Today.AddDays(-30);
            dtEndday.Value = DateTime.Now;
            LoadData();

        }
        private void butThisyear_Click(object sender, EventArgs e)
        {
            bt_color(sender);
            dtStartday.Value = DateTime.Today.AddDays(-365);
            dtEndday.Value = DateTime.Now;
            LoadData();

        }
        private void butOK_Click(object sender, EventArgs e)
        {
            if(dtStartday.Value < dtEndday.Value)
            {
                LoadData();
            }
            else
            {
                MessageBox.Show("Khoảng thời gian không hợp lệ, vui lòng kiểm tra lại ...");
            }
        }
        private void butTuyChinh_Click(object sender, EventArgs e)
        {
            bt_color(sender);
        }
    }
}
