## 24 Ekim — v0.8.1 yerleşim düzeltmesi

Yeni ToyRoomTidy sahnesi, fix/table-layout-v081 dalı. Mavi parçalar v0.8 oyuncak kasabası kenar dekorlarıydı; 42 m dekor döngüsüyle 64 m defter/kalemlik döngüsü bağımsız olduğundan tekrar tekrar çakışıyordu. Yeni sahnede iki grup aynı 64 m döngüyü ve ayrılmış boşlukları kullanır. Defter/kalemlik en fazla 1.25 birim genişliğe indirildi, ±4.45 kenar merkezlerine oturtuldu; oyun alanına ve masa dışına taşmaları sınandı. Tüm bölge parçaları hücre olarak birlikte akar, pencere/ev parçaları ayrışmaz.

Radyo eski birleşik masa mesh'inden ayrılıp soldaki oyuncak rafının üstüne taşındı; alt yüzeyi gerçek raf üstünden 0.025 birim yukarıda. Sürüş mesafesiyle hareket etmez. Domino model/üretim/ayar düğmesi aktif Tidy sahnesinden kaldırıldı; yerine mevcut rampa fırsatı kullanılır. Önceki v0.8 sahnesi korunur. Hız eğrisi, karşılaşma aralığı, güç süreleri/fiyatları ve kontrol ayarları değiştirilmedi. Kariyer/TP/araç/güç hakları aynı dosyada korunur; geometrisi farklı eski parkurun ghost'u yeni sahnede yüklenmez.

**104/104 Unity testi** (48 EditMode +56 PlayMode) geçti. Üç bölge, dört tam ortak döngü ve yarım metre adımlı yerleşim örnekleri; raf desteği/radyo sabitliği; domino yokluğu ve eski 102 regresyon dahil. Fiziksel cihaz ve insan görüntü/oynanış kabulü bekler. Notion yeni içerik işlemi önceki ücretsiz blok sınırı nedeniyle engelli; otomatik tekrar yapılmadı, yerel/GitHub notları güncel.

## 24 Ekim — v0.8 Playground

Yeni ToyRoomPlayground sahnesi ve feat/playground-v08 dalı; v0.7 korunur. Güvenli/riskli yol, bağlı altı karşılaşma ailesi, isteğe bağlı domino ile yol açma, sinyalli açılıp kapanan oyuncak garaj kapısı/hareketli oyuncak geçişleri, üç yatay konuma dağılan kitap rampaları ve farklı hareketleri bağlayan ustalık bonusu uygulandı. Rampa artık dar merkez toleransı yerine aracın teker/gövde temas alanını kullanır; yan giriş/çıkış ve kenarları sınandı.

3 sn yavaşlatma, 3 sn korumalı roket, 5 sn jetpack. İlk kariyere üçer kalıcı hak; menüde kullanım başına 350/500/650 TP yenileme. Retry hak vermez; pause süreyi durdurur. Gerçek para/coin/IAP yok. Yardımlı tur etiketlenir; güç kullanıldığında kişisel ghost gizlenir ve yeni ghost yazılmaz. Yeni rekor/kariyer dosyası ayrı; TP/açılmış araçlar taşınır.

Masa bölgeleri ve domino Ayarlar'dan ayrı kapatılır. Oda ışığında yön/kontrast, plastik-kumaş-kauçuk-metal yüzey ayrımı; odanın üst kısmını açık bırakan menü, simgeli görev/rozet kartları ve farklı araç siluetleri eklendi. Müzik korunur. Araçlar ayrıntılı son model değil, sonraki model agent çalışmasına temel siluetlerdir.

Son Unity: **102/102** (48 EditMode +54 PlayMode). Fiziksel v0.8 FPS/p95/ısınma ve insan sanat/oynanış kabulü bekler. Canlı online ve mağaza kabulü yok. Notion yeni sayfa oluşturma ücretsiz blok sınırından reddedildi; NOTION_PENDING-v08.md içinde eşitleme kaydı tutulur.

# 23 Ekim 2026 — Faz 6 revizyon adayı kontrol noktası

## v0.7 Joyful Room — uygulama ve görsel inceleme

Kullanıcının ses/oynanış/oda revizyonu PLAN-v0.7.md ile başlatıldı. 5b9671b plan/araştırma; 7a7b993 ilk çalışan uygulama ve 84 test ile GitHub'a gönderildi. Notion 32 revizyon/araştırma sayfası ve günlük güncellendi. v0.6 korunuyor.

Gerçek CC0 menü/yarış müzikleri, plastik çocuk kornası ve oyuncak şakırtısı; açıklamalı üç ses kanalı; hızlı hız eğrisi, recent-pattern hafızası, farklı oyuncak/stagger/jitter, falsolu ve ikili/üçlü top, dönen tekerlekli bus, kitap rampası/havada TP, yeni oyuncaklar ve gündüz/gece odası uygulandı. Yeni kariyere sahip olunan ürün/TP taşınır, değişen skor ve X+Y ghost dosyaları ayrıdır. v0.7 review-r1 sunucusuna bağlanmaz.

Gerçek render incelemesinde düz masa/boş duvar, radyo omuz açıklığı ve hızlı topların duvar arkasında geç görünmesi yakalandı. Masa mikro doku/kenarları, pastel oda duvarı, bunting/gezegen mobil ve daha derin arka oda hazırlandı. Maksimum ileri top yaklaşımı için görünürlük ve model vertex/collider sınır testi eklendi. Uçuş pozunda dönüşün kare kare birikmesi ayrıca bulundu; X açısı mutlak hesaplanarak düzeltildi, pause/iniş regresyonu geçti. Son geçit/rampa kalibrasyonu sonrası test/render tekrar koşusu sürüyor. Henüz yeni APK ve kullanıcı ses/sanat/cihaz kabulü yok.

## Son teslim ve eşitleme

Kaynak ca7aa2ab18094269f6d2e9d0d9aed7f87d3321b6, PR #5 ve v0.6.0 GitHub'a gönderildi. GitHub APK digest yerel 65750071216c7742625eab7f8e410f027a815f96741d3518c6eecb6821ead042 ile aynı; 38.232.631 bayt. v0.5 ile aynı Debug sertifikası doğrulandı. Main README 179f9370750dc89e1503292c6e0fbc131e418894 güncel teslim ve ayrı kaynak dalını açıkça gösterir. PR'lar birleştirilmedi.

Notion kökü 31 alt sayfayla korundu; 28–31 plan/geri bildirim/servis/teslim sayfaları ayrı. Günlük, README/tasarım/mimari/yol haritası/test/değişiklik, cihaz, Android, Unity kanıtı ve model sayfaları güncellendi. Teslim sayfası tekrar okunup APK SHA256/görsel/eski bekleme metninin kaldırılması doğrulandı. Fiziksel v0.6, ısı, 16KB gerçek ortam, canlı servis/kalıcı online kimlik ve mağaza kabulü açık. Tablet dosya kopyası iddia edilmedi; doğrudan APK linki sağlandı.

Kullanıcı tüm bağımsız geliştirmeye devam izni verdi. v0.5 tablet geri bildirimi alındı: kontrol iyi, tam grafikte öznel akıcılık; ilk 30 sn/parkur çok kolay ve sığ, müzik eksik, sanat hedefin altında. FPS/p95 ve kesinti ölçülmedi. Kullanıcı bu kalite revizyonlarını sonraki tura ayırdı.

feat/complete-review-build üzerinde Review sahnesi, 10 araç varyantı, garaj, kariyer/ekonomi, kozmetik, 3 görev/15 başarım, Relax, ghost/günlük ve REST sınırı kodlandı. 39 EditMode +36 PlayMode geçti. 9 Python test ve gerçek C#HTTP yerel probe geçti. On araç ve tüm yeni menüler motor renderıyla incelendi. 316 renderer/133 mesh/768322 üçgen tüm inaktif araç/havuz dahil; eksik referans/shader/dekor collider sıfır. İlk yanlış mesh testi ve DX12 kapanışı düzeltildi, son test/yakalama başarılı.

Notion 28 plan, 29 geri bildirim, 30 servis sayfaları oluşturuldu; günlük güncellendi. Canlı servis yok, offline varsayılan kapısı korunuyor. Android non-Development test paketi derleniyor; paket/son teslim kanıtı sonraki günlüğe eklenecek.

# Geliştirme günlüğü

## 21 Ekim 2026 — İlk sürüm ve görsel yön

- Faz 1: gerçek 3D sürüş, mouse/touch, kayan dünya, swept collision, retry ve pause oluşturuldu.
- Unity 6000.3.26f1: 19 EditMode ve 4 PlayMode testi geçti. Ayrı .NET çalıştırıcısında aynı 19 çekirdek kontrolü ve 10.000 çarpışma örneği geçti.
- Android ARM64 IL2CPP Development APK üretildi ve imzası/arşivi doğrulandı. Fiziksel cihaz performansı doğrulanmadı.
- Kullanıcı ilk sürümü denedi; çocuk odası hissini yetersiz buldu ve odadaki masa üzerinde sürüş istedi. Bu geri bildirim kontrol hissinin onaylandığı anlamına gelmez.
- Masa/oda kompozisyonu için imagegen konsepti oluşturuldu; kullanıcı görünümü onayladı ve sıradaki sürümü yetkilendirdi.

## 21 Ekim 2026 — GitHub ve Notion bağlantısı

- GitHub hesabında mevcut brscncn1/Brake-Toy deposu bulundu. Başlangıç README commit'i korunarak yerel proje geçmişi birleştirildi; Faz 1 kaynakları ve onaylanan konsept main dalına gönderildi.
- Yeni çalışma dalı: feat/toy-room-tabletop.
- Notion bağlantısı önce görünmüyordu; bağlantı kullanılabilir olduğunda arama ile doğrulandı. BRAKE TOY — Proje Merkezi özel sayfası oluşturuldu; belgeler ayrı alt sayfalara aktarılıyor.
- Kullanıcının isteği doğrultusunda bundan sonraki önemli karar, uygulama, test ve teslim sonuçları yerel günlük ve Notion günlüğüne kaydedilecek.
- Aktif iş: odanın 3D kabuğu, mobilyalar, ahşap masa, oda görünürlüğünü koruyan kamera, geliştirilmiş Sparky ve oynanabilir yeni sahne.

## 21 Ekim 2026 — Oda ve masa sahnesi üretildi

- ToyRoomTabletop.unity ayrı bir sahne olarak Unity içinde üretildi. Eski ToyRoom ve Faz 1 prefabları korundu.
- Masa yüzeyi/kenarları/ayakları, oda zemini/duvarları, pencere/perdeler, raf/oyuncak ayı, yatak, poster, sandalye ve kırtasiye dekorları eklendi.
- Yeni Sparky ve üç engel tipi yuvarlatılmış mesh'lere geçti. Tekerlek animasyonu yalnızca sürüşte çalışır. Gameplay collider ve kontrol parametreleri korundu.
- 20 derecelik kamera ile ilk gerçek Unity Play Mode renderı incelendi. Oda görünür hale geldi; büyük yüzeylerdeki bevel normal interpolasyonu düzeltilerek koyu bantlar giderildi. Ahşap deseni sadeleştirildi.
- Masa genişliği oda zemini ve yan mobilyaları göstermek için 12 birime ayarlandı; kullanılabilir sürüş alanı 7 birim olarak korundu.
- Yeni sahne için üç PlayMode regresyon testi yazıldı; bu aşamada henüz çalıştırılmadı.
- GitHub Releases üzerinde v0.1.0 APK ve derleme raporu yayımlandı. Yeni APK henüz üretilmedi.

## 21 Ekim 2026 — v0.2 doğrulama ve teslim hazırlığı

- 20°/28° gerçek Unity renderları karşılaştırıldı; oda görünürlüğü için 20° seçildi.
- Yakın masa dekorları WorldScroller'ın gerçek kayma/reset olaylarına bağlandı; pause ve çarpışma kesriyle tutarlı ilerler.
- Unity 27/27 test geçti: 19 EditMode, 8 PlayMode. Yeni dekor akışı/reset testi dahil.
- v0.2 Android ARM64 IL2CPP Development APK üretildi; boyut 37.0 MiB. Cihaz çalıştırma/performance testi yapılmadı.
- Sürüm raporları ve önceki raporların v0.1 arşivi hazırlandı. Kaynak/Notion/Release son eşitlemesi teslimden önce doğrulanacak.

## v0.2 yayın ve Notion eşitlemesi tamamlandı

- Kaynaklar feat/toy-room-tabletop dalına gönderildi; PR #1 açıldı: https://github.com/brscncn1/Brake-Toy/pull/1
- v0.2.0 ön sürümü yayımlandı; APK, derleme raporu ve gerçek Unity görüntüsü yüklendi. Uzak APK özeti yerelle eşleşiyor.
- Notion proje durumu ve etkilenen 10 belge/test sayfası güncellendi; 16 — v0.2 Masa ve oda sürümü oluşturuldu. Geliştirme günlüğüne gerçek test/yayın sonuçları eklendi.
- Unity ToyRoomTabletop sahnesiyle tekrar açıldı. Gerçek cihaz ve kullanıcı görsel geri bildirimi bekleniyor.

## v0.3 araştırma ve uygulama başladı

- Kullanıcı erken heyecan/hız, daha gerçekçi modeller ve yuvarlanan topları yetkilendirdi. Yeni feat/arcade-workshop-v03 dalı açıldı.
- Birincil Halfbrick/GDC, Milestone, SYBO ve Unity kaynakları incelendi; kapsam ve çıkarımlar RESEARCH-v0.3.md içinde. Tüm yayınların tarandığı veya video izlendiği iddia edilmiyor.
- Ayrı ToyRoomArcade sahnesi oluşturuldu: seedli satırlar, 24 tehlike havuzu, hız eğrisi, sürekli hareketli top çarpışması, Near Miss/kombo/rekor, HUD/ses.
- Özgün diecast araç geometrisi ve daha detaylı oyuncaklar; Poly Haven CC0 ahşap diffuse/normal, ayrı renderer/SSAO/reflection. Topun merkez çevresinde dönmesi düzeltildi.
- 128.000 satır/10.000 çarpışma örneği ve motor içi regresyon testleri başlatıldı; sonuçlar henüz doğrulanmadı.
- Notion 17 — v0.3 Araştırma ve uygulama planı oluşturuldu.

## v0.3 son doğrulama ve paket

- 26 EditMode + 15 PlayMode = 41/41 Unity testi geçti. İki dakikalık tanık sürüşü, 128.000 satır ve 10.000 top örneği dahil.
- Son yakın planlarda far/kaput-lastik kesişmesi ve gövde normalleri düzeltildi; gerçek renderlar incelendi. Sabit oda birleştirildi, pencere açıklığı ve gölgesiz dolgu eklendi.
- Android ARM64 IL2CPP APK oluşturuldu; imza/paket/arşiv kontrolü geçti. Fiziksel cihaz ve kullanıcı hissi onayı bekliyor.
- GitHub yayın ve Notion son eşitlemesi hazırlanıyor.

## v0.3 GitHub ve Notion teslimi tamamlandı

- Kaynak feat/arcade-workshop-v03 dalına gönderildi; PR #2: https://github.com/brscncn1/Brake-Toy/pull/2
- v0.3.0 ön sürüm APK/rapor/model kanıtı ve gerçek renderlarla yayımlandı. Uzak APK SHA256 yerelle eşleşiyor: a6a9cbdb0aa321f6e54acb744654c23b75b49eda272dabf3dde0cfb0d73d4ed7.
- Notion proje merkezi ve etkilenen belgeler eşitlendi; 17 araştırma, 18 teslim, 19 varlık hakları, 20 model kanıtı sayfaları mevcut. Gerçek sürüş/Fever/model görselleri kaynak commitine sabitlenmiş URLlerle eklendi.
- Son kabul kanıtı 41/41 Unity testi; fiziksel telefon ve insan oyuncu tempo/kalite geri bildirimi bekleniyor. Unity ToyRoomArcade sahnesiyle açıldı.

## v0.4 kalite incelemesi ve çalışma başlangıcı

- Kullanıcı sonraki aşama öncesi kapsam, öz eleştiri ve öneriler istedi. Gerçek v0.3 görüntüleri/kodları incelendi; QUALITY_REVIEW-v0.3.md ve PLAN-v0.4.md yazıldı.
- Öncelik kamera/UI, malzeme/ışık ve araç oranları, engel kataloğu/çeşitlenen turlar ve grafik profilleri. Ses/çarpışma cilası daha sonra; garaj/yeni harita/online bu sürümde yok.
- feat/refined-room-v04 dalı açıldı; Notion 21/22 sayfaları ve günlük güncellendi. Henüz v0.4 test sonucu yok.

## Kullanıcının kapsam kararı — tüm öneriler bu sürümde

Kalite raporundaki dokuz başlık aynı v0.4 çalışmasına alındı. Ses ve çarpışma cilası sonraya bırakılmayacak: katmanlı motor/teker sesi, kuru oyuncak çarpışması ve kısa görsel tepki. Oda ayrıntıları ve gerçek motor önce/sonra karşılaştırması da teslimde yer alacak. Fiziksel cihaz FPS/ısı/dokunma kabulü için cihaz matrisi ve ölçüm kaydı hazırlanacak; cihaz olmadan ölçüldü denmeyecek. Önceki dar kapsam ifadeleri bu kararla güncellendi. Yeni harita/garaj/online bu kalite raporunun önerileri arasında değildir.

## v0.4 implementation and first checks

- User expanded the milestone to all nine review recommendations, including audio/impact polish. Separate refined scene/assets preserve previous versions and real user records.
- Added defined pooled encounters, varied gaps/ball slopes/events and new-run/same-route retry. Catalog prefabs instantiate once at scene startup; no new hazards while driving.
- Added private runtime high/low pipeline copies, menu quality controls, local opt-in performance recorder and explicit development benchmark. No USB-debuggable device detected; user could not locate the setting, so delivery proceeds with direct tablet APK testing.
- Reduced wood contrast and adjusted tint after the first render looked washed out, softened sunlight, refined cabin/roof, opened wheel arches and added window joinery/plant. Actual Unity front/rear/game renders inspected; concept still exceeds current art quality.
- Layered motor/friction/ratchet, dry wood/metal and rubber impact cues; short grounded wobble/camera impulse synchronized with impact and delayed result panel.
- First tests: 28/28 EditMode +25/25 PlayMode. Final catalog/art/audio changes are being rechecked before Android packaging.
- Updated GitHub main landing README; fixed a local text-encoding conversion before delivery. All further Python file IO uses explicit UTF-8.

## v0.4 final paket kontrolü

Masa chunk uçlarındaki bevel ve duvar panel çizgileri görüldü; yalnızca yeni sahnenin kesintisiz yüzey geometrisiyle giderildi. PlayMode 25/25 yeniden geçti; model denetimi 61 unique mesh /192 renderer /158146 üçgen (inaktif havuz dahil), hata sıfır. APK yeniden derlendi ve imza/paket/ZIP denetimi geçti. SHA256: 0ce3f84da0b58b3e98dc0fab0f1922985fcd194bb22c017b6f172e45f6dca4a0. Kullanıcının tableti Windows MTP aygıtı olarak görünüyor; APK aktarımı yapılıyor, henüz tablet oynanış/FPS kabulü yok.

## v0.4 yayımlandı ve eşitlendi

Kaynak commit 7e36334d85211c5baa3ce1a42440c17e92043f6b, PR https://github.com/brscncn1/Brake-Toy/pull/3 (v0.3 dalı üzerine). v0.4.0 prerelease APK/render/raporları yayımlandı; GitHub APK asset digest yerel SHA256 ile eşleşti. Ana sayfa README 517c4df ile v0.4 durumuna güncellendi; main kaynak tabanının önceki prototip olduğu ve yeni kaynak dalı açıkça belirtildi.

APK Huawei MatePad 11 /Dahili depolama/Download altında görünüyor; kullanıcıya kurulum ve isteğe bağlı 60 sn sürüş ölçümü adımları verildi. Fiziksel kabul henüz alınmadı. Notion 24 alt sayfayla korundu; 21 eleştiri/22 plan güncellendi, 23 kalite sonuçları ve24 cihaz kabulü oluşturuldu. İlgili kurulum/tasarım/mimari/yol haritası/görsel/test/değişiklik/lisans/model/derleme kanıtı sayfaları güncellendi; root ve görsel/teslim sayfaları tekrar okunarak doğrulandı. Eski kullanıcı içerikleri/alt sayfalar silinmedi.

## v0.5 başladı

Kullanıcı sonraki aşama ile altı çözümün birlikte yapılmasını onayladı. Dal feat/living-workshop-v05. Aktif Faz 3 ve Faz 2 kapanışı, ardından Faz 4/5/6 olmak üzere mevcut dahil dört ana çalışma bloğu açıklandı; sürüm/tarih garantisi verilmedi. Kaynak araştırması: Poly Haven Painted Wooden Chair02/Cotton Jersey/Studio Small09, Kenney Impact Sounds, qubodup marble rolling, CC0. Önceki v0.4 Play durdurulup yeni sahne için Unity kapatıldı. Henüz yeni test/build sonucu yok.

## v0.5 uygulama ve ilk doğrulama

Living sahnesi ve kaynak varlıklar üretildi; önceki sürümler korundu. Eğrisel Sparky, gerçek pamuk dokulu kıvrımlı oda tekstili, taranmış sandalye, HDR yansıma/sıcak gölge, üç tur hedefi/isteğe bağlı risk yıldızları ve kayıt tabanlı efektler bağlandı. İlk gerçek 6.3 sn render: 70.87542 m / 275 stil / zincir 2. İlk koşu 31 EditMode +30 PlayMode =61 geçti. Dört seed ×güvenli/riskli rota ×120 sn testinde çarpışma/havuz tükenmesi yok. Yatak/sandalye kadrajı ve poster ayrıntıları son incelemede iyileştirildi; nihai test/render/APK kontrolü takip ediyor. Fiziksel tablet ölçümü ve insan kabulü açık.

## v0.5 son yerel doğrulama

Son PlayMode 31/31, EditMode 31/31:62/62. Model raporu 173 renderer /63 mesh /205.618 üçgen (inaktif havuz dahil), sıfır eksik referans/shader/dekor collider. Son Android paketi ZIP/ARM64/paket/API/v2 imzasıyla doğrulandı. SHA256: d23bad3943464dc0d08103ce9daf2d887868a70fd58a5c5a05ee369427f64341. Gerçek fiziksel cihaz ve insan kabulü hâlâ açık. Kaynak/ön sürüm/README ve Notion eşitlemesi teslim adımıdır.


## v0.5 yakın plan düzeltmeleri

Mobilya/masa kesişmesi ve örtüye gömülen yıldızlar gerçek ayrıntı renderında yakalanıp düzeltildi. FBX kök ölçek/dönüş korunarak sandalye doğru oda boyutuna alındı. Üreticide ±6.3 masa açıklığı ve birleştirme sonrası sandalye ölçüsü kontrolü var. Yeni testle son koşu 31 EditMode +31 PlayMode =62/62 geçti. 87 kullanılmayan ara Living mesh kaldırıldı; mevcut sahne/prefab bağımlılıkları korundu. Nihai APK yeniden derlendi ve v2 imza/paket/ZIP doğrulandı. İlk ara APK yayımlanmadı.

## v0.5 teslim eşitlemesi kapandı

Kaynak a7e889ae4264656eca81db093034c63ed15f3906, PR #4 ve v0.5.0 yayımlandı. GitHub APK digest son yerel SHA256 ile aynı: d23bad3943464dc0d08103ce9daf2d887868a70fd58a5c5a05ee369427f64341. Main README 0a91e43 ile güncellendi. Son APK tablet Download içinde 40,4 MB olarak doğrulandı; kurulum/oynanış ölçümü bekler. Notion kökü 27 çocuk sayfayla korundu, 25 plan/26 sonuç/27 cihaz sayfaları ve ilgili 12 belge/kanıt sayfası güncellendi; kök/teslim/görsel/cihaz sayfaları yeniden okunarak doğrulandı. Unity ToyRoomLiving sahnesiyle yeniden açıldı. Fiziksel FPS/ısı/dokunma ve insan tempo/ses/sanat kabulü açık.

## v0.6 son Android paket doğrulaması

38.232.631 bayt non-Development ARM64 IL2CPP APK üretildi. SHA256 65750071216c7742625eab7f8e410f027a815f96741d3518c6eecb6821ead042. ZIP/v2 imza, paket/API/portre, ZIP16KB ve altı native ELF LOAD16KB geçti. Android Debug sertifikası test dağıtımı içindir. Tek izin INTERNET; varsayılan endpoint boş/uygunluk kapalı. allowBackup özel olarak kapalı değil; OEM/OS yedek davranışı gizlilik taslağında açıklandı. Cihaz/16KB gerçek ortam/mağaza onayı yok.

## v0.7 son doğrulama ve teslim hazırlığı

44 EditMode +43 PlayMode =87/87 geçti. Son 16 gerçek render incelendi; pitch/pause, rampa kapağı, masa çerçevesi ve duvar/oda düzeni düzeltildi. Model denetimi 0 eksik mesh/materyal/shader/dekor collider; tüm inaktif araçlar ve eski başlangıç havuzu dahil 406 renderer/1.012.691 üçgen/237 mesh (runtime görünür maliyet veya FPS değildir). Son Android paket v2/ZIP/ELF16KB ve sürüm kontrolünden geçti. Notion 33 sonuç sayfası oluşturuldu. Film kalitesi, insan dinleme, yeni tablet kabulü ve canlı servis açık kalır.

## v0.7 yayın ve eşitleme tamamlandı

Kaynak 0a2cbf03fce7acdc796dfa9dd2cbfb56fab430e9, PR #6 ve v0.7.0 ön sürüm GitHub üzerinde. APK GitHub digest/yerel SHA256 eşleşti. Main README 8fa898a ile güncel kaynak dalını gösterir. Notion 32 plan/33 sonuç, günlük ve ilgili 12 sayfa güncellendi; root/sonuç/görsel readback doğrulandı ve 33 alt sayfa korundu. Gerçek tablet ve insan kalite kabulü açık; film/mağaza final onayı diye sunulmaz.

## v0.8 son kabul koşusu

48 EditMode +54 PlayMode =102/102 geçti. Son ekler: gerçek zemin/hava risk yıldızları ve görev bağlantısı; tur bitişinde aktif güç/lift temizliği ve hakların iade edilmemesi. Kenar/yan rampa, kapı sinyali, domino açma ve iki mod/üç seed güvenli tanık geçti. Garaj yakın planda far/arka kanat bağlantıları düzeltilip yeni prefab/sahne varyantlarına kaydedildi. Kullanıcı TP/araçları ve önceki sahneler korunur. Notion yeni sayfa blok limiti nedeniyle reddedildi; otomatik tekrar yok, eşitleme bekleyen notlar yerel/GitHub'da.

## v0.8 yayın ve kaynak eşitlemesi

Kaynak 0cda6024cfe585500df58cc61e3c232d7125bb35; PR #7 ve v0.8.0 ön sürüm yayımlandı. APK GitHub digest/yerel SHA256 aynı. Main README c356447d1fcd48026b024fc45b854c6e9f7f7ea9 ile güncel, gerçek kaynak dalı ayrımı açık. Son 102/102 test, 29 gerçek görüntü, model/Android raporları kaynakta. Notion yeni sayfa blok limiti nedeniyle oluşturulamadı; başlangıç günlüğü önceden doğrulandı, sonraki notlar GitHub/yerelde. Fiziksel ve insan kabulü açık.
