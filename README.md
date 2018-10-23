# The SpatialOS GDK for Unity Blank Project readme (alpha)

This is a blank project that you can use to build your own game with the [SpatialOS GDK for Unity](https://github.com/spatialos/gdk-for-unity).

Ensure that the GDK for Unity is alongside the blank project side-by-side. You can use scripts to automatically do this or follow manual instructions.

##### Scripts
From the root of the `gdk-for-unity-fps-starter-project` repository:
    * If you are using Windows run: `powershell scripts/powershell/setup.ps1`
    * If you are using Mac run: `bash scripts/shell/setup.sh`

##### Manually
1. Clone the [GDK for Unity](https://github.com/spatialos/gdk-for-unity) repository alongside the FPS Starter Project so that they sit side-by-side:
  |     |     |
  | --- | --- |
  | SSH | `git clone git@github.com:spatialos/gdk-for-unity.git` |
  | HTTPS | `git clone https://github.com/spatialos/gdk-for-unity.git` |

2. Navigate to the `gdk-for-unity` directory and checkout the pinned version which you can find in the `gdk.pinned` file, in the root of the `gdk-for-unity-blank-project` directory.
  - `git checkout <pinned_version>`

This should ensure that the GDK for Unity and the blank project share a common parent directory.
```
  <common_parent_directory>
      ├── gdk-for-unity-blank-project
      ├── gdk-for-unity
```

&copy; 2018 Improbable
