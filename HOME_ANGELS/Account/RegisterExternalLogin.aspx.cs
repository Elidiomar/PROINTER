using System;
using System.Web;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.AspNet.Membership.OpenAuth;

public partial class Account_RegisterExternalLogin : System.Web.UI.Page
{
    protected string ProviderName
    {
        get { return (string)ViewState["ProviderName"] ?? String.Empty; }
        private set { ViewState["ProviderName"] = value; }
    }

    protected string ProviderDisplayName
    {
        get { return (string)ViewState["ProviderDisplayName"] ?? String.Empty; }
        private set { ViewState["ProviderDisplayName"] = value; }
    }

    protected string ProviderUserId
    {
        get { return (string)ViewState["ProviderUserId"] ?? String.Empty; }
        private set { ViewState["ProviderUserId"] = value; }
    }

    protected string ProviderUserName
    {
        get { return (string)ViewState["ProviderUserName"] ?? String.Empty; }
        private set { ViewState["ProviderUserName"] = value; }
    }

    protected void Page_Load()
    {
        if (!IsPostBack)
        {
            ProcessProviderResult();
        }
    }

    protected void logIn_Click(object sender, EventArgs e)
    {
        CreateAndLoginUser();
    }

    protected void cancel_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl();
    }

    private void ProcessProviderResult()
    {
        // Processar o resultado a partir de um provedor de autenticação da solicitação
        ProviderName = OpenAuth.GetProviderNameFromCurrentRequest();

        if (String.IsNullOrEmpty(ProviderName))
        {
            Response.Redirect(FormsAuthentication.LoginUrl);
        }

        ProviderDisplayName = OpenAuth.GetProviderDisplayName(ProviderName);

        // Criar a url de redirecionamento para verificação de OpenAuth
        var redirectUrl = "~/Account/RegisterExternalLogin";
        var returnUrl = Request.QueryString["ReturnUrl"];
        if (!String.IsNullOrEmpty(returnUrl))
        {
            redirectUrl += "?ReturnUrl=" + HttpUtility.UrlEncode(returnUrl);
        }

        // Verificar a carga de OpenAuth
        var authResult = OpenAuth.VerifyAuthentication(redirectUrl);
        if (!authResult.IsSuccessful)
        {
            Title = "Falha no logon externo";
            userNameForm.Visible = false;
            
            providerMessage.Text = String.Format("Falha no logon externo {0},", ProviderDisplayName);
            
            // Para exibir esse erro, habilite o rastreamento de página em web.config (<system.web><trace enabled="true"/></system.web>) e visite ~/Trace.axd
            Trace.Warn("OpenAuth", String.Format("Houve um erro ao verificar a autenticação com {0})", ProviderDisplayName), authResult.Error);
            return;
        }

        // O usuário conectou com o provedor com êxito
        // Verifique se o usuário já está registrado localmente
        if (OpenAuth.Login(authResult.Provider, authResult.ProviderUserId, createPersistentCookie: false))
        {
            RedirectToReturnUrl();
        }

        // Armazenar os detalhes do provedor em ViewState
        ProviderName = authResult.Provider;
        ProviderUserId = authResult.ProviderUserId;
        ProviderUserName = authResult.UserName;

        // Remover a cadeia de caracteres de consulta da ação
        Form.Action = ResolveUrl(redirectUrl);

        if (User.Identity.IsAuthenticated)
        {
            // O usuário já está autenticado; adicione o logon externo e redirecione para a url de retorno
            OpenAuth.AddAccountToExistingUser(ProviderName, ProviderUserId, ProviderUserName, User.Identity.Name);
            RedirectToReturnUrl();
        }
        else
        {
            // O usuário é novo; solicite o nome de sua associação desejada
            userName.Text = authResult.UserName;
        }
    }

    private void CreateAndLoginUser()
    {
        if (!IsValid)
        {
            return;
        }

        var createResult = OpenAuth.CreateUser(ProviderName, ProviderUserId, ProviderUserName, userName.Text);
        if (!createResult.IsSuccessful)
        {
            
                userNameMessage.Text = createResult.ErrorMessage;
                
        }
        else
        {
            // Usuário criado e associado com êxito
            if (OpenAuth.Login(ProviderName, ProviderUserId, createPersistentCookie: false))
            {
                RedirectToReturnUrl();
            }
        }
    }

    private void RedirectToReturnUrl()
    {
        var returnUrl = Request.QueryString["ReturnUrl"];
        if (!String.IsNullOrEmpty(returnUrl) && OpenAuth.IsLocalUrl(returnUrl))
        {
            Response.Redirect(returnUrl);
        }
        else
        {
            Response.Redirect("~/");
        }
    }
}