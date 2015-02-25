# resxible

##What's resxible?
resxible is a simple tool to generate automatically **several** platform-dependent resource files from a **single** RESX file.

##Overview

We are using resxible in our Xamarin based projects but it's compatible for any cross-platform solutions. From one or several resx files it generates the native files needed to localize your application. We are working on improving the 

##Installation

###from the nuget package
* add the nuget package resxible to your common project which contains the RESX file

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
resxible.exe -t=android -c="Common.resx" -m="MyApp.resx" -d="..\..\MyApp.Droid\Resources\Values\Strings.xml"
resxible.exe -t=ios -c="Common.resx" -m="MyApp.resx" -d="..\..\MyApp.iOS\en.lproj\Localizable.strings"
```

##FAQ

- Does resxible support several languages? Do I need to specify each language in the command line arguments?

- What's the common file argument for?

- Where can I find an eample?

##But resxible doesn't support XYZ or I found a bug
Don't hesitate to create an [issue](https://github.com/apcurium/amp-tool/issues) or better, fork it and send us a pull request!
