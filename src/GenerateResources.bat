echo Generate String Resources
Resxible.exe -t="android" -c="Common.resx" -m="Master.resx" -d="..\..\Android\Resources\Values\Strings.xml"
Resxible.exe -t="ios" -c="Common.resx" -m="Master.resx" -d="..\..\iOS\en.lproj\Localizable.strings"
PAUSE