# BRAKE TOY

**NO BRAKES. BIG TROUBLE.** Unity 6.3 LTS / C# / gerçek 3D URP / Android portre.

## v0.6 — Faz 6 revizyon adayı

10 araç varyantı ve gerçek 3D garaj; Toy Points/kariyer/yedekli kayıt; üç görev ve 15 başarım; üç haklı Relax; günlük antrenman ve kişisel ghost. Bu aday kullanıcı revizyonları içindir; hedef görsel/oynanış kalitesi veya mağaza kabulü tamamlandı demek değildir.

![Gerçek Unity garajı](https://raw.githubusercontent.com/brscncn1/Brake-Toy/ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6/UI-review-1.png)

[Android test APK / v0.6.0](https://github.com/brscncn1/Brake-Toy/releases/tag/v0.6.0) · [Kaynak](https://github.com/brscncn1/Brake-Toy/tree/feat/complete-review-build) · [Notion](https://app.notion.com/p/3ffdce66583181868ac1d4e4bd6feeac)

## Kaynak dalı

Bu main dalının oyun kaynak tabanı Faz 1'dir; özellik PR'ları henüz birleştirilmedi. Güncel v0.6 oyunu için **feat/complete-review-build** dalını veya hazır APK'yı kullan. Bu README güncel teslimi gösterir; main'in aynı oyun kaynaklarını içerdiği anlamına gelmez. [PR #5](https://github.com/brscncn1/Brake-Toy/pull/5).

## Aç ve oyna

1. `feat/complete-review-build` dalını Unity **6000.3.26f1** ile aç. Android Build Support, SDK/NDK ve OpenJDK kurulu olmalı.
2. Tools → Brake Toy → **Open Review Candidate v0.6**. Game görünümü 9:16 Portrait. Play → ARCADE veya RELAX.
3. Göreli mouse/parmak sürükleme. Pause sağ üstte. Sonuçtan yeni parkur veya aynı parkur + ghost.
4. Garajdan araç aç/seç; görevler TP kazandırır. Bütün araçların oynanış değerleri eşittir. [Oyuncu el kitabı](https://github.com/brscncn1/Brake-Toy/blob/ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6/PLAYER_GUIDE.md).
5. APK'yı önceki oyunu kaldırmadan güncelle. Paket kimliği `com.braketoy.prototype`; ARM64 IL2CPP, 0.6.0/kod6. Non-Development test APK'sı; kullanıcıya ait mağaza release anahtarı değildir.

## Gerçek durum

**75/75 Unity testi** (39 EditMode +36 PlayMode), **9/9 Python sunucu testi** ve gerçek C# HTTP yerel entegrasyon probe'u geçti. [Rapor](https://github.com/brscncn1/Brake-Toy/blob/ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6/REVIEW_MILESTONE.md), Tests/EditMode-review.xml, Tests/PlayMode-review.xml, Tests/Network-review.txt, UNITY_TEST_RESULTS-review.json.

Sıralama adapter'ı ve girdi yeniden simülasyonu yapan REST referans sunucusu vardır. **Canlı servis kurulmadı; APK online kapalıdır.** Günlük antrenman cihaz UTC tarihini kullanır. Referans anonim kimlik kalıcı değildir; üretim kimliği/kurtarma ve yaş/gizlilik kabulü açık iştir. [Servis sınırı ve kurulum](https://github.com/brscncn1/Brake-Toy/blob/ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6/Server/README.md), [gizlilik taslağı](https://github.com/brscncn1/Brake-Toy/blob/ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6/PRIVACY-REVIEW.md).

Kullanıcı v0.5'te kontrolü sorunsuz ve tam grafiği öznel olarak akıcı buldu; zorluk/parkur derinliği, müzik ve görüntü beklentinin altında. Sayısal FPS/p95 ölçülmedi. Yeni v0.6 tablet/uzun süre/mağaza kabulü bekler. [Revizyon listesi](https://github.com/brscncn1/Brake-Toy/blob/ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6/REVISION_BACKLOG.md).

## İnceleme ve belgeler

- [Gerçek ekran/model galerisi](https://github.com/brscncn1/Brake-Toy/blob/ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6/REVIEW_GALLERY.html); UI-review-0..17.png. Bunlar motor renderıdır, AI konsepti değildir.
- [Kapsam, eleştiri, faz durumu ve kullanıcı işlemleri](https://github.com/brscncn1/Brake-Toy/blob/ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6/REVIEW_MILESTONE.md); [yürütme planı](https://github.com/brscncn1/Brake-Toy/blob/ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6/PLAN-REVIEW.md).
- [Tasarım](https://github.com/brscncn1/Brake-Toy/blob/ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6/GAME_DESIGN.md), [mimari](https://github.com/brscncn1/Brake-Toy/blob/ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6/ARCHITECTURE.md), [yol haritası](https://github.com/brscncn1/Brake-Toy/blob/ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6/ROADMAP.md), [günlük](https://github.com/brscncn1/Brake-Toy/blob/ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6/DEV_LOG.md), [lisanslar](https://github.com/brscncn1/Brake-Toy/blob/ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6/THIRD_PARTY_ASSETS.md).
- ANDROID_BUILD_REPORT-review.json ve MODEL_VALIDATION-review.json bu adaya aittir. Önceki raporlar/sahneler korunur.

Editor testleri: Test Runner EditMode/PlayMode. Sunucu: `cd Server` ardından `python -m unittest -v test_server`. Android paket: Tools → Brake Toy → Build Android Prototype, en yeni Review sahnesini seçer. Üretim online bağlantısı ve mağazaya yükleme bu komutla otomatik etkinleşmez.
