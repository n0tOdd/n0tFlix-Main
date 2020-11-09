$file=$args[1]
$Path=$args[0]
Set-Location $Path
write-host "Ziping and generating md5 for manifest" + $file

$zip = $file -replace ".dll",".zip"
if(Test-Path($zip)){
    Remove-Item -Path $zip -Force
}
$Dir = Get-ChildItem -Path $Path -include *.dll -recurse |
Where-Object { $_.VersionInfo.LegalCopyright -notmatch 'Microsoft' } 

foreach ($element in $Dir) {
   $element
}
Compress-Archive -Force -Path $Dir -DestinationPath $zip 
$md5 = New-Object -TypeName System.Security.Cryptography.MD5CryptoServiceProvider
$hash = [System.BitConverter]::ToString($md5.ComputeHash([System.IO.File]::ReadAllBytes($zip)))
$hash = $hash -replace "-",""
$outhash = $file -replace ".dll",".md5"
Set-Content -Path $outhash -Value $hash
write-host $hash