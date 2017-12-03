if exist "%cd%\ServerManagerForms\bin\Debug\PokerWebClient\" RD /s /Q "%cd%\ServerManagerForms\bin\Debug\PokerWebClient\"

xcopy  /s /E /Y /exclude:%cd%\ServerManagerForms\exclude.txt %cd%\PokerWebClient\*.* %cd%\ServerManagerForms\bin\Debug\PokerWebClient\*.*