Write-Output "-------"
for ($i = 1; $i -le 12; $i++) {
  if ($i -lt 10) {
    $str_bin, $str_obj = ".\Lab_0$i\bin\", ".\Lab_0$i\obj\"
    try { Remove-Item -Path $str_bin -Recurse -Force -ErrorAction Stop; Write-Output "Bin 0$i deleted" } catch { Write-Output "Folder bin is not present."}
    try { Remove-Item -Path $str_obj -Recurse -Force -ErrorAction Stop; Write-Output "Obj 0$i deleted" } catch { Write-Output "Folder obj is not present."}
    Write-Output "-------"
  } else {
    $str_bin, $str_obj = ".\Lab_$i\bin\", ".\Lab_$i\obj\"
    try { Remove-Item -Path $str_bin -Recurse -Force -ErrorAction Stop; Write-Output "Bin $i deleted" } catch { Write-Output "Folder bin is not present."}
    try { Remove-Item -Path $str_obj -Recurse -Force -ErrorAction Stop; Write-Output "Obj $i deleted" } catch { Write-Output "Folder obj is not present."}
    Write-Output "-------"
  }
}
