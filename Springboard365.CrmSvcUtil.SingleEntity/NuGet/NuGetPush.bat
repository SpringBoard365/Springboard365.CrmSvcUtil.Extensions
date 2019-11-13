SET packageVersion=1.0.0-alpha003

NuGet.exe pack ../Springboard365.CrmSvcUtil.SingleEntity.csproj -Build -symbols -Version %packageVersion%

NuGet.exe push Springboard365.CrmSvcUtil.Extensions.%packageVersion%.nupkg -Source https://api.nuget.org/v3/index.json

pause