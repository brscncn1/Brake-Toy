# BRAKE TOY — Ana tasarım

Ana gereksinim kaynağı `SOURCE_REQUIREMENTS.md` dosyasıdır. Bu dosya uygulanacak tasarımı düzenler; aşağıdaki nihai özelliklerin listelenmesi hazır oldukları anlamına gelmez. Uygulama durumu `ROADMAP.md` içindedir.

## Onaylanan oda/masa yönü — 21 Ekim 2026

Kullanıcı ilk prototipte oda hissinin eksik olduğunu belirtti ve çocuk odasındaki masa üzerinde sürüş istedi. Konsept görüntüsünü onayladı. Toy Room'un ilk oynanabilir görsel çalışması bu yönde: çevrede oda mobilyaları, ön planda ahşap oyun masası. v0.2 bu kompozisyonu gerçek Unity sahnesinde uygular; nihai sanat kalitesi değildir. Orijinal serbest kontrol, arcade kuralları ve aşamalı kapsam korunur.

## Kimlik ve deneyim

**NO BRAKES. BIG TROUBLE.** Pilleri değiştirilen Sparky'nin güç düğmesi takılır. Küçük kırmızı oyuncak yarış arabası hiç durmadan dev ev eşyalarının arasından geçer. Oyuncu tek parmakla serbest sağ-sol yönlendirir; gaz/fren veya üç şerit sistemi yoktur. Çocuklar için saniyeler içinde anlaşılır, yetişkinler için mesafe ve stil ustalığı sunar. Kısa seans ve hesap gerektirmeyen çevrimdışı oynanış temel koşuldur.

Gerçek 3D, stilize low-poly/soft plastik oyuncak estetiği, canlı renkler, okunabilir silüetler, sıcak ışık ve yumuşak gölgeler. Önce mekanik, sonra görsel cila. İlk harita **Toy Room**: ahşap zemin, bloklar/kuleler, kitaplar, silgiler, açık defterler, tenis topları, çıkartmalar, küçük dekorlar. Dekorlar ve tehlikeler ayırt edilebilir. Sonraki haritalar **Desk Disaster** ve **Kitchen Chaos**; içerik, ışık, materyal, engel ve olaylar ayrı dünya tanımlarıyla yüklenir.

## Kontrol ve kamera

Portre; hafif perspektifli yüksek açılı kamera, araba alt %20–30'da, yatay kamera takibi yok. Göreli sürükleme çözünürlükten bağımsızdır. Dokununca ışınlanma ve yeniden dokununca sıçrama olmaz. Parmak bırakıldığında X sabit kalır. Hassasiyet/hız/tepki ayarlanabilir. Editörde mouse desteği bulunur. Görsel yatış çarpışma kurallarını değiştirmez. Araç X boyunca hareket eder, dünya -Z boyunca kayar; zemin/engeller tekrar kullanılır.

## Modlar ve zorluk

| Kural | Arcade | Relax |
|---|---|---|
| Çarpışma hakkı | 1 ciddi çarpışma | 3 |
| Hız | Zamanla yükselir | Daha düşük başlangıç ve tavan |
| Dizilim | Rekabetçi ama geçilebilir | Daha toleranslı |
| Geri bildirim | Skor/kombo | Büyük ve kolay anlaşılır |
| Küresel sıralama | Yalnızca uygun ve doğrulanmış koşu | Gönderilmez |

Arcade öneri eğrisi: 0–25 s 1.00×; 25–50 s 1.12×; 50–75 s 1.25×; 75–100 s 1.40×; 100–125 s 1.58×; sonrası en fazla yaklaşık 1.80×. Geçişler yumuşak, kısa motor/vites ve görsel bildirimli. Aralıklar aşırı daraltılmaz; hızın tepki baskısı önceliklidir. Değerler denge testlerine açıktır. Faz 1 yalnızca sabit hızlı tek çarpışmalı temeli uygular.

## Engel sistemi ve adalet

İlk türler büyük kitap, blok kulesi, silgi, oyuncak blok, hareketli tenis topu. Sonra kurmalı araba, yuvarlanan kalem, domino zinciri ve mutfakta meyveler. Her tür prefab ve yapılandırma ile tanımlanır.

Nihai üretici deterministik seed, ağırlıklı kalıplar, yatay varyasyon, boyut, aynalama, tekli/çoklu kombinasyon, boş dinlenme aralığı, nadir olay ve yakın geçmiş tekrar azaltması kullanır. Bağımsız saf rastgele spawn yapılmaz. Ulaşılabilirlik kontrolü araç mevcut X konumu, yatay hız, dünya hızı, engel hareketi ve kalan süreyi birlikte değerlendirir. Yaklaşık 1.5 s minimum tepki başlangıç hedefidir. Başarısız aday yeniden üretilir; sınırlı denemeden sonra güvenli yedek gelir. Birleşik ve hareketli engeller de doğrulanır. Binlerce seed otomatik sınanır; insan reflekslerinin kusursuz modeli olduğu iddia edilmez.

Kutu/küre şekilleri görsellerle uyumlu olur. Yüksek hızda kareler arası atlama önlenir. Çarpışmada kontrol kapanır, kısa komik animasyon, hafif sarsıntı, oyuncak sesi/parçacık ve sonuç ekranı gelir. Takla, küçük teker çıkması, kısa spin, korna, yıldızlar gibi çeşitler 1–2 saniyeyi aşmaz; retry bekletilmez. Kan/korkutucu şiddet yoktur.

## Near Miss, kombo ve Fever

Fiziksel collider ve yapılandırılabilir yakın geçiş bölgesi ayrıdır. Ödül engel tamamen geçildikten sonra, aynı engel/geçiş için bir kez verilir. Çarpılan manevra ödül vermez. Başlangıç stil ödülü 100.

| Ardışık Near Miss | Çarpan |
|---|---:|
| 1–2 | 1× |
| 3–4 | 2× |
| 5–7 | 3× |
| 8–11 | 4× |
| 12+ | 5× |

Yaklaşık 6 s yeni Near Miss olmazsa veya çarpışmada kombo biter. Yüksek komboda renkli iz, parçacık, FEVER yazısı, müzik katmanı ve güçlü geri bildirim olur. Fever tamamen görsel/işitsel ödüldür; dokunulmazlık veya kontrol avantajı vermez.

## Toy Chaos ve yaşayan çevre

Önce **Rolling Ball**: anlaşılır ön uyarı ile yandan gelen büyük tenis topu. Sonra Pencil Roll, Wind-Up Racer, Domino Chain, Kitchen'da Rolling Orange. Olaylar ulaşılabilirlik hesabına girer; kaçınılmaz sürpriz tehlike yaratmaz, aynı olay art arda sık tekrarlanmaz. Hareketli engel de Near Miss verebilir. Yoğunluk hız/zorluğa göre değişir.

Dekoratif domino tepkileri ilk sürümde yeni collider tehlikesi üretmez. Mümkün olduğunca kontrollü animasyonla, az fizik maliyetiyle gösterilir.

## Skor, sıralama ve çevrimdışı güven

Mesafe (metre olarak sunulacak ölçek) ve tek koşu stil rekorları ayrıdır. İki sekme: **DISTANCE RANKING** / **STYLE RANKING**. Rank, güvenli takma ad, sonuç, ilk 100, kendi sıra/yakın sıralar, tüm zamanlar; ileride haftalık ve ayrı günlük tablo. Serbest kullanıcı adı yerine onaylı takma adlar değerlendirilecek.

`ILeaderboardService` arayüzü, önce yerel mock, uygunluk sonrasında UGS Authentication/Leaderboards ve gerekirse Cloud Code. Entegrasyondan önce güncel resmî yetenek/fiyat kontrolü gerekir. Kritik yazmalar sunucu katmanından; koşu başlangıcı, süre, seed, hız sınırı ve mümkün skor doğrulaması; gerekirse güçlü replay doğrulaması. İstemci kontrolü güvenli sunucu doğrulaması yerine geçmez.

Bağlantı yoksa oyun/yerel kayıtlar devam eder, sıralamada bağlantı durumu açıklanır. Doğrulanamayan offline skor otomatik küresel güvenilir skor sayılmaz. Relax rekorları Arcade'den ayrı tutulur.

## Garaj, ekonomi ve kozmetik

| Araç | Görsel tema | Önerilen TP |
|---|---|---:|
| Sparky | Kırmızı klasik | Ücretsiz |
| Bubble Bug | Turkuaz yuvarlak mini | 200 |
| Mini Muscle | Turuncu kaslı spor | 500 |
| Candy Cruiser | Pembe şeker renkleri | 900 |
| Pixel Kart | Oyun makinesi | 1.500 |
| Rocket Pop | Roket/sportif | 2.400 |
| Thunder Volt | Elektrik/neon | 3.600 |
| Galaxy Glider | Uzay/yıldız | 5.200 |
| Golden Comet | Altın lüks | 7.200 |
| Crown Racer | Prestij/özel gövde | 10.000 |

Tüm araçlarda gameplay collider, hız ve çeviklik aynıdır. Fiyatlar denge önerisidir. Garaj gerçek 3D seçili/döndürülebilir model, kilitler, maliyet, açma ve kalıcı seçimi sunar. Toy Points sürüş mesafesi ve stil başarısından türetilen ayrı harcanabilir paradır. Harcama mesafe/stil rekorlarını azaltmaz; merkezi ekonomi sistemi yönetir. Yerel kayıt hilesi küresel rekabet avantajı sağlamaz.

Boyalar, lastik izi renkleri, çıkartmalar, korna, parçacık/iz ve rozetler kozmetiktir; performans avantajı vermez. İlk kozmetik havuzu küçük, veri yapısı genişletilebilir olur.

## Görevler ve başarılar

Aynı anda üç kısa görev: 300 m, 5 Near Miss, 3× kombo, tek turda 1.000 stil, topu geçme, araç açma veya 10 yakın geçiş gibi. Başlangıç hedefi en az **15 başarı**. Veri odaklı görevler, ScoreManager değişmeden eklenebilir. Günlük görev ile Daily Challenge ayrıdır; çocukları cezalandıran günlük giriş baskısı yoktur.

## Günlük parkur ve ghost

Daily Challenge'da aynı gün tüm uygun oyuncular aynı seed, başlangıç hızı/koşulları ve kurallarla yarışır; ayrı günlük sıralama. Gün değişimi mümkün olduğunda güvenilir sunucu saatine dayanır. Online/gizlilik uygunluğu sağlanmadan etkinleşmez.

Ghost önceki koşunun zaman/konum kaydıyla oynar, çarpışmasız/yarı saydamdır. Önce aynı seed replay veya Daily Challenge'da; farklı rastgele parkurda bağlamsız ghost gösterilmez. Kayıt sıkıştırılmış, sürümlü ve performans dostu olur.

## Ekranlar ve ses

Main Menu: Play, Garage, Leaderboards, Daily Challenge, Missions, Settings. Mode Selection: Arcade/Relax. HUD: mesafe, stil, kombo, vites, pause. Pause: Resume/Restart/Main Menu. Game Over: mesafe, yeni rekor, stil, TP, retry ve ana menü. Ayrıca Garage, iki sekmeli Leaderboards, Missions ve Settings ekranları.

Ayarlar müzik/SFX/titreşim/hassasiyet/dil/gizliliği içerir. Büyük dokunma alanları, okunabilir semboller; oyun içinde gereksiz düğme kalabalığı yoktur.

Motor/vites, Near Miss/kombo, Fever müzik katmanı, hafif kayma, yüksek hızda duman/rüzgâr, çarpışma/korna/parçacık ve sarsıntı ile game feel. Sesler rahatsız edici tekrara/yüksekliğe kaçmaz; tüm sesler kapatılabilir. Titreşim isteğe bağlı, efekt yoğunluğu kaliteye uyarlanır.

## Yayın ve gizlilik

Ücretsiz oyun; uygun çevrimiçi kullanımda isteğe bağlı TP rewarded ad, bitişlerde seyrek interstitial. Gameplay kapatan reklam veya sürekli banner yoktur. Reklam zorunlu değildir; offline çalışır. Uygunluk doğrulanana kadar reklam/analiz/ödeme SDK'sı kapalı/eklenmemiştir. İleride reklam kaldırma satın alımı için sınır tasarlanır.

Konum/kamera/mikrofon izni, gereksiz kişisel veri, zorunlu hesap, açık sohbet/mesaj, fotoğraf veya açık profil yoktur. Çocuklara uygun olmayan online kimlik/veri özellikleri etkinleşmez. Gerekli ebeveyn erişimi/izin mekanizması ayrıca değerlendirilir; ebeveyn kapısı hukuki izinle eşdeğer sayılmaz.

Yayın öncesinde hedef yaş ve mağaza sınıflandırması, güncel Google Play Families/uygun reklam çözümleri ve kişiselleştirmeme şartları, Apple Kids Category, GDPR, COPPA ve yerel kurallar resmî kaynaklardan ayrıca incelenecek. Bu belge hukuki uygunluğun sağlandığını iddia etmez.

## Kalite hedefi

Orta Android'de ölçülmüş kararlı 60 FPS, gerekirse 30 FPS kalite; düşük bellek, hızlı retry, mesh/materyal paylaşımı, sınırlı gölge/fizik/postprocessing, gerektiğinde baked ışık. Hedefler gerçek cihaz testine bağlıdır. İlk yayın öncesi Android çeşitliliği, erişilebilirlik, mağaza dokümanları, ikon/splash/görseller, test dağıtımı ve hata düzeltmeleri tamamlanır. Desk/Kitchen genişlemeleri ilk yayın için zorunlu değildir.
