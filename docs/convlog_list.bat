@REM ���̃X�N���v�g�� Bing AI �ɐ��������Ă݂悤�Ƃ��܂������A
@REM ��肭���삵�Ȃ������̂Ŏ����ō쐬���܂����B
@ECHO OFF

PUSHD "%~dp0"
WHERE dotnet.exe >NUL 2>&0
IF %ERRORLEVEL% == 0 (
	CALL dotnet.exe run --project ../tools/ConvLogListGen/ConvLogListGen.csproj -- %*
) ELSE (
	ECHO .NET �̎��s�������t����܂���B >&2
)
POPD