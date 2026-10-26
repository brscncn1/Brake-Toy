# v0.10 Adventure — fiziksel cihaz kabul föyü

Durum: tablet 27 Ekim çalışmasında bağlanamıyor. Aşağıdaki sonuçlar henüz ölçülmedi.
PC otomasyonu tablet dokunmasını, ısınmasını veya sürdürülebilir FPS'yi kanıtlamaz.

## Hazır olduğunda

1. Huawei MatePad 11'i USB ile bağla, hata ayıklama bilgisayar iznini onayla.
2. Mevcut uygulamayı kaldırmadan güncelle; kaldırmak yerel kariyeri silebilir.
3. APK kaynak sürümü ve SHA256 değerini teslim raporuyla karşılaştır.
4. Önce ses/grafik/geri düğmesi/garaj/görevler/yerel rekorları kontrol et.
5. Full profilde en az 10 dakika; sonra ekonomik profilde aynı süre dene.

## Kaydedilecekler

| Kontrol | Sonuç |
|---|---|
| Model / HarmonyOS / APK sürümü | Bekliyor |
| Full: ortalama FPS / p95 / 50ms üzeri kare | Bekliyor |
| Ekonomik: ortalama FPS / p95 / 50ms üzeri kare | Bekliyor |
| 10 dakika sonunda ısınma / takılma | Bekliyor |
| Parmağı kaldırıp yeniden koyma / kenar sürüklemesi | Bekliyor |
| 30sn ve 60sn zorluk / açık rampa ve riskli rampa | Bekliyor |
| Pause, uygulamadan çıkıp geri dönüş, yeniden başlatma | Bekliyor |
| Kayıt / güç stoku / ses ayarlarının korunması | Bekliyor |
| Yeni masa kaplaması / araç örnekleri sanat kabulü | Bekliyor |

Ayarlar → Ölçüm → ölçümü başlat → geri → sürüş başlat.
3 saniye ısınmadan sonra 60 saniye sürüş kaydedilir; pause süresi sayılmaz.
JSON yalnız uygulamanın yerel dosyalarına yazılır; otomatik gönderim yok.
Üretim APK'sında adb otomatik benchmark etkin değildir; ölçüm elle başlatılabilir.
Development build benchmark tanığı gerçek insan kontrol testi olarak raporlanamaz.

## Geri alma

Masa kaplaması Ayarlar düğmesiyle eski/yeni arasında değişir; çarpışma değişmez.
Önceki araç prefabları silinmez; model referans değişimi ExperienceArtUpgrade ile sınırlıdır.
Eski APK daha yeni kayıt verisiyle uyumlu varsayılmaz; geri dönüşten önce yedek alınır.
