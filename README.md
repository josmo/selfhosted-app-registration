# selfhosted-app-registration
A POC on how to have a C#/Sql Server backed self hosted application registration service
# C# Template for services - 

## Tech to know about

* Nancy FX
* .NET Core 2
* Entity Framework - might switch for speed
* Docker

## Tools needed

* Docker-machine

## To get started


# Mac or linux
```sh
drone exec
docker build -t service .
docker run -e SQL_HOST=sqlhost -e SQL_USER=sa -e SQL_PASSWORD=mypassword service
```

# Db to know about

## EF database create

```
dotnet restore
dotnet migrations add InitialMigration -v
dotnet ef database update -v
```


