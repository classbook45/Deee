# POCKET REALMS

A mixed-reality companion game for Meta Quest 3S. Your room becomes the play space, and a tiny animated AI creature explores it with you.

**Status:** Early prototype foundation; not yet a ready-to-install APK.

## Vision
- A polished holographic main menu in your real room.
- A tiny companion that spawns nearby, follows you, and explores safe room surfaces.
- Room understanding through Meta Scene API / MRUK after explicit player permission.
- Provider-neutral conversation adapters for ChatGPT, Claude, and Grok.
- Expressive animations, reactions, discoveries, and ambient effects.

## Target stack
- Meta Quest 3S, standalone Android
- Unity 6 LTS, URP
- OpenXR + Meta XR SDK
- Meta Scene API + Mixed Reality Utility Kit (MRUK)

## Setup
1. Install Unity 6 LTS with Android Build Support, SDK/NDK tools, and OpenJDK.
2. Open this repository as a Unity project.
3. Install XR Plug-in Management, OpenXR Plugin, XR Interaction Toolkit, and AR Foundation using versions compatible with your editor.
4. Import Meta XR All-in-One SDK and MRUK through Meta's official Unity package distribution.
5. Enable OpenXR for Android and configure Meta Quest features. Set Android, ARM64, and the appropriate Quest minimum API level.
6. Enable passthrough and Scene permissions. Request room/scene access at runtime and explain the request to the player.
7. Create a scene with an XR Origin, passthrough layer, world-space canvas, companion prefab, and RoomWorldBootstrap component.
8. Build and test on Quest 3S. Room scanning must be approved in-headset.

## Code foundation
- Assets/Scripts/Companion/CompanionController.cs: movement, gaze, bobbing, mood, and celebration.
- Assets/Scripts/World/RoomWorldBootstrap.cs: safe spawn placement and validated exploration-point registration.
- Assets/Scripts/UI/HolographicMenuController.cs: animated menu entrance and menu events.

The MRUK adapter is intentionally isolated from core gameplay because Meta SDK APIs and package versions can change. Feed validated reachable points into RoomWorldBootstrap after permission is granted.

## AI architecture
Use a small authenticated backend. Never ship provider API keys in the APK. The headset sends only text the player explicitly chooses to submit. Keep room meshes, images, spatial maps, anchors, object labels, and precise headset location off the AI network. Include an offline demo provider.

## Comfort and privacy
- Never move the player's camera or force locomotion.
- Provide recenter, pause, and hide controls.
- Pause exploration if tracking or room understanding is unavailable.
- Explain room access and obtain explicit permission.

## Roadmap
1. Quest passthrough + holographic menu.
2. Companion prefab, animations, and hand/controller interaction.
3. MRUK room surfaces and safe exploration points.
4. Backend-backed ChatGPT / Claude / Grok adapters.
5. Save data, personality, sound, optimization, and release QA.

All rights reserved until a project license is selected.
