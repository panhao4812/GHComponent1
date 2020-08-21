set path1=C:\Users\Administrator\Documents\GitHub\GH_DataView_Component\GH_DataView_Component\bin\Debug\
set path2=C:\Users\Administrator\AppData\Roaming\McNeel\Rhinoceros\5.0\Plug-ins\Grasshopper (b45a29b1-4343-4035-989e-044e8580d9cf)\0.9.76.0\Components
set path3=C:\Program Files\Rhinoceros 5 (64-bit)\System\Rhino.exe
ren "%path1%GH_DataView_Component.dll" "GH_DataView_Component.gha"
move "%path1%GH_DataView_Component.gha" "%path2%"
start "" "%path3%"
Pause