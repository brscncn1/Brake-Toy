"""Check relocated evidence and links against the immutable pre-move Git tree."""
from pathlib import Path
import hashlib
import json
import posixpath
import re
import subprocess
import sys
import urllib.parse

root = Path(__file__).resolve().parents[1]
manifest = json.loads((root / 'Reports/ORGANIZATION_MANIFEST.json').read_text())
base = manifest['sourceCommit']
tracked = subprocess.check_output(['git', 'ls-tree', '-r', '--name-only', base], cwd=root, text=True).splitlines()
mapping = {item['from']: item['to'] for item in manifest['moves']}
errors = []
links_checked = 0
preserved = 0

def targets(text, name):
    values = re.findall(r'\]\(([^\s)]+)\)', text)
    values += re.findall(r'(?:src|href)=["\']([^"\']+)["\']', text)
    result = []
    for value in values:
        if re.match(r'^[a-zA-Z][a-zA-Z0-9+.-]*:', value) or value.startswith(('#', '/')):
            continue
        value = urllib.parse.unquote(value.partition('#')[0])
        result.append(posixpath.normpath(posixpath.join(posixpath.dirname(name), value)))
    return result

for item in manifest['moves']:
    target = root / item['to']
    if not target.is_file() or (root / item['from']).exists():
        errors.append('Missing destination or old file remains: ' + item['from'])
    elif target.suffix in ('.json', '.xml', '.png', '.txt'):
        if hashlib.sha256(target.read_bytes()).hexdigest() != item['originalSha256']:
            errors.append('Evidence bytes changed: ' + item['to'])
        else:
            preserved += 1

for old in tracked:
    if Path(old).suffix not in ('.md', '.html'):
        continue
    new = mapping.get(old, old)
    before = subprocess.check_output(['git', 'show', base + ':' + old], cwd=root).decode('utf-8-sig')
    after = (root / new).read_text(encoding='utf-8-sig')
    old_links, new_links = targets(before, old), targets(after, new)
    if len(old_links) != len(new_links):
        # Newly added navigation is checked separately; original links must still resolve.
        new_links_set = set(new_links)
        for target in old_links:
            if target in tracked:
                links_checked += 1
                destination = mapping.get(target, target)
                if destination not in new_links_set or not (root / destination).is_file():
                    errors.append('Lost link in ' + new + ': ' + target)
    else:
        for old_target, new_target in zip(old_links, new_links):
            if old_target in tracked:
                links_checked += 1
                if new_target != mapping.get(old_target, old_target) or not (root / new_target).is_file():
                    errors.append('Link regression in ' + new + ': ' + new_target)

for path in (root / 'Reports/Visuals').glob('*.html'):
    for target in targets(path.read_text(encoding='utf-8-sig'), path.relative_to(root).as_posix()):
        if not (root / target).is_file():
            errors.append('Broken gallery asset: ' + target)

for name in ('AGENTS.md', 'YOL_HARITASI.md', 'MIMARI.md', 'MEKANIKLER.md', 'CLAUDE.md', 'KARARLAR.md', 'DOGRULAMA.md'):
    if len((root / name).read_text(encoding='utf-8-sig').splitlines()) > 300:
        errors.append('Document over 300 lines: ' + name)

print(json.dumps({'moves': len(mapping), 'byteIdenticalEvidence': preserved,
                  'existingLinksChecked': links_checked, 'errors': errors}, ensure_ascii=False, indent=2))
sys.exit(bool(errors))
