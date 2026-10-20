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
