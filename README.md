# Flux Evaluation Project

* Unity version: **2020.3.16**
* Last Standalone build: [TODO]

## Summary

This Unity project is an evaluation for the Gameplay Programmer position at Flux Games.

All the legacy code was replaced for new scripts. Each one is code-documented for the main public functions and fields/properties.

Also, I did small changes for each commit to better reference those changes.

## Tasks

In the next sections you'll check all required tasks and its details and commits.

## Use new scripts for the Player

Although not required, I decided to create new scripts for the Player since the given ones were very coupled, confused and hard to work upon.

I created 3 main scripts which encapsulate only one main functionality:

1. [PlayerInputHandler][17]: use the new Input System to receive and process the inputs, forwarding them into `PlayerMotor` actions.
2. [PlayerMotor][18]: updates the physics (using a local CharacterController) and the `PlayerAnimator` according with the received inputs.
3. [PlayerAnimator][19]: deal only with the Player animations inside the AnimationController asset.

With those scripts, it become easy to change or add new features since each one does only one thing alone, as the Single Responsibility Principle (from SOLID) advise us.

## Double Jump

After the PlayerMotor component was created, only a [few changes were added][1] in order to active the double jump. 
A possible future feature could be to add multiple jumps in this same component.

## Change Color Randomly

![Player using random colors](/Images/PlayerRandomColors.png)

Some small classes were created. Each one has its own single responsibility:

* [PlayerMaterialColors][2]: it's a simple color container for the Player body, arms and legs.
* [PlayerColorsData][3]: a *ScriptableObject* containing an array of *PlayerMaterialColors*. A public functions is used to get a random Player color set.
* [PlayerMaterialColorSwapper][4]: component used inside Player prefab, inside the `/Geometry/Armature_Mesh` child. A unique random color for the Player arms, legs and body is selected from the local *PlayerColorsData* field when the game starts.
* [PlayerColorsDataEditor][5]: editor class responsible for displaying a custom Window where new colors can be added or edited. You can open it by selecting `Window/Flux Games/Player Colors`.

When the windows is opened, a default *PlayerColorsData* asset is fetched. If none is found, a new one is created and it'll be used now on.

>**Note**: This custom Window edits the *PlayerColorsData* asset used at runtime to swap colors.

## Attacks Counter

![Player attacks counter](/Images/PlayerAttacksCounter.gif)

The *PlayerMotor* component has some events that are triggered in some special occasions such as when the Player kicks or punches.

Therefore, the [PlayerAttackCounterManager][6] component listens to the Player `OnKick` and `OnPunch` events, updating the counters UI using a simple animation.

## Revert Resources.Load

The Environment should be loaded using AssetBundles. To do this, I chose to use only the AssetBundles build-in package, without Addressables package nor Asset Bundle Browser tool.

My first approach was to load bundles locally using `AssetBundle.LoadFromFileAsync()` function. For this I created the [AssetBundleLocalLoader][16] component.

The problem with this approach is that the bundle files must be in the same machine where the game will run, i.e. the player should download these files beforehand. 
Alternatively, the bundles files could be inside some special project folder, like the `StreamingAssets`, which would lead to increase the build size.

With that in mind, I chose to download the bundles files remotely from a server at runtime, using the GitHub repository since the files will be stored in the project root folder.

The following steps were necessary:

1. [Moved Environment_Prefab out from Resources folder][10]
2. [Set an asset bundle name and variant into Environment_Prefab][11]
3. [Code a simple Menu Item to build all asset bundles][12]
4. [Build the bundles files into a folder inside the project root][13]
5. [Create a script to load any remote Asset Bundle using an URL][14]

Since loading any remote asset is an asynchronous operation, a script was necessary in order to activate the Player only when the `Environment_Prefab` was completed loaded.

The [AssetBundleWaiterActivator][15] component was created and attached into the Player prefab. It uses the events inside [AssetBundleRemoteLoader][14] to deactivate the player when the loading process starts and activate it back when the loading process finishes.

## Bug fixes

A lot of bugfixes and improvements were made when replacing the legacy code. However, some specifics bugfixes were required: 

1. **Infinite attack when holding attack button**: fixed when improvements were made at StarterAssetsThirdPerson AnimationController on [this commit][8].
2. **Player sometime jumps when kick attack is pressed**: fixed by removing `Random.Range` from `buttonY` property at [this commit][7].
3. **Fix ResourcesLoad component**: although it is not a required bugfix, [this fix][9] was necessary since it prevented to build the project.

## Other Improvements

* Added namespace for each scripts
* Added Assembly definition files for Runtime and Editor scripts
* Standardized both button input names and player actions
* Improved AnimationControler for the Player
* Improved Player Punch and Kick animation to increase feedback responsivity
* Added feature to Cancel jump when button is released
* Added a SpriteAtlas on UI images to decrease draw calls
* Added a Canvas component inside the UI Texts witch frequently changes

---

**Hyago Oliveira**

[GitHub](https://github.com/HyagoOliveira) -
[BitBucket](https://bitbucket.org/HyagoGow/) -
[LinkedIn](https://www.linkedin.com/in/hyago-oliveira/) -
<hyagogow@gmail.com>

[1]: <https://github.com/HyagoOliveira/FluxEvaluationProject/commit/d98d2713974317bc2df442fbadee2a8645fb1d93>
[2]: <https://github.com/HyagoOliveira/FluxEvaluationProject/commit/a16854238dc894cf5b479ea9986ca85e1b2f9c48>
[3]: <https://github.com/HyagoOliveira/FluxEvaluationProject/commit/fc71567e71489abe92b191e7258e79e8d8c53d7d>
[4]: <https://github.com/HyagoOliveira/FluxEvaluationProject/commit/bc5af1c4d11c7d6bd39c68504a9bf80dc7402bd8>
[5]: <https://github.com/HyagoOliveira/FluxEvaluationProject/commit/07f63ec6d94c29dfb3d452a9f0765e6c1dd53df8>
[6]: <https://github.com/HyagoOliveira/FluxEvaluationProject/commit/de0472bf8deb2637e244893891ddbf50e557477f>
[7]: <https://github.com/HyagoOliveira/FluxEvaluationProject/commit/9001ea6b1284b31f1fb921bd27fbb1dbbb8e1501>
[8]: <https://github.com/HyagoOliveira/FluxEvaluationProject/commit/c64658cd4e76388dd7b81b562c013f478511e4fc>
[9]: <https://github.com/HyagoOliveira/FluxEvaluationProject/commit/91908061a5969d98c61779ead606bb59d007f37b>
[10]: <https://github.com/HyagoOliveira/FluxEvaluationProject/commit/68e29a1fa4c4838cf349490c8153be756498241e>
[11]: <https://github.com/HyagoOliveira/FluxEvaluationProject/commit/1584d2445971052d3c018d1ca8dce675db8c74bc>
[12]: <https://github.com/HyagoOliveira/FluxEvaluationProject/blob/main/Assets/Editor/Scripts/CreateAssetBundles.cs>
[13]: <https://github.com/HyagoOliveira/FluxEvaluationProject/tree/main/AssetsBundles>
[14]: <https://github.com/HyagoOliveira/FluxEvaluationProject/blob/main/Assets/Scripts/AssetBundle/AssetBundleRemoteLoader.cs>
[15]: <https://github.com/HyagoOliveira/FluxEvaluationProject/blob/main/Assets/Scripts/AssetBundle/AssetBundleWaiterActivator.cs>
[16]: <https://github.com/HyagoOliveira/FluxEvaluationProject/blob/main/Assets/Scripts/AssetBundle/AssetBundleLocalLoader.cs>
[17]: <https://github.com/HyagoOliveira/FluxEvaluationProject/blob/main/Assets/Scripts/Player/Inputs/PlayerInputHandler.cs>
[18]: <https://github.com/HyagoOliveira/FluxEvaluationProject/blob/main/Assets/Scripts/Player/PlayerMotor.cs>
[19]: <https://github.com/HyagoOliveira/FluxEvaluationProject/blob/main/Assets/Scripts/Player/PlayerAnimator.cs>
[2]: <>
