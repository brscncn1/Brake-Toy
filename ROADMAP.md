# Yol haritası

Durum anahtarı: **Kaynak hazır** = uygulama dosyası var, Unity kabulü bekleniyor. **Bekliyor** = henüz uygulanmadı. Bir fazın tamamlanması kendi kabul kriterine bağlıdır.

## Faz 1 — Çalışan 3D prototip

- [x] Çalışma alanını ve standart Unity kurulum yollarını inceleme; kullanıcı Unity kurulu olmadığını doğruladı.
- [x] Ana promptu eksiksiz saklama ve nihai tasarımı düzenleme.
- [x] Unity 6.3 LTS / URP / Input System paket manifesti ve assembly sınırları.
- [x] Tutarlı dünya ölçeği ve perspektif kamera matematiği.
- [x] Primitive 3D Sparky, zemin, kitap, blok kulesi ve silgi üreten editör aracı.
- [x] Göreli dokunma/mouse, sınır ve hız denetimi kaynakları.
- [x] Kayan ve tekrar kullanılan zemin/engel dizileri.
- [x] Sürekli çarpışma, Ready/Running/Paused/Crashed, retry ve odak kaybı yönetimi.
- [x] Çekirdek C# derlemesi ve 19 kontrol; 10.000 örnek çarpışma karşılaştırması.
- [x] Unity EditMode/PlayMode test kaynakları, kullanım ve doğrulama belgeleri.
- [x] Unity Editor ilk import: paket çözümü ve runtime/editor/test assembly derlemesi.
- [x] Üretilmiş sahne/prefab/URP varlıkları ve Unity Play Mode 3D önizlemesinin kontrolü.
- [x] Unity EditMode 19/19 ve PlayMode 4/4 testleri.
- [x] Unity Input System üzerinden mouse/dokunma/UI ve ikinci parmak entegrasyon testleri.
- [ ] Kullanıcının gerçek mouse/telefon ile öznel sürüş hissini onaylaması.
- [x] Android ARM64 / IL2CPP Development APK derlemesi; imza ve arşiv kontrolü.
- [ ] Fiziksel Android dokunma, safe area, kesintiler ve cihaz performansı kontrolü.

**Kabul kriteri:** Telefonda veya Unity editöründe temel sürüş düzgün çalışır. Unity otomatik testleri ile temel döngü/giriş/kamera doğrulandı; oynanabilir sahne hazır. Gerçek kullanıcı sürüş hissi ve fiziksel Android kabulü açık. Faz 2 öncesi ilk adım bu kısa oynanış kontrolüdür.

## Faz 2 — Ana oynanış döngüsü (bekliyor)

- [ ] Deterministik, ağırlıklı, tekrar azaltmalı kalıp üretimi; aynalama/konum/boyut varyasyonu ve dinlenme aralıkları.
- [ ] Mevcut oyuncu konumu, yatay hız, dünya hızı ve hareketli tehlikelere göre ulaşılabilirlik; yaklaşık 1.5 s tepki.
- [ ] Yeniden deneme ve güvenli yedek; seed/bilinen sorun raporu; binlerce dizilim testi.
- [ ] Genel object pool, ObstacleDefinition ve genişletilebilir dünya verileri.
- [ ] Yumuşak hız eğrisi ve vites bildirimi.
- [ ] Mesafe, Near Miss, stil, doğru tek sefer ödül, kombo/zaman aşımı/çarpışma reseti.
- [ ] Görsel/işitsel Combo Fever; gameplay avantajı yok.
- [ ] Sonuç ekranı ve temel yerel rekor sınırı.

**Kabul:** Ana döngü teknik olarak tamam; adalet, yüksek hız collision ve ödül sınırları otomatik/manuel doğrulanır.

## Faz 3 — Toy Room ve game feel (bekliyor)

- [ ] Çocuk odası art yönü: ahşap/dokulu yüzey, blok/kule, kitap, silgi, açık defter, dekor/çıkartma.
- [ ] Araç ve engel modelleri, plastik materyaller, ışık/gölge; tehlike/dekor ayrımı.
- [ ] Hareketli tenis topu ve önden uyarılı Rolling Ball; üretici doğrulamasına dahil etme.
- [ ] Motor, vites, Near Miss, kombo, korna, lastik/rüzgâr; katmanlı müzik ve Fever.
- [ ] Hafif sarsıntı, parçacık/duman/iz; düşük kalite yoğunluğu seçeneği.
- [ ] Çeşitli 1–2 s komik çarpışmalar, bekletmeyen retry.
- [ ] Yeni collider tehlikesi yaratmayan dekoratif domino zinciri.

**Kabul:** Oyun okunabilir, canlı oyuncak dünyası hissi verir; model/ses kullanım hakları ve mobil maliyet belgelenir.

## Faz 4 — İlerleme ve Relax (bekliyor)

- [ ] 10 araç ve eşit gameplay parametreleri/collider'lar.
- [ ] Garaj: gerçek 3D gösterim, döndürme, kilit/maliyet, açma, seçim.
- [ ] Toy Points kazanma/harcama; mesafe/stil rekorlarından bağımsız ekonomi.
- [ ] Genişletilebilir boya/iz/çıkartma/korna/parçacık/rozet kozmetikleri.
- [ ] Üç kısa görev, veri odaklı tanımlar ve en az 15 başarı; günlük giriş cezası yok.
- [ ] Sürümlü kalıcı kayıt, bozuk kayıt ve tekrar açılış testleri.
- [ ] Relax: düşük hız/tavan, daha toleranslı düzen, 3 hak, büyük geri bildirim; Arcade kaydı ve küresel sıralamadan kesin ayrım.
- [ ] Tam ana menü, mod seçimi, pause/restart/menu, sonuç, Missions ve Settings; dil/erişilebilirlik.

**Kabul:** Tekrar oynama hedefleri ve kalıcı ilerleme çalışır; kozmetik/para/araç rekabet avantajı yaratmaz.

## Faz 5 — Çevrimiçi sistemler (bekliyor)

- [ ] Yaş kitlesi, gizlilik/izin ve mağaza uygunluğu; sağlayıcıların güncel özellik/fiyat incelemesi.
- [ ] ILeaderboardService + yerel mock; sonra uygun koşullarda UGS Authentication/Leaderboards/Cloud Code adapter.
- [ ] Güvenli takma adlar; distance/style ayrı tablolar, top100, kendi/yakın sıralar, tüm zamanlar; haftalığa hazır tasarım.
- [ ] Koşu başlangıcı/seed/süre/hız/skor ve kritik yazma sunucu doğrulaması; istemci sınırlarını belgeleme.
- [ ] Ağ hatası/oturum/çevrimdışı toleransı; doğrulanmamış offline skoru güvenilir küresel sonuç saymama.
- [ ] Güvenilir zamanlı Daily Challenge, aynı koşullar, ayrı günlük sıralama; görevlerden bağımsız.
- [ ] Sürümlü/sıkıştırılmış replay ve aynı seed için çarpışmasız yarı saydam ghost.
- [ ] Çocuklar için zorunlu hesap/sohbet/mesaj/fotoğraf/açık profil olmaması ve gerekli izin süreçleri.

**Kabul:** Uygun oyuncuların güvenilir sıralaması, offline oyunu bozmadan çalışır.

## Faz 6 — Yayına hazırlık (bekliyor)

- [ ] Düşük/orta Android cihaz matrisi, uzun sürüş, ısınma/bellek/60 FPS veya 30 FPS profili.
- [ ] Gerçek dokunma hassasiyeti, safe area, erişilebilirlik ve kesinti/geri dönüş testleri.
- [ ] Güncel Google Play Families, Apple Kids Category, GDPR, COPPA ve yerel yükümlülük değerlendirmesi.
- [ ] Gizlilik/mağaza dokümanları, yaş sınıfı, izinler, paket kimliği ve imzalama.
- [ ] Uygunluk sağlanırsa isteğe bağlı rewarded TP ve seyrek bitiş interstitial; kişiselleştirme/sertifikalı çözüm koşulları.
- [ ] Banner veya gameplay kapatan reklam yok; offline engeli yok; ileride reklam kaldırma satın alım sınırı.
- [ ] İkon, splash, ekran görüntüleri, test dağıtımı, oyuncu geri bildirimi ve hata düzeltme.

**Kabul:** Gerçek oyuncu testine ve mağaza incelemesine sunulabilecek Android sürümü. Mağazada yayımlama ayrı bir dış işlem olarak ele alınır.

## Faz 7 — Genişleme (bekliyor, ilk yayın için zorunlu değil)

- [ ] Desk Disaster: çalışma masası, kalem/defter/silgi; Pencil Roll ve Wind-Up Racer.
- [ ] Kitchen Chaos: yiyecek/kutu/gereçler, yuvarlanan meyve ve Rolling Orange.
- [ ] Yeni engeller, olaylar, kozmetik ve görevler; Toy Room oyuncu testleri sonrası.

## Gereksinim izlenebilirliği

Ana prompt bölümleri 1/23/26/27 çalışma yöntemi, mimari ve dokümantasyonda; 2–4 kimlik/dünya/kontrol/modlarda; 5–8 zorluk/üretim/collision/stilde; 9 çevrimiçi planında; 10–12 ilerleme kapsamındadır. 13–15 Toy Chaos/çevre/çarpışma hissinde, 16–17 Daily/ghost'ta, 18–19 UI/seste, 20–21 yayın/gizlilikte, 22 performansta, 24 bu fazlarda, 25 test planında ve 28 mevcut Faz 1 tesliminde izlenir. Ayrıntı kaynağı eksiksiz `SOURCE_REQUIREMENTS.md` dosyasıdır.
