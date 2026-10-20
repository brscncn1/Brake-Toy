# Brake Toy project workflow

- Follow SOURCE_REQUIREMENTS.md and the user's later decisions. The approved next milestone is a playable bedroom tabletop scene, before the larger Phase 2 gameplay systems.
- Keep existing working scenes and user edits. Save new scene/config/prefab variants for visual milestones.
- Keep DEV_LOG.md current at meaningful checkpoints: decisions, implementation, tests, limitations, and next steps. Do not claim a test passed unless it was executed.
- The user explicitly authorized uploading this project's work to https://github.com/brscncn1/Brake-Toy and maintaining its Notion documentation. Push completed checkpoints to the appropriate feature branch. Do not force-push or overwrite unrelated remote work. Large APKs belong in GitHub Releases, not source commits.
- Notion project root: https://app.notion.com/p/3ffdce66583181868ac1d4e4bd6feeac . Docs/NOTION_INDEX.json maps source documents to child pages. During active work, update the development journal and affected document pages after meaningful implementation/test/release checkpoints. Preserve any user additions when editing Notion pages.
- If GitHub or Notion cannot be reached, keep local changes and record pending synchronization in DEV_LOG.md. Report the actual connection/sync state; do not claim an unattended daemon is running.
- Before delivery, run checks appropriate to the change, update versioned reports and the roadmap, sync Notion, push the branch, and provide source/release/Notion links. Never commit credentials, Library, Temp, .utmp, native build intermediates, or signing keys.
