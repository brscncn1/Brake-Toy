## 23 Ekim 2026 — v0.7 Joyful Room

Yeni sahne ToyRoomJoyful, dal feat/joyful-room-v07. Ayrı CC0 menü ve yarış müziği, gerçek plastik korna/oyuncak çarpma kaydı ve açıklamalı üç ses kanalı eklendi. Arcade hız 14→30; 10. saniyede yaklaşık 22.2, 30. saniyede 28.1. Son sekiz kalıbı hatırlayan seedli 32 kalıp ailesi; sürekli yatay ofset, oyuncak bebek/ayı/ördek/roket/robot, hareketli otobüs ve gelişen falsolu 1–3 top var. Yeşil oklu kitap rampaları 1.65 saniyelik uçuş ve yalnız havada kazanılan TP bölümüne 2x verir.

Mint oyun matı/kremsi masa, daha geniş oda kadrajı, radyo, oyuncak sandıkları, renkli halı, flama/gezegen süsü ve 120 saniyelik güneş/ay/yıldız/kuş döngüsü eklendi. Önceki sahneler korundu. TP/açılmış araçlar ilk yüklemede taşınır; yeni hız kuralları nedeniyle rekor, kariyer dosyası ve X+Y ghost sürümlendi. Eski review-r1 sunucuya yeni joyful-r2 skoru gönderilmez; canlı online zaten kurulmamıştı.

Son Unity koşusu 44 EditMode +43 PlayMode =87/87 geçti. Bu sonuç fiziksel cihaz FPS/ısınma veya insan ses/görsel kabulü değildir. Ayrıntılar MILESTONE-v07.md ve Tests/*-v07.xml. Android paket ve teslim kanıtları ANDROID_BUILD_REPORT-v07.json / DELIVERY-v07.json içinde ayrı kaydedilir.

# v0.6 uygulama ekleri

- Core/Career.cs: saf kariyer/ekonomi/görev/başarım/Relax kuralları ve kayıt doğrulaması.
- CareerSave: sürümlü JSON payload +SHA256 checksum, pending/backup fallback. Yerel bozulma tespiti; anti-cheat değildir.
- ReviewSession: mod/ödül/araç seçimi, Relax hakları, kalıcılık. GameManager tur akışıyla bağlanır.
- ReviewFrontend: bir kez kurulan menüler, gerçek 3D RenderTexture garajı, mod ve ayarlar.
- VehicleDefinition: ScriptableObject verisi; 10 görsel aynı gameplay kökünü paylaşır.
- GhostReplay: 20 Hz quantized short X, gzip, 12000 örnek/10 dakika, seed/mod/header kontrolü; mod başına son seed dosyası.
- ILeaderboardService +Local/OnlineLeaderboardService; ReviewOnline: yapılandırma uygunluk kapısı, isteğe bağlı UnityWebRequest, yalnızca bellekte oturum, sunucu bileti ve input kaydı.
- Server/app.py/rules.py: SQLite ve server UTC, one-time ticket, yeniden simülasyon, ayrı mesafe/stil rekorları. Üretim account continuity/kurtarma tamamlanmadı; Server/README.md sınırdır.

Önceki v0.5 ve eski sahneler Review=null yoluyla korunur. Testler Persist=false ile gerçek oyuncu kariyer/rekorlarına yazmaz. Android offline aday HTTPS dışı endpoint kabul etmez; loopback yalnız Editor entegrasyon aracı içindir.

# Mimari

## İlk kararlar

Unity 6.3 LTS, gerçek 3D URP, Android öncelikli portre. Bir dünya birimi, oyuncak ölçeğindeki bir oyun birimidir; gerçek metre iddiası yoktur. Sparky collider'ı 0.78 × 0.70 × 1.40, merkezi (0, 0.35, 0). Araç kökü Y=0, Z=0; yalnızca X değişir. Parkur kullanılabilir X aralığı ±3.5, araç merkezi sınırı ±3.11. Engel kökleri ölçek 1/dönüş 0. Zemin üst yüzeyi yaklaşık Y=-0.02.

Kamera 52 derece yüksek açı ve 50 derece dikey perspektif kullanır. Orthographic daha sabit boyut sunar; hafif perspektif oyuncak ölçeğini ve ileri derinliği daha iyi hissettirdiği için ilk tercih perspektiftir. Kamera mesafesi aspect oranından hesaplanır; araç gövde merkezi alttan %25'te kalır, parkur kenarlarında 0.8 birim çerçeve payı bulunur. Yatay takip yoktur. Bu matematik Unity PlayMode kamera testinde 9:16, 9:19.5, 9:21 ve 3:4 oranlarında doğrulandı.

## Bağımlılıklar

```text
BrakeToy.Core (System; Unity bağımlılığı yok)
  SteeringModel         göreli sürükleme, sınır, hız, bırakma/reset
  SweptCollision        sürekli iki eksenli slab kesişimi
       ↑
BrakeToy.Runtime (Unity + Input System + UnityEngine.UI)
  GameManager → InputController → PlayerController → SteeringModel
              → CollisionManager → SweptCollision
              → WorldScroller → ObstacleView
              → PrototypeUI
  PortraitCamera → GameConfig (ScriptableObject)
       ↑
BrakeToy.Editor (yalnızca editör; URP asset oluşturma dahil)
  PrototypeBuilder → sahne, prefab, materyal, konfigürasyon
```

## Kare sırası

GameManager tek Update içinde önce giriş örnekler, sonra aracı hareket ettirir. Önceki/yeni araç X konumları ve engellerin Z ilerlemesi birlikte sürekli çarpışma testine girer. Tüm engeller içinden ilk temas zamanı seçilir. Çarpışmada hem araba hem dünya kare içindeki bu zamana kadar ilerletilip durur. Collider trigger callback'leri oyun kuralını yürütmez; BoxCollider'lar editör görünürlüğü ve şekil tanımı sağlar. Rigidbody/karmaşık fizik gerekmez.

Simülasyon adımı `min(Time.deltaTime, 0.1)`; uzun takılmalarda oyun zamanı yavaşlar. Faz 1 skor/replay veya deterministik koşu garantisi içermez. Faz 2 ve replay öncesinde sabit zaman çizelgesi, hız eğrisi ve seed sözleşmesi geliştirilecek. Çarpışma matematiği verilen aynı giriş için deterministiktir; farklı kare hızlarında tüm oyunun bit düzeyinde aynı olduğu iddia edilmez.

Giriş EnhancedTouch ile tek touchId yakalar; ikinci parmak kontrolü devralmaz. Yakalama yalnızca Began'de başlar. Mouse ayrı desteklenir; basılı duruma geçiş yerel olarak izlenerek aynı karede birden fazla input update durumunda da yakalama korunur. UI üzerine başlayan sürükleme filtrelenir. Bırakma, duraklatma, odak kaybı ve restart yakalamayı temizler. Ekran genişliğine normalize edilen yatay fark, hızla sınırlandırılan hedefe gider. Görsel yatış collider kökünden ayrıdır.

## Yaşam döngüsü

`Ready → Running → Crashed → Running`, ayrıca `Running ↔ Paused`. İlk açılış oyunu kendiliğinden başlatmaz. Odağa geri dönmek oyunu otomatik başlatmaz; kullanıcı DEVAM ET'e basar. Restart sahneyi yeniden yüklemez, başlangıç konumlarını geri yükler. İlk kurulumda yaratılan 7 zemin parçası ve 8 engel tekrar kullanılır. Sürekli Instantiate/Destroy yapılmaz. Genel amaçlı havuz yöneticisi Faz 2'de gerekir; şimdilik sabit diziler yeterlidir.

## Üretilen varlıklar

`ToyRoom.unity`, `Sparky.prefab`, `Book.prefab`, `BlockTower.prefab`, `Eraser.prefab`, `WoodFloor.prefab`, ortak plastik/zemin materyalleri, `Phase1Config.asset`, `MobileRenderer.asset` ve `MobileURP.asset` ilk Unity açılışında oluşturulur. Mevcut ana sahnenin üzerine yazılmaz; var olan prefab/materyal/config de korunur. Manuel model değişimi prefabın Visual alt ağacından yapılabilir. Yeniden üretim bir güncelleme/migration sistemi değildir.

## Genişleme sınırları

- Faz 2: PatternGenerator, ulaşılabilirlik doğrulaması, DifficultyManager, Score/NearMiss/Combo ve genel havuz. Sabit parkuru değiştirecek.
- Faz 3: ObstacleDefinition/EnvironmentDefinition verileri, hareket tahmini, tenis topu ve ön uyarı. Mevcut XZ kutu testi küreler ve dönük/hareketli nesneler için yeterli sayılmayacak.
- Faz 4: CarDefinition (10 eşit gameplay profili), ekonomi, kozmetik, görev/başarı tanımları, sürümlü kayıt. Core kuralları UI'ya bağlanmayacak.
- Faz 5: ILeaderboardService sınırı; önce yerel mock, sonra uygunluk değerlendirmesiyle UGS adapter. Sunucu skor doğrulaması ve replay protokolü ayrı. Günlük seed güvenilir sunucu saatine bağlı; ghost yalnızca aynı seed/kural sürümünde.
- Faz 6: Reklam/ödeme sağlayıcısı soyutlaması ancak yayın uygunluğu doğrulanınca. İlk prototipe SDK eklenmez.

Gelecek sistemler için boş, çalışıyor izlenimi veren sınıflar eklenmedi. Adlar sorumluluk önerileridir; gereksiz EventBus veya servis katmanları kurulmadı.

## Performans bütçesi ve sınırlamalar

Tek yönlü ışık, 1024 shadow map, 35 birim gölge mesafesi, tek cascade, 2× MSAA, HDR/postprocessing/depth/opaque texture kapalı. Ortak materyaller ve basit primitive modeller. Hedef 60 FPS; 30 FPS seçeneği gelecekte ölçüme göre sunulacak. UI pointer raycast tamponu tekrar kullanılır; ilk başlatma tahsisleri kabul edilir. Düşük/orta Android Profiler ölçümü yoktur. Uzun seans, ısınma, bellek, overdraw ve GPU maliyeti fiziksel cihazda sınanacak.

## v0.2 oda sunumu

ToyRoomTabletop ve RoomConfig ilk prototipten ayrı varlıklardır. Aktif kamera 20°'dir; önceki 52° Faz 1 konfigürasyonu korunur. Perspektif çerçeve hesabı aynı, kontrol/collider değerleri aynıdır. Masa 12, kullanılabilir sürüş alanı 7 birimdir.

TabletopSceneBuilder yeni ortak materyal, yuvarlatılmış mesh, ahşap doku, prefab ve oda geometrisini oluşturur. Büyük yüzeylerde düz normalleri korumak için bevel sınırları mesh'te açıkça örneklenir. Eski sahnenin üzerine yazılmaz.

ToyWheelAnimation yalnızca Running sırasında görsel tekerlekleri döndürür. TabletopDecorScroller, WorldScroller'ın DistanceAdvanced ve RunReset olaylarını dinler; aynı çarpışma kesri ve restart zamanında yakın dekorları ilerletir. Oda mobilyaları gameplay collider'ı taşımaz; statik oda, kayan yakın yüzeyden ayrıdır. Bu sonsuz arcade sunumudur; fiziksel olarak sonsuz bir masa veya serbest odada dolaşma sistemi değildir.

AndroidPrototypeBuild yeni sahne varsa onu derler. Unity test sahne listesinde eski sahne de regresyon için korunur. Notion doküman/günlük eşitlemesi Docs/NOTION_INDEX.json ve AGENTS.md ile sonraki çalışmalar için tanımlanmıştır.

## v0.3 Arcade Workshop

GameManager opsiyonel ArcadeDirector ile yeni sahnede arcade yolunu çalıştırır; eski sahneler eski yolu korur. Core ArcadeRules/ArcadeSequence/StyleScore motor bağımsızdır. ArcadeDirector 24 ArcadeHazard örneğini aktifleştirir/geri alır; ilerleme ve göreli hareketli çarpışma alt adımlarla hesaplanır. Top Minkowski toplamı üzerinden SweptCircleBox kullanır; kutularda mevcut SweptCollision devam eder. WorldScroller mesafe/reset olayları yakın dekorları aynı temas kesrinde taşır.

ArcadeHUD ayrı sürüş/sonuç/uyarı, ArcadeAudio özgün sentez sesleri, ArcadeFeedback collider/hız değiştirmeyen yeniden kullanılan Fever şeritleridir. Yerel v03 mesafe/stil PlayerPrefs anahtarları küresel sonuç değildir.

WorkshopSceneBuilder ayrı scene/config/prefab/mesh/material/renderer üretir. Araç gövdesi/tekerlekler ve sabit oda materyal bazında birleştirilir; yakın dekor hedefleri ayrı korunur. ModelAuditCapture ve PreviewCapture gerçek Unity çıktıları üretir. Kaydedilmiş sahnede sanat üretim/refine komutları geliştirici araçlarıdır, Open yeterlidir.

## v0.4 yapı

ObstacleDefinition: prefab/kind/pool size/collision footprint. ArcadeDirector: katalog örnekleri sahne açılışında bir kez, CurrentSeed/BeginRun(sameRoute), sürüşte tahsissiz havuz yeniden kullanımı. ArcadeSequence varied modu eski diziyi varsayılan durumda korur. GraphicsProfileController iki renderer kullanan özel runtime pipeline kopyasını sahiplenir ve sahne kapanışında geri yükler. PerformanceProbe yalnızca isteğe bağlı yerel 60 sn kare kaydı tutar; Android JNI yerleşik modülü explicit development benchmark intent'ini okur. ToyImpactFeedback kök/collider yerine visual/kameraya kısa impulse uygular. Quiet Wood, lisanslı URP Lit türevidir. v0.4 varlık/sahne üreticileri eski sanat dosyalarını değiştirmez.

## v0.5 Living Workshop

ToyRoomLiving / LivingConfig / Art/Living ayrı varyanttır. LivingCarBuilder eğrisel kapalı gövde ve cam yüzeyleri; LivingRoomBuilder pamuk kıvrımları ve kaynak FBX sandalye üretir. LivingMeshFactory yalnızca Living/Meshes içine kayıt yapar. Oda materyal bazında birleştirilir; yakın dekorlar WorldScroller olaylarına bağlı kalır. Eski sahne/varlıklar değiştirilmez.

ArcadeSequence üçüncü choreographed seçeneği eskilerin varsayılanını korur. RunGoals saf C# durumudur. RunChallenges 12 yıldız havuzunu ArcadeDirector alt adımına bağlar; pause hareketi durdurur, retry sıfırlar, hit adımı ödülü engeller. v0.5 stil anahtarı BrakeToy.v05.style; eski BrakeToy.v03.style ve mesafe kaydı korunur. Kamera sınırlı yatay takip kullanır. Runtime kalite kopyası yüksek profilde ACES/Bloom'u açar, ekonomikte kapatır. ToyAudioSettings motor ve efekt ayarlarını ayrı saklar. Kayıtlar CC0; ses kaynakları sahne başlangıcında oluşturulur.
