$file=$args[0]
write-host "Ziping and generating md5 for manifest" + $file
$zip = $file -replace ".dll",".zip"
Compress-Archive -Force -LiteralPath $file -DestinationPath $zip

$md5 = New-Object -TypeName System.Security.Cryptography.MD5CryptoServiceProvider
$hash = [System.BitConverter]::ToString($md5.ComputeHash([System.IO.File]::ReadAllBytes($zip)))
$hash = $hash -replace "-",""
$outhash = $file -replace ".dll",".md5"
Set-Content -Path $outhash -Value $hash
write-host $hash