using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Permits
{
    public partial class Permits : Form
    {
        static OleDbConnection con; //static connection object
        static OleDbCommand cmd;    //static command object 
        static OleDbDataReader reader;  //static reader object 
        static OleDbDataAdapter adapt;  //static adapter
        static String ConStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Chris\Documents\College\NET\Project\ParkingPermits.accdb; Persist Security Info=False;";
        //connection string for database

        public Permits()
        {
            InitializeComponent();
            getPermitsf();
        }

        private void getPermitsf()
        {
            con = new OleDbConnection(ConStr);  //new connection object with connection string 
            cmd = new OleDbCommand();   //new command object 
            adapt = new OleDbDataAdapter(cmd);
            cmd.Connection = con;   //assigns connection to command 
            cmd.CommandText = "SELECT * FROM Permits";  //defines what command does
            DataTable dt1 = new DataTable(); //defines table to be filled
            adapt.Fill(dt1);    //adapter fills table
            fulldb.DataSource = dt1;    //assigns table to a table in the form 
            fulldb.AutoResizeColumns();
            fulldb.AutoResizeRows();
        }




    }
}
