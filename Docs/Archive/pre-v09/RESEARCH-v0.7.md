# v0.7 — ses, hareket ve sanat kararları

## Gerçek ses kaynakları

- Menü: [leo's theme — Bobjt](https://opengameart.org/content/leos-theme). Yazar CC0 ve CC BY seçenekleri sunuyor; CC0 seçildi. Giriş/son sessizlik kesildi, 2 sn equal-power örtüşme ile 87.01 sn döngü hazırlandı.
- Sürüş: [Pixel Sprinter — Zane Little Music](https://opengameart.org/content/pixel-sprinter), CC0. Yazarın yarış/koşu için yayımladığı 34.29 sn loop sürümü; ayrı bestelenmiş yarış müziği, sentetik motor tonu değil.
- Korna: [Bicycle Horn — AntumDeluge](https://opengameart.org/content/bicycle-horn), CC0. Çocuk plastik bisiklet kornasının gerçek kaydı. Near-miss melodisi yeniden korna diye kullanılmıyor.
- Çarpma: [LEGO Pour 1 — rioforce](https://freesound.org/people/rioforce/sounds/233662/), CC0. Plastik oyuncak parçalarının masaya dökülmesi; kamuya açık HQ önizlemesinden 0.85 sn kırpılıp normalize edildi. Kullanım plastik oyuncak şakırtısı içindir; marka/film ilişkisi veya ürünün onayı yoktur.

Kaynak URL/kimlik/lisans/SHA256: Assets/BrakeToy/Audio/Joyful/SOURCES.json. İşleme özeti AUDIO_CHECK.json. Menü/sürüş için iki ayrı preloaded AudioSource kullanılır; DSP zamanlı başlangıç, equal-power geçiş ve genel/müzik sesi. Çarpışmada müzik kısa kısılır. İnsan dinleme ve döngü hissi kabulü henüz yapılmadı; matematiksel süreklilik herkeste fark edilmeyen müzik garantisi değildir.

## Yeni kurallar ve top hissi

Eski v0.6 doğrulamasını değişmiş oyun için kanıt saymıyoruz. Joyful-r2: Arcade başlangıcı 14, 10 sn'de yaklaşık22.2, 30 sn'de28.1, tavan30. Relax9→14. Mesafe analitik hız integralidir. Encounter zamanı bu integralin tersiyle üretilir. Son sekiz desen reddedilir; 32 desen ailesi, değişken oyuncak/evre/phase ve seed kullanılır. Bu gerçek sonsuz benzersizlik garantisi değildir.

Toplar görsel olarak hareket eden masaya göre yer değişimleri üzerinden yuvarlanır. İlave ileri hız, artan yanal eğrilik, renkli paneller ve dikişler dönüşü görünür kılar. İkili toplar 12 sn sonrası evrelerde, üçlüler 36 sn sonrası evrelerde açılır. Üçüncü top gecikmelidir; güvenli geçit kapatılmaz. Otobüs ayrı kinematik X yolu kullanır. Otomatik tanık/erişilebilirlik testleri insan refleksinin tam modeli değildir.

## Rampa ve ekonomi

Yeşil yaklaşma oku; kitap rampasının eğiminde görsel yükselme, uçtan1.65 sn sıçrama ve iniş. Oyuncu görseli yükselir, çarpışma mantığı engel yüksekliğini de değerlendirir. Yalnızca havada kazanılan mesafe/stil TP bölümünün bir katı daha eklenir; görev/başarım ödüllerinin tamamı ikiye katlanmaz. Normal toplam artı hava bölümü merkezi CareerRules'ta hesaplanır. Havada2x bilgisi HUD'dadır.

Yeni rekor dosyası/anahtarları ve yeni X+Y ghost başlığı kullanılır; v0.6 rekorları ezilmez. Önceki TP ve açılan ürünler yeni kariyere taşınır. Yeni kurallar eski review-r1 sunucusuna gönderilmez; v0.7 online bağlantısı kapalıdır, üretim kabulü zaten yapılmamıştır.

## Sanat yaklaşımı

Toy Story sıcak sinematik oyuncak odası hissi için kullanıcının referansıdır; karakter/kopya varlık kullanılmadı. Kamera geri alınır, masa/araç kadrajı küçülür, oda mobilyası/oyuncaklar görünür. Ahşap boş alan yerine mint oyun inlay'i, krem boyalı masa, renkli kenar/dikiş/çıkartmalar; bez bebek/ayı/robot/ördek/roket/otobüs; radyo, oyuncak sandıkları ve renkli oda ayrıntıları. Mevcut lisanslı pamuk normal dokuları bez oyuncaklara da uygulanır.

Pencere: ayrı dış ortam dioraması, sürekli güneş/ay yolu, değişen oda ışığı, gece yıldızları ve gündüz bulut/kuşlar. Gece oynanış kararmasın diye minimum dolgu ve sıcak oda ışığı korunur. Bunlar gerçek zamanlı Unity sistemleridir; film kalitesi veya cihaz performansı kabulü için gerçek render ve insan testi gerekir.
