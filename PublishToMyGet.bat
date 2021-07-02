@echo off

::create nuget_pub
if not exist nuget_pub (
    md nuget_pub
)

::clear nuget_pub
for /R "nuget_pub" %%s in (*) do (
    del %%s
)

::dotnet pack src/Ref.1.x -c Release -o nuget_pub
::dotnet pack src/Ref.2.x -c Release -o nuget_pub
::dotnet pack src/Ref.3.x -c Release -o nuget_pub
dotnet pack src/Ref.5.x -c Release -o nuget_pub --no-restore

for /R "nuget_pub" %%s in (*symbols.nupkg) do (
    del "%%s""
)

echo.
echo.

for /R "nuget_pub" %%s in (*.nupkg) do ( 
    dotnet nuget push "%%s" -s "Beta" --skip-duplicate
	echo.
)

pause