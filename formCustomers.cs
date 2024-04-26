using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBPROJECT
{
    public partial class formCustomers : Form
    {
        DataTable DTable;

        SqlDataAdapter DAdapter;
        SqlCommand DCommand;
        BindingSource DBindingSource;

        Boolean CancelUpdates;

        int idcolumn = 0;

        public formCustomers()
        {
            InitializeComponent();
        }

        private void formCustomers_Load(object sender, EventArgs e)
        {
            this.CancelUpdates = true;
            this.BindMainGrid();
            this.FormatGrid();
            this.CancelUpdates = false;
        }

        private void BindMainGrid()
        {
            this.CancelUpdates = true;
            if (Globals.glOpenSqlConn())
            {
                this.DCommand = new SqlCommand("spGetAllCustomers", Globals.sqlconn);
                this.DAdapter = new SqlDataAdapter(this.DCommand);

                this.DTable = new DataTable();

                this.DAdapter.Fill(DTable);

                this.DBindingSource = new BindingSource();
                this.DBindingSource.DataSource = DTable;

                dgvCust.DataSource = DBindingSource;
                this.bNavCust.BindingSource = this.DBindingSource;
            }
            this.CancelUpdates = false;
        }
        private void FormatGrid()
        {
            this.dgvCust.Columns["idCustomer"].Visible = false;
            this.dgvCust.Columns["nameCustomer"].HeaderText = "Login Name";
            this.dgvCust.Columns["addressCustomer"].HeaderText = "Address";
            this.dgvCust.Columns["emailCustomer"].HeaderText = "Email";
            this.dgvCust.Columns["contactCustomer"].HeaderText = "Contact";

            this.BackColor = Globals.gDialogBackgroundColor;

            this.dgvCust.BackgroundColor = Globals.gGridOddRowColor;
            this.dgvCust.AlternatingRowsDefaultCellStyle.BackColor = Globals.gGridEvenRowColor;

            this.dgvCust.EnableHeadersVisualStyles = false;
            this.dgvCust.ColumnHeadersDefaultCellStyle.BackColor = Globals.gGridHeaderColor;
        }

        private void dgvCust_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCustSearch_Click(object sender, EventArgs e)
        {

        }

        private void dgvCust_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
