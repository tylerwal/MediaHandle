param(
	[System.IO.DirectoryInfo]
	$MovieDirectory,
	
	[string]
	$OutputDirectory = 'C:\MediaHandleTest\'
)

function Write-Info($title, $value, $titleColor = 'Green')
{
	Write-Host "$title - " -ForegroundColor $titleColor -NoNewline
	Write-Host $value -ForegroundColor DarkGray
}

# length of $MovieDirectory is used to remove the path
$movieDirectoryStringLength = $MovieDirectory.FullName.Length

# get FullName's of all files that are not directories
$fullNames = Get-ChildItem -Path $MovieDirectory -Recurse |
	Where-Object { $_.PSIsContainer -eq $false } |
	% { $_.FullName }
	
$files = @()
	
# remove the $MovieDirectory
foreach ($fullName in $fullNames)
{
	$files += $fullName.Remove(0, $movieDirectoryStringLength)
}

# if output directory doesn't exist, create it
if (!(Test-Path $OutputDirectory))
{
	New-Item -Path $OutputDirectory -Type Directory | Out-Null
}

# create new file path
$outputFile = Join-Path -Path $OutputDirectory -ChildPath 'MediaHandleTest.txt'

[System.IO.File]::WriteAllLines($outputFile, $files)

Write-Info 'File Created' $OutputFile