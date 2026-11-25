# AI provider integration brief

Build a provider-neutral companion conversation system for POCKET REALMS.

## Interface
Define IAssistantProvider with an async method accepting conversation history, the player's latest text, and a small explicitly selected companion-context string. Return response text plus optional mood and animation hints from strict allowlists.

## Providers
- OpenAI / ChatGPT
- Anthropic / Claude
- xAI / Grok
- LocalDemoProvider for offline play

All providers share one interface and can be switched in settings.

## Security
Never embed API keys in Unity assets, source code, app settings, or the APK. Route requests through an authenticated backend. Do not transmit room meshes, room images, spatial anchors, maps, object labels, or precise headset location. Only send text the player explicitly submits. Add timeouts, rate limits, response-size limits, and local fallback.

## Companion behavior
Keep responses warm, playful, curious, and concise. Do not claim to see the room unless the player describes it or a separately permissioned on-device feature provides that information. Never execute locomotion, scene changes, purchases, or external actions from model output. Treat provider output as untrusted text and validate all mood and animation hints.
