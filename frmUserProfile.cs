using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBPROJECT
{
    public partial class frmUserProfile : Form
    {
        long iduser;
        String loginname;
        public frmUserProfile(long liduser, String lname)
        {
            InitializeComponent();
            this.iduser = liduser;
            this.loginname = lname;
        }

        private void frmUserProfile_LoadUserData()
        {
            if (Globals.glOpenSqlConn())
            {
                SqlCommand cmd = new SqlCommand("spGetUserProfile", Globals.sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@liduser", this.iduser);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                int rowcount = dt.Rows.Count;

                if (rowcount == 0)
                {
                    csMessageBox.Show("Invalid User ID", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    this.txtLoginName.Text = dt.Rows[0][0].ToString();
                    this.txtEmail.Text = dt.Rows[0][1].ToString();
                    this.txtSMTPHOST.Text = dt.Rows[0][2].ToString();
                    this.txtSMTPport.Text = dt.Rows[0][3].ToString();
                    this.pkrBirthdate.Value = Globals.glConvertBlankDate(dt.Rows[0][4].ToString());
                    this.cbxGender.SelectedItem = Globals.glConvertBlankGender(dt.Rows[0][5].ToString());
                }
            }
        }

        private void frmUserProfile_GetPhotofromField()
        {
            if (Globals.glOpenSqlConn())
            {
                SqlCommand cmd = new SqlCommand("select photo from users where id=@liduser", Globals.sqlconn);
                cmd.Parameters.AddWithValue("@liduser", this.iduser);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                int rowcount = dt.Rows.Count;

                if (rowcount == 0)
                {
                    csMessageBox.Show("Invalid User ID", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (dt.Rows[0][0] != null)
                    {
                        byte[] UserImg = (byte[])dt.Rows[0][0];

                        MemoryStream imgstream = new MemoryStream(UserImg);

                        if (imgstream.Length > 0)
                            this.picBoxUser.Image = Image.FromStream(imgstream);
                    }
                    da.Dispose();
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmUserProfile_Load(object sender, EventArgs e)
        {
            this.frmUserProfile_LoadUserData();
            this.frmUserProfile_GetPhotofromField();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if(csMessageBox.Show("Erase User Photo?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(Globals.glOpenSqlConn())
                {
                    SqlCommand cmd = new SqlCommand("spGetUserProfile", Globals.sqlconn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@liduser", this.iduser);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.ExecuteNonQuery();
                    this.picBoxUser.Image = null;
                }
                csMessageBox.Show("User Photo Erased", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLoadPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openPhoto = new OpenFileDialog();
            openPhoto.Filter = "Choose Image(*.jpg; *.png; *.gif;)|*.jpg; *.png; *.gif";
            if(openPhoto.ShowDialog() == DialogResult.OK)
            {
                picBoxUser.Image = Image.FromFile(openPhoto.FileName);
            }
        }
    }
}
