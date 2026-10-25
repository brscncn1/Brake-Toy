# Brake Toy — Faz 1 teslim raporu

## Sonuç

Unity 6.3 LTS ile gerçek 3D prototip oluşturuldu, motor içinde derlendi ve otomatik testleri geçti. Telefonda denenmek üzere Android APK üretildi. Nihai oyunun sonraki fazları uygulanmış gibi gösterilmedi.

## Oluşturulan dosyalar

- `Assets/BrakeToy/Scenes/ToyRoom.unity`: bağlantıları hazır oynanabilir sahne.
- `Assets/BrakeToy/Prefabs`: 3D Sparky, kitap, blok kulesi, silgi ve zemin.
- `Assets/BrakeToy/Art/Materials`, `Settings`, `ScriptableObjects`: ortak materyaller, mobil URP ve ayarlanabilir oyun konfigürasyonu.
- `Assets/BrakeToy/Scripts/Core`: Unity'den bağımsız direksiyon ve sürekli çarpışma hesabı.
- `Assets/BrakeToy/Scripts/Runtime`: giriş, oyuncu, dünya, kamera, oyun döngüsü ve arayüz.
- `Assets/BrakeToy/Editor`: tekrarlanabilir sahne üretimi, Android build ve 3D önizleme aracı.
- `Assets/BrakeToy/Tests` ve `Tests`: çekirdek/entegrasyon testleri ve gerçek sonuçlar.
- `SOURCE_REQUIREMENTS.md`: gönderdiğin ana promptun eksiksiz kopyası.
- `GAME_DESIGN.md`, `ROADMAP.md`, `ARCHITECTURE.md`, `README.md`, `CHANGELOG.md`, `TEST_PLAN.md`: tasarım, aşamalar, mimari ve kullanım.
- `Builds/Android/BrakeToy-Phase1.apk`: yaklaşık 34.5 MiB prototip APK.
- `Preview.png`, `TEST_REPORT.md`, `UNITY_TEST_RESULTS.json`, `TEST_RESULTS.json`, `ANDROID_BUILD_REPORT.json`: önizleme ve doğrulama kanıtları.

## Uygulanan sistemler

Dikey perspektif kamera; primitive ama gerçek hacimli Sparky; serbest X hareketi; göreli tek parmak ve mouse sürükleme; parmak bırakıldığında konumu koruma; sınır/hız denetimi; tekrar kullanılan kayan zemin ve engeller; kareler arası atlamayı kontrol eden swept collision; çarpışmada durma; hızlı retry; temel Türkçe ekranlar, pause ve odak kaybı yönetimi.

## Test sonuçları

**Unity EditMode 19/19, PlayMode 4/4 geçti.** Çekirdek kontrolleri .NET ile de 19/19 geçti; bunlar aynı çekirdek kontrolleridir. Seed 4719 ile 10.000 çarpışma örneği bağımsız geometri hesabıyla karşılaştırıldı. Mouse, touch, UI filtresi, ikinci parmak, restart/pause ve portre kamera oranları sınandı.

Android ARM64 IL2CPP APK derlendi; imzası, arşiv bütünlüğü, paket/izin/ABI bilgileri doğrulandı. Grafik açık Unity Play Mode renderı incelendi.

## Doğrulanmayan noktalar

Fiziksel telefon dokunmatiği ve hissiyatı, cihaz safe area/kesintileri, uzun seans FPS/bellek/ısı ve iOS testleri yapılmadı. Mağaza yayını ve nihai görsel kalite tamamlanmış değildir.

## Unity'de yapacağın işlem

Proje açık değilse Hub → Projects → Add project from disk ile bu BrakeToy klasörünü seç. ToyRoom sahnesini aç, Game oranını 9:16 yap. Üstte **Play**, oyunda **OYNA**. Sol fare tuşunu basılı tutup sağa sola sürükle; çarpışıp **TEKRAR OYNA** düğmesini dene. Nesne/referans bağlamana gerek yok. Telefon kurulumu ve ayarlar README'de.

## Sonraki adım

Önce gerçek sürüş hissini ve Android cihazı kontrol ederek Faz 1 kabulünü kapatacağız. Ardından Faz 2'de geçilebilirliği doğrulanan seed'li engel üreticisi, hızlanma, mesafe, Near Miss, stil ve kombo geliştirilecek. Garaj/ekonomi, online servisler ve reklamlar bu prototipe eklenmedi.
