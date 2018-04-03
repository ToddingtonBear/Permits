using System;
using System.Data;
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
            GetPermits();
        }

        private void GetPermits()
        {
            con = new OleDbConnection(ConStr);  //new connection object with connection string 
            cmd = new OleDbCommand();   //new command object 
            adapt = new OleDbDataAdapter(cmd);  //new adapter object
            cmd.Connection = con;   //assigns connection to command 
            cmd.CommandText = "SELECT * FROM Permits";  //defines what command does
           // con.Open();
            DataTable dt1 = new DataTable(); //defines table to be filled
            adapt.Fill(dt1);    //adapter fills table
            fulldb.DataSource = dt1;    //assigns table to a table in the form 
            fulldb.AutoResizeColumns();
            fulldb.AutoResizeRows();
        }

        private void Find()
        {
            con = new OleDbConnection(ConStr);  //new connection object with connection string 
            cmd = new OleDbCommand();   //new command object 
            adapt = new OleDbDataAdapter(cmd);  //new adapter object
            cmd.Connection = con;   //assigns connection to command 
            cmd.CommandText = "SELECT * FROM Permits WHERE Student_ID =" + int.Parse(ID.Text);  //defines command
            if (ID.Text != "")
            {
                DataTable dt2 = new DataTable(); //defines table to be filled
                adapt.Fill(dt2);    //adapter fills table
                queries.DataSource = dt2;    //assigns table to a table in the form 
                queries.AutoResizeColumns();
                queries.AutoResizeRows();
            }
        }

        private void Clear()
        {
            //clear all text boxes
            ID.Text = ""; owner.Text = ""; apnum.Text = ""; exp.Text = "";
            v1.Text = ""; r1.Text = ""; v2.Text = ""; r2.Text = ""; v3.Text = ""; r3.Text = "";
        }

        private void Add()
        {
            con = new OleDbConnection(ConStr);  //new connection object with connection string 
            cmd = new OleDbCommand();   //new command object 
            cmd.Connection = con;   //assigns connection to command 
            cmd.CommandText = "INSERT INTO Permits (Student_ID,Owner,Apartment,Expires,Vehicle_Model_1,Registration_1,Vehicle_Model_2,Registration_2,Vehicle_Model_3,Registration_3) VALUES ('" + int.Parse(ID.Text) + "','" + owner.Text + "','" + int.Parse(apnum.Text) + "','" + DateTime.Parse(exp.Text) + "','" + v1.Text + "','" + r1.Text + "','" + v2.Text + "','" + r2.Text + "','" + v3.Text + "','" + r3.Text + "'  )";  //defines what command does
            con.Open(); //open connection
            cmd.ExecuteNonQuery();  //run command
            con.Close();    //close connection
        }

        private void Edit()
        {
            con = new OleDbConnection(ConStr);  //new connection object with connection string 
            cmd = new OleDbCommand();   //new command object 
            cmd.Connection = con;   //assigns connection to command 
            cmd.CommandText = "SELECT * FROM Permits WHERE Student_ID =" + int.Parse(ID.Text);  //defines command
            con.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                owner.Text = reader["Owner"].ToString();
                apnum.Text = reader["Apartment"].ToString();
                exp.Text = reader["Expires"].ToString();
                v1.Text = reader["Vehicle_Model_1"].ToString();
                r1.Text = reader["Registration_1"].ToString();
                v2.Text = reader["Vehicle_Model_2"].ToString();
                r2.Text = reader["Registration_2"].ToString();
                v3.Text = reader["Vehicle_Model_3"].ToString();
                r3.Text = reader["Registration_3"].ToString();
            }
            con.Close();
        }
        private void Up()
        {
            con = new OleDbConnection(ConStr);  //new connection object with connection string 
            cmd = new OleDbCommand();   //new command object 
            cmd.Connection = con;   //assigns connection to command 
            cmd.CommandText = "UPDATE Permits SET [Owner]='" + owner.Text + "', [Apartment]=" + int.Parse(apnum.Text) + ", [Expires]='" + DateTime.Parse(exp.Text) + "', [Vehicle_Model_1]='" + v1.Text + "', [Registration_1]='" + r1.Text + "', [Vehicle_Model_2]='" + v2.Text + "', [Registration_2]='" + r2.Text + "', [Vehicle_Model_3]='" + v3.Text + "', [Registration_3]='" + r3.Text + "', WHERE [Student_ID] =" + int.Parse(ID.Text);  //defines command
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void Delete()
        {
            con = new OleDbConnection(ConStr);  //new connection object with connection string 
            cmd = new OleDbCommand();   //new command object 
            cmd.Connection = con;   //assigns connection to command
            cmd.CommandText = "DELETE FROM Permits WHERE [Student_ID]=" + ID.Text; ;  //defines command
            if (ID.Text != "")
            {
                con.Open(); //open connection
                cmd.ExecuteNonQuery();  //run command
                con.Close();    //close connection
            }
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Add();
            GetPermits();

        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Find();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
            Clear();
            GetPermits();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Up();
            GetPermits();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit();
        }
    }
}
