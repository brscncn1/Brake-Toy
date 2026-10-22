# Brake Toy reference service — review-r1

This is an implemented self-hosted REST reference backend, **not a Unity Gaming Services deployment**. It has no paid dependency. It binds loopback by default. The shipped scene has an empty endpoint and eligibility disabled, so ordinary gameplay makes no requests.

## Local reproduction

Use Python 3.10+ (tested here with 3.14). From this directory:

```sh
python -m unittest -v test_server
python app.py --host 127.0.0.1 --port 8765 --db braketoy.sqlite3
```

The database and tokens must never be committed. Unity Editor accepts loopback HTTP only for the explicit integration probe; Android accepts HTTPS endpoints only. `BrakeToy.Editor.ReviewNetworkProbe.Run` checks the real C# request flow against a fresh local server database. It uses temporary in-memory configuration and does not change the saved scene.

## Contract

- GET /health, GET /daily: rules version and server UTC daily seed.
- POST /session: random safe Toy-hex nickname and expiring anonymous bearer session. No user supplied name, profile, chat or photo.
- POST /runs with mode arcade/daily: server-owned seed and one-time ticket, expires after two hours. Relax rejected.
- POST /submit: ticket, rules=review-r1, dt/x input steps. Distance and style are recomputed; a client score is ignored. Bounds, time, ownership and duplicate tickets are checked.
- GET /leaderboard?mode=arcade|daily&metric=distance|style: separate independent bests, top100, own rank and five neighbors; deterministic ties. Daily day comes from server UTC.
- POST /delete-player: deletes the authenticated anonymous player's rows/tickets and revokes their session. Hosting operator must supply a verified recovery/deletion process for players who lose their session.

Each request is limited to 4 MB and a socket timeout of 15 seconds; simple in-memory throttling allows 120 requests/minute/IP. Expired sessions and old rate buckets are removed; run tickets expire after two hours and are cleaned after an additional day. Submitted input trajectories are processed in memory, not stored. Scores persist in SQLite. IP addresses are used transiently for throttling; default request logging is disabled.

## Honest limits and production work

This service is locally tested, not deployed, load-tested, independently security-audited or approved for a child audience. Re-simulation does not stop bots, modified clients choosing optimal inputs or all replay abuse. The SQLite write transaction serializes submissions; this is unsuitable as a claim of production scale.

The anonymous token is held in client memory and expires after 24h; quitting/restarting creates a new identity. **Stable player identity, account recovery and portable leaderboard ownership remain production integration work.** Do not enable this reference connection publicly and promise account continuity. No secret or hosting credential belongs in the Unity client.

Before deployment, provide a managed HTTPS endpoint, restricted reverse proxy/body/connection/rate limits, secrets and backup policy, monitoring, retention/deletion process, recovery mechanism, production load/security tests and actual target-audience/privacy approval. IP-derived throttling behind a proxy must be configured by the operator; do not blindly trust forwarded headers.

Then set `Assets/BrakeToy/ScriptableObjects/OnlineConfiguration.asset` Endpoint and enable EligibilityAndPrivacyReviewed only after real review, build a separate connected test candidate, test on Android against that service, and verify outages, token renewal, UTC boundary and recovery. Turning a boolean on is not consent or proof of legal eligibility.

UGS was considered as a managed alternative. This adapter avoids requesting a Cloud project before the owner chooses a hosting path; it does not emulate UGS ownership/authentication guarantees. Provider cost and current audience eligibility must be checked for the eventual provider before buying or deploying anything.
