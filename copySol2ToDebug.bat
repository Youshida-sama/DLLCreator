cd %~dp0
@RD /S /Q "DLLCreator\bin\Debug\ForCompiling\"
xcopy "ForCompiling\*.*" "DLLCreator\bin\Debug\ForCompiling\" /E/Y/D