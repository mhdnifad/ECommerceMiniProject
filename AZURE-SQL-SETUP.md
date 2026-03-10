# Connect ECommerceMini to Azure SQL

Follow these steps to use Azure SQL with your app on Render.

---

## Step 1: Create Azure SQL Database

1. Go to [Azure Portal](https://portal.azure.com) and sign in.

2. **Create a resource** → Search **"Azure SQL"** → Select **"SQL Database"** → Create.

3. **Basics:**
   - **Subscription:** Your subscription
   - **Resource group:** Create new (e.g. `ecommerce-rg`) or use existing
   - **Database name:** `ECommerceMiniDb`
   - **Server:** Click "Create new"
     - **Server name:** `ecommercemini-sql` (or another unique name)
     - **Location:** Same region as your users (e.g. East US)
     - **Authentication:** Use SQL authentication
     - **Server admin login:** `sqladmin` (or your choice)
     - **Password:** Set a strong password
   - **Compute + storage:** Choose **Free** or **Basic** (lowest cost)

4. Click **Review + create** → **Create**.

5. After creation, go to your **SQL server** (not the database) → **Overview** and note:
   - **Server name** (e.g. `ecommercemini-sql.database.windows.net`)

---

## Step 2: Configure Firewall (Allow Render)

1. In Azure Portal → Your **SQL server** → **Networking** (under Security).
2. Under **Firewall rules:**
   - Ensure **"Allow Azure services and resources to access this server"** = **Yes**
3. Add a rule to allow Render:
   - **Rule name:** `Render`
   - **Start IP:** `0.0.0.0`
   - **End IP:** `255.255.255.255`
   - (This allows all IPs; for production, use [Render's outbound IPs](https://render.com/docs/outbound-ip-addresses) instead)
4. Click **Save**.

---

## Step 3: Create Database (if not done)

If you chose "Create new" database in Step 1, it's already created. Otherwise create a database named `ECommerceMiniDb`.

---

## Step 4: Run Migrations (Create Tables)

From your **local machine** (with SQL Server tools), run migrations against Azure SQL:

```powershell
cd c:\ECommerceMini

# Set the connection string (replace with your values)
$env:ConnectionStrings__DefaultConnection = "Server=tcp:YOUR-SERVER.database.windows.net,1433;Database=ECommerceMiniDb;User ID=sqladmin;Password=YOUR-PASSWORD;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# Run migrations
dotnet ef database update
```

Or use the connection string from Azure Portal:
- Go to your database → **Connection strings** → Copy the **ADO.NET** connection string and replace placeholders.

---

## Step 5: Connection String Format

Use this format (replace placeholders):

```
Server=tcp:YOUR-SERVER.database.windows.net,1433;Database=ECommerceMiniDb;User ID=YOUR-USERNAME;Password=YOUR-PASSWORD;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

Example:
```
Server=tcp:ecommercemini-sql.database.windows.net,1433;Database=ECommerceMiniDb;User ID=sqladmin;Password=MyStr0ng!Pass;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

---

## Step 6: Add to Render

1. **Render Dashboard** → Your **ecommerce-mini** service → **Environment**.

2. Add **Secret** environment variable:
   - **Key:** `ConnectionStrings__DefaultConnection`
   - **Value:** Your full Azure SQL connection string (from Step 5)

3. Click **Save** → **Manual Deploy** to redeploy.

---

## Step 7: Verify

- Visit your Render URL and try login/register.
- Check Render logs for connection errors.

---

## Security Notes (Production)

- Use a strong password for the SQL admin account.
- Prefer restricting firewall to [Render's outbound IPs](https://render.com/docs/outbound-ip-addresses) instead of `0.0.0.0`–`255.255.255.255`.
- Keep the connection string secret (only in Render env vars, never in code).
