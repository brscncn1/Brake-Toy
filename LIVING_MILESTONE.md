# v0.5 Living Workshop — sonuç ve eleştiri

## Bu sürümde yapılanlar

1. **Oda/model:** Ayrı ToyRoomLiving sahnesi; taranmış ahşap sandalye, gerçek pamuk dokuları, kıvrımlı perde/yorgan/yastık ve kumaş saklama kutuları. Derin pencere/doğrama, döndürülmüş mobilya ayrıntıları, metal spiralli defter, içi boş kalemlik ve sivriltilmiş altıgen kalemler. Yatak/sandalye kadrajı son renderdan sonra düzeltildi.
2. **Işık:** Sıcak pencere yönü/masa gölgesi, azaltılmış soğuk dolgu, HDR stüdyo yansıması, yüksek profilde hafif ACES/Bloom. Ekonomik profil postprocessing/SSAO'yu kapatır. Bütçe fiziksel cihaz ölçümü bekler.
3. **Araç:** Sürekli eğrisel pres gövde, teker çevresi açıklıkları, kıvrımlı cam/çatı, gövdeyi izleyen şeritler, aynalar, kapı çizgisi, far/arka stop, tampon ve spoyler. Dört teker animasyonu ve gameplay ölçüleri korundu.
4. **Oynanış:** İlk engel yaklaşık 2 sn; ayna yönleri değişen kısa ritimli kaçışlar. Üç tur hedefi: 60 m, 3 yıldız, 6 yakın geçiş. İsteğe bağlı engel kenarındaki yıldız +75 stil verir; güvenli merkez koridoru kalır. Puan kuralı değiştiği için v0.5 stil rekoru ayrı, eski rekor silinmez.
5. **Ses:** CC0 yuvarlanma ve ahşap/metal/yumuşak çarpışma kayıtları. Sentez motoru daha düşük seviyede kalır. MOTOR/EFEKT menü ayarları ayrı saklanır. Tablet hoparlöründe dinleme kabulü bekler.
6. **Cihaz hazırlığı:** Android APK, iki kalite profili ve sürüşe bağlı 60 sn FPS/p95 ölçümü hazır. Fiziksel kurulum/performans/ısı/dokunma sonucu kullanıcı denemesiyle kapanacak.

## Kanıt

62/62 Unity testi geçti. Dört seed ve iki rota tercihiyle 16 dakika simüle sürüş; model/kamera/ödül/pause/reset/profil/ses referansı kontrolleri başarılı. Gerçek Unity oynanış ve ön/arka araç/oda renderları Preview-v0.5*.png, Models-v0.5-*.png ve Room-v0.5-detail.png içindedir; konsept görüntüsü değildir. COMPARISON-v0.5.html önce/sonra görünümüdür. TEST_REPORT.md ve JSON/XML kanıtları daha ayrıntılıdır.

## Hâlâ beğenmediğim / kabul etmediğim taraflar

- Oda daha ayrıntılı ama ışık dolaylı aydınlatma açısından hâlâ sınırlı; pencere dışı görünüm düz. Sonraki sanat kapanışında mobil bütçeye uygun baked/ışık probu ve gerçek dış ortam kompozisyonu değerlendirilmeli.
- Araç biçimi önceki kutu kabinden daha iyi; tekerlek dişleri ve metal parlaklığı oyuncak stilinde baskın. Yakın plan renderını final otomotiv modeli diye sunmuyorum. Fiziksel ekranda okunabilirlik ve kullanıcı sanat kabulü gerekli.
- Üç hedef kısa turda amaç veriyor; uzun dönem çeşitlilik, görev havuzu, kalıcı ödül/garaj ve Relax henüz yok. Faz 4'te bunlar eklenecek.
- Sesler artık yalnız sentez değil; motor hâlâ sentez. Tablet hoparlöründe tekrar/yoruculuk ve mix dengesi henüz dinlenmedi.
- 205.618 üçgen sayımı tüm inaktif havuzu da içerir; FPS değildir. FPS/ısınma ölçülmeden yüksek profili cihazda başarılı saymıyorum.

## Faz ve kalan yol

Aktif odak Faz 3; Faz 2 kapanışları bu blokta. Mevcut Faz 2–3 kapanışı dahil **4 ana blok**: mevcut sanat/oynanış/cihaz kabulü; Faz 4 ilerleme/garaj/Relax; Faz 5 online/günlük/ghost; Faz 6 yayın hazırlığı. Faz 7 yeni odalar ilk yayın için isteğe bağlı. Bu dört sürüm ya da sabit tarih vaadi değildir. Bu sürüm altı çözümün uygulamasını ilerletir; fiziksel ve öznel kabulü tamamlanmış saymaz.
