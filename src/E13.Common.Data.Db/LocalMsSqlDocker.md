## Install Docker with SQL Server instance (for local testing)
1.  Install Docker if it is not already on your machine 
- [Docker Getting Started](https://www.docker.com/get-started)

2. Open Powershell
3. Download the SQL Server docker image
```powershell
docker pull mcr.microsoft.com/mssql/server
```

4. Set up the Server
```powershell
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=P@ssword!" -p 1433:1433 --name <container_name> -d mcr.microsoft.com/mssql/server
```

5. Ensure that SQL Server services are not running on your system, stop and set them to manual start so they do not start again.

6. You can now log into the server using SSMS
```
Server Name: localhost,1433
User Id: sa
Password: P@ssword!
```
> ❗ Obviously 'P@ssword!' is not a secure password but local dev environments should be private and short-lived in my opinion
