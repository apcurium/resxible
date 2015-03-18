# resxible

##What's resxible?
resxible is a simple tool to generate automatically **several** platform-dependent resource files from a **single** RESX file.

##Overview

We are using resxible in our Xamarin based projects but it's compatible for any cross-platform solutions. From one or several resx files it generates the native files needed to localize your application. We are working on improving the developer experience (aka Visual/Xamarin Studio integrations)

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
- **-d|destination** : destination path and file name to be generaetd (required)
- **-b|backup** : backup file (optional)

Example:
```Batchfile
resxible.exe -t=android -c="Common.resx" -m="MyApp.resx" -d="..\..\MyApp.Droid\Resources\Values\Strings.xml"
resxible.exe -t=ios -c="Common.resx" -m="MyApp.resx" -d="..\..\MyApp.iOS\en.lproj\Localizable.strings"
```

##FAQ

- Does resxible support several languages? Do I need to specify each language in the command line arguments?

Yes resixble is supporting several languages and you don't need to specify them in the script. By default resxible will look for others files with language suffixes and will generate the platform-specific artefacts accordingly. For example if you have a Master.resx file, resxible will also handle a Master.fr.resx file transparently.

- What's the common file argument for?

To specify a shared resx file between several projects to reuse generic strings. Any key present in your project specific file will override the one from this common source.

- Where can I find an example?

This video: https://www.youtube.com/watch?v=Fnv5Q4NLKQo or look into the sample folder in the repository.

##But resxible doesn't support XYZ or I found a bug
Don't hesitate to create an [issue](https://github.com/apcurium/amp-tool/issues) or better, fork it and send us a pull request!

##Contact

