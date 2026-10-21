# BRAKE TOY

**NO BRAKES. BIG TROUBLE.** Unity 6.3 LTS / C# / gerçek 3D URP / Android portre.

## v0.5 — Living Workshop

Eğrisel oyuncak araba, taranmış ahşap sandalye, gerçek pamuk dokulu kıvrımlı perde/yatak ve sıcak pencere gölgesi. Ritimli erken kaçışlar, üç kısa tur hedefi, isteğe bağlı +75 stil yıldızları, kayıt tabanlı çarpışma/yuvarlanma sesleri ve ayrı motor/efekt ayarları.

![Gerçek Unity v0.5 oynanış](https://raw.githubusercontent.com/brscncn1/Brake-Toy/a7e889ae4264656eca81db093034c63ed15f3906/Preview-v0.5.png)

[Android APK / v0.5.0](https://github.com/brscncn1/Brake-Toy/releases/tag/v0.5.0) · [Kaynak dalı](https://github.com/brscncn1/Brake-Toy/tree/feat/living-workshop-v05) · [Notion proje merkezi](https://app.notion.com/p/3ffdce66583181868ac1d4e4bd6feeac)

## Kaynak dalı

Bu main dalının oyun kaynak tabanı Faz 1'dir; daha sonraki PR'lar henüz birleştirilmedi. Güncel v0.5 oyunu için **feat/living-workshop-v05** dalını veya yukarıdaki hazır APK'yı kullanın. Bu README güncel teslimi gösterir; main'in aynı kaynakları içerdiği anlamına gelmez. [PR #4](https://github.com/brscncn1/Brake-Toy/pull/4).

## Aç ve oyna

1. v0.5 kaynak dalını kullan. Unity **6000.3.26f1** ile bu proje klasörünü aç.
2. Tools → Brake Toy → **Open Living Workshop v0.5**. Game: **16:9 Portrait (9:16)** /Scale 1×. Play → OYNA. Fareyi basılı tutup/parmakla sağa sola sürükle.
3. Hedefler: **60 m →3 risk yıldızı →6 yakın geçiş**. Güvenli yolu kullanabilir veya yıldız için engele yaklaşabilirsin. İlk kaçış yaklaşık 2 saniyede gelir.
4. **YENİ PARKUR** farklı seed; **AYNI PARKURU TEKRARLA** aynı dizi. Sağ üstten pause. Menüde MOTOR/EFEKT, genel ses ve grafik profili bulunur.
5. **60 sn PERFORMANS TESTİ** gerçek sürüşten FPS/p95 kaydeder. Yüksek 60 /ekonomik 30 FPS hedeflerdir; fiziksel tablet sonucu henüz yok. [Tablet kontrol adımları](https://github.com/brscncn1/Brake-Toy/blob/a7e889ae4264656eca81db093034c63ed15f3906/DEVICE_TEST-v0.5.md).

APK: ARM64 IL2CPP Development /Android 7.1+; Builds/Android/BrakeToy-v0.5-Living.apk. Öncekini kaldırmadan güncelleme kur. Eski sahneler ve kayıtlar korunur; değişen puan kuralları nedeniyle v0.5 stil rekoru ayrıdır. APK'lar kaynak geçmişine değil GitHub Releases'e yüklenir.

## Sonuç ve sınırlar

**62/62 Unity testi geçti** (31 EditMode +31 PlayMode). Yeni 128.000 ritimli satır kontrolü, dört seed ×iki rota ×120 sn simüle sürüş, eski regresyonlar, kamera/model/ödül/ses/profil kontrolleri. APK paket/imza/arşiv kontrolü geçti. Fiziksel FPS/ısı/dokunma ve insan tempo/ses/sanat kabulü bekliyor.

- [Ayrıntılı sonuç, eleştiri ve kalan fazlar](https://github.com/brscncn1/Brake-Toy/blob/a7e889ae4264656eca81db093034c63ed15f3906/LIVING_MILESTONE.md), [plan](https://github.com/brscncn1/Brake-Toy/blob/a7e889ae4264656eca81db093034c63ed15f3906/PLAN-v0.5.md).
- [Test raporu](https://github.com/brscncn1/Brake-Toy/blob/a7e889ae4264656eca81db093034c63ed15f3906/TEST_REPORT.md), UNITY_TEST_RESULTS.json, Tests/*-v05.xml, ANDROID_BUILD_REPORT.json, MODEL_VALIDATION.json.
- [Gerçek önce/sonra](https://github.com/brscncn1/Brake-Toy/blob/a7e889ae4264656eca81db093034c63ed15f3906/COMPARISON-v0.5.html), Preview-v0.5-late-run.png, Models-v0.5-*.png, Room-v0.5-detail.png.
- [Kaynak/lisanslar](https://github.com/brscncn1/Brake-Toy/blob/a7e889ae4264656eca81db093034c63ed15f3906/THIRD_PARTY_ASSETS.md), [araştırma](https://github.com/brscncn1/Brake-Toy/blob/a7e889ae4264656eca81db093034c63ed15f3906/RESEARCH-v0.3.md), GAME_DESIGN.md, ARCHITECTURE.md, ROADMAP.md, DEV_LOG.md.

Gerçek render ile Concepts altındaki AI hedefi farklıdır; bu test sürümü final kalite kabulü değildir. Aktif odak Faz 3. Mevcut kapanış dahil ilk yayına 4 ana iş bloğu; dört sürüm garantisi değildir. Kalıcı ilerleme/garaj/Relax/online ve yayın hazırlığı sonraki kapsamda.
