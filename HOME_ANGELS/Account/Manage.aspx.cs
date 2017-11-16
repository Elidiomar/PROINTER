using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.UI.WebControls;

using Microsoft.AspNet.Membership.OpenAuth;

public partial class Account_Manage : System.Web.UI.Page
{
    protected string SuccessMessage
    {
        get;
        private set;
    }

    protected bool CanRemoveExternalLogins
    {
        get;
        private set;
    }

    protected void Page_Load()
    {
        if (!IsPostBack)
        {
            // Determinar as seções a serem renderizadas
            var hasLocalPassword = OpenAuth.HasLocalPassword(User.Identity.Name);
            setPassword.Visible = !hasLocalPassword;
            changePassword.Visible = hasLocalPassword;

            CanRemoveExternalLogins = hasLocalPassword;

            // Renderizar mensagem de êxito
            var message = Request.QueryString["m"];
            if (message != null)
            {
                // Remover a cadeia de caracteres de consulta da ação
                Form.Action = ResolveUrl("~/Account/Manage");

                SuccessMessage =
                    message == "ChangePwdSuccess" ? "A senha foi alterada."
                    : message == "SetPwdSuccess" ? "A senha foi definida."
                    : message == "RemoveLoginSuccess" ? "O logon externo foi removido."
                    : String.Empty;
                successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
            }
        }
        

        // Associar dados da lista de contas externas
        var accounts = OpenAuth.GetAccountsForUser(User.Identity.Name);
        CanRemoveExternalLogins = CanRemoveExternalLogins || accounts.Count() > 1;
        // externalLoginsList.DataSource = accounts;
        // externalLoginsList.DataBind();
        
    }

    protected void setPassword_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            var result = OpenAuth.AddLocalPassword(User.Identity.Name, password.Text);
            if (result.IsSuccessful)
            {
                Response.Redirect("~/Account/Manage?m=SetPwdSuccess");
            }
            else
            {
                
                newPasswordMessage.Text = result.ErrorMessage;
                
            }
        }
    }

    
    protected void externalLoginsList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        var providerName = (string)e.Keys["ProviderName"];
        var providerUserId = (string)e.Keys["ProviderUserId"];
        var m = OpenAuth.DeleteAccount(User.Identity.Name, providerName, providerUserId)
            ? "?m=RemoveLoginSuccess"
            : String.Empty;
        Response.Redirect("~/Account/Manage" + m);
    }

    protected T Item<T>() where T : class
    {
        return GetDataItem() as T ?? default(T);
    }
    

    protected static string ConvertToDisplayDateTime(DateTime? utcDateTime)
    {
        // É possível alterar esse método para converter a data e hora do UTC na diferença e 
        // no formato de exibição desejados. Aqui, estamos convertendo-o para o fuso horário e a formatação do servidor,
        // como uma cadeia de caracteres de data abreviada e de hora longa, usando a cultura do thread atual.
        return utcDateTime.HasValue ? utcDateTime.Value.ToLocalTime().ToString("G") : "[nunca]";
    }
}