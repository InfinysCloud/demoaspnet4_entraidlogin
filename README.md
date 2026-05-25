# Konfigurasi Azure Entra ID (Azure AD)

Tambahkan konfigurasi berikut pada file Web.config:

<appSettings>
    <add key="owin:AutomaticAppStartup" value="true" />

    <!-- Azure Entra ID Configuration -->
    <add key="ClientId" value="YOUR_CLIENT_ID" />
    <add key="TenantId" value="YOUR_TENANT_ID" />
    <add key="ClientSecret" value="YOUR_CLIENT_SECRET" />

    <!-- Redirect URL after successful login -->
    <add key="RedirectUri" value="https://yourdomain.com/auth/azure/callback.aspx" />
</appSettings>

Parameter Description
## Parameter Description

| Key | Description |
|---|---|
| `ClientId` | Application (client) ID from Azure Entra ID |
| `TenantId` | Directory (tenant) ID from Azure Entra ID |
| `ClientSecret` | Client Secret generated from Azure App Registration |
| `RedirectUri` | Callback URL after successful authentication |
| `owin:AutomaticAppStartup` | Enables OWIN middleware startup automatically |


# Azure Entra ID Setup

1. Open **Microsoft Azure Portal**

2. Navigate to:
   - **Microsoft Entra ID**
   - **App registrations**
   - **New registration**

3. Configure:
   - **Supported account types**
   - **Redirect URI**
```text
     https://yourdomain.com/auth/azure/callback.aspx
```

4. Save the following values:
   - ** Application (client) ID
   - ** Directory (tenant) ID

5. Create a Client Secret:
   - ** Certificates & secrets
   - ** New client secret
