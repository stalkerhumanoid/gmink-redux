**_Looking for a maintainer! If you use this and are interested in keeping it up to date, message me!_**

# GMInk Redux
Revival of the GMInk GameMaker extension [originally made by GMWolf.](https://marketplace.gamemaker.io/assets/5852/gmink)

Acts as a bridge between the Ink narrative scripting language and the GameMaker game engine.

Updated by me ([@stalkerhumanoid](https://github.com/stalkerhumanoid)) to help get the game [Decade](https://store.steampowered.com/app/3272360/Decade/) working on newer versions of Proton.

## Main Changes:
- Updated to the latest .NET and Ink versions (9.0.x and 1.2.0 respectively)
- Now exports native shared libraries using built-in .NET functionality rather than 3rd party DllEXport package
- Added GitHub action which builds .dll for Windows _and_ .so for Linux
- Includes ink-runtime-engine.dll file locally rather than relying on package
