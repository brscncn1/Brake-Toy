# v0.3 — Arcade Workshop

## Oyuncuya değişen deneyim

Artık bekleyen düz bir sürüş yerine yaklaşık ilk 2.2 saniyede ilk kaçış ve sürekli artan hız var. 10 birim/s başlangıç, 18 tavan; yaklaşık 10. saniyede 1.34×, 30. saniyede 1.65×. Tek/duble geçitler, yön değişimleri, kısa nefes araları ve yaklaşık ilk 6–7 saniyede ilk uyarılı top karşılaşması. Seed 7331 aynı diziyi yeniden denemeyi sağlar.

Yakın geçiş: tehlike tamamen arkada kalınca, temas olmadan .005–.48 birim kenar boşluğu. Tek ödül, başlangıç 100 puan. 1–2 kombo 1×, 3–4 2×, 5–7 3×, 8+ 5×; 6 saniye ara veya çarpışma komboyu sıfırlar. Combo Fever renkli görsel iz verir; hız/collider avantajı yok. Mesafe ve stil rekorları ayrı yerel kayıtlardır; küresel sıralama değildir.

HUD: mesafe, stil, vites, çarpan ve süre çubuğu; topun geldiği taraf uyarısı. Motor/vites/yakın geçiş/çarpışma için özgün kısa sentez sesleri ve kalıcı ses kapatma. Sonuçta mesafe/stil/rekor ve bekletmeyen retry.

## Görseller ve modeller

Özgün detaylı Sparky diecast gövdesi; ayrı cam, boya, metal, kauçuk; jant/spoke/tread, ayna, far, tampon ve kapı detayları. Kitap sayfaları/kapak/ayraç, silgi kılıfı, kabartma bloklar ve dikişli top. Pencere açıklığı, kumaş perde ve açık abajur, oda ölçeği düzeltmeleri, gerçek CC0 ahşap diffuse/normal, küçük reflection cubemap, temas gölgesi ve güneş/dolgu ışığı. Dolgu ışığı dolaylı ışığı yaklaşıklar; ray tracing veya tam baked GI yok.

Kaynak gövde ve her tekerlek materyal bazında birleştirilir; sabit oda materyal bazında birleştirildi, kayan dekorlar ayrı. 24 tehlike örneği sürüş sırasında tekrar kullanılır. Eski v0.1 ve v0.2 sahne/varlık/APK korunur.

## Kullanım

Unity Tools → Brake Toy → Open Arcade Workshop v0.3. Game: 16:9 Portrait (9:16), Scale 1×. Play → OYNA; fareyi basılı tutup sağa/sola sürükle. Telefonda parmakla sürükle. Ses düğmesi alt ortadadır.

Sahne: `Assets/BrakeToy/Scenes/ToyRoomArcade.unity`. Yeni modeller/materyaller: `Art/Workshop`. Kamera 20° ve oyuncu alt %25'te. Aracın gameplay kutusu .78 × .7 × 1.4 olarak korundu.

APK: `Builds/Android/BrakeToy-v0.3-Arcade.apk` (38.9 MiB). SHA256 `a6a9cbdb0aa321f6e54acb744654c23b75b49eda272dabf3dde0cfb0d73d4ed7`.

## Kanıt ve sınırlar

**41/41 Unity testi geçti**, Android paket/imza kontrolü geçti. Gerçek görüntüler: Preview-v0.3.png, Models-v0.3-front.png, Models-v0.3-rear.png, Models-v0.3-obstacles.png. Araştırma ve kaynakları RESEARCH-v0.3.md; haklar THIRD_PARTY_ASSETS.md; ayrıntılı kontroller TEST_REPORT.md.

Bu sürüm Faz 2 çekirdeğini ve Faz 3 görsel çalışmalarını ilerletir. Genel ObstacleDefinition kataloğu/keyfi hareketli tehlike çözücüsü, başka araçlar, garaj/ekonomi/görevler/Relax, online ve final art tamamlanmadı. Üretici doğrulanmış sınırlı kalıp ailesidir; her yanlış oyuncu kararını kurtarmaz. Fiziksel cihaz ve kullanıcı geri bildirimi bekleniyor. Orijinal tam kapsam korunursa yayın hazırlığı Faz 6; önceki Faz 5 final ifadesi düzeltilmiştir.

Üretici araçları yalnızca boş ilk üretim için kullanılmalı; elle düzenlenmiş sahnede Refine/Polish çağrıları mevcut sanat düzenini değiştirir. Kaydedilmiş sahneyi Open ile açmak yeterlidir.
