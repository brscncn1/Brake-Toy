# Third-party assets

## v0.7 composed music and real toy recordings

- Bobjt, **leo's theme**, [author page](https://opengameart.org/content/leos-theme), CC0 option of the author's dual licensing. Trimmed silence and a two-second equal-power loop overlap; 87.01 seconds. File menu-leo-loop.ogg.
- Zane Little Music, **Pixel Sprinter**, [author page](https://opengameart.org/content/pixel-sprinter), CC0. Author-provided 34.29-second racing/running loop; race-pixel-sprinter.ogg.
- AntumDeluge, **Bicycle Horn**, [author page](https://opengameart.org/content/bicycle-horn), CC0. Actual child's plastic bicycle horn; toy-horn.wav. Three cosmetic pitch settings, separate from near-miss chime.
- rioforce, **LEGO Pour 1**, [author page](https://freesound.org/people/rioforce/sounds/233662/), CC0. Public HQ preview of plastic toy bricks poured on a table, cut/normalized to 0.85-second toy clatter. plastic-toy-clatter.ogg; no brand affiliation implied.

Exact downloaded file URLs, authors, SHA256 and license selection: Assets/BrakeToy/Audio/Joyful/SOURCES.json. Processing and waveform measurements: AUDIO_CHECK.json. No human listening acceptance is claimed by the file checks. The original source files are retained for provenance; only referenced game clips enter the APK. Prior Poly Haven cotton textures are also reused on new cloth toy materials. New models and procedural playmat grain are project-authored.

## Poly Haven — Wood Table 001

- Asset: https://polyhaven.com/a/wood_table_001
- License: CC0; https://polyhaven.com/license
- Authors: Dimitrios Savva (Photography), Rico Cilliers (Processing).
- Retrieved: 2026-10-21. Public asset API metadata stored in `Assets/BrakeToy/Art/Workshop/Textures/SOURCE.json`.
- `wood_table_001_diff_1k.jpg`, MD5 `56652a7339b16a76afad20d3585f66ea`.
- `wood_table_001_nor_gl_1k.jpg`, MD5 `6360e09967add8baecaa5ecc55fb5e03`.
- Downloads from `https://dl.polyhaven.org/file/ph-assets/Textures/jpg/1k/wood_table_001/`.
- Used as wood albedo and normal texture; original asset credits retained voluntarily. Material tint, UV mapping and normal strength configured for the tabletop.

All newly authored mesh geometry, softbox cubemap and synthesized audio cues are original project content. Unity engine/package assets remain under their respective Unity licenses. Competitor screenshots, music, models and trademarks are not bundled.

## v0.4 Quiet Wood shader

`Art/Refined/Shaders/QuietWood.shader` and `QuietWoodInput.hlsl` are modified copies of Unity URP 17.3's Lit shader/input from the installed package. The shader name and albedo contrast calculation were changed; rendering passes and material constant layout are retained. The package license is redistributed as `Unity-LICENSE.md` in that folder. This is Unity-licensed source, not original Brake Toy shader authorship. The existing CC0 wood images remain unmodified. v0.4 mesh additions and layered synthesized audio are project-authored.

## v0.5 Living Workshop — CC0 sources

- [Painted Wooden Chair 02](https://polyhaven.com/a/painted_wooden_chair_02), Kirill Sannikov, CC0. Original 1k FBX, diffuse, normal and AO maps; 1,246 source polygons according to metadata. Imported readable for geometry audit; fitted to room scale, shared material assigned.
- [Cotton Jersey](https://polyhaven.com/a/cotton_jersey), colormass (Photography) / Rico Cilliers (Processing), CC0. Original 1k diffuse and normal maps; project-authored curved cloth meshes, UV scale and tint for curtains, bedding, storage and teddy.
- [Studio Small 09](https://polyhaven.com/a/studio_small_09), Sergej Majboroda, CC0. Original 1k HDR imported as 256px cubemap for material reflections. It is not an outdoor window photograph or baked room lighting.
- Poly Haven [CC0 license](https://polyhaven.com/license); asset API metadata retained alongside files as SOURCE.json. Original downloads were checked against API MD5. Delivered file SHA256 and sizes: Docs/ASSET_HASHES-v0.5.json.
- [Kenney Impact Sounds](https://kenney.nl/assets/impact-sounds), Kenney, CC0. Six selected OGG recordings bundled; wood/metal/soft impacts used. License text retained under ThirdParty/Audio/Kenney-LICENSE.txt. Download archive SHA256: 029d734af1582474edf3a694d1b0cebc97c1c152f2f39fa34d4c2bafc5de77f8.
- [Bowling Ball Rolling](https://opengameart.org/content/bowling-ball-rolling), qubodup, CC0. Author describes a marble-ball recording, possibly a wooden-door component. The non-fadeout OGG provides approaching ball texture; motor and near-miss synthesis remain project-authored. Archive SHA256: 3c99cfe42ecf8a87ab13f66f0f4c16d0a2bdbf75f825eb2e959baab581091af8.

Retrieved 2026-10-21. No competitor models, screenshots, music or proprietary audio were bundled. The earlier blanket statement about original mesh/audio authorship applies to the earlier releases; this version includes the above credited third-party chair, textures, HDR and recordings.
