# BRAKE TOY

**NO BRAKES. BIG TROUBLE.** Unity 6.3 LTS / C# / gerçek 3D URP / Android portre.

## Kaynak dalı

Bu main dalının oyun kaynak tabanı Faz 1'dir; özellik PR'ları henüz birleştirilmedi. Güncel oyun **fix/table-layout-v081** dalında veya hazır v0.8.1 APK'dadır. README güncel teslimi gösterir; main aynı oyun kaynaklarını içermez. [PR #8](https://github.com/brscncn1/Brake-Toy/pull/8).

## v0.8.1 — Güncel yerleşim düzeltmesi

Mavi kenar dekorları defter/kalemlikten ayrıldı, radyo soldaki rafın üstüne taşındı, domino aktif oyundan kaldırıldı. Hız/zorluk ayarları korunur. Önceki kariyer/TP/araç/güç hakları sürer.

![Gerçek Unity yerleşimi](https://raw.githubusercontent.com/brscncn1/Brake-Toy/af0c0e6bdacbc435f3252a6dd5498035deffe402/Tidy-6.png)

[Güncel APK v0.8.1](https://github.com/brscncn1/Brake-Toy/releases/tag/v0.8.1) · [Güncel kaynak](https://github.com/brscncn1/Brake-Toy/tree/fix/table-layout-v081) · [104/104 test ve sonuç](https://github.com/brscncn1/Brake-Toy/blob/af0c0e6bdacbc435f3252a6dd5498035deffe402/MILESTONE-v081.md)

Unity 6000.3.26f1 → Tools → Brake Toy → **Open Tidy Room v0.8.1** → Play. Paket com.braketoy.prototype, 0.8.1/kod9; APK öncekinin üzerine güncellenir. Önceki v0.8 açıklamaları aşağıda tarihsel olarak korunur; güncel sahne Tidy'dir.


## v0.8 — Playground

Güvenli/riskli yollar, bağlı karşılaşmalar, kitap rampalarının teker kenarı düzeltmesi, domino ile yol açma ve sinyalli oyuncak kapısı. 3 sn yavaşlatma, korumalı roket ve 5 sn jetpack; başlangıçta üçer hak, TP yenileme. Geri alınabilir masa bölgeleri, ışık/malzeme düzenlemesi, kompakt menü, görsel görev kartları ve ayrı araç siluetleri. Ayrıntılı final model/sanat kabulü bekler.

![Gerçek Unity menüsü](https://raw.githubusercontent.com/brscncn1/Brake-Toy/af0c0e6bdacbc435f3252a6dd5498035deffe402/Playground-0.png)

[APK v0.8.0](https://github.com/brscncn1/Brake-Toy/releases/tag/v0.8.0) · [Kaynak](https://github.com/brscncn1/Brake-Toy/tree/feat/playground-v08) · [Ayrıntılı sonuç/eleştiri](https://github.com/brscncn1/Brake-Toy/blob/af0c0e6bdacbc435f3252a6dd5498035deffe402/MILESTONE-v08.md)

## Aç ve oyna

1. feat/playground-v08 dalını Unity **6000.3.26f1** ile aç. Tools → Brake Toy → **Open Playground v0.8** → Play. Game 9:16, 1×.
2. Sürükle; yeşil rota güvenli, altın rota isteğe bağlı risk/ödül. Kitap rampasındaki yeşil yaklaşma oku atlayışı gösterir. Havada kazanılan TP bölümü 2x.
3. Üç güç düğmesi sürüşte; aynı anda bir güç. Haklar kalıcıdır, retry yenilemez. Menü → GÜÇ HAKLARI: 350/500/650 TP. Gerçek para/coin yok.
4. Ayarlar → MASA: KLASİK / DOMİNO: KAPALI; iki deney ayrı geri alınabilir. Eski v0.7 sahnesi ayrıca korunur.
5. APK öncekinin üzerine güncellenir: com.braketoy.prototype, 0.8.0/kod8, ARM64 IL2CPP non-Development test paketi; mağaza imzası değildir. TP/açık araçlar taşınır, yeni rekor/kariyer/ghost ayrıdır. Güç kullanılan turda ghost yazılmaz.

## Kanıt ve belgeler

**102/102 Unity testi** (48 EditMode +54 PlayMode).

[Gerçek görüntüler](https://github.com/brscncn1/Brake-Toy/blob/af0c0e6bdacbc435f3252a6dd5498035deffe402/PLAYGROUND_GALLERY.html), [plan](https://github.com/brscncn1/Brake-Toy/blob/af0c0e6bdacbc435f3252a6dd5498035deffe402/PLAN-v0.8.md), [model agent devir notu](https://github.com/brscncn1/Brake-Toy/blob/af0c0e6bdacbc435f3252a6dd5498035deffe402/MODEL_HANDOFF-v08.md), [tasarım](https://github.com/brscncn1/Brake-Toy/blob/af0c0e6bdacbc435f3252a6dd5498035deffe402/GAME_DESIGN.md), [mimari](https://github.com/brscncn1/Brake-Toy/blob/af0c0e6bdacbc435f3252a6dd5498035deffe402/ARCHITECTURE.md), [günlük](https://github.com/brscncn1/Brake-Toy/blob/af0c0e6bdacbc435f3252a6dd5498035deffe402/DEV_LOG.md). ANDROID_BUILD_REPORT-v08.json, UNITY_TEST_RESULTS-v08.json ve MODEL_VALIDATION-v08.json.

Fiziksel v0.8 FPS/p95/ısınma ve insan kalite kabulü bekler. Canlı online kapalı; yeni kurallar eski referans sunucuya gönderilmez. Notion mevcut proje merkezi korunur, ücretsiz blok sınırı yeni sayfayı reddetti: [eşitleme durumu](https://github.com/brscncn1/Brake-Toy/blob/af0c0e6bdacbc435f3252a6dd5498035deffe402/NOTION_PENDING-v08.md). Müzik ve önceki sürüm kanıtları korunur.
