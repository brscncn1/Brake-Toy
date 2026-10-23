# BRAKE TOY

**NO BRAKES. BIG TROUBLE.** Unity 6.3 LTS / C# / gerçek 3D URP / Android portre.

## v0.8 — Playground

Güvenli/riskli yollar, bağlı karşılaşmalar, kitap rampalarının teker kenarı düzeltmesi, domino ile yol açma ve sinyalli oyuncak kapısı. 3 sn yavaşlatma, korumalı roket ve 5 sn jetpack; başlangıçta üçer hak, TP yenileme. Geri alınabilir masa bölgeleri, ışık/malzeme düzenlemesi, kompakt menü, görsel görev kartları ve ayrı araç siluetleri. Ayrıntılı final model/sanat kabulü bekler.

![Gerçek Unity menüsü](https://raw.githubusercontent.com/brscncn1/Brake-Toy/0cda6024cfe585500df58cc61e3c232d7125bb35/Playground-0.png)

[APK v0.8.0](https://github.com/brscncn1/Brake-Toy/releases/tag/v0.8.0) · [Kaynak](https://github.com/brscncn1/Brake-Toy/tree/feat/playground-v08) · [Ayrıntılı sonuç/eleştiri](https://github.com/brscncn1/Brake-Toy/blob/0cda6024cfe585500df58cc61e3c232d7125bb35/MILESTONE-v08.md)

## Kaynak dalı

Bu main dalının oyun kaynak tabanı Faz 1'dir; özellik PR'ları henüz birleştirilmedi. Güncel oyun **feat/playground-v08** dalında veya hazır APK'dadır. Bu README güncel teslimi gösterir; main aynı oyun kaynaklarını içermez. [PR #7](https://github.com/brscncn1/Brake-Toy/pull/7).

## Aç ve oyna

1. feat/playground-v08 dalını Unity **6000.3.26f1** ile aç. Tools → Brake Toy → **Open Playground v0.8** → Play. Game 9:16, 1×.
2. Sürükle; yeşil rota güvenli, altın rota isteğe bağlı risk/ödül. Kitap rampasındaki yeşil yaklaşma oku atlayışı gösterir. Havada kazanılan TP bölümü 2x.
3. Üç güç düğmesi sürüşte; aynı anda bir güç. Haklar kalıcıdır, retry yenilemez. Menü → GÜÇ HAKLARI: 350/500/650 TP. Gerçek para/coin yok.
4. Ayarlar → MASA: KLASİK / DOMİNO: KAPALI; iki deney ayrı geri alınabilir. Eski v0.7 sahnesi ayrıca korunur.
5. APK öncekinin üzerine güncellenir: com.braketoy.prototype, 0.8.0/kod8, ARM64 IL2CPP non-Development test paketi; mağaza imzası değildir. TP/açık araçlar taşınır, yeni rekor/kariyer/ghost ayrıdır. Güç kullanılan turda ghost yazılmaz.

## Kanıt ve belgeler

**102/102 Unity testi** (48 EditMode +54 PlayMode).

[Gerçek görüntüler](https://github.com/brscncn1/Brake-Toy/blob/0cda6024cfe585500df58cc61e3c232d7125bb35/PLAYGROUND_GALLERY.html), [plan](https://github.com/brscncn1/Brake-Toy/blob/0cda6024cfe585500df58cc61e3c232d7125bb35/PLAN-v0.8.md), [model agent devir notu](https://github.com/brscncn1/Brake-Toy/blob/0cda6024cfe585500df58cc61e3c232d7125bb35/MODEL_HANDOFF-v08.md), [tasarım](https://github.com/brscncn1/Brake-Toy/blob/0cda6024cfe585500df58cc61e3c232d7125bb35/GAME_DESIGN.md), [mimari](https://github.com/brscncn1/Brake-Toy/blob/0cda6024cfe585500df58cc61e3c232d7125bb35/ARCHITECTURE.md), [günlük](https://github.com/brscncn1/Brake-Toy/blob/0cda6024cfe585500df58cc61e3c232d7125bb35/DEV_LOG.md). ANDROID_BUILD_REPORT-v08.json, UNITY_TEST_RESULTS-v08.json ve MODEL_VALIDATION-v08.json.

Fiziksel v0.8 FPS/p95/ısınma ve insan kalite kabulü bekler. Canlı online kapalı; yeni kurallar eski referans sunucuya gönderilmez. Notion mevcut proje merkezi korunur, ücretsiz blok sınırı yeni sayfayı reddetti: [eşitleme durumu](https://github.com/brscncn1/Brake-Toy/blob/0cda6024cfe585500df58cc61e3c232d7125bb35/NOTION_PENDING-v08.md). Müzik ve önceki sürüm kanıtları korunur.
