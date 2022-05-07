Write-Output "-------"
for ($i = 1; $i -le 12; $i++) {
  if ($i -lt 10) {
    $str_bin, $str_obj, $str_readme = ".\Lab_0$i\bin\", ".\Lab_0$i\obj\", ".\Lab_0$i\README.md"
    try { Remove-Item -Path $str_bin -Recurse -Force -ErrorAction Stop; Write-Output "Bin 0$i removed" } catch { Write-Output "Folder bin is not present."}
    try { Remove-Item -Path $str_obj -Recurse -Force -ErrorAction Stop; Write-Output "Obj 0$i removed" } catch { Write-Output "Folder obj is not present."}
    try { Remove-Item -Path $str_readme -Force -ErrorAction Stop; Write-Output "README Lab_0$i removed" } catch { Write-Output "README Lab_0$i is not present" }
    Write-Output "-------"
  } else {
    $str_bin, $str_obj, $str_readme = ".\Lab_$i\bin\", ".\Lab_$i\obj\", ".\Lab_$i\README.md"
    try { Remove-Item -Path $str_bin -Recurse -Force -ErrorAction Stop; Write-Output "Bin $i removed" } catch { Write-Output "Folder bin is not present."}
    try { Remove-Item -Path $str_obj -Recurse -Force -ErrorAction Stop; Write-Output "Obj $i removed" } catch { Write-Output "Folder obj is not present."}
    try { Remove-Item -Path $str_readme -Force -ErrorAction Stop; Write-Output "README Lab_$i removed" } catch { Write-Output "README Lab_$i is not present" }
    Write-Output "-------"
  }
}
