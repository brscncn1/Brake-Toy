# v0.8 Playground — sonuç ve değerlendirme

## 24 Ekim — v0.8 Playground

Yeni ToyRoomPlayground sahnesi ve feat/playground-v08 dalı; v0.7 korunur. Güvenli/riskli yol, bağlı altı karşılaşma ailesi, isteğe bağlı domino ile yol açma, sinyalli açılıp kapanan oyuncak garaj kapısı/hareketli oyuncak geçişleri, üç yatay konuma dağılan kitap rampaları ve farklı hareketleri bağlayan ustalık bonusu uygulandı. Rampa artık dar merkez toleransı yerine aracın teker/gövde temas alanını kullanır; yan giriş/çıkış ve kenarları sınandı.

3 sn yavaşlatma, 3 sn korumalı roket, 5 sn jetpack. İlk kariyere üçer kalıcı hak; menüde kullanım başına 350/500/650 TP yenileme. Retry hak vermez; pause süreyi durdurur. Gerçek para/coin/IAP yok. Yardımlı tur etiketlenir; güç kullanıldığında kişisel ghost gizlenir ve yeni ghost yazılmaz. Yeni rekor/kariyer dosyası ayrı; TP/açılmış araçlar taşınır.

Masa bölgeleri ve domino Ayarlar'dan ayrı kapatılır. Oda ışığında yön/kontrast, plastik-kumaş-kauçuk-metal yüzey ayrımı; odanın üst kısmını açık bırakan menü, simgeli görev/rozet kartları ve farklı araç siluetleri eklendi. Müzik korunur. Araçlar ayrıntılı son model değil, sonraki model agent çalışmasına temel siluetlerdir.

Son Unity: **102/102** (48 EditMode +54 PlayMode). Fiziksel v0.8 FPS/p95/ısınma ve insan sanat/oynanış kabulü bekler. Canlı online ve mağaza kabulü yok. Notion yeni sayfa oluşturma ücretsiz blok sınırından reddedildi; NOTION_PENDING-v08.md içinde eşitleme kaydı tutulur.

## Karşılaşmalar ve geri alma

Yeşil rota geçilebilir ana koridordur; altın rota zamanlama/atlayışla ek yıldız ve ustalık fırsatı verir. Sahneler; oyuncak+kitap atlayışı, otobüs ardından rampa, sinyalli garaj kapısı, iki araçlı oyuncak geçişi, domino anahtarı ve top ardından atlayış olarak tasarlandı. Phrases seedli seçilir, ardışık aynı aile gelmez. Yaklaşma/kaçış için minimum 48 m ve hıza göre 2.65 sn ritim; Relax daha seyrektir. Bu sonsuz benzersizlik veya her seed için tam geçilebilirlik ispatı değildir. Gerçek güvenli tanık üç seed×iki mod×120 sn tamamlandı.

Masa bölümleri oyuncak kasabası/yapım/yarış köşesi olarak 220 m aralıkla döner; dekor masa mesafesiyle akar. Bu sürümde masa geometrisini bükmedik; bölgesel dekor denemesidir. Ayarlar → MASA: KLASİK ile yalnız yeni bölgeler kapanır. DOMİNO: KAPALI mevcut domino bariyerini güvenli kaldırır ve yenilerini durdurur. Tercihler kaydedilir. Önceki tam oyun Tools → Brake Toy → Open Joyful Room v0.7 ile ayrıca korunur. Dönen platform veya fiziksel hareketli köprü eklenmedi; önerideki uyarılı kapı alternatifi uygulandı. İki araçlı geçiş mevcut otobüs modelinden yararlanır; ayrıntılı tren modeli yapılmadı.

## Rampa düzeltmesi

Eski yüzey yaklaşık 1.9 birim genişken çıkış yalnız merkezden ±0.55 kabul ediyordu. Yeni hesap kapağın ±0.95 kenarını ve aracın yarı genişlik/uzunluğunu birlikte örnekler. Ön teker temasıyla yükselir, arka teker kitabın destekli ucunu geçince uçar; yan kenardan çıkışta kısa düşüş hareketi yüzeyden yere aniden ışınlanmayı önler. Sol/orta/sağ ve iki kenar; çapraz yan giriş/çıkış, pause/iniş testleri var. Görsel/collider kabulü gerçek kullanıcı tekrarını yine gerektirir.

## Güç ve ekonomi

Yavaşlatma dünyayı yaklaşık %32 hıza indirir; direksiyon normal zamanda kalır. Roket 1.85x'e yükselir, başlangıç/bitiş yumuşatılır ve temas eden engelleri temizler. Jetpack 5 aktif oyun saniyesinde yumuşak kalkış/inişle ~2.25 birime yükselir. Aynı anda bir güç; roket/jetpack bitişinde .85 sn iniş güvenliği ve yakın engel temizliği vardır. Menü/paused durumunda güç tüketilmez. Aktif parmağın direksiyon yakalaması güç düğmesiyle kesilmez.

Starter üçer hak kariyerde bir kez yazılır. Tüketim/yenileme hemen yedekli kariyere kaydedilir; yazma başarısızsa işlem geri alınır. Üst sınır her güçte 99. TP fiyatları test adayıdır, ekonomik kullanıcı dengesi kabul edilmiş değildir. Güç kullanılan tur sonuçları etiketlidir; yerel rekorlar destekli/desteksiz ayrılmıyor. Üretim rekabet/ödeme kurulumu yoktur.

## Menü, görevler ve araçlar

Ana panel ekranın alt bölümüne alındı; oda görülebilir. Güç yenileme ayrı çekmecede, sürüş sırasında satın alma yok. Görevler yol/yıldız/yakın geçiş simgesi, ilerleme çubuğu ve TP ödülü gösterir. 15 rozet üçlü sayfalanır; tamamlama gerçek kariyer bitlerinden okunur. Yeni karşılaşmalar zemin ve havadaki risk yıldızlarını gerçekten üretir; yıldız görevleri erişilebilir.

Sparky korunur; diğer dokuz araç yuvarlak mini, uzun coupe, açık tavan, açık teker kart, ikiz roket, alçak elektrikli, kubbeli uzay, roadster ve prestij olarak farklı oran/parçalara sahiptir. Bunlar ayrıntılı son varlıklar değildir; bazı gövdeler hâlâ basit. MODEL_HANDOFF-v08.md sonraki model çalışmasının ölçü/bağlantı sözleşmesidir. Collider ve sürüş bütün araçlarda aynı.

## Test, görüntü ve açık işler

Tests/EditMode-v08.xml ve Tests/PlayMode-v08.xml son otomatik kanıt. 128000 phrase planı; kalıcı starter/yenileme; rampanın kenar ve yan geçişleri; üç seed/iki mod; güç süre/pause/koruma/iniş; domino yol açma; kapı sinyali; gerçek risk yıldızları; eski sahne regresyonları. 29 gerçek 720×1280 render Playground-0..28.png ve PLAYGROUND_GALLERY.html içinde. D3D12 render aracının kapanışında bir native çıkış sorunu görüldü; son görüntü koşusu D3D11 ile temiz tamamlandı. Bu fiziksel cihaz başarısı demek değildir.

Işık/malzeme ayrımı iyileştirildi ama sinematik film kalitesi elde edildi denmiyor. Bölge dekoru hâlâ basit; parkur düz masadır. Görev/menü ve yeni siluetler kullanıcı görsel kabulünü bekler. Müzik değiştirilmedi. Android ve yayın kanıtı ANDROID_BUILD_REPORT-v08.json / DELIVERY-v08.json ile ayrı tutulur. Notion blok sınırı nedeniyle sonraki güncellemeler bekler; GitHub/yerel günlük sürdürülür.

## Son Android paket

Başarılı non-Development ARM64 IL2CPP APK: **39,074,371 bayt**, SHA256 `a67b7f605583efa7b685f9118f29b7caf90703d30194f85e9f243d44aecaef60`. V2 imza/önceki test sürümleriyle aynı debug sertifikası, ZIP bütünlüğü, ZIP16KB ve native ELF LOAD16KB, paket/sürüm/portre kontrol edildi. Gerçek fiziksel v0.8 ve 16KB işletim ortamı testi yapılmadı. Mağaza release anahtarı değildir.
