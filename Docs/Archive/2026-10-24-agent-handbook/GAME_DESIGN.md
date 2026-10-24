## 24 Ekim — v0.8 Playground

Yeni ToyRoomPlayground sahnesi ve feat/playground-v08 dalı; v0.7 korunur. Güvenli/riskli yol, bağlı altı karşılaşma ailesi, isteğe bağlı domino ile yol açma, sinyalli açılıp kapanan oyuncak garaj kapısı/hareketli oyuncak geçişleri, üç yatay konuma dağılan kitap rampaları ve farklı hareketleri bağlayan ustalık bonusu uygulandı. Rampa artık dar merkez toleransı yerine aracın teker/gövde temas alanını kullanır; yan giriş/çıkış ve kenarları sınandı.

3 sn yavaşlatma, 3 sn korumalı roket, 5 sn jetpack. İlk kariyere üçer kalıcı hak; menüde kullanım başına 350/500/650 TP yenileme. Retry hak vermez; pause süreyi durdurur. Gerçek para/coin/IAP yok. Yardımlı tur etiketlenir; güç kullanıldığında kişisel ghost gizlenir ve yeni ghost yazılmaz. Yeni rekor/kariyer dosyası ayrı; TP/açılmış araçlar taşınır.

Masa bölgeleri ve domino Ayarlar'dan ayrı kapatılır. Oda ışığında yön/kontrast, plastik-kumaş-kauçuk-metal yüzey ayrımı; odanın üst kısmını açık bırakan menü, simgeli görev/rozet kartları ve farklı araç siluetleri eklendi. Müzik korunur. Araçlar ayrıntılı son model değil, sonraki model agent çalışmasına temel siluetlerdir.

Son Unity: **102/102** (48 EditMode +54 PlayMode). Fiziksel v0.8 FPS/p95/ısınma ve insan sanat/oynanış kabulü bekler. Canlı online ve mağaza kabulü yok. Notion yeni sayfa oluşturma ücretsiz blok sınırından reddedildi; NOTION_PENDING-v08.md içinde eşitleme kaydı tutulur.

## 23 Ekim 2026 — v0.7 Joyful Room

Yeni sahne ToyRoomJoyful, dal feat/joyful-room-v07. Ayrı CC0 menü ve yarış müziği, gerçek plastik korna/oyuncak çarpma kaydı ve açıklamalı üç ses kanalı eklendi. Arcade hız 14→30; 10. saniyede yaklaşık 22.2, 30. saniyede 28.1. Son sekiz kalıbı hatırlayan seedli 32 kalıp ailesi; sürekli yatay ofset, oyuncak bebek/ayı/ördek/roket/robot, hareketli otobüs ve gelişen falsolu 1–3 top var. Yeşil oklu kitap rampaları 1.65 saniyelik uçuş ve yalnız havada kazanılan TP bölümüne 2x verir.

Mint oyun matı/kremsi masa, daha geniş oda kadrajı, radyo, oyuncak sandıkları, renkli halı, flama/gezegen süsü ve 120 saniyelik güneş/ay/yıldız/kuş döngüsü eklendi. Önceki sahneler korundu. TP/açılmış araçlar ilk yüklemede taşınır; yeni hız kuralları nedeniyle rekor, kariyer dosyası ve X+Y ghost sürümlendi. Eski review-r1 sunucuya yeni joyful-r2 skoru gönderilmez; canlı online zaten kurulmamıştı.

Son Unity koşusu 44 EditMode +43 PlayMode =87/87 geçti. Bu sonuç fiziksel cihaz FPS/ısınma veya insan ses/görsel kabulü değildir. Ayrıntılar MILESTONE-v07.md ve Tests/*-v07.xml. Android paket ve teslim kanıtları ANDROID_BUILD_REPORT-v07.json / DELIVERY-v07.json içinde ayrı kaydedilir.

# v0.6 uygulanmış tasarım ekleri

Arcade korunur. Relax 7→10 hız ve üç hak kullanır; tek engelli toleranslı satırlar ve çarpışma sonrası yakın tehlike temizliği vardır. Skorları Arcade/günlükten ayrıdır.

Toy Points = min(2500, floor(mesafe/10)+floor(stil/100)) +görev/tek seferlik başarım ödülleri. Merkezi satın alma 10 aracın örnek fiyatlarını kullanır; üç iz/korna seçeneği vardır. Üç görev iki tamamlanan turda yenilenir, 15 başarım kalıcıdır. Bu ekonomi oyuncu dengesi kabulü değildir.

Ghost mod başına son parkurun en iyi kişisel kaydıdır; farklı seed'de oynatılmaz. Günlük antrenman çevrimdışı cihaz UTC gününe bağlıdır. Doğrulanmış daily yalnızca uygunluk sonrası sunucu biletiyle mümkündür; gönderilen input sunucuda tekrar simüle edilir.

Kullanıcıya sunulan 10 model aynı temel aileden türetilmiş varyantlardır; bağımsız nihai tasarım kalitesi iddiası yoktur. Zorluk/parkur, müzik ve görsel kalite sonraki revizyon turuna kaydedildi.

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

## v0.3 kullanıcı yönü ve uygulanan çekirdek

Kullanıcının sonraki kararıyla 0–25 saniye sabit hız yerine ilk saniyeden yumuşak hız artışı (10→18) ve yaklaşık 2.2 saniyede ilk karşılaşma uygulanır. Görsel hedef daha gerçekçi oyuncak malzemeleri ve modellerdir; onaylanan AI konsept kalite referansıdır, mevcut gerçek motor çıktısı ayrıca değerlendirilir.

Sınırlı seedli kalıp ailesi, tek/duble kapılar ve nefes satırları, ilk 6–7 saniyede uyarılı top; Near Miss .005–.48 kenar boşluğu, tehlike tamamen geçtikten sonra bir ödül; 100 temel stil, 1/2/3/5× kombo, 6 saniye timeout, 8+ Combo Fever görsel iz. Mesafe/stil ayrı yerel rekorlar. Tam genel üretici/ilerleme/online henüz tamam değil. Ayrıntı ARCADE_MILESTONE.md.

## v0.4 karşılaşma ve kalite güncellemesi

Her yeni tur farklı seed, aynı parkur düğmesi oturumdaki aynı diziyi kurar. Güvenli koridor en fazla iki kez tekrarlanır; mesafeler 25–31, nefes aralığı 34; toplar 5–8 satır arayla farklı kesişim/eğimler alır. Komşu şerit geçişi ve güvenli koridor ailesi korunur. Hız/ilk tehlike v0.3'teki erken eğriyi kullanır. Görsel impact gameplay kutusunu etkilemez. Final sanat ve insan eğlence kabulü henüz yok.

## v0.5 kısa tur derinliği

Üç kümülatif tur hedefi: 60 m sürüş, 3 risk yıldızı, 6 yakın geçiş. Her yeni turda sıfırlanır. Sabit engel sıralarında güvenli koridorun engel tarafına .72 birim yaklaştırılan yıldız isteğe bağlıdır; top satırında yıldız yoktur. Her yıldız +75 stil verir; yakın geçiş/kombo sayısını yükseltmez. Çarpışma alt adımında yıldız ödülü verilmez. İlk engel 24 m'de; ritimli sekizli diziler yan yana erişilebilir şerit geçişleri kullanır, ayna yönü/engel/top aralıkları seed ile değişir. Bu kısa hedefler Faz 4 kalıcı görev/garaj/ekonomi sisteminin yerine geçmez.
