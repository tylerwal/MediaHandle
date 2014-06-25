param(
	[System.IO.FileInfo]
	$FileList = 'D:\MediaHandleTest\MediaHandleTest.txt',
	
	[string]
	$OutputDirectory = 'D:\MediaHandleTest\'
)

function Write-Info($title, $value, $titleColor = 'Green')
{
	Write-Host "$title - " -ForegroundColor $titleColor -NoNewline
	Write-Host $value -ForegroundColor DarkGray
}

if (!(Test-Path -LiteralPath $FileList))
{
	Write-Host 'File List Not Found.' -ForegroundColor Red
	Exit
}

$lines = [System.IO.File]::ReadAllLines($FileList)

foreach ($line in $lines)
{
	$fullName = Join-Path -Path $OutputDirectory -ChildPath $line
	
	$directory = Split-Path $fullName
	
	# use LiteralPath parameter to deal with paths with spaces (and other escape characters)
	if (!(Test-Path -LiteralPath $directory))
	{
		New-Item -Path $directory -Type Directory | Out-Null
		Write-Info "Directory Created" $directory
	}
	
	New-Item -Path $fullName -Type File -Force | Out-Null
	$fileCreated = Get-Item -LiteralPath $fullName
	Write-Info "File Created" $fileCreated.Name 'Blue'
}