# 設定ファイルの読み込み
$config = Get-Content -Raw -Path "config.json" | ConvertFrom-Json

# パラメータの設定
$output = $config.output
$title = $config.title
$extension = $config.extension

try {
    # 出力ファイルの初期化
    "# $title" > $output
    "" >> $output

    # 文書ファイルの検索と処理
    Get-ChildItem -Recurse -Filter "*.$extension" | ForEach-Object {
        $filename = $_.BaseName
        if ($filename -match "(\d{4})/(\d{2})/(\d{2})/(\d{4})\..*") {
            $yyyy = $matches[1]
            $MM = $matches[2]
            $dd = $matches[3]
            $xxxx = $matches[4]

            # 題名の抽出
            $title = Select-String -Path $_.FullName -Pattern "^# " | Select-Object -First 1
            if ($title) {
                $title = $title.Line -replace "^# ", ""
                # Markdown 形式で出力
                Add-Content -Path $output -Value ("- [$title]($($_.FullName)) - $yyyy/$MM/$dd")
            }
        }
    }

    Write-Output "文書一覧が $output に生成されました。"
} catch {
    Write-Error "エラーが発生しました: $_"
}
