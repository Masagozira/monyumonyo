# Copilot Instructions - Monyo Game Project

## Project Overview
**Monyo** is a 2D platformer game built with **Unity** using C# and the **new Input System**. The project is a team effort with member-organized code architecture.

### Architecture
- **Member-organized structure**: Code is divided into `Assets/Member/{Hurusawa,Mori,Takahasi}/` folders
  - `Hurusawa/`: UI buttons, player camera, enemies, stage management
  - `Mori/`: Player character (Maru) AI, search mechanics, goal effects
  - `Takahasi/`: Enemy scripts
- **Core Systems**:
  - Input: `Assets/Input/InputActions.cs` (auto-generated from Unity InputActions)
  - Player Control: `Assets/Member/Hurusawa/Script/Player/PlayerMovement.cs`
  - Enemy AI: `Assets/Member/Mori/Scripts/Maru.cs` (pursues player)
  - UI Navigation: Button selection systems with gamepad support

## Key Patterns & Components

### 1. Input System Integration
- Uses **Unity's new Input System** with a C# wrapper class `InputActions`
- Instantiated in Awake: `_inputActions = new InputActions()`
- Common action maps: `Player`, `UI`
- Access actions: `_inputActions.UI.UpSelect.performed`, `_playerInput.actions["Jump"].triggered`

**Example** (`UpDownButtonSelection.cs`):
```csharp
_inputActions.UI.UpSelect.performed += ctx => SelectButton(currentButtonIndex - 1);
_inputActions.UI.Decision.performed += ctx => buttons[currentButtonIndex].onClick.Invoke();
```

### 2. Player Movement & Physics
- Uses **Rigidbody2D** with raycast-based ground detection
- Layer-based collision: `groundLayer`, `mushroomLayer`, `DestroyobjLayer`
- Jump mechanics check ground contact via raycast before allowing jump
- Sprite switching tied to grounded state (in-air vs landing animations)

**Key File**: `Assets/Member/Hurusawa/Script/Player/PlayerMovement.cs` (122 lines)

### 3. Enemy AI & Detection
- Enemy `Maru` tracks player via boolean flags: `_playerHere` (general detection), `_playerFrontHere` (forward detection)
- Multiple search scripts detect player proximity: `MaruSeach.cs`, `MaruFrontSeach.cs`, etc.
- Switches between patrol (speed ~2) and chase modes (speed ~3.5)

**Key File**: `Assets/Member/Mori/Scripts/Maru.cs` (210 lines)

### 4. UI & Scene Management
- Button navigation uses `InputActions` (controller d-pad Up/Down)
- Scene transitions wrapped in coroutines waiting for audio completion
- Scene names: "StageSample 1", "StageSample 2", "Start", etc.

**Example** (`ButtonManager.cs`):
```csharp
private IEnumerator LoadSceneAfterSound(string sceneName) {
    Audio.PlayOneShot(sound);
    yield return new WaitForSeconds(sound.length);
    SceneManager.LoadScene(sceneName);
}
```

## Development Conventions

### Naming
- Private fields: `_camelCase` (e.g., `_playerHere`, `_maruspeed`)
- Public fields: `camelCase` (e.g., `moveSpeed`, `jumpForce`)
- Classes: `PascalCase` inheriting `MonoBehaviour`

### Serialization
- Use `[SerializeField]` for inspector-accessible private fields
- Use `[Header("...")]` to organize inspector sections
- Example: `[SerializeField, Header("通常時の歩行スピード")] private float _maruspeed = 2;`

### Audio
- Dedicated `AudioSource` component reference
- Audio clips stored as `public AudioClip` properties
- Play sounds: `audioSource.PlayOneShot(clip)`

### Comments & Japanese Text
- Some comments are in Japanese; understand context without translation dependency

## Build & Scene Structure

### Solution
- **File**: `monyo.sln` (single project: `Assembly-CSharp.csproj`)
- Scenes are located in `Assets/Scenes/` (referenced by name in `SceneManager.LoadScene()`)

### Testing/Running
- Project is a Unity game—open in Unity Editor to test
- Common scenes: "StageSample 1", "StageSample 2", "Start"

## Common Tasks

1. **Add new input action**: Modify `Assets/Input/InputActions.inputactions` in Unity Inspector, then regenerate `InputActions.cs`
2. **Adjust player movement**: Edit `PlayerMovement.cs` properties (moveSpeed, jumpForce) or raycasts
3. **Modify enemy behavior**: Update `Maru.cs` or search scripts in `Assets/Member/Mori/Scripts/`
4. **Update UI navigation**: Extend `UpDownButtonSelection.cs` or `LeftRightButtonSelection.cs`
5. **Add scene transitions**: Use `ButtonManager.OnButtonPressed()` pattern with coroutine + audio

## Important Notes

- **InputActions.cs is auto-generated**—do not edit directly; modify the `.inputactions` asset in Unity
- **Physics layer names matter**: Ensure "Ground", "Mushroom", "Destroy" layers exist in ProjectSettings
- **Scene management**: Always reference scenes by exact name as seen in `SceneManager.LoadScene()`
- **Member code isolation**: Each member's scripts typically don't cross-reference—maintain loose coupling
