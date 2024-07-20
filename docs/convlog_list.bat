@REM このスクリプトも Bing AI に生成させてみようとしましたが、
@REM 上手く動作しなかったので自分で作成しました。
@ECHO OFF

PUSHD "%~dp0"
WHERE dotnet.exe >NUL 2>&0
IF %ERRORLEVEL% == 0 (
	CALL dotnet.exe run --project ../tools/ConvLogListGen/ConvLogListGen.csproj -- %*
) ELSE (
	ECHO .NET の実行環境が見付かりません。 >&2
)
POPD
