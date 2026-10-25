# K2.3/K2.4 — iki ailede bitiriş çeşitliliği

Tarih: 26 Ekim 2026. Dal `feat/varied-encounter-endings`; taban PR #16.

## Oyuncuya etkisi

- Kurmalı oyuncak ve tren karşılaşmaları iki bitirişten birini kullanır:
  yerden üç yıldızlı ödül rotası veya mevcut kitap rampası/hava ödülü.
- Yer rotasında ana oyuncağın arkasında +13/+19/+25 birimde yıldızlar bulunur;
  risk yoluna ek altın ok yerleştirilir. Yeni bir fiziksel garaj kapısı eklenmez.
- Rampa bitirişinde iki yaklaşma yıldızı ve bir hava yıldızı korunur.
- Her cümlede üç yıldız vardır; yer/hava erişilebilirliği ve hava TP bonusu aynı değildir.
  Daha kolay yer rotası ile uçuş bonusu arasında seçim farkı korunur.
- Seçim seed varyantından gelir; frog/duck seçen düşük bitten ayrı bit kullanılır.
- Diğer altı aile, ana aile sırası, güvenli yol, üçüncü şerit engeli, hız ve cümle mesafesi değişmez.
- Bu iki ailelik bir deneydir; bütün parkurun tekrar hissinin çözüldüğü iddia edilmez.

## Geri alma ve kayıtlar

- AdventureEncounters.VariedEndings varsayılan true'dur; mevcut sahne yeni varsayılanı alır.
- Inspector'da false yapıp yeni tur başlatmak aynı seed'de klasik bitirişe döner.
  ActiveVariedEndings tur başında sabitlenir; tur ortasındaki Inspector değişikliği
  nesneleri, sonraki spawn'ları veya ghost profilini değiştirmez; yeni tur kullan.
- AdventureRules sürümü adventure-r2 oldu. Ghost anahtarları r2-varied/r2-classic olarak
  ayrılır; eski ghost-adventure-latest dosyaları silinmez veya uyumsuz biçimde oynatılmaz.
- Kariyer, TP, araçlar, güç stokları ve yerel rekorlar korunur; yeni kariyer sıfırlaması yoktur.
- Yerel eski/yeni stil rekorlarının rekabet eşdeğerliği kanıtlanmadı. Canlı online zaten kapalıdır;
  bu paketin sonuçları eski review-r1 servisine gönderilmez.

## Doğrulama kapsamı

82 EditMode +77 PlayMode =159/159; son görsel fixture kontrolü5/5 ayrıca geçti.
[Klasik/yeni motor görüntüleri](../Reports/Visuals/ENDING_GALLERY.html).
Yeni altın okun0.079yüksekliği motor karesinde yüzeyin üzerinde doğrulandı.

- Core: iki ailede model paritesinden bağımsız iki bitiriş, diğer altı ailenin korunması,
  100 seed/64 cümle kapsamı ve ghost namespace ayrımı.
- Motor: 16 dizilimde üç yıldız/rampa/yükseklik bütçesi ve klasik seçeneğe dönüş.
- İki aile, iki bitiriş, 30/60/120 Hz: 12 risk rotası koşusu; güç kullanmadan ödül ve kalkış kontrolü.
- Aynı üç seed, Arcade/Relax, klasik/yeni: 12 adet 60 saniyelik güvenli witness.
  Aynı mesafe, aynı üretilen cümle sayısı, korunmuş haklar ve havuz sınırları kontrol edilir.
- Aynı aile/varyant/yerleşim için dört klasik/yeni motor karesi alınır. Bunlar kontrollü
  poz karşılaştırmasıdır; gerçek zamanlı oynanış videosu veya kullanıcı sanat kabulü değildir.
- Nihai XML, kaynak ve görüntü hashleri `Reports/ENDING_VARIANTS_VALIDATION.json` içindedir.
- Yeni SDK/model/paket bağımlılığı eklenmedi. Yeni bitiriş sürüşte koleksiyon/oyuncak üretmez;
  mevcut havuzlardan yıldız ve işaret seçer.
- APK, tablet FPS/p95/ısı ve insan zorluk kabulü bu paketle tamamlanmış sayılmaz.

## Sonraki paket

K2.5: uyarı metinlerinin yerleşim/kontrast/önceliği. K2.6'nın diğer ailelerdeki risk
rotaları ve K2.8 insan geri bildirimi açık kalır. Hız değişikliği ayrı deney olmalıdır.
Son planlı aşamaya henüz geçilmedi; K6 başlangıcında kullanıcıya özellikle haber verilir.
Notion tekrar denenmez; masaüstü BrakeToy_Notion_Yedek.txt aktarım yedeği güncellenir.
