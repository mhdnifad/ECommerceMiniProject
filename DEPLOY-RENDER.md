# Deploy ECommerceMini to Render (with SQL Server)

Render hosts the **web app** only. SQL Server must be hosted **separately** (Render does not offer SQL Server).

---

## Step 1: Set Up SQL Server (Choose One)

### Option A: Azure SQL Database
- Create free Azure account
- Create Azure SQL Database (free tier available)
- Get connection string (format below)

### Option B: SQL Server on a VPS
- Use DigitalOcean, Linode, Hetzner, or similar
- Install SQL Server or run it in Docker
- Ensure port 1433 is open and accessible from the internet

### Option C: Free SQL Server Hosts
- Some providers offer free SQL Server (e.g., free tier trials)
- Check: [FreeSQLDatabase.com](https://www.freesqldatabase.com/) or similar

**Connection string format:**
```
Server=your-server.database.windows.net;Database=ECommerceMiniDb;User Id=youruser;Password=yourpassword;Encrypt=True;TrustServerCertificate=False;
```

For self-hosted SQL Server:
```
Server=your-server-ip;Database=ECommerceMiniDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True;Encrypt=False;
```

**Important:** Run migrations on your SQL Server before deploying. Locally:
```powershell
$env:ConnectionStrings__DefaultConnection="YourProductionConnectionString"
dotnet ef database update
```

---

## Step 2: Deploy to Render

1. Push your code to **GitHub** (already done)

2. Go to [render.com](https://render.com) and sign up (GitHub login)

3. Click **New +** → **Web Service**

4. Connect your GitHub repo: `mhdnifad/ECommerceMiniProject`

5. Configure:
   - **Name:** `ecommerce-mini`
   - **Region:** Choose closest to your users
   - **Runtime:** Docker
   - **Dockerfile Path:** `./Dockerfile`
   - **Instance Type:** Free (or paid for better performance)

6. **Environment Variables** (add these):

   | Key | Value | Secret? |
   |-----|-------|---------|
   | `ASPNETCORE_ENVIRONMENT` | `Production` | No |
   | `ASPNETCORE_URLS` | `http://+:8080` | No |
   | `ConnectionStrings__DefaultConnection` | Your SQL Server connection string | **Yes** |

7. Click **Create Web Service**

8. Render will build and deploy. Your app will be at `https://ecommerce-mini.onrender.com` (or similar)

---

## Step 3: Allow Render to Reach Your SQL Server

- **Azure SQL:** In Azure Portal → Firewall → Add `0.0.0.0/0` or Render's IP ranges
- **Self-hosted:** Ensure firewall allows inbound on port 1433 from Render's outbound IPs

Render's outbound IPs: [Render docs - Outbound IPs](https://render.com/docs/outbound-ip-addresses)

---

## Blueprint (Optional)

If using `render.yaml`, create the service manually first, then add the `ConnectionStrings__DefaultConnection` as a **Secret** in Render Dashboard (Render will not sync secret env vars from the blueprint).

---

## Default Login Credentials

- **Admin:** admin@shop.com / Admin@123  
- **Customer:** customer@shop.com / Customer@123

Change these after first login in production.
