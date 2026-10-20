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
