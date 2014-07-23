C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild ../Truncon.Collections.sln /p:Configuration=Release
nuget pack ../Truncon.Collections/Truncon.Collections.csproj -Properties Configuration=Release
nuget push *.nupkg
del *.nupkg