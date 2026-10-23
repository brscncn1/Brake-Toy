# v0.9 — otomatik kanıt

50 EditMode +62 PlayMode: 112/112. Altı tohum, iki mod, 120 saniye/koşu; toplam24 dakika. Fiziksel cihaz ölçümü değildir. UNITY_TEST_RESULTS-v09.json.

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

# v0.6 son doğrulama

39/39 EditMode ve 36/36 PlayMode geçti; 75 toplam. 9/9 Python sunucu testi ve Tests/Network-review.txt gerçek Unity HTTP entegrasyonu geçti. Unity XML saatleri UTC'dir; yerel tarih 23 Ekimdir. Eski regresyonlar dahil.

Yeni kapsam: yedek kayıt/kötü şema/alan doğrulaması, TP idempotence/harcama, Relax kaydı/hakları/120 sn tanık, 10 modelin ortak collider/dört tekerleği, günlük offline/gated online, sıkıştırılmış ghost, gerçek Unity input →Python simülasyonu eşleşmesi. Ayrıntı UNITY_TEST_RESULTS-review.json ve REVIEW_MILESTONE.md.

İlk model çeşitlilik testi yanlış tekerlek mesh'ini seçti; gövde mesh'i seçilerek düzeltildi ve son 36/36 geçti. İlk DX12 yakalama native kapanış hatası sonrası temizlik düzeltildi; D3D11 yakalama 0 ile tamamlandı. Bu olaylar gizlenmedi.

v0.5 tablette kullanıcı kontrol/full grafik öznel akıcılık bildirdi; FPS/p95 ölçülmedi. v0.6 gerçek cihaz, ısı, uzun süre, üretim servis, iOS ve mağaza kabulü yapılmadı.

# v0.5 doğrulama sonuçları

Unity 6000.3.26f1: **31/31 EditMode +31/31 PlayMode =62/62 geçti**. Ayrıntılı test isimleri/saatleri UNITY_TEST_RESULTS.json ve Tests/EditMode-v05.xml / PlayMode-v05.xml içindedir. Önceki raporlar Tests/Archive/v0.4 altında.

- Üç hedefin kümülatif/tek seferlik tamamlanması ve reset; risk yıldızının +75 puan verirken yakın geçiş/kombo uydurmaması.
- 1.000 seed ×128 yeni ritimli satır: deterministik güvenli şerit, bitişik erişilebilir geçişler, tekrar sınırı ve top koridor açıklığı. Eski çeşitlenme/çarpışma matematiği kontrolleri de geçti.
- Dört seed ×güvenli/riskli rota ×120 sn =16 dakika simüle edilmiş v0.5 sürüş: çarpışma yok, engel/yıldız havuzu tükenmedi, instance sayısı büyümedi, riskli rotalar üç hedefi tamamladı. Bu süre gerçek cihazda geçen süre değildir.
- Yıldız çarpışma alt adımında ödül vermiyor, bir kez toplanıyor, pause ilerlemiyor, retry sıfırlıyor. Kayıtlar testlerde yazılmaz.
- Kamera 9:16 /9:19.5 /9:21 /3:4 oranlarında ve iki direksiyon ucunda araç ile 27 birimlik tepki bölgesini kadrajda tutuyor.
- Yeni ses referansları, iki ayrı ses ayarı, menü görünürlüğü, özel kalite pipeline kopyası ve postprocessing geçişi doğrulandı.
- Araç sonlu vertex/normal ve collider sınırları içinde. Scene mesh/material/index/shader/dekor collider denetimi: MODEL_VALIDATION.json. Sayımlar inaktif engel havuzunu da içerir.
- Önceki sahnelerin kontrol/döngü/giriş/kamera/model/kombo testleri geçti.
- Android ARM64 IL2CPP Development paket derlemesi, ZIP/native library, aapt2 sürüm/API/paket ve apksigner v2 doğrulaması geçti; APK özeti ANDROID_BUILD_REPORT.json.

**Açık kabul:** Fiziksel Huawei MatePad 11 FPS/p95/ısı/dokunma/arka plan ve insan ses/tempo/sanat değerlendirmesi yapılmadı. Otomatik input testi gerçek dokunma gecikmesini ölçmez. Yüksek 60 /ekonomik 30 FPS birer hedeftir. v0.5 final sürüm veya onaylanan konseptin birebir gerçekleşmesi değildir.
