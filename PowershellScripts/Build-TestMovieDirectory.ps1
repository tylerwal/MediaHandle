param(
	[System.IO.FileInfo]
	$FileList = 'C:\MediaHandleTest\MediaHandleTest.txt',
	
	[string]
	$OutputDirectory = 'C:\MediaHandleTest\'
)

function Write-Info($title, $value, $titleColor = 'Green')
{
	Write-Host "$title - " -ForegroundColor $titleColor -NoNewline
	Write-Host $value -ForegroundColor DarkGray
}

$dataLength = 65536

function Create-HashCopy(
	[IO.Stream]$readStream, 
	[IO.Stream]$writeStream
	)
{
	$hashLength = 8
	[byte[]]$buffer = New-Object -TypeName byte[] -ArgumentList $hashLength
	
	$numberOfIterations = $dataLength / $hashLength
	
	$iteration = 0
	while (($iteration -lt $numberOfIterations) -and ($readStream.Read($buffer, 0, $hashLength) -gt 0))
	{
		$iteration++
		
		$writeStream.Write($buffer, 0, $hashLength)
	}
	
#	$n = 0
#	do
#	{
#		$iteration++
#		
#		$n = $readStream.Read($buffer, 0, $hashLength)
#		
#		$writeStream.Write($buffer, 0, $n)
#		
#	} while ($iteration -lt $numberOfIterations)
}

if (!(Test-Path -LiteralPath $FileList))
{
	Write-Host 'File List Not Found.' -ForegroundColor Red
	return
}

$lines = [System.IO.File]::ReadAllLines($FileList)

# first line just holds the root directory
$movieDirectory = $lines | Select-Object -First 1
$movieDirectoryStringLength = $movieDirectory.Length

$originalFilePaths = $lines | Select-Object -Last ($lines.Count - 1)

foreach ($originalFilePath in $originalFilePaths)
{
	$newFilePath = Join-Path -Path $OutputDirectory -ChildPath $originalFilePath.Remove(0, $movieDirectoryStringLength)
	
	$directory = Split-Path $newFilePath
	
	# use LiteralPath parameter to deal with paths with spaces (and other escape characters)
	if (!(Test-Path -LiteralPath $directory))
	{
		New-Item -Path $directory -Type Directory | Out-Null
		Write-Info "Directory Created" $directory
	}	
	
	New-Item -Path $newFilePath -Type File -Force | Out-Null
	$fileCreated = Get-Item -LiteralPath $newFilePath
	Write-Info "File Created" $fileCreated.Name 'Blue'	
	
	$originalFile = Get-Item -LiteralPath $originalFilePath
		
	if ($originalFile.Length -ge (10 * 1024 * 1024))
	{
		$readStream = [IO.File]::OpenRead($originalFilePath)
		$writeStream = [IO.File]::OpenWrite($newFilePath)
		
		# beginning of file
		Create-HashCopy $readStream $writeStream
		
		# adjust stream positions
		$readStream.Position = [Math]::Max(0, $readStream.Length - $dataLength)
		
		# end of file
		Create-HashCopy $readStream $writeStream
				
		$writeStream.Close()
		$readStream.Close()
		
		Write-Host 'Hash Copied' -ForegroundColor Cyan		
			
		$originalFileHash = . .\Get-Hash.ps1 $originalFilePath
		Write-Host $originalFileHash
		$newFileHash = . .\Get-Hash.ps1 $newFilePath
		Write-Host $newFileHash
		
		. .\Get-Hash-TwoFileComparison.ps1 $originalFilePath $newFilePath
		
		if ($originalFileHash -eq $newFileHash)
		{
			Write-Host 'Hashes match!' -ForegroundColor Green
		}
		else
		{
			Write-Host 'Hashes do not match!!!' -ForegroundColor Red
		}
	}
	else
	{
		#Write-Host 'Hash Skipped' -ForegroundColor Red
	}

}