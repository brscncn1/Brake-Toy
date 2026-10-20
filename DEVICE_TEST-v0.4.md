# v0.4 gerçek cihaz kabulü

Durum: APK hazırlığı sürüyor. Cihazda ölçülmüş FPS, ısı veya fiziksel dokunma sonucu henüz yok.

## Cihaz matrisi

| Cihaz | Bilinen donanım | Yüksek | Ekonomik | Dokunma / ısı |
|---|---|---|---|---|
| Kullanıcının Huawei MatePad 11 | HarmonyOS 3.0.0, Snapdragon 865, 6 GB, 2560×1600 (kullanıcı fotoğrafından) | Ölçüm bekliyor | Ölçüm bekliyor | Kullanıcı denemesi bekliyor |
| Orta sınıf Android telefon | Henüz sağlanmadı | Bekliyor | Bekliyor | Bekliyor |

Tabletin bilgisayara kabloyla bağlı olması adb yetkilendirmesi anlamına gelmez. USB hata ayıklaması ayarı bulunamadığından kullanıcı daha fazla ayarla uğraştırılmadan APK ile deneme yoluna geçildi. Cihaz fotoğrafı depoya yüklenmedi.

## USB olmadan deneme

1. v0.4 APK'sını tablette indirip açın. Bu geliştirme APK'sıdır.
2. Dikey tutun; OYNA. Sürükleme, bırakıp tekrar dokunma, ikinci parmağın karışmaması, duraklat/devam ve uygulamaya geri dönmeyi kontrol edin.
3. Menüde 60 sn PERFORMANS TESTİ düğmesine basıp oynayın. İlk 3 saniye ısınma, sonra toplam 60 saniye gerçek sürüş kareleri ölçülür. Çarpma veya duraklatma sırasında süre ilerlemez; tekrar oynayarak tamamlanabilir.
4. Menüde ortalama FPS ve p95 kare süresi görünür. Sonucu ve profil adını paylaşın. Ölçümü başlat düğmesine tekrar basmak yeni ölçüm açar.
5. Ekonomik profilde aynı işlemi tekrarlayın. Yüksek: hedef 60 FPS, ölçek 1, 4×MSAA, 75 birim gölge/2 kademe ve SSAO. Ekonomik: hedef 30 FPS, ölçek .8, 2×MSAA, 35 birim gölge/1 kademe ve SSAO kapalı. Hedefler ölçülmüş sonuç değildir.
6. Yaklaşık 10 dakikalık denemede hissedilir ısınma, takılma, ses/çarpışma zamanlaması ve kontrol rahatlığını ayrıca not edin. Kısa FPS ölçümü termal kararlılığı veya fiziksel dokunma gecikmesini kanıtlamaz.

Ölçüm yalnızca cihazda JSON'a kaydedilir; otomatik ağ gönderimi yoktur. Tekil cihaz kimliği kaydedilmez. p95, ölçülen karelerin %95'inin altında kaldığı süredir; düşük değer daha düzenli kare anlamına gelir.

## İsteğe bağlı yetkili adb ölçümü

Development APK, explicit intent ile 60 saniyelik güvenli tanık sürüşü çalıştırabilir: brakeToyBenchmark=true ve brakeToyLow=true/false. Bu mod kullanıcı rekorlarını ve grafik tercihini yazmaz. normal açılışta otomatik devreye girmez. Logdaki BRAKE_TOY_PERFORMANCE kaydı ile uygulamanın kendi dosyasındaki performance-v04-high/low.json eşleştirilebilir. Otomatik tanık sürüşü fiziksel dokunma kabulü değildir.
