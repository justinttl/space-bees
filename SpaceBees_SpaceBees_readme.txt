# SpaceBees
Members: Jon, Zeke, Justin, Yize

Starting Scene: StartingMenu

## Controls
For gamepad:
    - Movement is Left Stick
    - Look is Right Stick
    - Jump is Button South (A)
    - Dodge roll is Button East (B)
    - Punch is Button West (X)
    - Kick is Button North (Y)
    - Throw Projectile is Right Trigger
    - Pause is Start Button

For keyboard & mouse:
    - Movement is WASD
    - Look is Mouse
    - Jump is Space
    - Dodge roll is Shift
    - Punch is Left Click
    - Kick is Right Click
    - Throw Projectile is F
    - Pause is Esc

The game starts with a narrative cutscene, transitions to tutorial level (if selected), then Level_1 and then Level_2.
You can view the credits from the StartingMenu, or by beating the game.
The goal is to run around as the character and eliminate bees and progress towards killing the boss - Big Buzz, the Chonk.

## Where to Observe Rubric Requirements:
### It must be a 3D game feel game!
    - This is a third person action game with dynamic camera control.
    - 3D game feel can be experienced by running and looking around the level.
    - Jump, punch, kick, directional running, and double jump add to a smooth game feel experience.
    - Success and failure is (currently) determined by kill or be(e) killed.
### Precursor to Fun Gameplay
    - Player has interesting choices in combat, chosing either punching or kicking, or throwing ranged attacks.
    - Player engagement in the world with ability powerups.
    - Varied enemy attacks that require real time dodging.
    - Multi phase boss encounter that is challenging.
### 3D Character with Real-Time Control
    - Multiple animation blends for smooth character control.
    - Movements are root motion so there's no moonwalking or sliding.
    - Decouple looking and moving vectors allows player to execute directional attacks and dodging intuitively.
    - Camera control follows the character smoothly.
    - Character control has low latency.
### 3D World with Physics and Spatial Simulation
    - Hexagon floor tiles and space gems reflect the SpaceBee theme.
    - Graphics and align with the physical representation to minimize clipping.
    - Projectiles interacts with obstacle and ranged enemies will chase player to gain line of sight.
    - Explosive damage radius mechanic
    - Attacks have knockback effects
    - Realistic position based sound effects
    - Player is unable to leave the intended game space. Leaving the hexagons kills the player.
### Real-time NPC Steering Behavior / Artificial Intelligence
    - AI states for follow and attack.
    - Fluid transitions between AI states and animations.
    - Smooth steering and locomotion of NPCs.
### Polish
    - Start menu, pause menu, and quit menu are all available in GUI.
    - Style for levels, characters, and GUI is consistent.
    - A complete tutorial section introduces player to controls and the objective.
    - Camera panning level introduction makes encounters feel dynanmic and exciting.
    - Realtime spectral lighting gives metalic materials a sci-fi look and feel.
    - Particle effects are used to give incoming projectile a sense of directionality and impact
### Fun / Engaging / Immersiveness / Emotional Response / Gestalt / Flow
    - Narrative cutscene hooks player to the story.
    - There is something quite comical about the light hearted and somewhat absurd game concept!
    - Music and sound effects give player actions tangible feedback.
    - Level design is highly polished to remain immersive.

## Known Problem Areas
    - We originally intended to create rogue-like dynamic multilevel game with an ability progression tech tree system.
      - Did not manage to build the full original scope but glad we focused our effort in building an engaging boss fight and adding necessary polish.

## Manifest
    - The below assets were primarily developed by the team member listed but it was not strictly enforced and there are areas of overlap.
    - Zeke worked on camera control, character moving and attacking animation, narrative scenes, powerups and end to end game flow.
    - Justin worked on level design, boss encounter, enemy AI (ranged bee and aggro mechanics), lighting, stats system and balance.
    - Jon worked on attack animations, hit and hurt box mechancis, tutorial level and more
    - Yize worked on movement animations, enemy AI (attack and chase mechanics), common gameplay script interfaces and more.

### Scripts Credits
    We have quite a lot of scripts but here is a high level breakdown on some of the files

    Enemy Scripts
    - Assets/Enemy Scripts/Interfaces/* by Yize
    - Assets/Scripts/Enemy Scripts/<BaseBee, MeleeBee, RangedBee>/* by Yize and Justin
    - Assets/Scripts/Enemy Scripts/BossBee/* by Justin

    Combat
    - Assets/Scripts/Combat/* by Justin
    - Assets/Scripts/DamangeNumberController by Zeke
    - Assets/Scripts/PowerUpController.cs by Zeke

    Player
    - RootMotionControl.cs by Zeke, Yize and Jon
    - Assets/Scripts/PlayerScripts/* by Zeke and Jon
    - Assets/Scripts/PlayerScripts/PlayerStats by Justin

    HUD and Menu
    - Assets/Scripts/PlayerScripts/HealthBar.cs by Zeke
    - Assets/Scripts/BeeCounterController.cs by Zeke

    Levels
    - Assets/Scripts/TutorialMode.cs by Jon
    - Assets/Scripts/CreditsController.cs by Zeke
    - Assets/Scripts/Levels/LevelManger.cs by Justin
    - Assets/Scripts/Levels/DoorController.cs by Justin

    Misc (Dolly Cam, Animation glue code, HUDs and Menus)
    - Assets/Scripts/AnimationManger.cs by Zeke
    - Assets/Scripts/DollyCamEventEmitter.cs by Zeke
    - Menu's controller by Zeke and Justin
