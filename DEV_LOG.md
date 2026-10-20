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
