echo Generate String Resources
Resxible.exe -t="android" -c="Common.resx" -m="Master.resx" -d="..\..\Sample.Android\Resources\Values\Strings.xml"
Resxible.exe -t="ios" -c="Common.resx" -m="Master.resx" -d="..\..\Sample.iPhone\en.lproj\Localizable.strings"
PAUSE