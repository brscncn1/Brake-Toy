# Brake Toy — güncel teknik mimari

İnceleme tarihi: 24 Ekim 2026. Çalışan taban v0.9 Adventure; bu dosya mevcut kodu ve sınırlarını açıklar.
İyileştirme önerileri uygulanmış mimari değildir; ilgili işler `YOL_HARITASI.md` içindedir.

## 1. Teknoloji ve çalışma ortamı

- Unity 6.3 LTS `6000.3.26f1`, C#, URP `17.3.0`, Input System `1.17.0`.
- uGUI `2.0.0`; Canvas, Button, Slider, Text ve RenderTexture tabanlı garaj.
- Test Framework `1.6.0`: NUnit/EditMode ve UnityTest/PlayMode.
- Paketler `Packages/manifest.json` ve `packages-lock.json` ile sabittir; gerekçesiz yükseltme yapma.
- Android ARM 64/IL 2CPP; uygulama kimliği `com.braketoy.prototype`; portre.
- Referans backend Python/SQLite ve standart HTTP; UGS dağıtımı yoktur.
- Windows editör yolu yaygın olarak `C:/Program Files/Unity/Hub/Editor/6000.3.26f1/Editor/Unity.exe`.
- Diğer agent ortamında yolu keşfet; Baris kullanıcı dizinini kod veya kalıcı ayara sabitleme.

## 2. Assembly sınırları

```text
BrakeToy.Core       System; UnityEngine bağımlılığı yok
      ↑
BrakeToy.Runtime    UnityEngine, Input System, uGUI, URP
      ↑
BrakeToy.Editor     UnityEditor; üretim, denetim, render, derleme
      ↑
EditMode / PlayMode ayrı test assembly'leri
```

- Core: SteeringModel, SweptCollision/SweptCircleBox, üretim dizileri, StyleScore, CareerRules.
- Runtime: yaşam döngüsü, gerçek giriş, Unity varlıkları, görsel poz ve kalıcılık adaptörleri.
- Editor: UnityEditor API'leri burada kalır; Runtime/Core içine taşınmaz.
- ScriptableObject ayarları var; bütün mekanikler veri odaklı değildir, birçok sabit hâlâ kodda.
- Yeni kural saf hesaplanabiliyorsa Core'a; Unity nesnesi/yaşam döngüsü gerekiyorsa Runtime'a koy.

## 3. Aktif sahne ve tarihsel zincir

- Güncel sahne `Assets/BrakeToy/Scenes/ToyRoomAdventure.unity`.
- Zincir: ToyRoom → Tabletop → Workshop → Refined → Living → Review → Joyful → Playground → Tidy → Adventure.
- Eski sahneler regresyon ve geri dönüş içindir; aktif işleme kazara eski sahne üzerinden başlama.
- Adventure, Tidy yerleşimini ve Playground/Joyful çalışma bileşenlerini birlikte kullanır.
- GameManager, ArcadeDirector, ReviewSession, JoyfulRun, PlaygroundRun ve AdventureEncounters birlikte çalışır.
- Birçok dal GetComponent/özellik bayrağıyla seçilir; eski uyumluluk kollarını körlemesine kaldırma.
- Yeni sahne üretimi pahalı ve kalıcıdır; küçük davranış düzeltmesinde tüm zinciri üretme.

## 4. Tek sürüş adımının veri akışı

```text
GameManager.Update
  InputController.Sample → PlayerController.Move → oldX/newX
  ArcadeDirector.Step → alt adımlar, hareket tahmini, ilk swept temas
    eski hazard havuzu + AdventureEncounters.FirstHit
    WorldScroller / mesafe / süre / skor / RunChallenges
    JoyfulRun.Advance → yüzey, kalkış, uçuş, hava mesafesi
    PlaygroundRun.Tick → güç, ustalık, dekor/oyuncak ilerlemesi
    ghost kaydı → gerekiyorsa çarpışma/Relax/sonuç
```

- GameManager dt'yi en çok 0.1 saniye alır; uzun takılmada görülmeyen büyük mesafe atlatmaz.
- ArcadeDirector adımı yaklaşık 1/60 saniyelik alt adımlara böler; ilk temas kesrinde durur.
- Ana simülasyon FixedUpdate/Rigidbody tarafından yürütülmez; trigger callback'i asıl oyun kuralı değildir.
- Model, hareket tahmini ve çarpışma aynı denklemleri kullanmalıdır; yalnız animasyonu değiştirmek yeterli olmaz.
- Sabit seed aynı üretim dizisini verir; tüm oyunun farklı FPS'lerde bit düzeyinde aynı olduğu iddia edilmez.
- Durumlar Ready/Running/Paused/Crashed; arka plandan dönüş otomatik devam ettirmez.

## 5. Koordinatlar, kontrol ve kamera

- Araba gameplay kökü Z=0/Y=0; X değişir. Dünya/engeller -Z yönünde akar, ileri yön +Z'dir.
- Üç üretim merkezi X=-2.25/0/+2.25'tir; oyuncu bu merkezler arasında serbestçe sürer.
- JoyfulConfig: TrackHalfWidth 3.5, PlayerHalfWidth 0.39, PlayerHalfLength 0.7.
- Merkez sınırı yaklaşık ±3.11; DragSensitivity 9, MaxLateralSpeed 9.5.
- Aktif sahnenin bağlı config'ini doğrula; eski Phase 1Config değerlerini genel kural sanma.
- InputController EnhancedTouch'ta tek touchId yakalar; UI üzerinde başlayan sürüklemeyi filtreler.
- İkinci parmak sürüşü devralmaz; güç düğmesi mevcut sürüş yakalamasıyla birlikte sınanır.
- PlayerController/SteeringModel göreli delta/ekran genişliği kullanır; bırakma/pause/reset hedefi temizler.
- Kaygan kartta sınırlı yanal atalet; normal yüzeyde mevcut göreli sürükleme davranışı sürer.
- Görsel Visual yatışı/yüksekliği gameplay kökünden ayrıdır; model dönüşü collider dönüşü sayılmaz.
- PortraitCamera perspektif kadrajını aspect'e göre ayarlar; oda ve masanın birlikte görünmesi tasarım şartıdır.

## 6. Parkur üretimi ve havuzlar

- AdventureSequence: xorshift seed, sekizli karıştırılmış torba, son üç aileyi tekrarlamama.
- PlaygroundBeat; index, safe/risk lane, family, variant ve mutlak mesafeyi taşır.
- DenseCourse aktif akış: ilk beat 24; Arcade aralığı 78/81/84, Relax 86/89/92 birim.
- TabletopCourseRules üç geçidin konumunu ve değişen boşluğunu belirler; kalıcı güvenli şerit yoktur.
- Eski 2.65/3 saniye aralığı yalnız DenseCourse=false tarihsel karşılaştırmada korunur.
- ArcadeDirector yaklaşık 115 birim önünü doldurur; aktif dal AdventureSequence kullanır.
- PlaygroundEncounters Adventure bileşenine yönlendirir; eski sahnede eski aileleri üretir.
- AdventureEncounters ana oyuncak + kitap rampası +22/40/58±2 birimde üç çift engelli geçit kurar.
- Katalog 0–6 oyuncaklar; 7 kitap, 8 spiral defter, 9 kalemliktir. Otobüs 6 tekrar üretilir.
- TabletopCourseUpgrade yalnız Adventure kataloğunu genişletir; oda ve eski prefabları yeniden üretmez.
- DenseCourse tur başında sabitlenir; ghost tabletop-r3 anahtarına ayrılır, eski dosyalar korunur.
- Adventure havuzu başlangıçta 8 tür ×6 örnek; ayrıca mevcut hazard/rampa/yıldız havuzları vardır.
- Havuz tükenmesi sessiz nesne kaybı olarak gizlenmez; sayaç/exception ile görünürdür ve test edilmelidir.
- Template/pivot/mesh hazırlanması sürüş başlangıcında yapılır; sürüş döngüsünde yeni maket üretilmez.
- Daha hızlı misket başlangıç Z'si TravelFactor ile ölçeklenir; sonraki beat'e erken girmesi önlenir.

## 7. Hareket, çarpışma ve uçuş sözleşmesi

- AdventureToy.Motion ve Move/Pose görsel hareketi; AdventureEncounters.Centre/FirstHit temas tahminini besler.
- AdventureSolid: Part, yerel Centre/Half ve Sphere bayrağı; bağımsız Unity Collider gerektirmez.
- Hacimler XZ swept kutu veya küre/kutu testiyle; yükseklik ise ayrı dikey açıklık kontrolüyle değerlendirilir.
- Yeni dönük/karmaşık geometri desteklenecekse mevcut eksene hizalı yaklaşımın sınırını ayrıca ele al.
- Oyuncunun görsel yüksekliği için yaklaşık 0.6 üst açıklık kullanılır; model büyürse bu varsayımı doğrula.
- Prefab ölçeği, Part konumu ve Solid ölçüsü birlikte değişir; root scale değişimi otomatik fizik değildir.
- JoyfulRun; destek yüzeyi, rampanın arka uçtan ayrılması, kısa yan düşüş ve uçuş yüksekliğini yönetir.
- Kitap uçuşu 1.65 saniye; cetvel LaunchFromToy 1.35 saniye; jet yüksekliği ayrı katkıdır.
- PredictHeight ileri konumdaki desteği de hesaplar; yalnız mevcut yüksekliğe bakarak temas atlama.
- Surface kitap/oyuncak mesafelerini ayrı alır: Advance'te kitap hareket etmiş, cetvel henüz etmemiştir.
- Jet son 0.45 saniyede korumalı alçalır; iniş temizliği bu geçişte ve güç sonunda çalışır.
- Roket koruması ve güç sonu iniş temizliği hem eski hem yeni engellerle bağlanmıştır.
- Relax toparlanması 14 birim içindeki eski/yeni engelleri, bağlı prop ve rampaları temizler.
- Uzak karşılaşmalar korunur; Adventure zemin yavaşlatma/kayma durumu da sıfırlanır.

## 8. Skor, kariyer ve kayıt

- StyleScore: yakın geçiş zinciri, yıldız ve ustalık bonusu; CareerRules tur ödülü/araç/görev/başarım.
- ReviewSession sonuç ödemesini runId ile bir kez yapar; tekrar ödeme engeli kayıtlı son koşuları da denetler.
- Adventure kariyer yolu `Application.persistentDataPath/career-adventure-v1.json`.
- CareerSave: JSON payload, SHA 256 checksum, `.pending`, `.backup` ve yükleme doğrulaması.
- Checksum yerel bozulma kontrolüdür; oyuncunun kaydı değiştirmesini engelleyen güvenlik mekanizması değildir.
- Power tüketim/yenilemede kayıt başarısızsa bellek değişikliği geri alınır; gerçek veri üzerinde test yapma.
- İlk Adventure geçişi Playground → Joyful → ilk kariyer sırasıyla mevcut kaynaktan ilerlemeyi taşır.
- TP, araçlar, görev/başarımlar ve harcanmış güç stokları korunur; altı rekor alanı ve dailyDate sıfırlanır.
- Yeni kariyer/yedeği varsa eski kaynak tekrar aktarılmaz; eski dosya değiştirilmez.
- Yeni dosya ilk normal başarılı kayıtta yazılır; başarısız kayıt eski dosyayı silmez.
- Mesafe/stil yalnız ArcadeDirector.DistanceRecordKey/StyleRecordKey ile birleştirilir.
- Eski v0.9 ortak kariyerindeki rekorların kökeni bilinmediği için topluca taşınmaz; eski kaynak korunur.
- Persist=false oturumu PlayerPrefs rekorlarını da içeri almaz; testler geçici veri dizini kullanır.
- GhostReplay: 20 Hz X/Y short örnekleri, gzip, 12000 örnek sınırı, seed/mod ve sürüm başlığı kontrolü.
- Adventure ghost ad alanı adventure-r2-varied/classic; eski ghost dosyaları silinmez.
- ActiveVariedEndings tur başında sabitlenir; tur ortasında Inspector değişikliği profili etkilemez.
- Güç kullanılan tur ghost oynatmaz/yazmaz; kariyer/yerel rekor dosyası bu deneyde sıfırlanmaz.
- Yeni simülasyon/rota değişikliğinde ghost/sunucu kural uyumluluğunu ve migration'ı açık sürümle yönet.

## 9. Görsel, ses ve arayüz

- Art/Adventure/Prefabs altında sekiz maket; Moving/Options/ExitArrows/Signal/Link/Shadow alanları sözleşmedir.
- Statik parçalar materyale göre birleştirilmiştir; dinamik pivotu statik mesh'e gömme.
- TidyRoom kenar hücreleri ve yakın dekoru ortak 64 birim fazda taşır; defter/kalemliğe boşluk ayırır.
- Oda mobilyası ve raf radyosu statiktir; oyuncuya göre akan gameplay/yakın yüzeyden ayrıdır.
- RoomDayCycle güneş/ay, pencere dışı atmosfer ve oda ışığını; PlaygroundAtmosphere masa bölgelerini yönetir.
- ReviewFrontend/PlaygroundUI menü, görev, garaj ve güçleri; PrototypeUI temel durum görünümünü kurar.
- VehicleDefinition.VisualPrefab garajı, ReviewSession.Visuals sürüş modelini besler; ikisini birlikte güncelle.
- ToyMusic ayrı menü/yarış müziği; ToyAudioSettings motor/efekt/müzik kanallarını kaydeder.
- Üçüncü taraf içerik için `THIRD_PARTY_ASSETS.md` kaynak/lisans kaydını koru.

## 10. Grafik profili ve cihaz ölçümü

- GraphicsProfileController paylaşılan varlığı değiştirmek yerine çalışma zamanı URP kopyası oluşturur.
- Yüksek: renderScale 1, MSAA 4, gölge 75/cascade 2/2048, hedef 60 FPS.
- Ekonomik: renderScale 0.8, MSAA 2, gölge 35/cascade 1/1024, hedef 30 FPS.
- Renderer seçimi ve postprocessing bileşen ayarına bağlıdır; eski ilk-prototip bütçesiyle karıştırma.
- PerformanceProbe yerel FPS/p95/RAM raporu üretir; editör ve otomatik witness bayraklarını kaydeder.
- Benchmark'ın eski ArcadeSequence yolu vardır; Adventure ölçümü öncesi K4.3 kontrolünü yap.
- Draw-call/GC/CPU/GPU etkisini gerçek cihazda ölç; üçgen sayısından tek başına FPS çıkarma.

## 11. Online sınırı ve yayın

- ReviewOnline / ILeaderboardService yerel ve online adaptör sınırıdır; UnityWebRequest kullanır.
- Configured, JoyfulRun varsa false; Adventure bu bileşeni taşıdığı için online etkin değildir.
- Server `review-r1`: SQLite, server UTC, geçici oturum, tek kullanımlık bilet, input yeniden simülasyonu.
- Adventure hız/güç/zıplama/yüzey kuralları bu eski sunucunun eşdeğeri değildir.
- Sunucu doğrulaması botlara/optimal input'a tam koruma sağlamaz; üretim ölçeği kanıtı yoktur.
- Kalıcı kimlik, kurtarma, üretim HTTPS, veri saklama/silme ve yük/güvenlik kabulü açık işlerdir.
- Yeni sağlayıcı veya backend seçimi K5 kararıdır; istemciye sır gömme, eski endpoint'i açma.
- AndroidBuildPlan açık profille sahne/çıktı/seçenek seçer; eksik sahnede fallback veya generator yoktur.
- AndroidPrototypeBuild.Build/Validate batch girişleridir; CLI -brakeToyProfile kullanılır, varsayılan Adventure.
- APK adayları Builds/Android/Candidates altına yazılır; profil mevcut PlayerSettings sürümünü değiştirmez.
- AdventureValidation.Check/CheckAndBuild denetimi ve APK akışı için `DOGRULAMA.md` kullan.

## 12. Değişiklikte etki haritası

- Mekanik: Core kuralı +Runtime tahmin/poz +prefab hacmi +UI uyarısı +hedefli PlayMode testi.
- Araç: prefab/sahne görseli +garaj +teker/power pivotu +ortak gameplay ölçüleri.
- Ekonomi/kayıt: CareerRules +ReviewSession +CareerSave +migration/çift ödeme testi.
- Üretim/seed: Sequence +aktif director dalı +witness/ghost +sunucu kural sürümü değerlendirmesi.
- Sanat/ışık: aktif sahne/kalite kopyası +önce/sonra motor görüntüsü +cihaz bütçesi.
- Belge: güncel sözleşme +kaynak bağlantısı +Notion eşleştirmesi; tarihi raporları yeniden yazma.
