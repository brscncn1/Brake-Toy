# Brake Toy — oyuncu deneyimi ve mekanik sözleşmesi

Taban: v0.9 Adventure, 24 Ekim 2026. Sayılar mevcut koddur; denge önerileri ayrı görevle değiştirilir.
“Hedef” etiketleri kabul ölçütüdür; tamamlandıkları anlamına gelmez.

## 1. Konsept, dünya ve temel döngü

- Sparky'nin motor düğmesi takılı kalmıştır; oyuncak araba fren yapamadan ileri gider.
- Oyuncu çocuk odasındaki masada, dev görünen nesne/oyuncakların arasından sıyrılır.
- Oda, pencere, raf, yatak ve oyuncaklar dünyayı anlatır; yalnız yakın plan yol görüntüsü hedefi karşılamaz.
- Döngü: modu seç → sürükle/geçişi oku → değişen boşluk/rampa → skor/TP → sonuç → garaj/görev → tekrar.
- Mesafe ve stil farklı başarı eksenleridir; araçlar gameplay avantajı için satılmaz.
- Hedef: çocuk için açıklamasız anlaşılır giriş, deneyimli oyuncu için ustalık ve tekrar oynama isteği.

## 2. Kontrol ve durumlar

- Tek parmakla göreli yatay sürükleme; mouse eşdeğeri editörde desteklenir.
- Parmağın konumuna ışınlanma yok; hedef ve en yüksek yatay hız üzerinden takip vardır.
- Üç üretim şeridi bulunur, fakat direksiyon serbesttir; merkezler -2.25/0/+2.25.
- Normal zeminde bırakma hedefi mevcut konuma getirir; kaygan kartta kısa atalet sürer.
- Güç/korna düğmesine dokunmak mevcut sürüş parmağını başka parmakla değiştirmemelidir.
- Ready başlangıç menüsü; Running sürüş; Paused mola; Crashed tur sonucu.
- Odak kaybı/arka plan pause yapar; geri dönüşte otomatik devam olmaz.
- Retry sahne yüklemek yerine dünyayı/havuzları/tur sayaçlarını sıfırlar; kariyeri sıfırlamaz.

## 3. Modlar ve hız

- Arcade: tek can; hız `14 +16*(1-exp(-t/14))`, üst sınır 30 oyun birimi/saniye.
- Yaklaşık 10. saniyede 22.2,30. saniyede 28.1; bunlar fiziksel metre veya km/saat değildir.
- Relax: üç can; aktif hız `9 +5*(1-exp(-t/30))`, üst sınır 14.
- Eski CareerRules içindeki Relax 7→10, Adventure'ın aktif hız kuralı değildir.
- Daily: yerel günlük antrenman akışı vardır; canlı doğrulanmış günlük rekabet etkin değildir.
- Aynı rota seçeneği seed'i korur; normal yeni tur değişken seed kullanabilir.
- Güç ve bant, gerçek ilerlemeyi çarpar; HUD vitesi temel hız eğrisine dayanır.
- Hedef: erken ilgi ve okunabilir zorluk; sürekli daha fazla hız tek çözüm değildir.

## 4. Parkur üretimi ve adalet

- Sekizli torbada her aile bir kez; son üç ailenin hemen tekrarı önlenir.
- DenseCourse bütün uygun ailelerin kitap rampasını korur; cetvel kendi sıçrayışını kullanır.
- Önceki yer yıldızı/rampa bitiriş karşılaştırması yalnız DenseCourse=false profilindedir.
- Seed aile/yan yol/varyant/faz seçimlerini tekrar oynatılabilir kılar.
- İlk karşılaşma 24 birim; Arcade 78/81/84, Relax 86/89/92 birim aralıkla üçlü geçit dizileri gelir.
- K2.1 ölçümünde 10/30/60 sn Arcade 4/11/22, Relax 2/7/15 cümle referansı geçilir.
  Bu adetler insan karar sayısı değildir; ayrıntı Docs/OPENING_PACE-2026-10-25.md.
- Sürekli açık yeşil yol kaldırıldı; ana oyuncağın çıkışı sonraki geçit boyunca açık kalmaz.
- Rampa ilk çıkışı seed'e göre açık veya alçak engelli olur; açık varyantta nesne silinmeden başka hatta taşınır.
- Açık ilk çıkış bütün sonraki parkurun engelsiz olacağı anlamına gelmez; sonraki geçit kararları sürer.
- 22/40/58±2 birimdeki çift engeller kısa slalom, dar merkez, S ve yön değiştiren çıkış üretir.
- Kitap/spiral defter/kalemlik her cümlede vardır; otobüs altı düzenin ikisinde yatay hareket eder.
- Hareketli otobüs ve oyuncaklarla yeni diziler birleşir; ana sekiz oyuncak ailesi korunur.
- Geçitler komşu boşluğa yönlendirir; bir karede tam genişlik kaçışı beklenmez.
- Yaklaşma yıldızları → ana oyuncak kararı → rampa/atlayış/çıkış ödülü bağlanır.
- Tünel çıkışına kurmalı oyuncak, yüzey bölümüne misket eklenebilir.
- Torba çeşitlilik sağlar; bütün kombinasyonların eşit eğlenceli olduğu henüz kullanıcı kabulü değildir.
- Hedef: görünür uyarı, okunabilir hareket, reachable kaçış ve yeterli iniş alanı.
- Güç stokunun sıfır olması oyuncuyu geçilemez bölüme mahkûm etmemeli.

## 5. Sekiz Adventure ailesi

### 5.1 Karton tünel

- Üç kemerli makette iki kullanılabilir çıkış; üçüncü çıkış kapalıdır.
- Yeşil çıkış güvenli, altın çıkış sonraki risk/ödül hattıdır.
- Duvar/tavan analitik temasa dahildir; yüksek tavan mevcut uçuş ve jetpack'e açıklık bırakır.
- Hedef: girişten önce çıkış anlaşılır; önceki rampadan havada gelen arabaya kaçınılmaz tavan engeli yok.

### 5.2 Kurmalı kurbağa / ördek

- Çift varyant: zıplayan kurbağa; tek varyant: yürüyen/boblayan ördek.
- Yanal hareket ve kurma anahtarı görünür; kurbağa yeterince yükseldiğinde altından geçiş okunabilir olmalı.
- Model ayak/gövde/anahtar bağlantıları korunmalı; yalnız kökü yukarı taşımak kaliteli animasyon kabulü değildir.

### 5.3 Oyuncak vinç

- Sabit kule/kol, sabit uzunluklu askı ve sallanan yumuşak oyuncak yükü.
- Yük masada takip eden gölge verir; salınım ve yükselme geçiş aralığı oluşturur.
- Hedef: çarpışma yükün görünen yerine uyar; kule, halat ve yük birbirinden ayrışmaz.

### 5.4 Oyuncak tren

- Riskli yarıda enine hareket eden lokomotif/vagon grubu ve uyarı lambası/raylar.
- Güvenli orta şerit gerektiğinde kısa lokomotif varyantı kullanılır; yeşil yol kesilmez.
- Birlikte hareket eden vagonlar arası bağlantı ve geçiş zamanlaması görünür olmalı.

### 5.5 Misket oluğu

- Görünür oluk; varyanta göre 1–3 misket ve daha hızlı, yanal eğrili ilerleme.
- Düz/çapraz/tek kavis/çift kavis profilleri; aynı grupta farklı hareketli1–3 misket bulunabilir.
- Büyük toplar da bağımsız ilerler; ForwardFactor1.20–1.45, büyük top yanal sınırı±0.55'tir.
- Küçük misket yanal sınırı±0.48; hız varyanta bağlıdır, büyük toplarda ilerleme kademesi de vardır.
- Görsel dönüş gerçek masa üstü yol/radyus kadardır; sabit top sırf masa kaydığı için dönmez.
- Başlangıç mesafesi bu hızla ölçeklenir; gelecek karşılaşma erkenden önceki kaçışı kapatmaz.
- Sayı/hız şu anda varyanttan seçilir; sürekli zamana bağlı ek zorluk kademesi olarak sunma.

### 5.6 Yapışkan bant / parlak kart

- Pembe bant yerde ileri hareketi 0.62 ile çarpar; bant üzerinde motor gücü değil dünya ilerlemesi etkilenir.
- Parlak kart direksiyonda sınırlı yanal atalet oluşturur.
- Havadaki araca yüzey etkisi verilmez; retry etkiyi sıfırlar.
- Hedef: yüzey görünüşünden etkisi anlaşılır; gecikmiş dokunma hatasıyla karıştırılmaz.

### 5.7 Cetvel tahterevalli

- Giriş yeşil okla gösterilir; eğimli cetvel teker temas alanıyla aracı destekler.
- Yaklaşık 0.59 yüksekliğinden 1.35 saniyelik uçuş; mevcut havada TP hesabına katılır.
- Merkez/iki kenar/yan giriş-çıkışta yüzeyin içinden geçme ve ani düşme olmamalı.

### 5.8 Oyuncak garaj

- Kalkan/inen kapı, uyarı ışıkları ve farlı iki oyuncak araç.
- Araçlar kapının arkasından öne çıkar; görünen gövdeler ve teması aynı hareket takip eder.
- Hedef: far/kapı önceden bilgi verir; görsel olarak açık kapı görünmez engel olmaz.

## 6. Kitap rampaları ve uçuş

- Kitap rampası yalnız orta şeritte değildir; karşılaşmanın risk yoluna yerleşir.
- Yaklaşma oku rampayı sıradan kitap engelinden ayırır.
- Teker/gövde temas alanı yüzeye çıkar; aracın arkası uçtan ayrılınca kalkış olur.
- Standart kitap uçuşu 1.65 saniye; yandan çıkışta kısa düşüş, havada yatay yönlendirme devam eder.
- Havada 2x, bütün turu veya görev/başarım ödüllerini ikiye katlamak değildir.
- Koddaki ek ödeme hava mesafesi ve havada kazanılan stil üzerinden tur sonunda hesaplanır.
- Hedef: destek/kalkış/iniş tutarlı; görüntü ve temas farklı yüksekliklerde davranmaz.

## 7. Güçler ve haklar

- Yavaşlatma:3 saniye, dünya ilerlemesi yaklaşık 0.32; oyuncunun yatay kontrolü korunur.
- Roket:3 saniye, ilerleme yaklaşık 1.85; yumuşak giriş/çıkış ve çarpışma koruması.
- Jetpack:5 saniye, yaklaşık 2.25 yüksekliğe yumuşak yükselme/iniş.
- Son 0.45 saniyelik jet alçalması da korumalıdır; iniş alanı alçalmaya geçişte temizlenir.
- Roket/jetpack sonunda 0.85 saniye iniş koruması ve yakındaki tehlikeleri temizleme.
- Aynı anda tek güç; mola süreyi durdurur. Sonuç ekranı aktif gücü/uçuşu bitirir.
- Başlangıçta ilk kariyere bir kez her güçten 3 hak; kullanımdan önce kalıcı tüketim yapılır.
- Menüden kullanım başına yavaşlatma 350, roket 500, jetpack 650 TP yenileme.
- Kayıt başarısızsa tüketim/yenileme geri alınır; retry hak vermez.
- Ücretli coin veya gerçek para satın alımı mevcut değildir; gelecekteki ürün kararıdır.

## 8. Stil, zincir ve TP

- Yakın geçiş, temas olmadan dar aralıktan geçmektir; havada otomatik yakın geçiş sayılmaz.
- Her yakın geçiş 100 ×zincir çarpanı:1/2/3/5; eşikler 3/5/8 yakın geçiş.
- Zincir zaman penceresi 6 saniye; çarpışma veya süre bitmesi zinciri sıfırlar.
- Fever mevcut stil zincirinde 8+ koşuludur; bütün gelecekteki Fever fikirlerinin uygulandığı anlamına gelmez.
- Risk yıldızı 75 stil; yüksekteki yıldız için araç yüksekliği de hesaba katılır.
- Ustalık: yakın geçiş, yıldız, kalkış, iniş türlerini 7 saniyelik pencerede birleştirir.
- Ustalık 2/3/4 adımında 50 ×adım bonusu; aynı hareketi tekrar yapmak farklı tür yerine geçmez.
- Temel TP: floor(mesafe/10) +floor(stil/100) +hava mesafesi/stili ek payı; taban en çok 2500.
- Görev ve tek seferlik başarım ödülleri taban ödüle ayrıca eklenir.
- Ödeme aynı runId için bir kezdir; TP, puan göstergesi ve rekor birbirinden farklı alanlardır.

## 9. İlerleme, görev, garaj ve sonuç

- Tur içi üç kısa hedef:60 mesafe,3 risk yıldızı,6 yakın geçiş; sırayla ilerler.
- Kariyerde aktif görev kartları ve 15 başarım vardır; görevle tur içi hedef aynı sistem değildir.
- On araç/temel siluet, görsel kozmetik, korna/iz seçimi ve 3D garaj bulunur.
- Araç fiyatları 0/200/500/900/1500/2400/3600/5200/7200/10000 TP; aynı gameplay çarpışma ölçüsü.
- Ayrıntılı araç modelleri sonraki sanat çalışmasıdır; farklı renkleri tek başına çeşitlilik sayma.
- Güç kullanılan tur yardımlı işaretlenir; kişisel ghost gizlenir ve yeni ghost yazılmaz.
- Yardımlı/yardımsız skorun tamamen ayrı sıralanması uygulanmış değildir; K5 kararı açık.
- Adventure kariyeri ayrı dosyaya geçer; TP/araç/güç stokları korunurken eski parkur rekorları aktarılmaz.

## 10. Oda, ses ve oyuncu açısından kabul

- Gün/gece döngüsü güneş/ay/yıldız/kuşlarla odayı canlandırır; oynanış kuralları gün saatine bağlanmaz.
- Adventure içi gece de gündüz kadar aydınlık kalır; kararan bölüm yalnız dış manzaradır.
- Gece radyonun yanındaki raf ve yatak başlığındaki iki loş lamba yanar; gündüz ışıkları söner.
- Masa bölgeleri geri alınabilir; kenar dekorları 64 birim ortak fazda defter/kalemlik boşluğu bırakır.
- Radyo sol raf üzerinde statiktir; domino aktif sürümde yoktur.
- Menü ve oyun içi müzik ayrıdır; motor/efekt/müzik slider'ları ayrı kanalları yönetir.
- Müzik şimdilik korunur; kusursuz döngü ve öznel ses kalitesi kullanıcı kabulüyle değerlendirilir.
- Hedef: okunabilir buton/uyarı, birbirinden ayrılan tehlike/dekor, görünür rampa ve anlaşılır ödül.
- Tam erişilebilirlik/çok dil/screen-reader kabulü yoktur; yapılmış gibi belgeye ekleme.
- Sanat/tempo kabulü otomatik test başarısından çıkmaz; cihaz ve kullanıcı kanıtı ayrıca gerekir.

## 11. Öncelikli doğrulama adayları

- Relax aynı çarpışmada tek hak tüketir; yakın yeni oyuncak/zemin/rampa temizlenir, uzak parkur korunur.
- K1.5: güç bitişi, iki taraflı rampa/cetvel sınırı ve yan çıkış testleri geçti; cihaz kabulü ayrıdır.
- Eski kariyer rekorlarının Adventure sonuçlarına karışması ayrı profil ve doğru anahtarla giderildi.
- Bu liste inceleme adaylarıdır; yeni test yapılmadan kesin hata veya giderildi diye işaretleme.
