using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cuidador : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            // this.BindGrid();
        }

    }

    protected void Search(object sender, EventArgs e)
    {
        this.BindGrid();
    }

    private void BindGrid()
    {


        string connectionStrings = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        using (var con = new SqlConnection(connectionStrings))
        {
            var query = "SELECT Id, Nome, Telefone, Estado, Cidade FROM Cliente WHERE" +
                " Nome LIKE '%" + UserName.Text + "%' and" +
                " Id LIKE '%" + NumeroRegistro.Text + "%' and" +                
                " Estado LIKE '%" + Estado.Text + "%' and" +
                " Cidade LIKE '%" + Cidade.Text + "%'";
            // var query = "SELECT Email, Password, CreateDate FROM Memberships WHERE Email LIKE '%" + UserName.Text + "%'";

            using (var sqlDataAdapter = new SqlDataAdapter(query, con))
            {
                DataSet dataSet = new DataSet();
                con.Open();
                sqlDataAdapter.Fill(dataSet);
                GridViewCuidador.DataSource = dataSet;
                GridViewCuidador.DataBind();
                con.Close();
            }
        }
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewCuidador.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }
    
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Regex.Replace(e.Row.Cells[0].Text, NumeroRegistro.Text.Trim(), delegate (Match match)
            {
                return string.Format("<span style = 'background-color:#D9EDF7'>{0}</span>", match.Value);
            }, RegexOptions.IgnoreCase);

            e.Row.Cells[1].Text = Regex.Replace(e.Row.Cells[1].Text, UserName.Text.Trim(), delegate (Match match)
            {
                return string.Format("<span style = 'background-color:#D9EDF7'>{0}</span>", match.Value);
            }, RegexOptions.IgnoreCase);
            
            e.Row.Cells[3].Text = Regex.Replace(e.Row.Cells[3].Text, Estado.Text.Trim(), delegate (Match match)
            {
                return string.Format("<span style = 'background-color:#D9EDF7'>{0}</span>", match.Value);
            }, RegexOptions.IgnoreCase);

            e.Row.Cells[4].Text = Regex.Replace(e.Row.Cells[4].Text, Cidade.Text.Trim(), delegate (Match match)
            {
                return string.Format("<span style = 'background-color:#D9EDF7'>{0}</span>", match.Value);
            }, RegexOptions.IgnoreCase);

        }
    }

}