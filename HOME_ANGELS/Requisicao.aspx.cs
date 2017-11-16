using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Requisicao : Page
{
    public bool Cuidador = false;
    Guid userID = new Guid();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            var user = Membership.GetUser(User.Identity.Name);
            if (user != null)
            {
                userID = (Guid)user.ProviderUserKey;
            }

            Cuidador = Roles.IsUserInRole(User.Identity.Name, "Cuidador");
            string[] users;
            DropDownList dropDown;

            if (Cuidador) { 
                users = Roles.GetUsersInRole("Cuidador");
                dropDown = DropDownUserCuidador;
            }
            else
            {
                users = Roles.GetUsersInRole("Cliente");
                dropDown = DropDownUsers;
            }
            
            foreach (var item in users)
            {
                dropDown.Items.Add(item);
            }

            if (!this.IsPostBack)
            {
                
               this.BindGrid();
                
            }

        }
        else
        {
            Response.Redirect("~/Account/Login.aspx");
        }



    }

    protected void Salvar(object sender, EventArgs e)
    {
        var user = Membership.GetUser(User.Identity.Name);
        if (user != null)
        {
            Guid userID = (Guid)user.ProviderUserKey;
            this.Salve(userID);
        }
        
    }


    private void BindGrid()
    {       

        string connectionStrings = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //try
        //{
        using (var con = new SqlConnection(connectionStrings))
        {
            var query = "SELECT Nome, Status, DataInicial, DataFinal FROM Requisicao WHERE CodUser ='" + userID + "'";
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
            e.Row.Cells[0].Text = Regex.Replace(e.Row.Cells[0].Text, UserName.Text.Trim(), delegate (Match match)
            {
                return string.Format("<span style = 'background-color:#D9EDF7'>{0}</span>", match.Value);
            }, RegexOptions.IgnoreCase);

            e.Row.Cells[2].Text = Regex.Replace(e.Row.Cells[2].Text, Estado.Text.Trim(), delegate (Match match)
            {
                return string.Format("<span style = 'background-color:#D9EDF7'>{0}</span>", match.Value);
            }, RegexOptions.IgnoreCase);

            e.Row.Cells[3].Text = Regex.Replace(e.Row.Cells[3].Text, Cidade.Text.Trim(), delegate (Match match)
            {
                return string.Format("<span style = 'background-color:#D9EDF7'>{0}</span>", match.Value);
            }, RegexOptions.IgnoreCase);

        }
    }

    private Control FindControlRecursive(Control root, string id)
    {
        if (root.ID == id)
        {
            return root;
        }

        foreach (Control c in root.Controls)
        {
            Control t = FindControlRecursive(c, id);
            if (t != null)
            {
                return t;
            }
        }

        return null;
    }

    protected void DropDownUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        ContatoCuidador.Text = DropDownUsers.SelectedItem.Value;
    }


    private void Salve(Guid userID)
    {
        int userId = 0;
        string connectionStrings = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        try
        {
            using (var con = new SqlConnection(connectionStrings))
            {
                var query = "INSERT INTO Requisicao (Nome, Telefone, CodUser, Cidade, Estado, Email, DataInicial, DataFinal, Status, NameCuidador) VALUES (@Nome,@Telefone,@CodUser,@Cidade,@Estado,@Email,@DataInicial,@DataFinal,@Status,@NameCuidador)";

                using (var insertCommand = new SqlCommand(query, con))
                {
                    insertCommand.Parameters.AddWithValue("@Nome", UserName.Text);                    
                    insertCommand.Parameters.AddWithValue("@Telefone", Telefone.Text);
                    insertCommand.Parameters.AddWithValue("@CodUser", userID);                    
                    insertCommand.Parameters.AddWithValue("@Cidade", Cidade.Text);
                    insertCommand.Parameters.AddWithValue("@Estado", Estado.Text);
                    insertCommand.Parameters.AddWithValue("@Email", Email.Text);
                    insertCommand.Parameters.AddWithValue("@DataInicial", DataInicial.SelectedDate);
                    insertCommand.Parameters.AddWithValue("@DataFinal", DataFinal.SelectedDate);
                    insertCommand.Parameters.AddWithValue("@Status", Status.Text);                    
                    insertCommand.Parameters.AddWithValue("@NameCuidador", DropDownUsers.SelectedItem.Value);
                    con.Open();
                    userId = Convert.ToInt32(insertCommand.ExecuteScalar());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            string message = string.Empty;
            switch (userId)
            {
                case -1:
                    message = "O nome de usuário já existe. \\ nPor favor, escolha um nome de usuário diferente.";
                    break;
                case -2:
                    message = "O endereço de e-mail fornecido já foi usado.";
                    break;
                default:
                    message = "Registro bem sucedido. \\ n ID do usuário: " + userId.ToString();
                    break;
            }

            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
        }
    }
}