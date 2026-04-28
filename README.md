# 🎮 2D Platformer — Unity

A fast-paced 2D platformer built in Unity, featuring smooth player movement, checkpoints, portals, camera tracking, and obstacle-based death mechanics.

![image alt](https://github.com/Chandan-Baskey/SaveMe-2dGame/blob/d6e476d886aed06e384cd267e21cda7e1b68326f/GAME%20VIEW.jpg)
---

## 📁 Project Structure

```
Assets/
└── Scripts/
    ├── PlayerControl.cs    # Player movement & wall detection
    ├── GameControl.cs      # Death, respawn & checkpoint logic
    ├── Checkpoint.cs       # Checkpoint trigger & state management
    ├── Portal.cs           # Teleportation portal logic
    └── CameraControl.cs    # Smooth follow camera with bounds
```

---

## 🕹️ Gameplay Features

- **Hold-to-move** — Player accelerates while the mouse button is held down
- **Auto wall-flip** — Player automatically reverses direction upon hitting a wall
- **Checkpoint system** — Touch a flag/checkpoint to update your respawn position
- **Portals** — Step into a portal and teleport to its linked destination
- **Obstacle death** — Colliding with an obstacle triggers a shrink-and-respawn animation
- **Scene progression** — Reaching the finish line loads the next scene
- **Moving platforms** — Player velocity accounts for platform movement

---

## ⚙️ Script Overview

### `PlayerControl.cs`
Handles all player movement logic.

| Field | Description |
|-------|-------------|
| `speed` | Base horizontal speed |
| `acceleration` | How quickly speed ramps up/down |
| `wallLayer` | Layer mask used for wall detection |
| `wallCheckPoint` | Transform used as the wall-check origin |
| `wallCheckSize` | Size of the overlap box for wall detection |

- Uses `Input.GetMouseButton(0)` for hold detection
- Uses `Physics2D.OverlapBox` to detect walls and flip the player
- Adds platform velocity when `isOnPlatform` is true

---

### `GameControl.cs`
Attached to the Player. Manages death, respawn, and level transitions.

- On collision with `"Obstacle"` tag → calls `Die()`, which shrinks the player and respawns after a short delay
- On collision with `"Finish"` tag → loads the next scene via `SceneManager`
- `UpdateCheckpoint(Vector2 pos)` — called by `Checkpoint.cs` to store the latest respawn position

---

### `Checkpoint.cs`
Attached to each checkpoint object in the scene.

- Requires a `respawnPoint` Transform (set in Inspector) for precise respawn placement
- On player trigger:
  - Updates the respawn position in `GameControl`
  - Swaps sprite from `passive` → `active`
  - Disables its own collider to prevent re-triggering

---

### `Portal.cs`
Handles bidirectional teleportation.

- Requires a `destination` Transform (linked to another Portal)
- Uses a `HashSet` to prevent teleport loops (player exits before being re-teleportable)
- Automatically registers the collision object with the destination portal on entry

---

### `CameraControl.cs`
Smooth follow camera with clamped bounds.

| Field | Description |
|-------|-------------|
| `smoothTime` | SmoothDamp factor `[0–1]` |
| `positionOffset` | Offset from player position |
| `xLimits` | Min/max X camera position |
| `yLimits` | Min/max Y camera position |

- Uses `Vector3.SmoothDamp` for fluid movement
- Camera Z is fixed at `-10` for 2D rendering

---

## 🏷️ Required Tags & Layers

| Tag / Layer | Used By |
|-------------|---------|
| `"Player"` | `Checkpoint.cs`, `CameraControl.cs` |
| `"Obstacle"` | `GameControl.cs` |
| `"Finish"` | `GameControl.cs` |
| Wall Layer (custom) | `PlayerControl.cs` |

---

## 🚀 Getting Started

1. Clone the repository
2. Open the project in **Unity 2021.3+** (or your target version)
3. Assign required references in the Inspector:
   - `Checkpoint` → `respawnPoint`, `passive` sprite, `active` sprite
   - `Portal` → `destination` Transform
   - `PlayerControl` → `wallLayer`, `wallCheckPoint`
   - `CameraControl` → `xLimits`, `yLimits`, `positionOffset`
4. Tag the Player GameObject with `"Player"`
5. Set obstacle and finish objects with their respective tags
6. Hit **Play** ▶

---

## 📌 Notes

- The `GameControl` script must be on the same GameObject as the Player (or have its tag set to `"Player"`)
- Checkpoints are **one-use** — the collider disables itself after activation
- Portals require **two** Portal components — each pointing to the other as its `destination`
- Ensure your Wall layer is correctly assigned in the Physics 2D settings

---

## 📄 License

This project is open for personal and educational use.
