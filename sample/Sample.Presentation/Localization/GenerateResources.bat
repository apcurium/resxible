﻿echo Generate String Resources
Amp.LocalizationTool.exe -t="android" -c="Common.resx" -m="Master.resx" -d="..\..\Sample.Android\Resources\Values\Strings.xml"
Amp.LocalizationTool.exe -t="ios" -c="Common.resx" -m="Master.resx" -d="..\..\Sample.iPhone\en.lproj\Localizable.strings"
PAUSE