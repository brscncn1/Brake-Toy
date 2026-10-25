# Masa eşyalarının dönüşü ve değişen geçitler

## Sorun ve kullanıcı kararı

Adventure üreticisi önceki Playground engel akışının yerine geçmişti.
Otobüs katalogda kalmasına rağmen aktif Adventure kodundan çağrılmıyordu.
Her cümle yalnızca bir eski hazard seçiyordu; bu katalog da Joyful sırasında oyuncaklara çevrilmişti.
Dolayısıyla yalnız eski sayısal türleri artırmak kitap/defter/kalemliği geri getirmeyecekti.
Yeni oyuncakların mevcut öğeleri azaltarak eklenmesi kullanıcı talebine aykırıydı.
26 Ekim talebi: mevcut öğeleri koruyarak artır; kalıcı güvenli şeridi kaldır; slalom/dar geçit ekle.

## Uygulama

- İlk yedi oyuncak prefabı ve sekiz Adventure ailesi korunur.
- İlk yedi hazard türünün tümü altı düzen içinde fiilen üretilir; yalnız katalogda bırakılmaz.
- Katalog 7: Workshop kitap modeli; 8: odadaki spiral defter; 9: odadaki kalemlik.
- Ayrı kopyalar parkur ölçeğine alınır; oda dekorunun konumu veya ölçeği değişmez.
- Analitik hacim yeni modellerin gerçek renderer sınırlarından çıkarılır.
- Yeni öğeler başlangıçta havuzlanır; sürüş sırasında Instantiate yapılmaz.
- Her cümlede üç çift engelli geçit; kitap/defter/kalemlik her cümlede en az bir kez.
- Geçitler ana oyuncak referansının yaklaşık +22/+40/+58 birimindedir; seed ±2 kaydırır.
- Kısa slalomdaki iki engel ayrıca1.5 birim şaşırtılır; bütün sıralar aynı ritimde dizilmez.
- Altı düzen: kısa slalom, dar merkez çıkışı, S, otobüslü dönüş, oyuncaklı çıkış, dar otobüs geçişi.
- Altı düzen sırayla döner; sekizli oyuncak torbası ve seed varyantlarıyla birleşir.
- Otobüs altı düzenin ikisinde ±0.65 birim yatay hareket eder; tekerlek animasyonu korunur.
- Sürekli yeşil yan yol yoktur. Geçit boşluğu bölüm içinde komşu konuma değişir.
- Tünelin kısa çıkış işaretleri ve rampanın yeşil yaklaşma oku işlevsel olarak korunur.
- Kitap rampaları geri korunur; cetvel kendi sıçrayışını kullanır.
- Önceki iki bitiriş karşılaştırması DenseCourse=false ile ayrı test profilidir.
- DenseCourse tur başında sabitlenir; yeni ghost dosyaları tabletop-r3 ad alanını kullanır.
- Eski kariyer/ghost dosyaları silinmez. Online Adventure hâlâ kapalıdır.

## Tempo ve sınırlar

İlk ana oyuncak referansı yine24 birimdir. Arcade aralıkları78/81/84, Relax86/89/92 birimdir.
Bu mesafeler içi dolu üç geçitli cümleler içindir; eski tek engelli cümle sayısıyla kıyaslanmamalıdır.
Ana oyuncak→ilk geçit yaklaşık19, geçitler arası14–22 birimdir; son geçit→sonraki oyuncak arası kısadır.
Rampa/uçuş ek seçenek olarak kalır. Geçilebilirlik tanığı güç kullanmadan, yerde yön değiştirir.
Tanık otomatik test içindir; oyunda otomatik yönlendirme veya görünmez koruma sağlamaz.

Bu paket yeni sanat/model kalitesi iddiası değildir; var olan modelleri parkura geri katar.
İnsanların algıladığı tekrar, zorluk ve görsel kalabalık oyun denemesiyle kabul edilecektir.
Güncel Android APK henüz bu değişikliği içermez; kaynak/PC doğrulaması ayrı raporlanır.

## Doğrulama

Sonuçlar `Reports/TABLETOP_COURSE_VALIDATION.json` ve ilgili Unity XML kanıtlarında tutulur.
85 EditMode +GPU açık80 PlayMode =165/165 geçti. Gerçek kariyer/ghost hash'leri aynı kaldı.
Kapsam: deterministik100 seed, üç modelin fiili üretimi, otobüs hareketi, kalıcı şerit bulunmaması,
30/60/120 Hz ×3 seed ×2 modda güçsüz geçilebilirlik, havuzlar, eski profillerin regresyonu,
model/çarpışma sınırları ve gerçek Unity oyun kamerası görüntüleri.

Notion yeni blok isteği yapılmaz; günlük masaüstü BrakeToy_Notion_Yedek.txt dosyasına eklenir.

## Geri dönüş

AdventureEncounters.DenseCourse=false yapılıp yeni tur başlatılırsa önceki bitiriş profili kullanılır.
Bu karşılaştırma içindir; kullanıcı talebi doğrultusunda üretim sahnesi DenseCourse=true kaydedildi.
Yeni üç masa eşyası prefabı oda dekorundan ayrı kopyadır; eski kaynaklar silinmedi/değiştirilmedi.
Önceki yerel rekorlar korunur; iki parkurda skor eşdeğerliği veya online uyumluluk iddiası yoktur.
