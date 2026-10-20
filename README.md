# BRAKE TOY

**NO BRAKES. BIG TROUBLE.** Unity 6.3 LTS / C# / gerçek 3D URP / Android portre.

## v0.4 — Refined Room

Daha sakin sıcak masa ve gölge, daha açık oda/pencere ayrıntıları, alçaltılmış araç kabini ve açılmış teker boşlukları, kompakt HUD ve tek menü. Her yeni turda değişen parkur, aynı parkuru tekrar etme seçeneği, değişken top olayları, katmanlı oyuncak sesleri ve kısa çarpışma tepkisi. Yüksek/ekonomik grafik profili ve yerel performans ölçümü.

![Gerçek Unity v0.4 oynanış](https://raw.githubusercontent.com/brscncn1/Brake-Toy/7e36334d85211c5baa3ce1a42440c17e92043f6b/Preview-v0.4.png)

Bu görüntü gerçek Unity sahnesidir. Concepts altındaki AI hedefi ayrı bir referanstır; bu sürüm final sanat kalitesi olarak sunulmaz.

[Android APK / v0.4](https://github.com/brscncn1/Brake-Toy/releases/tag/v0.4.0) · [v0.4 kaynakları](https://github.com/brscncn1/Brake-Toy/tree/feat/refined-room-v04) · [Notion](https://app.notion.com/p/3ffdce66583181868ac1d4e4bd6feeac)

## Kaynak dalı

Bu ana sayfa yayımlanan **v0.4** durumunu gösterir. `main` içindeki proje kaynakları önceki prototip tabanındadır; v0.3 PR #2 ve v0.4 PR #3 açık geliştirme dallarıdır. Güncel Unity projesi için yukarıdaki **v0.4 kaynakları** bağlantısını kullanın; APK Releases içindedir.

## Aç ve oyna

1. v0.4 kaynak dalını indirin/seçin. Unity Hub → Add project from disk → proje klasörü. Unity **6000.3.26f1**.
2. Tools → Brake Toy → **Open Refined Room v0.4**.
3. Game: **16:9 Portrait (9:16)**, Scale 1×. Play → OYNA. Fareyi basılı tutup/parmakla sağa sola sürükle.
4. Sonuçta **YENİ PARKUR** farklı bir tur, **AYNI PARKURU TEKRARLA** aynı dizidir. Duraklat düğmesi sağ üstte.
5. Menüde ses ve grafik profili seçilir. Ekonomik profil SSAO'yu kapatır, render ölçeği/gölge/AA bütçesini azaltır; hedef 30 FPS. Yüksek hedef 60 FPS. Bunlar cihaz ölçümü değildir.
6. **60 sn PERFORMANS TESTİ** düğmesine basıp oynayın; toplam 60 sn gerçek sürüş sonunda menüde FPS/p95 görünür. Detaylar [tablet testi](https://github.com/brscncn1/Brake-Toy/blob/7e36334d85211c5baa3ce1a42440c17e92043f6b/DEVICE_TEST-v0.4.md).

APK: ARM64 IL2CPP Development, Android 7.1+; Builds/Android/BrakeToy-v0.4-Refined.apk. APK'lar kaynak geçmişine değil GitHub Releases'e yüklenir. Eski ToyRoom/ToyRoomTabletop/ToyRoomArcade sahneleri ve sürümleri korunur. Önceki rekor anahtarları değiştirilmedi.

## Kanıt ve açık kalanlar

**53/53 Unity testi geçti**: 28 EditMode +25 PlayMode. Yeni ve eski 128.000 satırlık koridor kontrolleri, 10.000 top çarpışma örneği, iki dakikalık motor içi tanık sürüşleri, kamera/model/UI/profil/impact kontrolleri. APK imza/paket kontrolü geçti. Fiziksel tablet FPS/ısı/dokunma, insan tempo/ses/sanat kabulü henüz doğrulanmadı.

- [Sonuçlar ve hâlâ beğenmediğim taraflar](https://github.com/brscncn1/Brake-Toy/blob/7e36334d85211c5baa3ce1a42440c17e92043f6b/REFINED_MILESTONE.md)
- [v0.3 kalite eleştirisi ve öneriler](https://github.com/brscncn1/Brake-Toy/blob/7e36334d85211c5baa3ce1a42440c17e92043f6b/QUALITY_REVIEW-v0.3.md), [v0.4 planı](https://github.com/brscncn1/Brake-Toy/blob/7e36334d85211c5baa3ce1a42440c17e92043f6b/PLAN-v0.4.md)
- [Gerçek önce/sonra](https://github.com/brscncn1/Brake-Toy/blob/7e36334d85211c5baa3ce1a42440c17e92043f6b/COMPARISON-v0.4.html), Preview-v0.4-late-run.png ve Models-v0.4-*.png
- [Test raporu](https://github.com/brscncn1/Brake-Toy/blob/7e36334d85211c5baa3ce1a42440c17e92043f6b/TEST_REPORT.md), UNITY_TEST_RESULTS.json, Tests/*.xml, ANDROID_BUILD_REPORT.json, MODEL_VALIDATION.json
- [Araştırma ve birincil kaynaklar](https://github.com/brscncn1/Brake-Toy/blob/7e36334d85211c5baa3ce1a42440c17e92043f6b/RESEARCH-v0.3.md), [varlık lisansları](https://github.com/brscncn1/Brake-Toy/blob/7e36334d85211c5baa3ce1a42440c17e92043f6b/THIRD_PARTY_ASSETS.md)
- GAME_DESIGN.md, ARCHITECTURE.md, ROADMAP.md, SOURCE_REQUIREMENTS.md ve DEV_LOG.md

Bu sürüm Faz 2/3'ü ilerletir; tüm kapsamda yayın hazırlığı Faz 6'dır. Görev/garaj/ekonomi/Relax/online ve final sanat kabulü sonraki kapsamda kalır. Geliştirme sırasında önemli karar/test/teslim sonuçları Notion günlüğüne de işlenir.
