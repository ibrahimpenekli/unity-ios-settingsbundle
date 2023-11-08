# iOS Settings Bundle for Unity
Enables Unity developers to add their app preferences into the iOS Settings app.
For more information about Settings Bundle, see Apple’s documentation on [Implementing an iOS Settings Bundle](https://developer.apple.com/library/archive/documentation/Cocoa/Conceptual/UserDefaults/Preferences/Preferences.html).

## Install via Unity Package Manager:
* Add `"com.inscept.settingsbundle": "https://github.com/ibrahimpenekli/unity-ios-settingsbundle.git#1.0.0"` to your project's package manifest file in dependencies section.
* Or, `Package Manager > Add package from git URL...` and paste this URL: `https://github.com/ibrahimpenekli/unity-ios-settingsbundle.git#1.0.0`

## How to Use?

Settings Bundle and preference elements are assets. You can create them via `iOS > Settings Bundle`.
Alternatively you can create your Settings Bundle on post build process manually. Please see examples in `Assets/Editor/`