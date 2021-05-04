git fetch
git checkout --track -b master origin/master
git pull
git checkout --track -b deploy origin/deploy
git pull
git branch
$changes = git diff deploy...master --name-only

foreach($file in $changes){
    Write-Host $file
}

foreach($file in $changes){
    if($null -ne ("design/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]Design"
    break
    }
}

foreach($file in $changes){
    if($null -ne ("build/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]Build"
    break
    }
}

foreach($file in $changes){
    if($null -ne ("test/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]Tests"
    break
    }
}

foreach($file in $changes){
    if($null -ne ("src/E13.Common.Api/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]Api"
    Write-Host "##vso[task.setvariable variable=includesApi]true"
    Write-Host "variable $env:includesApi"
    break
    }
}

foreach($file in $changes){
    if($null -ne ("src/E13.Common.AspNet/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]AspNet"
    Write-Host "##vso[task.setvariable variable=includesAspNet]true"
    break
    }
}

foreach($file in $changes){
    if($null -ne ("src/E13.Common.Blazor/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]Blazor"
    Write-Host "##vso[task.setvariable variable=includesBlazor]true"
    break
    }
}

foreach($file in $changes){
    if($null -ne ("src/E13.Common.Core/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]Core"
    Write-Host "##vso[task.setvariable variable=includesCore]true"
    break
    }

foreach($file in $changes){
    if($null -ne ("src/E13.Common.Cli/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]Cli"
    Write-Host "##vso[task.setvariable variable=includesCli]true"
    break
    }
}

foreach($file in $changes){
    if($null -ne ("src/E13.Common.Data/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]Data"
    Write-Host "##vso[task.setvariable variable=includesData]true"
    break
    }
}

foreach($file in $changes){
    if($null -ne ("src/E13.Common.Domain/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]Domain"
    Write-Host "##vso[task.setvariable variable=includesDomain]true"
    break
    }
}

foreach($file in $changes){
    if($null -ne ("src/E13.Common.Logic/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]Logic"
    Write-Host "##vso[task.setvariable variable=includesLogic]true"
    break
    }
}

foreach($file in $changes){
    if($null -ne ("src/E13.Common.Nunit.Api/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]Nunit_Api"
    Write-Host "##vso[task.setvariable variable=includesNunitApi]true"
    break
    }
}

foreach($file in $changes){
    if($null -ne ("src/E13.Common.Nunit.UI/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]Nunit_UI"
    Write-Host "##vso[task.setvariable variable=includesNunitUI]true"
    break
    }
}

foreach($file in $changes){
    if($null -ne ("src/E13.Common.Nunit/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]Nunit"
    Write-Host "##vso[task.setvariable variable=includesNunit]true"
    break
    }
}

foreach($file in $changes){
    if($null -ne ("src/E13.Common.RestEase/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]RestEase"
    Write-Host "##vso[task.setvariable variable=includesRestEase]true"
    break
    }
}

foreach($file in $changes){
    if($null -ne ("src/E13.Common.Infra/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]Infra"
    Write-Host "##vso[task.setvariable variable=includesInfra]true"
    break
    }
}

foreach($file in $changes){
    if($null -ne ("src/E13.Common.React/"| ? { $file.StartsWith($_) })){
    Write-Host "##vso[build.addbuildtag]React"
    Write-Host "##vso[task.setvariable variable=includesReact]true"
    Write-Host "variable $env:includesReact"
    Write-Host "variable $env:INCLUDESREACT"
    break
    }
}