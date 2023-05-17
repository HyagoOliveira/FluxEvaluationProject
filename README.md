# Flux Evaluation Project

* Unity version: **2020.3.16**
* Last Standalone build: [TODO]

## Summary

This Unity project is for the Gameplay Programmer Evaluation for Flux Games.

All the legacy code was replaced for new scripts. Each new class is code-documented for the main public functions and fields/properties.

## Tasks

Bellow you can check all required tasks and its details and commits: 

### Double Jump

After the PlayerMotor component was created, only a [few changes were added][1] in order to active the double jump. 
A possible future feature could be to add multiple jumps in this same component.

### Change Color

![Player using random colors](/Images/PlayerRandomColors.png)

Some small classes were created. Each one has its own single responsibility:

* [PlayerMaterialColors][2]: it's a simple color container for the Player body, arms and legs.
* [PlayerColorsData][3]: a *ScriptableObject* containing an array of *PlayerMaterialColors*. A public functions is used to get a random Player color set.
* [PlayerMaterialColorSwapper][4]: component used inside *PlayerArmature* prefab, inside `/Geometry/Armature_Mesh` child. A unique random color for the Player arms, legs and body is selected from the local *PlayerColorsData* field when the game starts.
* [PlayerColorsDataEditor][5]: editor class responsible for create and display a custom Window. You can open it by selecting `Window/Flux Games/Player Colors`. 
When the windows is opened, a default *PlayerColorsData* asset is fetched. If none is found, a new one is created and it'll be used for the future openings.
This custom Window edits the *PlayerColorsData* asset used at runtime to swap colors.

* **Attacks Counter**:
* **Revert Resources.Load**:
* **Bug fixes**:

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
[6]: <>
