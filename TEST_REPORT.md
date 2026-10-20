# Faz 1 doğrulama raporu

## Geçen testler

- Unity **6000.3.26f1** gerçek Editor importu, paket çözümü ve C# derlemesi tamamlandı.
- Sahne üreticisi gerçek ToyRoom.unity, Sparky/kitap/blok kulesi/silgi/zemin prefabları, ortak materyaller ve URP konfigürasyonu oluşturdu.
- **Unity EditMode: 19 / 19 geçti.** Kanıt: `Tests/EditMode-results.xml`.
- **Unity PlayMode: 4 / 4 geçti.** Kanıt: `Tests/PlayMode-results.xml`.
- Ayrıca aynı çekirdek üretim C# dosyaları taşınabilir .NET SDK 8.0.426 ile derlendi: **19 / 19 geçti.** Kanıt: `TEST_RESULTS.json`. Bu, Unity'deki 19 çekirdek testinin aynı kontrolleridir; benzersiz test sayısı diye toplanmaz.
- Birleşik Unity özeti: `UNITY_TEST_RESULTS.json`.

## Doğrulanan kapsam

Çözünürlük bağımsızlığı 360/720/1080/1440/2160 piksel, 30/60/120 FPS hedef konumu, dokunma başlangıcı/bırakma/retouch/sınırlar/hız/reset; ileri/yatay/çapraz swept collision, ilk overlap, temas, ayrı yollar ve farklı zamanlarda eksen kesişimi.

Seed **4719** ile **10.000** segment/kutu çarpışması bağımsız kenar kesişimi hesabıyla karşılaştırıldı. Bu sayı **engel dizilimi adalet testi değildir**; prosedürel üretici Faz 2'de yapılacak.

PlayMode testleri gerçek Unity Input System üzerinden sanal mouse ve touch cihazlarıyla UI üzerine başlayan girişin filtrelenmesini, sürüklemeyi, bırakmayı, yeniden dokunmayı ve ikinci parmağı yok saymayı sınadı. Başlangıç → çarpışma → durma → yeniden başlama ve 20 reset/pause/resume döngüsü geçti. Kamera 9:16, 9:19.5, 9:21 ve 3:4 oranlarında araç merkezi ve parkur görünürlüğünü korudu.

Mouse testinin ilk koşusunda bir giriş başlangıcı sorunu bulundu. `wasPressedThisFrame` önbelleğine bağımlılık kaldırılıp basılı duruma geçiş InputController içinde izlenerek düzeltildi; son dört PlayMode testi sıfır hatayla geçti.

Grafik açık Unity Play Mode'da 540 × 960 `Preview.png` renderı alındı ve incelendi: gerçek hacimli Sparky, kitap, gölgeler, perspektif ve ahşap renkli primitive zemin. Bu görüntü nihai Toy Room art kalitesi değildir. Önizleme yakalamasında ilk kare shader hazırlığı tamamlanmadan görülen geçici renkler, Play Mode'da hazırlık sonrası render alınarak giderildi.

## Android derlemesi

Android ARM64 / IL2CPP Development APK başarıyla üretildi. APK yaklaşık 34.5 MiB, minimum API 25 ve hedef API 36. `aapt2` ile paket kimliği, portre, ARM64 ve izinler kontrol edildi. `apksigner verify --verbose` imzayı doğruladı; ZIP bütünlüğü ve libil2cpp.so varlığı kontrol edildi. Rapor: `ANDROID_BUILD_REPORT.json`.

Bu, fiziksel cihaz çalıştırma veya FPS testi değildir.

## Henüz doğrulanmayan

- Fiziksel Android dokunmatik, cihaz safe area, arka plan/telefon kesintileri ve manuel sürüş hissi.
- Gerçek cihaz FPS/bellek/ısı ve uzun seans.
- iOS build ve cihaz testleri.

Faz 1 oynanabilir Unity sahnesi ve otomatik doğrulaması hazır. Android derleme durumu README ve derleme raporunda ayrıca belirtilir; APK üretmek fiziksel cihaz testiyle eşdeğer değildir.
