# ESBreaker: PSO2es Fan translation Patch tool - Honest investigation edition.

## Installation/Usage

First of all, while we don't include any code by SEGA (because copyright issues), the application needs access to certain DLL's and files from the game to function, but those must be provided by the user.

*This version was built for pso2es 2.70 so earlier newer versions may not work without changes.*

### Source code Instructions

1. Download the PSO2es APK.
2. Unzip it, and look for assets/bin/Data/Managed directory.
3. Copy up the following dll's to the lib directory.
 
		Assembly-CSharp-firstpass.dll
		ContentsSerializer.dll
		UnityEngine.dll
		protobuf-net.dll
		ProtoBuffSerializer.dll

4. Inside the Databases directory, put the directories from *android/data/com.sega.phantasystaronline2es/files/3hwQzp8KE9T1oTpJCHPvxI5JIedD3AuT/* , they should copy automatically to the binary once you compile. *unless sega updates the names*
5. Once you load Visual Studio / Xamarin / etc. make sure you have the libs referenced correctly.
6. Do stuff.

### Binary instructions.

1. Download the PSO2es APK.
2. Unzip it, and look for assets/bin/Data/Managed directory.
3. Copy up the following dll's to the same directory the exe is.

		Assembly-CSharp-firstpass.dll
		ContentsSerializer.dll
		UnityEngine.dll
		protobuf-net.dll
		ProtoBuffSerializer.dll

4. Inside the Databases directory, put the directories from *android/data/com.sega.phantasystaronline2es/files/3hwQzp8KE9T1oTpJCHPvxI5JIedD3AuT/* 
5. Run the ESbreakerCLI.exe

## TODO:

* Use a faster JSON lib that also does prettyprint.
* Replace en_something for trans_something to allow multiple language translations without confusion.
* Add launch flags for people who don't want to upgrade the database or any other options.
* Bit of code cleanup. Some of the code dates back from the first ultrahacky ESbreaker version

## Credits

* SEGA - For the libs that power this tool and the game.
* PolCPP - Aka Rupikachu for the tool
* SynthSy - For being Test Subject number 0

## License

MIT License (MIT)
Copyright (c) 2016 PolCPP

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.