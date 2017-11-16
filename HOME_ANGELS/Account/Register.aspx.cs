using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Membership.OpenAuth;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Account_Register : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];

        // VERIFICA SE EXISTE E CRIA AS PERMISSOES
        if (!Roles.RoleExists("Cliente")) Roles.CreateRole("Cliente");
        if (!Roles.RoleExists("Cuidador")) Roles.CreateRole("Cuidador");

        //pega todas roles cadastradas
        var roles = Roles.GetAllRoles();
        DropDownList DDL = (DropDownList)FindControlRecursive(this.Page, "ListRoles");
        //adiciona as roles no DropDrownList
        foreach (var item in roles)
        {
            DDL.Items.Add(item);
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


    protected void RegisterUser_CreatedUser(object sender, EventArgs e)
    {

        FormsAuthentication.SetAuthCookie(RegisterUser.UserName, createPersistentCookie: false);
        string continueUrl = RegisterUser.ContinueDestinationPageUrl;
        if (!OpenAuth.IsLocalUrl(continueUrl))
        {
            continueUrl = "~/Default.aspx";
        }

        // GET ID USER
        var user = Membership.GetUser(RegisterUser.UserName);
        if (user != null)
        {

            Guid userID = (Guid)user.ProviderUserKey;

            // ADD PERMISSAO
            var indexRole = AddRole();

            // NOVO CLIENTE                
            if (indexRole == 0) NewClient(userID);

            // NOVO CUIDADOR
            if (indexRole == 1) NewCaregiver(userID);
        }

        Response.Redirect(continueUrl);
    }

    private void NewClient(Guid userID)
    {
        TextBox Name = (TextBox)FindControlRecursive(RegisterUser.Page, "UserName");
        TextBox CPF = (TextBox)FindControlRecursive(RegisterUser.Page, "CPF");
        TextBox Email = (TextBox)FindControlRecursive(RegisterUser.Page, "Email");
        TextBox City = (TextBox)FindControlRecursive(RegisterUser.Page, "City");
        TextBox State = (TextBox)FindControlRecursive(RegisterUser.Page, "State");
        TextBox Address = (TextBox)FindControlRecursive(RegisterUser.Page, "Address");
        TextBox Phone = (TextBox)FindControlRecursive(RegisterUser.Page, "Phone");

        int userId = 0;
        string connectionStrings = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        try
        {
            using (var con = new SqlConnection(connectionStrings))
            {
                var query = "INSERT INTO Cliente (Nome, CPF, Telefone, CodUser, Endereco, Cidade, Estado, Email) VALUES (@Nome,@CPF,@Telefone,@CodUser,@Endereco,@Cidade,@Estado,@Email)";

                using (var insertCommand = new SqlCommand(query, con))
                {
                    insertCommand.Parameters.AddWithValue("@Nome", Name.Text);
                    insertCommand.Parameters.AddWithValue("@CPF", CPF.Text);
                    insertCommand.Parameters.AddWithValue("@Telefone", Phone.Text);
                    insertCommand.Parameters.AddWithValue("@CodUser", userID);
                    insertCommand.Parameters.AddWithValue("@Endereco", Address.Text);
                    insertCommand.Parameters.AddWithValue("@Cidade", City.Text);
                    insertCommand.Parameters.AddWithValue("@Estado", State.Text);
                    insertCommand.Parameters.AddWithValue("@Email", Email.Text);
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

    private int AddRole()
    {
        TextBox Name = (TextBox ) FindControlRecursive(RegisterUser.Page, "UserName");
        DropDownList RolesList = (DropDownList) FindControlRecursive(this.RegisterUser.Page, "ListRoles");
        Roles.AddUserToRole(Name.Text, RolesList.SelectedItem.Value);
        return RolesList.SelectedIndex;
    }

    private void NewCaregiver(Guid userID)
    {
        TextBox Name = (TextBox)FindControlRecursive(RegisterUser.Page, "UserName");
        TextBox CPF = (TextBox)FindControlRecursive(RegisterUser.Page, "CPF");
        TextBox Email = (TextBox)FindControlRecursive(RegisterUser.Page, "Email");
        TextBox City = (TextBox)FindControlRecursive(RegisterUser.Page, "City");
        TextBox State = (TextBox)FindControlRecursive(RegisterUser.Page, "State");
        TextBox Address = (TextBox)FindControlRecursive(RegisterUser.Page, "Address");
        TextBox Phone = (TextBox)FindControlRecursive(RegisterUser.Page, "Phone");

        int userId = 0;
        string connectionStrings = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        try
        {
            using (var con = new SqlConnection(connectionStrings))
            {
                var query = "INSERT INTO Cuidador (Nome, CPF, Telefone, CodUser, Endereco, Cidade, Estado, Email) VALUES (@Nome,@CPF,@Telefone,@CodUser,@Endereco,@Cidade,@Estado,@Email)";

                using (var insertCommand = new SqlCommand(query, con))
                {
                    insertCommand.Parameters.AddWithValue("@Nome", Name.Text);
                    insertCommand.Parameters.AddWithValue("@CPF", CPF.Text);
                    insertCommand.Parameters.AddWithValue("@Telefone", Phone.Text);
                    insertCommand.Parameters.AddWithValue("@CodUser", userID);
                    insertCommand.Parameters.AddWithValue("@Endereco", Address.Text);
                    insertCommand.Parameters.AddWithValue("@Cidade", City.Text);
                    insertCommand.Parameters.AddWithValue("@Estado", State.Text);
                    insertCommand.Parameters.AddWithValue("@Email", Email.Text);
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