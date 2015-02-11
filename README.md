# XYZ

##What's XYZ?
XYZ is a simple tool to generate automatically **several** platform-dependent resource files from a **single** RESX file.

##Installation

###from the nuget package
* add the nuget package XYZ to your common project which contains the RESX file

###from the binaries
* download the latest binaries here and unzip the content into your common project which contains the RESX file

##Resources Generation
* modify the Generate.sh and/or the Genearte.bat files under the *Localization* folder to target your resx file and the correct path for your iOS and Android projects
* execute the script

##Usage

XYZ.exe [-t] [-a] [-m] [-d] [-b]

Options: 

- **-t|target** : ios or android (required)
- **-c|common** : common resx file (optional). If you have a resx file containnning default values for different projects. Those values will be overriden if they exist in the main resx file (see below)
- **-m|master** : main resx file (required)
- **-d|destination** : destination path and file name to be genearetd (required)
- **-b|backup** : backup file (optional)

Example:
```Batchfile
XYZ.exe -t=android -a="Common.resx" -m="MyApp.resx" -d="..\..\MyApp.Droid\Resources\Values\Strings.xml"
XYZ.exe -t=ios -a="Common.resx" -m="MyApp.resx" -d="..\..\MyApp.iOS\en.lproj\Localizable.strings"
```

##Know limitations and future improvements

One 

##But... it doesn't support XZY or I found a bug
Don't hesitate to create an [issue](https://github.com/apcurium/amp-tool/issues) or better, fork it and send us a pull request!
