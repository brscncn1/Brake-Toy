# K2.1 — ilk dakika tempo ve tekrar ölçümü

Tarih: 25 Ekim 2026. Dal `feat/opening-pace-audit`; taban PR #15.
Bu paket ölçüm yapar; hız, engel üretimi, ödüller ve modeller değiştirilmedi.

## Yöntem

- Gerçek AdventureSequence ve JoyfulRules ile her modda seed 1–100 hesaplandı:
  200 parkur, 10/30/60 saniye için 600 pencere.
- Sayılan olay beat.Distance referansının geçilmesidir; tüm hareketli oyuncakların
  oyuncuya varış anı veya insanın verdiği anlamlı karar sayısı değildir.
- Güç ve bant yavaşlatması yoktur. Gerçek PlayMode güvenli yol witness'ı üç seed,
  iki mod, 60 Hz ve 60 saniye ile bu varsayımı kontrol eder (18 ara örnek).
- 7331 Arcade için 10/30/60 saniye 720×1280 motor görüntüleri alınır.
  Bunlar sabit adımla simüle edilmiş anlık karelerdir; gerçek zamanlı video değildir.
- İnsan tepkisi, uyarı okunabilirliği, eğlence ve tutunma bu ölçümle belirlenmez.

77 EditMode +72 PlayMode =149/149 geçti; gerçek motor witness'ı 18 örnekte analitik sayılarla eşleşti.
[Üç motor görüntüsü](../Reports/Visuals/OPENING_GALLERY.html). Kamera takip konumu
anlık karede sabitlenir; uyarı yerleşimi için insan değerlendirmesi gerekir.

## Sonuçlar: 100 seed'in tamamında aynı adetler

| Mod | Süre | Referansı geçilen cümle | Farklı ana aile | Ortalama aralık |
|---|---:|---:|---:|---:|
| Arcade | 10 sn | 4 | 4 | 2.70 sn |
| Arcade | 30 sn | 11 | 8 | 2.67 sn |
| Arcade | 60 sn | 22 | 8 | 2.66 sn |
| Relax | 10 sn | 2 | 2 | 4.91 sn |
| Relax | 30 sn | 7 | 7 | 4.38 sn |
| Relax | 60 sn | 15 | 8 | 4.02 sn |

Aralıklar pencereye giren ardışık referans geçişlerinin ortalamasıdır; ilk bekleyiş dahil değildir.
Ham mesafe/hız/ilk karşılaşma ve seed bazlı örnekler `Reports/OPENING_PACE.json` içindedir.

## Yapısal bulgular ve yorum

- Güvenli yol geçişlerinin %100'ü merkez↔kenar düzenindedir. Yön değişse de bu ritim değişmez.
- Arcade'te ilk 30 saniyede sekiz ana aile görülür; aile çeşitliliği tek başına akış çeşitliliğini sağlamaz.
- AdventureEncounters.Spawn her cümlede üçüncü şeride engel ve risk tarafına iki yıldız koyar.
  Cetvel dışındaki yedi aile kitap rampasına bağlanır; cetvel de uçuş fırsatı verir.
  Bu ortak bitirişler kod incelemesi bulgusudur; yeni algoritmada ölçülmüş oyuncu tercihi değildir.
- Hız JoyfulRules eğrisiyle ilk saniyelerden artar; aralık ise Arcade'te yaklaşık 2.65 saniyeye oturur.
  Tepki yoğunluğu değerlendirmesinde hız ve karşılaşma sıklığını ayrı ele almak gerekir.
- 30 birimlik yazılmış cümle bütçesi varsayılırsa Arcade'teki hesaplanan ara ortalaması
  10 saniyelik pencerede 1.09, 60 saniyelik pencerede 1.49 saniyedir. Bu, ekranda
  hiçbir şey görünmediği süre değildir; kamera ilerideki nesneleri gösterebilir.
- Çıkarım: merkez/kenar ritmi ve ortak bitirişler tekrar hissine katkı sağlayabilir.
  Oyuncu hissinin nedenini kesinleştirmek için önce/sonra oynanış karşılaştırması gerekir.

## Sonraki tek deney

K2.3/K2.4 için önce hız eğrisini koruyup bitiriş çeşitliliğini değiştir:
iki mevcut ailede yerden ödül geçidi ile kitap rampası bitirişlerini kurallı biçimde çeşitlendir.
Güvenli yol witness'ı ve erişilebilir iniş alanı korunmalı; güç kullanımı zorunlu olmamalı.
Sonra aynı seed'lerle önce/sonra karşılaştır; sıklık veya hızı aynı deneyde birlikte değiştirme.
Riskli yol witness'ı (K2.6) ve çocuk/ebeveyn geri bildirimi (K2.8) ayrıca açık kalır.

## Tekrar üretme ve sınırlar

- Analitik çıktı: `-executeMethod BrakeToy.Editor.OpeningPaceAudit.Capture`.
- Gerçek motor ölçümü: OpeningRuntimeAuditTests; GPU açık PlayMode gerekir.
- PNG aktarımı için Unity'nin yerleşik imageconversion 1.0.0 modülü manifest/lock'a eklendi.
  İlk derlemede bu açık bağımlılığın eksikliği yakalandı ve düzeltildi; yeni SDK eklenmedi.
- Nihai test/çıktı hashleri `Reports/OPENING_VALIDATION.json` içindedir.
- Yeni APK, cihaz FPS/p95/ısı, insan sanat kabulü ve video bu pakette yoktur.
- Notlar masaüstü BrakeToy_Notion_Yedek.txt dosyasına da eklenir; Notion tekrar denenmedi.
- Bu son planlı iş değildir. Son planlı yayın/devir paketine başlarken kullanıcıya haber verilir.
