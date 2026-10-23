# v0.9 — Adventure / 2026-10-24

Sekiz parkur ailesi, torba/hafıza, sinyaller, bağlı diziler, yüzey etkileri ve cetvel uçuşu. Tidy/kariyer korunur, domino yok. 112 test. PLAN-v0.9.md ve MILESTONE-v09.md.

## 24 Ekim — v0.8.1 yerleşim düzeltmesi

Yeni ToyRoomTidy sahnesi, fix/table-layout-v081 dalı. Mavi parçalar v0.8 oyuncak kasabası kenar dekorlarıydı; 42 m dekor döngüsüyle 64 m defter/kalemlik döngüsü bağımsız olduğundan tekrar tekrar çakışıyordu. Yeni sahnede iki grup aynı 64 m döngüyü ve ayrılmış boşlukları kullanır. Defter/kalemlik en fazla 1.25 birim genişliğe indirildi, ±4.45 kenar merkezlerine oturtuldu; oyun alanına ve masa dışına taşmaları sınandı. Tüm bölge parçaları hücre olarak birlikte akar, pencere/ev parçaları ayrışmaz.

Radyo eski birleşik masa mesh'inden ayrılıp soldaki oyuncak rafının üstüne taşındı; alt yüzeyi gerçek raf üstünden 0.025 birim yukarıda. Sürüş mesafesiyle hareket etmez. Domino model/üretim/ayar düğmesi aktif Tidy sahnesinden kaldırıldı; yerine mevcut rampa fırsatı kullanılır. Önceki v0.8 sahnesi korunur. Hız eğrisi, karşılaşma aralığı, güç süreleri/fiyatları ve kontrol ayarları değiştirilmedi. Kariyer/TP/araç/güç hakları aynı dosyada korunur; geometrisi farklı eski parkurun ghost'u yeni sahnede yüklenmez.

**104/104 Unity testi** (48 EditMode +56 PlayMode) geçti. Üç bölge, dört tam ortak döngü ve yarım metre adımlı yerleşim örnekleri; raf desteği/radyo sabitliği; domino yokluğu ve eski 102 regresyon dahil. Fiziksel cihaz ve insan görüntü/oynanış kabulü bekler. Notion yeni içerik işlemi önceki ücretsiz blok sınırı nedeniyle engelli; otomatik tekrar yapılmadı, yerel/GitHub notları güncel.

## 24 Ekim — v0.8 Playground

Yeni ToyRoomPlayground sahnesi ve feat/playground-v08 dalı; v0.7 korunur. Güvenli/riskli yol, bağlı altı karşılaşma ailesi, isteğe bağlı domino ile yol açma, sinyalli açılıp kapanan oyuncak garaj kapısı/hareketli oyuncak geçişleri, üç yatay konuma dağılan kitap rampaları ve farklı hareketleri bağlayan ustalık bonusu uygulandı. Rampa artık dar merkez toleransı yerine aracın teker/gövde temas alanını kullanır; yan giriş/çıkış ve kenarları sınandı.

3 sn yavaşlatma, 3 sn korumalı roket, 5 sn jetpack. İlk kariyere üçer kalıcı hak; menüde kullanım başına 350/500/650 TP yenileme. Retry hak vermez; pause süreyi durdurur. Gerçek para/coin/IAP yok. Yardımlı tur etiketlenir; güç kullanıldığında kişisel ghost gizlenir ve yeni ghost yazılmaz. Yeni rekor/kariyer dosyası ayrı; TP/açılmış araçlar taşınır.

Masa bölgeleri ve domino Ayarlar'dan ayrı kapatılır. Oda ışığında yön/kontrast, plastik-kumaş-kauçuk-metal yüzey ayrımı; odanın üst kısmını açık bırakan menü, simgeli görev/rozet kartları ve farklı araç siluetleri eklendi. Müzik korunur. Araçlar ayrıntılı son model değil, sonraki model agent çalışmasına temel siluetlerdir.

Son Unity: **102/102** (48 EditMode +54 PlayMode). Fiziksel v0.8 FPS/p95/ısınma ve insan sanat/oynanış kabulü bekler. Canlı online ve mağaza kabulü yok. Notion yeni sayfa oluşturma ücretsiz blok sınırından reddedildi; NOTION_PENDING-v08.md içinde eşitleme kaydı tutulur.

## 23 Ekim 2026 — v0.7 Joyful Room

Yeni sahne ToyRoomJoyful, dal feat/joyful-room-v07. Ayrı CC0 menü ve yarış müziği, gerçek plastik korna/oyuncak çarpma kaydı ve açıklamalı üç ses kanalı eklendi. Arcade hız 14→30; 10. saniyede yaklaşık 22.2, 30. saniyede 28.1. Son sekiz kalıbı hatırlayan seedli 32 kalıp ailesi; sürekli yatay ofset, oyuncak bebek/ayı/ördek/roket/robot, hareketli otobüs ve gelişen falsolu 1–3 top var. Yeşil oklu kitap rampaları 1.65 saniyelik uçuş ve yalnız havada kazanılan TP bölümüne 2x verir.

Mint oyun matı/kremsi masa, daha geniş oda kadrajı, radyo, oyuncak sandıkları, renkli halı, flama/gezegen süsü ve 120 saniyelik güneş/ay/yıldız/kuş döngüsü eklendi. Önceki sahneler korundu. TP/açılmış araçlar ilk yüklemede taşınır; yeni hız kuralları nedeniyle rekor, kariyer dosyası ve X+Y ghost sürümlendi. Eski review-r1 sunucuya yeni joyful-r2 skoru gönderilmez; canlı online zaten kurulmamıştı.

Son Unity koşusu 44 EditMode +43 PlayMode =87/87 geçti. Bu sonuç fiziksel cihaz FPS/ısınma veya insan ses/görsel kabulü değildir. Ayrıntılar MILESTONE-v07.md ve Tests/*-v07.xml. Android paket ve teslim kanıtları ANDROID_BUILD_REPORT-v07.json / DELIVERY-v07.json içinde ayrı kaydedilir.

# 0.6.0 — review candidate

10 araç varyantı/3D garaj; TP/kariyer yedekli kayıt; kozmetik; görev/başarım; Relax; menüler; günlük antrenman/ghost; gerçek REST transport ve referans skor simülasyonu. 75 Unity +9 server testi ve C# loopback probe geçti. Yeni Review sahnesi önceki sahneleri korur.

Online üretim bağlantısı kapalıdır; stabil online kimlik/kurtarma, cihaz/mağaza kabulü tamamlanmadı. Zorluk/müzik/görsel kullanıcı revizyon listesinde. Reklam/IAP eklenmedi.

# v0.4.0 — Refined Room

- Dokuz kalite başlığında uygulama: ayrı oda/malzeme seti, düşük kontrast ahşap, yumuşak gölge, kabin/çatı/teker boşlukları ve kesintisiz masa/duvar.
- Kompakt HUD/tek menü, yeni parkur/aynı parkur ayrımı, katalogdan başlangıç havuzu ve değişken aralık/top yolları.
- Katmanlı motor/teker/vites, ahşap-metal/kauçuk impact; kısa grounded görsel tepki ve sonuç paneli zamanlaması.
- İzole yüksek/ekonomik grafik profilleri, yerel ölçüm, development benchmark ve tablet kabul matrisi.
- 53/53 Unity testi ve Android ARM64 IL2CPP APK; gerçek önce/sonra ve açık kalite sınırları.

# v0.3.0 — Arcade Workshop

- Erken/sürekli hızlanma; seed'li tek/duble geçitler, yön değişimi ve nefes satırları.
- 24 örnek havuzu, uyarılı çapraz toplar ve sürekli göreli daire-kutu çarpışması.
- Tek sefer Near Miss, kombo/timeout/Fever, ayrı yerel mesafe/stil rekorları, sonuç ekranı.
- HUD/vites/yön uyarısı, özgün sentez sesleri ve ses kapatma.
- Özgün diecast araç, yeni kitap/silgi/blok/top modelleri, gerçek CC0 ahşap, pencere açıklığı, perde/abajur, ayrı renderer/SSAO/reflection ve oda mesh birleştirmesi.
- 41 Unity testi, 128.000 satır / 10.000 top çarpışması kontrolü ve Android APK.

# Değişiklik kaydı

## 0.2.0 — Bedroom Tabletop, 21 Ekim 2026

- Ayrı çocuk odası/masa sahnesi, oda mobilyaları ve kırtasiye dekorları.
- Yuvarlatılmış oyuncak mesh'leri, plastik/ahşap materyalleri ve tekerlek animasyonu.
- 20° oda kamerası; 28° karşılaştırması ve daha hafif menü örtüsü.
- World ilerlemesi/reset ile eşzamanlı yakın dekor akışı; kontrol/collider değerleri korundu.
- Unity 27/27 test geçti; Android ARM64 IL2CPP Development APK üretildi.
- GitHub deposu/ilk APK Release ve özel Notion proje merkezi/ayrı belgeler/günlük kuruldu.
- Gerçek cihaz FPS/dokunma testi ve nihai art tamamlanmadı.


## 0.1.0 — Faz 1 kaynak hazırlığı, 21 Ekim 2026

- Ana gereksinim belgesi eksiksiz korundu; tasarım, mimari ve yedi fazlık yol haritası yazıldı.
- Unity 6.3 LTS, URP 17.3, Input System 1.17, uGUI ve Test Framework manifesti eklendi.
- Dünya ölçeği, aspect'e göre perspektif kamera ve portre ayarları tanımlandı.
- Göreli dokunma/mouse, yatay sınır/hız, bırakma/yeniden dokunma ve görsel yatış eklendi.
- Sabit dizilimli tekrar kullanılan 7 zemin/8 engel; sürekli XZ çarpışma ve hızlı retry kaynakları eklendi.
- Ready/Running/Paused/Crashed ve odak kaybı yönetimi; Türkçe temel UI/safe area eklendi.
- Primitive Sparky, üç engel tipi, zemin, ortak materyal, mobil URP ve bağlantılı sahne üreten korumalı editör aracı yazıldı.
- 19 ortak C# kontrolü geçti; 10.000 seed çarpışma karşılaştırması dahil. Unity EditMode 19/19 ve PlayMode 4/4 geçti; gerçek Input System mouse/touch/UI testleri dahil.
- Unity kuruldu; sahne/prefab/URP assetleri motor içinde üretildi ve Play Mode 3D önizlemesi incelendi.
- Mouse basılı durum geçişinin izlenmesi, sanal giriş testinde bulunan başlangıç olayı sorunu üzerine düzeltildi.
- Unity/Android kurulumu ve doğrulama adımları belgelendi. Fiziksel cihaz ve manuel sürüş hissi kabulü açık bırakıldı.

- Android ARM64 IL2CPP Development APK başarıyla derlendi; imza/arşiv ve paket bilgisi kontrol edildi. Fiziksel cihaz testi yapılmadı.

## 0.5.0 — Living Workshop

- Ayrı Living sahnesi, eğrisel araç, kıvrımlı pamuk kumaşlar, taranmış sandalye, pencere gölgesi/HDR ve yüksek profil renk düzenlemesi.
- Tasarlanmış kısa engel ritimleri; üç tur hedefi, 12 örneklik havuzda isteğe bağlı +75 risk yıldızları, ayrı v05 stil rekoru.
- CC0 kayıt tabanlı yuvarlanma/çarpışma ve ayrı kalıcı motor/efekt ayarları.
- Dört ekran oranı, iki rota/dört seed, ödül/reset/pause/model/profil/ses dahil 62/62 test. Fiziksel kabul bekler.
- Android ARM64 IL2CPP Development v0.5.0/code5; kaynaklar/lisanslar/raporlar ve gerçek renderlar.
