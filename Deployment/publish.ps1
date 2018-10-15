&dotnet pack "..\Truncon.Collections\Truncon.Collections.csproj" --configuration Release --output $PWD

.\NuGet.exe push Truncon.Collections.*.nupkg -Source https://www.nuget.org/api/v2/package

Remove-Item Truncon.Collections.*.nupkg