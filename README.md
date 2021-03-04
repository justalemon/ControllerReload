# ControllerReload<br>[![AppVeyor][appveyor-img]][appveyor-url] [![Patreon][patreon-img]][patreon-url] [![PayPal][paypal-img]][paypal-url] [![Discord][discord-img]][discord-url]

ControllerReload is a simple script for Grand Theft Auto V that allows you to reload the scripts loaded in ScriptHookVDotNet by pressing a specific number of controller buttons. It reloads the scripts by simulating the `Reload()` console command. You can configure the numbers of buttons that you need to press.

## Download

* [GitHub](https://github.com/justalemon/ControllerReload/releases)
* [5mods](https://www.gta5-mods.com/scripts/controllerreload)
* [AppVeyor](https://ci.appveyor.com/project/justalemon/controllerreload) (experimental)

## Installation

Drag and drop all of the files inside of the 7zip into your **scripts** directory.

## Usage

After installing the Mod, you can use the following button combination to reload your scripts:

* Xbox 360 and Xbox One Controller: RB and B
* DualShock 3 and DualShock 4: R1 and Circle
* Wii U Gamepad, Joy Cons and Pro Controllers: R and A

You can configure the buttons that you need to press via **ControllerReload.json** (if the file does not exists, run the mod at least once).

When you open the file, you will see something like this:

```js
{
  "controls": [
    "FrontendRb",
    "FrontendCancel"
  ]
}
```

To change the buttons, specify any of the names in the [Control enum](https://github.com/crosire/scripthookvdotnet/blob/main/source/scripting_v3/GTA/Control.cs). For example, to use LB + RB + Y (L1 + R1 + Triangle):

```js
{
  "controls": [
    "FrontendLb",
    "FrontendRb",
    "FrontendY"
  ]
}
```

[appveyor-img]: https://img.shields.io/appveyor/build/justalemon/controllerreload?label=appveyor
[appveyor-url]: https://ci.appveyor.com/project/justalemon/controllerreload
[patreon-img]: https://img.shields.io/badge/support-patreon-FF424D.svg
[patreon-url]: https://www.patreon.com/lemonchan
[paypal-img]: https://img.shields.io/badge/support-paypal-0079C1.svg
[paypal-url]: https://paypal.me/justalemon
[discord-img]: https://img.shields.io/badge/discord-join-7289DA.svg
[discord-url]: https://discord.gg/Cf6sspj
