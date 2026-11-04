# Unity scene setup (first playable test)

1. Create a URP 3D project.
2. Add a Plane (floor), a few Cube obstacles and directional light.
3. Create a Capsule named Player.
4. Add CharacterController, PlayerMotor, HealthComponent, WeaponController, and PlayerInputBridge.
5. Add a child Camera and assign it to PlayerInputBridge.aimCamera.
6. Create an empty object named CameraRig, add a Camera and ThirdPersonCameraRig, then assign the Player transform as target.
7. Create a Cube or Capsule named PracticeTarget; add HealthComponent and PracticeTarget.
8. Set the target's HealthComponent max health to 100.
9. For weapon hit tests, set the target layer in the weapon's hitMask.
10. Enter Play mode. WASD moves; left mouse fires; R reloads; hold right mouse to orbit camera.

Input note: the starter uses legacy mouse axes for camera orbit while movement/fire use the Input System package. If the project is set to Input System only, set Active Input Handling to Both in Player Settings or convert the camera axes to Input Actions.
