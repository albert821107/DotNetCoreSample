# install scoope
Set-ExecutionPolicy RemoteSigned -scope CurrentUser
Invoke-Expression (New-Object System.Net.WebClient).DownloadString('https://get.scoop.sh')

# install go-task
scoop bucket add extras
scoop install task
Scoop search task

# install dotnet-ef 
dotnet tool update --global dotnet-ef

# insatll choco
Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))

# insatll python
choco install python -y

# install mssql-scripter
pip install mssql-scripter