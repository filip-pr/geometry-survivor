# Geometry Survivors

## User documentation

### Running the game

#### Using the prebuilt release version

- Download the latest release
- Unzip it
- Run the `geometry-survivor.exe` (if you're using Linux or MAC, you're gonna need to build it yourself)

#### Using Unity editor

- Clone the repository
- If you haven't already, install Unity Hub
- Open the repository as a project inside the Unity Hub
- Click on `File` in the top navigation bar
- Select `Build And Run`

### Controls

#### Menu

Navigating through the menu is done with a mouse.

#### Game

Movement is controlled by WASD or the arrow keys.

Escape key can be used to pause the game.

### Gameplay

The gameplay consists of trying to survive for as long as you can against waves of enemies. With each attempt you gain certain amount of `Upgrade Points` (based on survived time and gained levels) which you can spend on permanent upgrades, those will help you survive for longer in your next attempt. There is not direct goal or a way to win, your objective is to survive for as long as you can.

## Developer Documentation
This section gives a concise overview of how the project is organized so one can navigate and/or extend it quickly.

### Project Structure (Scripts)
`Assets/_Scripts/` is split by responsibility:
- `Core/` – High‑level game flow and meta progression ( `GameManager`, `UpgradeManager`, `PauseHandler`). Handles run lifecycle, screen/camera transitions, upgrade point calculation, and persistence (through Unity's PlayerPrefs).
- `Player/` – Player-centric logic: movement (`PlayerController`), stats (`PlayerStats`), health/leveling (`PlayerHealth`, `PlayerLevel`, `PlayerExperienceMagnet`), inventory and item system (`PlayerInventory`, `PlayerItem` and concrete item implementations in `Player/Items/`).
- `Enemies/` – Enemy spawning and scaling (`EnemyManager`), per-enemy movement (`EnemyController`), health (`EnemyHealth`) and experience drops (`EnemyExperience`).
- `Map/` – Infinite procedural map generation and structure placement (`MapManager`, `MapStructureSpawnData`). Map tiles get spawned around a moving center (ie. the player) and are culled beyond a destroy radius.
- `Common/` – Reusable gameplay building blocks: damage / knockback (`DamageDealer`, `KnockbackDealer`), movement base class (`MovementController`), health base (`Health`), stat math (`StatModifier`), weighted RNG (`WeightedRandom`).
- `UI/` – Runtime UI helpers: timers, health bar following, etc.
- `Camera/` – Simple smooth follow behavior.

### Game mechanics

#### Map generation
`MapManager` maintains a square area of tiles around a `GenerationCenter`:
- Spawns tiles within `GenerationDistance`, destroys those beyond `DestroyDistance` (number of tiles is determined by the size of the tile sprite).
- Adds object decoration: for each tile it seeds `Random` with tile coords + `seedOffset` (per game session seed), then attempts up to `structureSpawnTries` placements. Each attempt passes a `structureSpawnChance` roll, picks a structure via weighted RNG (`MapStructureSpawnData.SpawnWeight`), and only places it if its sprite bounds don't overlap already placed ones.
To tweak density: lower `structureSpawnChance`, `structureSpawnTries`, or individual weights to make a specific structure more/less common.

#### Enemy spawning and scaling
Handled by `EnemyManager`:
- Maintains elapsed `runTime` and two spawn point pools: wave (`waveSpawnPoints`) and ambient (`nonWaveSpawnPoints`). Both accumulate each frame by `SpawnPointsGainRate = spawnPointMultiplier * (1 + runTime / 60)` split by `waveSpawnPointRatio`.
- Ambient spawns: when enough time since `lastSpawnTime` (respecting `maxSpawnRate`) and enough points for a randomly weighted `EnemySpawnData` cost, an enemy is spawned, its cost is deducted.
- Waves: every `waveInterval` seconds (integer count check) a wave triggers: health and damage multipliers increase (`enemyHealthIncreaseRate`, `enemyDamageIncreaseRate` as percentage multipliers), a main enemy type is chosen (weighted), then repeatedly spawned while wave points suffice.
- Actual enemy spawn positions are always at least `minSpawnDistance` and at most `maxSpawnDistance` away from the player (in Unity coordinates).

#### Player and item leveling
`PlayerLevel` tracks `Level`, XP, and required XP = `Level * experienceRequiredMultiplier`.
- Gaining XP: enemies drop `EnemyExperience` objects, the `PlayerExperienceMagnet` pulls them in and awards `Experience` on pickup.
- On level up, a popup is shown, notifying the player of what the level up effects were. Every level up heals player to full health and if not already with all items at maximum level, either new item is added (up to a maximum of 5) or an existing item is upgraded. Probability of an item dropping (both as a new item or for upgrade) is determined by its `DropWeight`, if an item is already at max level or 5 items are already held, drop weights are adjusted accordingly.
- Each item's level up does something a little different, but it mostly upgrades it's damage, knockback or attack speed.

### Adding new content

#### Items
Steps to add a new item:
1. Decide type:
	- Common, projectile weapon → derive from `ProjectileWeaponItem`.
	- Custom, somehow different weapon → derive from `PlayerItem`.
2. Implement the necessary abstract methods and properties and for custom item also it's working logic.
3. Create an empty object prefab (inside `Assets\Prefabs\Items`) and attach the script to it.
4. Add the prefab to a new `PlayerItemData` entry in the `PlayerInventory`'s `allItemsData` array and set an initial `DropWeight` (higher = more common).
5. You can also optionally increase `MaxLevel` and add `OnLevelUp()` logic to allow for more weapon scaling.

#### Enemies
To introduce a new enemy type:
1. Create prefab with at minimum: `EnemyController`, `EnemyHealth` (or something that derives from them), a `Collider2D` and a `SpriteRenderer`. You probably also want to add a `DamageDealer` script and possibly even `KnockbackDealer` script.
2. Set its tag to Enemy (or possibly something different if you want to allow enemy friendly fire).
3. Add the prefab as an `EnemySpawnData` entry in the `EnemyManager`'s `enemies` list, pick a `SpawnWeight` (relative frequency) and `SpawnPointCost` (higher = rarer / later appearance).

#### Map structures
Add decorative / obstacle structures by:
1. Creating a prefab with a `SpriteRenderer` sized appropriately (should fit within a single map tile—otherwise overlapping rejection will rise).
2. Adding a `MapStructureSpawnData` element in `MapManager.structures` with the prefab and a weight.

Currently there is not structure persistence, if player gets far enough (`DestroyDistance`) the object gets destroy and if they get close enough `GenerationDistance` it gets recreated. This is something that would need adjusting if ie. loot drop structures were to be added.

#### Permanent upgrades
Permanent (meta) upgrades are defined by `UpgradeHandler` components listed in `UpgradeManager.upgradeHandlers`.
Adding a new one:
1. Duplicate an existing upgrade UI element (with `UpgradeHandler`). Set a unique `UpgradeName` (used as PlayerPrefs key).
2. Configure `baseUpgradeCost`, `upgradeIncreaseStep`, and (optionally) `maxUpgradeLevel` if you expose it.
3. Register the handler in the `UpgradeManager`'s serialized list so it appears and updates.
4. Extend `PlayerStats.SetUpgradeModifiers` to look up the handler by `UpgradeName` and apply its `UpgradeAmount` to the relevant `StatModifier` (usually via `IncreaseMultiplier(amount / 100f)`).
