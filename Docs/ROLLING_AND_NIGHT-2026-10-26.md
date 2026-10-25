# Hareket eden toplar ve gece lambaları

## Kök neden

Tabletop parkurundaki büyük top, kataloğun3 numaralı türü olarak normal sabit engel gibi üretiliyordu.
MotionKind=0, ForwardFactor=1 ve Slope=0 nedeniyle masaya göre hız sıfırdı.
Eski dönüş hesabı ise kameraya göre masa kaymasını da yuvarlanma sayıyordu.
Sonuç: masa üzerinde ilerlemeyen fakat dönen top. Kullanıcının bildirdiği hata budur.

## Top hareketi

- Düz, çapraz, tek kavis ve çift kavis olmak üzere dört seed profili.
- Büyük topun ileri hareketi masa kaymasından bağımsızdır; ForwardFactor1.20–1.45.
- İlerleyen cümlelerde hız faktörü kademeli artar ve üst sınırda kalır.
- Başlangıç Z'si hız faktörüyle ölçeklenir; top kendi hızına rağmen tasarlanan geçide zamanında varır.
- Yanal hareket büyük topta±0.55, küçük miskette±0.48 birimle sınırlıdır.
- Küçük misketlerde aynı grup içinde farklı profiller;1/2/3 top sayısı korunur.
- Dönüş açısı, masa üzerindeki gerçek yol/radyus üzerinden hesaplanır; yatay yön değişince eksen değişir.
- Masa kayması çıkarılır. Masaya göre hareketsiz topun sırf kamera yaklaşınca dönmesi engellenir.
- PositionAfter hareket tahmini ile Advance aynı yörüngeyi kullanır; swept çarpışma korunur.
- Uyarı topun mevcut sol/orta/sağ konumunu söyler; merkez topa yanlış sağdan gelişi yazılmaz.
- Eski sahnelerin yerleşim ve kontrol akışı değiştirilmez; ortak dönüş hesabındaki hata onlara da uygulanır.
- Kitap/defter/kalemlik, otobüs, rampalar, sekiz oyuncak ailesi ve değişen geçitler korunur.

Bu, deterministik arcade yuvarlanmasıdır; serbest Rigidbody/oda duvarı sekmesi simülasyonu değildir.
Kontrollü kavisler fiziksel görsel tutarlılık sağlar; rastgele kontrolsüz kuvvetlerle geçit kapatılmaz.
Yeni Adventure ghost ad alanı tabletop-r4-rolling; tarihsel karşılaştırmalar adventure-r3-rolling kullanır.
Önceki ghost/kariyer dosyaları silinmez; yeni hareketle eski hayalet aynı tur diye oynatılmaz.

## Gece

- Rafın üzerinde radyonun yanına sıcak okumalık lamba; yatak başlığına ikinci küçük lamba.
- İkinci lambanın sığ tabanı başlığa oturur; uzun direkle yatak/masa/oyuncak platformlarını delmez.
- İki yerel ışık gece açılır, gündüz söner; gövde/dekor gündüz de kalır.
- Raf lambası parkurun kenarına hafif sıcak spot bırakır; diğer lamba oda tarafını aydınlatır.
- TrackSpill=false ile raf ışığının parkura etkisi kapanabilir; oda lambası korunur.
- Gölgesiz iki yerel ışık; mevcut güneş/ortam ışığı ve gün döngüsü korunur.
- Materyal ve gövdeler başlangıçta bir kez oluşturulur, sahne kapanınca temizlenir.

İlk gerçek motor karşılaştırmasında hafif parkur ışığı kullanıldı; parlama/doygunluk baskısı görülmedi.
Film kalitesinde model veya ölçülmüş mobil performans iddiası değildir; kullanıcı gece kabulü açıktır.

## Kanıt ve sınırlar

Reports/ROLLING_NIGHT_VALIDATION.json sonuçları ve kaynak hash'lerini tutar.
90 EditMode +GPU84 PlayMode =174/174. Regresyon dört yörünge, masa üstü yer değiştirme, dönüş/radyus,
30/60/120 Hz adım tutarlılığı, top çarpışması ve lamba gündüz/gece kontrollerini kapsar.
Önceki18 güçsüz parkur tanığı ve on hazard türünün fiili üretim testi de regresyondadır.
Reports/Visuals/ROLLING_NIGHT_GALLERY.html gerçek kamera çıktılarıdır; cihaz FPS ölçümü değildir.
Yeni APK henüz üretilmedi. Notlar DEV_LOG/NOTION_PENDING ve masaüstü Notion yedeğine eklenir.
