# v0.4 — Refined Room sonuç ve dürüst kalite kaydı

Bu çalışma, kullanıcının v0.3 kalite raporundaki tüm önerileri aynı sürüme alma kararıyla yapıldı. Aşağıdaki uygulamalar oyunun final sanat seviyesine veya tüm oyuncularda eğlence kabulüne ulaştığı anlamına gelmez.

| Eleştiri | Bu sürümde yapılan | Kanıt / sınır |
|---|---|---|
| Görsel bütünlük | Ayrı Refined oda/malzeme seti, sıcak ahşap, daha açık duvar, pencere derinliği/doğrama/donanım ve bitki | Gerçek Unity görüntüleri; konseptteki kumaş/doku/aydınlatma zenginliği hâlâ daha yüksek |
| Baskın masa/gölge | Gerçek doku değiştirilmeden shaderda kontrast azaltıldı, normal zayıflatıldı; güneş açısı/şiddeti ve gölge baskınlığı düzeltildi | Aynı 540×960/6.3 sn koşulunda v0.3-v0.4 görüntüleri |
| Kutu kabin/cam | Alçaltılmış ve hafif daraltılmış/eğrilmiş çatı, daha açık mavi cam, gerçek teker boşlukları | Ön/arka yakın plan; gameplay kutusu .78×.7×1.4 korundu |
| Kamera/UI | 16° kamera, yan boşluk .45; üst şerit kaldırıldı, köşe göstergeleri; tek menü, sürüşte ikincil düğmeler kapalı | 4 portre oranı ve UI testleri |
| Tekrarlayan karşılaşma | Prefab/ölçü/havuz boyutunu tanımlayan 4 ObstacleDefinition; sahne başında oluşturulan 24 havuz nesnesi | Sürüşte yeni engel oluşturulmaz; keyfi birleşim çözücüsü değildir |
| Düzenli toplar | Değişken satır aralığı, en fazla iki aynı güvenli koridor, 5–8 satır arasında top olayı, farklı kesişim/eğim | 128.000 yeni kalıp satırı; eski 128.000 satır regresyonu da korunur |
| Ses/çarpışma | Katmanlı motor/teker sürtünmesi, ratchet/vites, kuru ahşap-metal ve kauçuk tepkileri; kısa grounded sallanma/kamera vuruşu, sonuç paneli zamanlaması | Altı ses katmanı dalga denetimi ve impact testi; profesyonel kayıt/dinleme kabulü değildir |
| Performans kanıtı | İzole yüksek/ekonomik profiller, yerel 60 sn ölçüm, isteğe bağlı development benchmark ve cihaz matrisi | Gerçek tablet FPS/ısı/dokunma sonucu hâlâ bekliyor; hedef 60/30 ölçüm yerine geçmez |
| Sunum | Gerçek önce/sonra, model renderları, ayrı test/Android raporları, kapsam ve eksik kabul ayrımı | AI konsepti mevcut çıktı gibi gösterilmez |

## Oynanış farkı

Yeni tur farklı bir seed üretir. Sonuç ekranında YENİ PARKUR ve AYNI PARKURU TEKRARLA ayrı seçeneklerdir. Aynı parkuru tekrar etme bu oturumdaki diziyi yeniden kurar; uygulama kapatıldıktan sonra kayıtlı seed tarayıcısı sunulmaz. İlk tehlike ve mevcut hız eğrisi korunur; asıl değişiklik dizilerin ve top olaylarının daha az ezberlenmesidir. 0–25 saniye yavaş girişe dönülmedi.

Katmanlı sesler özgün sentezdir; lisansı belirsiz ses/model paketi alınmadı. Görsel çarpışma gameplay kökünü veya collider'ı kaydırmaz, Fever hâlâ hız avantajı vermez. Rekorlar önceki v0.3 anahtarlarında korunur. Testler ve preview sürüşleri rekor yazmaz.

## Kendi çıktımda hâlâ yeterli bulmadıklarım

Oda kumaşları ve arka plan ayrıntıları referans kadar doğal değil. Araç artık daha okunur ama stilize/prosedürel oyuncak kalitesinde; fotogerçekçi model demiyorum. Karşılaşma ailesi genişletildi ama görev/ilerleme/Relax gibi uzun vadeli derinlik sistemleri bu kalite düzeltmesinin kapsamı değildir. Sesin tablet hoparlöründe dengesi, gerçek kontrol hissi ve uzun kullanım maliyeti insan/cihaz denemesi gerektirir. Bu sürüm bu açıkları saklayarak final olarak sunulmaz.

## Kanıt

53/53 Unity testi: 28 EditMode +25 PlayMode. Yeni ve eski sahnelerde iki dakikalık motor içi tanık sürüşü; 24 saniyelik v0.4 risk sürüşünde 325.9549 m /3000 stil /zincir 10 gerçek hesaplandı. Bu otomatik rota insan oyuncu değildir. MODEL_VALIDATION sayıları inaktif havuzları da içerir; gerçek draw call/FPS ölçümü değildir.

Sahne ToyRoomRefined, kaynak varlıklar Art/Refined. Eski sahneler, sanat dosyaları ve APK'lar korunur. Kurulum ve tablet ölçümü: README ve DEVICE_TEST-v0.4.
