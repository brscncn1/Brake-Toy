# v0.9 Adventure — 24 Ekim 2026

Kullanıcının onayladığı sekiz parkur mekaniği yeni ToyRoomAdventure sahnesine eklendi. ToyRoomTidy ve eski sahneler korunur. Hız eğrisi ve en az 48m karşılaşma aralığı aynı; yeni öğelerin çeşitliliği, yüzey ve zamanlama kararları zorluk algısını değiştirebilir.

| Mekanik | Uygulama |
|---|---|
| Karton tünel | Üç kemerli makette iki kullanılabilir çıkış; biri yeşil güvenli, diğeri altın riskli/ödüllü. Üçüncü çıkış kapalı. Duvarlar çarpışmaya dahil. |
| Kurmalı oyuncak | Anahtarlı kurbağa/ördek; varyanta göre zıplama/yürüme, yanal hareket ve alçak gövde teması. |
| Oyuncak vinç | İki kule, askı, sabit uzunlukta halat, sallanan yumuşak oyuncak yükü ve masa üzerinde takip eden gölge. |
| Oyuncak tren | Ayrı lokomotif/vagon maketleri, raylar ve uyarı lambası; riskli geçişi enine keser, yeşil yan yol açık. |
| Misket oluğu | Görünür oluk, 1/2/3 misket, farklı hız/yan eğri; dönme gerçek masa göreli hareketinden hesaplanır. Başlangıç mesafesi hızla ölçeklenerek diğer karşılaşmaya erken girme engellenir. |
| Bant/kart | Pembe bant yerde %62 ileri hız; parlak kartta sınırlı direksiyon ataleti. Havada etkisiz, yeniden başlatmada sıfırlanır. |
| Cetvel tahterevalli | İki kenardan destek yüzeyi, yeşil giriş oku, eğilme ve havaya kalkış; mevcut havada TP ×2 sistemine bağlı. |
| Oyuncak garaj | Açılan/kapanan kapı, uyarı ışıkları, farlı iki araç; araçlar kapının arkasından öne çıkar. |

## Kombinasyonlar

Sekizli karıştırılmış torba her aileyi gösterir, son üç aileyi tekrar etmez. Tohum, yan yol, varyant ve hareket fazı tekrar oynanabilir. Karşılaşmalar yaklaşma yıldızları → karar → isteğe bağlı rampa/havada yıldız şeklinde bağlanır. Tünel çıkışı kurmalı oyuncağa, yüzey bölgesi misketlere bağlanır. Üçüncü şeritte eski oyuncak engeller bulunur. Domino yoktur.

Yeşil yol tüm yeni hareketli hacimlerin dışında tutulur; ardışık güvenli şeritler komşudur. Powers, landing grace, duraklatma ve yeniden başlatma yeni öğelere bağlıdır. Sabit parçalar malzemeye göre birleştirilir; hareketli pivotlar korunur. Çalışma başlangıcında 8 × 6 havuz hazırlanır; sürüşte yeni model üretilmez.

## Korunanlar ve sınırlar

Raf üzerindeki radyo, 64m kenar yerleşimi/defter boşlukları, müzik, araçlar, görevler, TP ve güç envanteri korunur. Yeni rekor anahtarları/ghost dosyaları eski parkur kayıtlarıyla karışmaz; kariyer dosyası aynıdır. Çevrimiçi sıralama ve gerçek ödemeler açılmadı.

Bunlar mevcut sanat diline uygun hazırlanmış oyuncak maketleridir; kullanıcının daha sonra ayrı model çalışması yaptırma kararı geçerlidir. Mobil FPS/ısınma ve insan görsel/oynanış kabulü otomatik testlerden çıkarılamaz. Notion yeni blok ekleme kotası nedeniyle bekler; bu belgedeki yeni notlar henüz Notion'a eşitlenmemiştir.

## Doğrulama

**112/112 test geçti:** 50 EditMode +62 PlayMode. 100 tohum ×256 seçimde kapsama/tekrar/mesafe; 6 tohum ×2 mod ×120 saniye (24 dakika simüle koşu) açık yol, komşu şerit erişimi ve sıfır havuz tükenmesi. Yeni temas/tünel duvarı, yerde yüzey/jetpack, iki cetvel kenarı/kalkış/havada mesafe, güç/duraklatma, misket sayısı/hızı, gerçek tren yanal geçişi, garaj çıkışı ve halat desteği doğrulandı. Bu ölçümler fiziksel cihaz FPS testi değildir. Android/model/render son kanıtı teslim bölümüne eklenir. İlk uzun koşuda gelecek misketlerin erkenden ulaşması yakalandı ve düzeltildi; bu bulgu gizlenmeden geliştirici günlüğüne işlendi.


Tünelin yüksek tavanı kitap uçuşu ve jetpack için pay bırakır; tavanın kendisi de çarpışma hacmine dahildir. Önceki riskli rampadan havada gelen araba kaçınılmaz bir tavana sokulmaz.

## Görsel ve model kontrolü

13 gerçek Unity/URP görüntüsü ADVENTURE_GALLERY.html içinde: menü, 45 saniyelik gerçek koşu, sekiz aile yakın planı, parkur gösterimi, ördek ve kart varyantları. Kurbağa ön ayakları gövdeye bağlandı; cetvel oku masa yüzeyinden ayrılarak titreme giderildi. Yakın plan kamerası oynanış kamerası değildir. Sekiz prefab toplam83 renderer/90.649 üçgen içerir; sahne/kapalı havuz toplamı çalışma anındaki draw-call veya FPS ölçümü değildir. Model taramasında eksik mesh/malzeme/shader hatası ve dekoratif fizik collider'ı yoktur; çarpışma analitik hacimlerle yapılır.

## Android teslimi

0.9.0/kod10, com.braketoy.prototype, ARM64 IL2CPP non-Development: **40568255 bayt**, SHA256 `4cb72fc735be4f5dc6300eece1d4cac34f99da589ec7d4e05c97e11a883726ba`. APK v2 imza, önceki testlerle aynı sertifika, ZIP bütünlüğü/16KB ve ELF LOAD16KB kontrolleri geçti. Yalnız INTERNET izni; online kapalı. Kaynak `b656017cb48c7b10b62891647135d0bc2371cfde`. Fiziksel v0.9 tablet kurulumu/performansı ve 16KB cihaz kabulü henüz yapılmadı; mağaza imza anahtarı değildir.

[APK ve sürüm](https://github.com/brscncn1/Brake-Toy/releases/tag/v0.9.0) · [Kaynak](https://github.com/brscncn1/Brake-Toy/tree/feat/adventure-v09)
