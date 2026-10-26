# Brake Toy — oyuncu gözüyle inceleme ve çözüm planı

Tarih: 26 Ekim 2026. İncelenen taban: PR19 + bu paketin oda/rampa düzeltmeleri.
Genel değerlendirme: oda fikri anlaşılır ve sürüş temeli var; ürünün karar dili, sunumu ve sanat kalitesi henüz tutarlı değil.
Eksik olan yalnız daha fazla nesne değil: eldeki nesnelerin farklı kararlar doğurması ve bu kararların okunabilmesi.
Mevcut kitap, defter, kalemlik, otobüs ve oyuncaklar azaltılarak çeşitlilik sağlanmamalı.

## 1. Nasıl incelendi, ne iddia edilmiyor?

- Gerçek Unity sahnesinde menü → ilk10/30/60sn → mola → görev/ayar/rekor → garaj → sonuç akışı görüntülendi.
- Garajda10 aracın üçü yakından incelendi: Rocket Pop, Thunder Volt, Galaxy Glider.
-12 ürün akışı karesi ve4 oda/rampa karesi [galeride](../Reports/Visuals/CHOICES_REVIEW_GALLERY.html).
- Otomatik tanık yanal hız sınırına uyar; insan parmağını, öğrenmeyi, heyecanı veya çocuk davranışını ölçmez.
- Sesin müzikal niteliği bu oturumda dinlenerek değerlendirilmedi. Kodda ayrı menü/yarış parçaları var.
- Yeni tablet FPS/p95/ısı veya dokunmatik kullanılabilirlik ölçümü yapılmadı.
- Aşağıdaki ürün yargıları görsel gözlem ve kod incelemesidir; kullanıcı kabulü yerine geçmez.
- Bu pakette yalnız istenen oda aydınlığı/rampa seçenekleri düzeltildi; diğer öneriler uygulanmış sayılmaz.

## 2. Bu pakette giderilen iki sorun

### Oda gece kararıyordu

Hem RoomDayCycle hem PlaygroundAtmosphere iç ışığı geceye göre azaltıyordu.
Adventure'da temel ışığın şiddeti/rengi/yönü ve ortam ışığı gündüz değerinde sabitlendi.
Dışarıda güneş, ay, yıldızlar ve gökyüzü döngüsü sürer; iki dekor lambası dışarıdaki geceyle açılır.
Lambalar ek sıcak vurgu yapar; odayı aydınlık tutabilmek için lambaların yanması beklenmez.
Kanıt: [gündüz](../Reports/Visuals/Choices-room-day.png), [dışarı gece / içerisi aydınlık](../Reports/Visuals/Choices-room-night.png).

### Rampa çıkışı her seferinde engel anlamına geliyordu

İlk geçidin boşluğu daima girişteki kaçış konumuydu; farklı konumdaki rampa hattı bu yüzden sürekli kapatılıyordu.
Seed'e bağlı açık ve alçak engelli ilk çıkışlar geri getirildi;100seedlik kontrolde iki seçenek de yaklaşık yarı payda.
Açık çıkıştaki nesne silinmez; üçüncü şeritte ilk geçitten5birim önceye taşınır. Cümlede6hazard korunur.
Bu, bütün ileriki parkurun engelsiz olacağı anlamına gelmez; sonraki yön değiştiren geçitler sürer.
14 dizilimde7rampa ailesi ×2çıkış kontrol edildi; kurmalı oyuncak örneğinde12 güçsüz rampa/bypass rotası geçti.
Bu12rota bütün ailelerin bütün riskli hava yollarının insan kabulü değildir; genel18kaçış tanığı da ayrıca korunur.
Kanıt: [açık çıkış örneği](../Reports/Visuals/Choices-ramp-open.png), [engelli çıkış örneği](../Reports/Visuals/Choices-ramp-obstacle.png).
İki görüntü farklı seed varyantlarıdır; piksel düzeyinde tek değişkenli A/B ölçümü olarak sunulmaz.

## 3. Olumlu çalışan taraflar

- Masa, yatak, raf, radyo ve oyuncaklar birlikte görünüyor; oda içinde küçük araba fikri anlaşılır.
- Yeni oyuncaklar ile masa eşyaları artık aynı akışta fiilen bulunuyor; otobüs yeniden üretiliyor.
- Aile torbası son üç aileyi tekrar etmez. Bu, düzen tekrarını tek başına çözmese de yararlı bir temel.
- Sürekli açık tek şerit yok; yanal yön değişimi ve isteğe bağlı sıçrayış kullanılabiliyor.
- Görev kartları simge ve ilerleme çubuğuna sahip; salt metin listesi olmaktan çıkmış.
- Kontrol, kayıt, güçler, havuzlar ve çarpışma için tekrarlanabilir bir test temeli var.
- Çevrimiçi bağlı değil bilgisi rekor sayfasında açık; yerel kayıt küresel yarış diye sunulmuyor.

## 4. Olumsuz bulgular ve somut çözümler

### P1 — Parkurda düzen tekrarı nesne çeşitliliğini bastırıyor

Kanıt: TabletopCourseRules.Pattern = Index%6; üç ana geçit yaklaşık22/40/58 konumunda.
Sekiz oyuncak ailesi değişse de aynı geçit omurgası tekrar ediyor. ±2 mesafe farkı tek başına yeni karar üretmiyor.
Çözüm: varlıkları koruyup kısa slalom, çapraz akış, açık ödül, dar geçit ve zamanlama dizilerini farklı uzunluklarda birleştir.
Bir düzende son3tekrarı engelle; yalnız aile değil yön/boşluk/rampa rolü için de hafıza tut.
Kabul: aynı seed tekrar oynanabilir; farklı seedlerde son20karşılaşmanın düzen izi karşılaştırılır;
güçsüz geçilebilirlik ve bütün mevcut türlerin gerçek üretimi korunur. Eğlence ayrıca kullanıcıyla değerlendirilir.

### P1 — Rampa hâlâ fazla tek tip bir bölüm sonu

Kanıt: sekiz ailenin yedisinde kitap rampası, kalanında cetvel sıçrayışı var.
İlk çıkışı açmak mevcut hatayı giderdi; fakat hemen her bölümün aynı sıçrayış diliyle bitmesini çözmedi.
Çözüm: rampaları kaldırmadan serbest sıçrayış, alçak engel üstü sıçrayış ve sonraki ödüle bağlanan sıçrayış olarak farklı roller ver.
Giriş ve inişin okunabilirliğini birlikte tasarla; sürpriz, son anda kaçınılmaz çarpışma anlamına gelmesin.
Kabul: oyuncu rampaya girmeden riskini ve kazancını anlayabilsin; rampa seçmeyen yol da anlamlı kalsın.

### P1 — HUD bilgisi oyun dünyasına karışıyor

Kanıt: [30.sn](../Reports/Visuals/Review-run-30.png); hedef yazısı pencere/perde üzerinde, büyük uyarı parkurun içinden geçiyor.
Alt güç/kornaya ayrılan alan da arabanın yakınındaki karar bölgesini daraltıyor.
Çözüm: az sayıda sabit HUD bölgesi, okunaklı arka plaka, kısa uyarı ve tek tip tehlike simgesi.
Güçler kullanılabilir/aktif/tükenmiş durumlarını açık göstermeli; sürükleme bölgesi geniş kalmalı.
Kabul: karanlık dış manzara, parlak masa ve yoğun nesne durumunda da hedef/uyarı seçilebilsin;
MatePad portresi ve720×1280referansı ayrı kontrol edilsin. Odayı gizleyecek genel kamera yakınlaştırması yapılmasın.

### P1 — Ayarlarda alt kontrol alanı sorunlu görünüyor

Kanıt: [ayarlar karesi](../Reports/Visuals/Review-page-settings.png); ses, grafik ve performans düğmeleri alt alanda sıkışıyor, geri düğmesi görünmüyor.
Bu görsel bulgu: dokunmada hangi nesnenin raycast'i tuttuğu ve çözünürlük etkisi bu turda ölçülmedi.
Çözüm: genel/ses/görüntü bölümlerini düzenle; geri dönüşü sabit, ayrılmış bir alanda tut.
Performans testini oyuncunun sıradan ayarlarından ayrı bir tanılama bölümüne al.
Kabul: iki hedef portre oranında geri düğmesi görünür/tıklanabilir; büyük yazıda hiçbir kontrol üst üste binmez.

### P1 — Sonuç ekranı yaptığı şeyi her zaman doğru anlatmıyor

Kanıt: [sonuç](../Reports/Visuals/Review-result.png);0sıçrama/0hava mesafesine rağmen “Hava bölümünde TP×2 uygulandı” yazıyor.
Başlıkta mevcut ürün yerine PLAYGROUND kalmış; ANA MENÜ ve GERİ aynı ekranda gereksiz ikilik oluşturuyor.
Bu, yanlış ödeme yapıldığının kanıtı değil; açıklama koşulsuz yazılıyor.
Çözüm: uçuş bonusunu yalnız gerçekten oluştuğunda göster; mesafe/stil/bonus/TP dökümünü anlaşılır yap.
Tekrar dene, aynı rota ve ana menü seçeneklerini net hiyerarşiye sok; eski sürüm adlarını temizle.
Kabul: sıfır uçuşta bonus iddiası yok; ekrandaki TP hesabı kariyer kaydıyla eşleşiyor.

### P2 — İncelenen garaj araçları ortak gövdeyi fazla belli ediyor

Kanıt: [Rocket Pop](../Reports/Visuals/Review-page-garage-0.png), [Thunder Volt](../Reports/Visuals/Review-page-garage-1.png), [Galaxy Glider](../Reports/Visuals/Review-page-garage-2.png).
Üst aksesuarlar değişiyor; ana gövde/teker düzeni ve oranlar çok benzer. Parlak kubbe tek başına yeni araç kimliği oluşturmuyor.
Çözüm: model agent'ına farklı gövde oranı, kabin, aks mesafesi ve karakteri olan araç sınıfları için açık devir ver.
Önce az sayıda aracı hedef kaliteye getir; renk kapalı silüette farkını ve oyun kamerasındaki okunabilirliğini kabul et.
Kabul: yalnız boya/üst eklenti değişikliği yeterli sayılmaz; pivot, teker dönüşü ve hitbox sözleşmesi korunur.
Bu üç örnek üzerinden bütün10araca aynı kusur hükmü verilmedi; tam silüet dizisi sonraki sanat kabulüne eklenmeli.

### P2 — Görseller oyuncak dünyasını anlatıyor, hedef sanat kalitesine ulaşmıyor

Kanıt: [10.sn](../Reports/Visuals/Review-run-10.png), [30.sn](../Reports/Visuals/Review-run-30.png).
Masa geniş ve tek renk yüzey olarak baskın; birçok model blok/ilkel form hissini koruyor.
Ölçek/kenar yuvarlatması, plastik/kumaş/kâğıt farkı ve temas gölgeleri aynı kalite düzeyinde değil.
Çözüm: referans bir araba + kitap/kalemlik + masa köşesini hedef kalite örneği yap; sonra diğer varlıklara yay.
Materyal/normal/roughness ve oda ışığını bu örnekte birlikte kabul et. Unity/URP tek başına kaliteli model üretmez.
Kabul: gerçek oyun kamerasından aynı ışıkta önce/sonra; modelde kopuk parça, yüzey çakışması ve hitbox sapması yok.
Mevcut nesne çeşitliliğini sanat düzeltmesi bahanesiyle azaltma; eski görünüm geri alınabilir kalsın.

### P2 — İki hedef sistemi yeni oyuncu için ayrışmıyor

Kanıt: HUD'da60m/3yıldız/6yakın geçiş tur hedefleri; [görevlerde](../Reports/Visuals/Review-page-missions.png)150m/3yakın geçiş/500stil gibi farklı kartlar.
Bir sistemin diğerini bozduğu gösterilmedi; isimleri ve bağlamı yeni oyuncuya yeterince açıklanmıyor.
Çözüm: “Bu turun hedefleri” ve “Kariyer görevleri” ayrımı; kısa tamamlanma/ödül bildirimi ve sonraki hedef bağlantısı.
Kabul: oyuncu bir hedefi tamamlayınca ödülün nereden geldiğini ve görevin turda mı kariyerde mi ilerlediğini anlayabilsin.

### P2 — Menü çok sayıda seçeneği aynı anda sunuyor

Kanıt: [ana menü](../Reports/Visuals/Review-menu.png); Arcade, Relax, günlük, garaj, görev, sıralama, ayar, güç hakları.
Çözüm: başlatma eylemi baskın kalsın; ikincil seçenekler gruplanmalı. İlk sürüşte kısa etkileşimli öğretim yeterli olsun.
“Sıralama” yerel rekorlar olduğu için girişte de doğru adlandırılmalı; ileride online varsa ayrı durum olarak açılmalı.
Kabul: öğretim kalıcı güç harcamayı zorunlu kılmaz; ilk tekrar denemede gereksiz menü turu yaratmaz.

### P2 — Güvenli ilerlemek ile ustalık hedefi arasındaki bağ zayıf kalabilir

Kanıt: bu incelemenin özellikle risksiz oynayan tanığı60sn sonunda1579m,0stil ve0sıçrayış yapabiliyor.
Bu bir insan davranışı ölçümü veya oyunun kesinlikle kolay olduğunun kanıtı değildir.
Çözüm: risk/ödülün yıldız, hava bonusu ve ustalık zincirindeki kazancını daha görünür göster;
zorunlu güç tüketimiyle değil, okunabilir fırsatlar ve farklı geçişlerle risk almaya teşvik et.
Kabul: risksiz, ödül odaklı ve hata yapan insan turları ayrı izlenir; ekonomi gerçek tur süreleriyle ölçülür.

### P2 — Otomatik testler ürün kabulünü yeterince temsil etmiyordu

Geçilebilirlik tanığının geçmesi, rampanın iyi bir seçenek olduğu veya oyunun eğlenceli olduğu anlamına gelmiyor.
Rampa hattının sürekli engellenmesi gibi hatalar, yalnız kaçış yolunu izleyen testlerden kaçabiliyor.
Çözüm: her değişiklikte korunacak davranış matrisi: nesne türü/sıklığı, açık-riskli rampa, oda ışığı, kontrol, kayıt ve HUD.
Teknik test yanına karşılaştırmalı gerçek görüntü, kısa insan denemesi ve geri alma yolu ekle.
Kabul: “testler geçti” ifadesi sanat/oynanış kabulü veya mağaza finali yerine kullanılmaz.

## 5. Bundan sonraki küçük iş paketleri

| Sıra | Paket | Somut çıktı | Tamamlanma ölçütü |
|---|---|---|---|
|1|HUD ve menü sağlamlaştırma|Ayar geri dönüşü, okunaklı uyarılar, doğru sonuç dökümü|Hedef portrelerde tıklanabilirlik ve sıfır/pozitif bonus doğrulaması|
|2|Karşılaşma dilbilgisi|Sabit altılı döngü yerine hafızalı, farklı uzunluklu birleşimler|Tür kaybı olmadan seed çeşitliliği ve risk/bypass testleri|
|3|Sanat/model örneği|Bir araç ve masa köşesinde kabul edilmiş kalite|Gerçek oyun kamerasında kullanıcı kabulü ve model handoff|
|4|Gerçek tablet kabulü|Dokunma, ilk dakika, FPS/p95/ısı, arka plandan dönüş|Ölçüm raporu ve kullanıcı gözlemi|

Bu tablo öneridir; bu teslimde bu dört paketin hepsi yapılmadı. K2 devam eder, son K6 aşamasına geçilmedi.
Online, gerçek ödeme, yeni müzik ve mağaza yayını bu düzeltmeye sessizce eklenmedi.

## 6. Teslim kanıtı

91 EditMode +GPU88 PlayMode =179/179. Oda ışığı,14rampa dizilimi,12isteğe bağlı rampa/bypass rotası ve önceki regresyonlar geçti.
Kaynak/kanıt hash'leri: [doğrulama raporu](../Reports/ROOM_RAMP_REVIEW_VALIDATION.json).
Yeni APK/tablet paketi üretilmedi. Görseller ve kaynak güncellemesi PC/Unity içindir.
