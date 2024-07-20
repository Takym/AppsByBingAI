@echo off
setlocal enabledelayedexpansion

REM PowerShell スクリプトの呼び出し
powershell -ExecutionPolicy Bypass -File convlog_list.ps1

echo 文書一覧が生成されました。
