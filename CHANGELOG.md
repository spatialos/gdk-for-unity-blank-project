# Changelog

## Unreleased

## `0.3.4` - 2020-03-25

### Changed

- Upgraded to GDK for Unity version `0.3.4`
### Breaking Changes

- The blank project now uses [query-based interest](https://docs.improbable.io/reference/14.4/shared/authority-and-interest/interest/query-based-interest-qbi) instead of [chunk-based interest](https://docs.improbable.io/reference/14.4/shared/authority-and-interest/interest/chunk-based-interest-cbi). [#89](https://github.com/spatialos/gdk-for-unity-blank-project/pull/89)
- You must now pass in an `EntityId` to create a player `EntityTemplate`. [#90](https://github.com/spatialos/gdk-for-unity-blank-project/pull/90)

## `0.3.3` - 2020-02-14

### Changed

- Upgraded to GDK for Unity version `0.3.3`
- Updated the project app identifiers to `io.improbable.gdk.blankproject`. [#81](https://github.com/spatialos/gdk-for-unity-blank-project/pull/81)

## `0.3.2` - 2019-12-23

### Changed

- Upgraded to GDK for Unity version `0.3.2`
## `0.3.1` - 2019-11-25

### Changed

- Upgraded to GDK for Unity version `0.3.1`
## `0.3.0` - 2019-11-11

### Changed

- Upgraded to GDK for Unity version `0.3.0`
- Changed the default client network connection type from KCP to Modular UDP with compression enabled. [#77](https://github.com/spatialos/gdk-for-unity-blank-project/pull/77)

## `0.2.10` - 2019-10-15

### Changed

- Upgraded to GDK for Unity version `0.2.10`

## `0.2.9` - 2019-09-16

### Changed

- Upgraded to GDK for Unity version `0.2.9`

## `0.2.8` - 2019-09-03

### Changed

- Upgraded to GDK for Unity version `0.2.8`
- Upgraded the project to be compatible with `2019.2.0f1`.

## `0.2.7` - 2019-08-19

### Breaking changes

- Tidied up the project structure. [#68](https://github.com/spatialos/gdk-for-unity-blank-project/pull/68)
    - Moved `CreatePlayerEntityTemplate` to the new `EntityTemplates` static class.
    - Removed `Improbable.Gdk.TransformSynchronization` from the assembly definition.

### Added

- Created prefabs for `UnityClient`, `UnityGameLogic` and `MobileClient` workers under `Resources/Prefabs/Worker`. [#68](https://github.com/spatialos/gdk-for-unity-blank-project/pull/68)
- Created a new `EntityTemplates` static class. [#68](https://github.com/spatialos/gdk-for-unity-blank-project/pull/68)

### Changed

- Upgraded to GDK for Unity version `0.2.7`
- The **SpatialOS** > **Generate snapshot** button now generates a snapshot directly without displaying a pop-up window. [#68](https://github.com/spatialos/gdk-for-unity-blank-project/pull/68)

### Internal

- Removed old arguments from the worker JSON files. [#69](https://github.com/spatialos/gdk-for-unity-blank-project/pull/69)

## `0.2.6` - 2019-08-05

### Changed

- Upgraded to GDK for Unity version `0.2.6`.

### Added

- Added the `io.improbable.gdk.debug` package as a dependency. [#66](https://github.com/spatialos/gdk-for-unity-blank-project/pull/66)

## `0.2.5` - 2019-07-18

### Changed

- Changed manifest to use GDK Packages with NPM instead of sideloading.
- Upgraded to GDK for Unity version `0.2.5`

### Internal

- Split the `MobileClient` build into separate `iOS` and `Android` buildkite steps.

## `0.2.4` - 2019-06-28

### Changed

- Upgraded project to the new worker abstraction. [#56](https://github.com/spatialos/gdk-for-unity-blank-project/pull/56)

## `0.2.3` - 2019-06-12

### Changed

- Upgraded the project to be compatible with `2019.1.3f1`.
- Updated `GdkToolsConfiguration.json` to conform with the [schema copying change in the GDK](https://github.com/spatialos/gdk-for-unity/pull/953).
- Upgrade to Unity Entities preview.33
- The `default_launch.json` and `cloud_launch.json` launch configurations now uses the `w2_r0500_e5` template instead of the `small` template which was deprecated.

### Internal

- Disabled Burst compilation for all platforms except for iOS, because Burst throws benign errors when building workers for other platforms than the one you are currently using. [#52](https://github.com/spatialos/gdk-for-unity-blank-project/pull/52)
- Enabled Burst compilation for iOS, because disabling results in an invalid XCode project. [#52](https://github.com/spatialos/gdk-for-unity-blank-project/pull/52)

## `0.2.2` - 2019-05-15

### Breaking Changes

- Removed the `AndroidClientWorkerConnector` and `iOSClientWorkerConnector` and their specific scenes. You can now use the `MobileClientWorkerConnector` and its `MobileClientScene` to connect to a mobile device.
- Added the worker type `MobileClient` and removed the worker types `AndroidClient` and `iOSClient`.

### Added

- Added integration with the deployment launcher feature module.

## `0.2.1` - 2019-04-15

### Changed

- Updated to Unity version `2018.3.11`.
- Updated to GDK for Unity version `0.2.1`.
- Updated `CreatePlayerEntityTemplate` to work with latest player lifecycle Feature Module.

## `0.2.0` - 2019-03-19

### Changed

- Updated `BuildConfiguration` asset to work with latest build system changes.

## `0.1.5` - 2019-02-21

### Changed

- Upgraded the project to be compatible with `2018.3.5f1`.

## `0.1.4` - 2019-01-28

### Added

- Added support for connecting mobile clients to cloud deployments.

### Changed

- Upgraded the project to be compatible with `2018.3.2f1`.
- Updated all the launch configs to use the new Runtime.
- Upgraded to GDK for Unity version `0.1.4`.

## `0.1.3` - 2018-11-26

### Added

- Added a metric sending system for load, FPS and Unity heap usage.

### Changed

- Upgraded to GDK for Unity version `0.1.3`.

### Fixed

- Fixed an issue with the default snapshot that prevented the player lifecycle module from spawning a player.
- Fixed a bug where you could start each built-out worker only once on OSX.

## `0.1.2` - 2018-11-02

### Added

- Added a changelog.
- Added examples for mobile client-workers.

### Changed

- Upgraded to GDK for Unity version `0.1.2`

## `0.1.1` - 2018-10-31

The initial alpha release of the SpatialOS GDK for Unity.
