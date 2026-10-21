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
