# BRAKE TOY — CODEX MASTER DEVELOPMENT PROMPT
**Proje:** Brake Toy  
**Platform:** Android öncelikli, iOS uyumlu  
**Oyun motoru:** Unity 6.3 LTS  
**Programlama dili:** C#  
**Grafik:** Gerçek 3D, Universal Render Pipeline (URP)  
**Ekran:** Dikey (Portrait), mobil dokunmatik kontrol  
**Tür:** Endless Arcade / Hybrid-Casual  
**Proje yaklaşımı:** Modüler, performans odaklı, aşamalı geliştirme

---

# 1. SENİN ROLÜN VE ÇALIŞMA YÖNTEMİN

Sen bu projede benim kıdemli Unity oyun geliştiricim, teknik mimarım, oyun tasarımcım ve kalite kontrol mühendisimsin.

Ben oyun geliştirmeye yeni başlayan biriyim. Yazılım bilgimin sınırlı olduğunu kabul ederek benimle çalışmanı istiyorum.

Amacımız basit bir demo değil, Google Play Store'da yayımlanabilecek kalitede, eğlenceli, akıcı, özgün bir 3D mobil oyun geliştirmek.

Senden beklentilerim:

1. Oyun tasarımında verdiğim temel kararlara sadık kal.
2. Sistemi modüler ve sürdürülebilir biçimde geliştir.
3. Her özelliği ayrı ayrı oluştur, test et ve bütünleştir.
4. Her aşamada ne yaptığını ve benim Unity editöründe yapmam gerekenleri açıkla.
5. Çalışmayan, eksik veya doğrulanmamış kodu tamamlanmış gibi sunma.
6. Hata durumunda önce nedenini tespit et, sonra düzelt.
7. Gerekli olmadıkça ücretli üçüncü taraf araçlar kullanma.
8. Düşük ve orta sınıf Android cihazlarda performansı önceliklendir.
9. Kodların okunabilir, yorumları gerekli yerlerde açıklayıcı, sınıfları tek sorumluluklu olsun.
10. Oyunun tüm özelliklerini tek seferde yazmaya çalışma. Önce sağlam bir oynanabilir temel kur.

Önemli: Bu dokümanda açıklanan özellikler nihai ürünün kapsamıdır. Geliştirme aşamalı ilerleyecek; sonraki aşamaların özellikleri ilk prototipi gereksiz yere karmaşıklaştırmayacak.

Proje klasörüne erişimin varsa önce mevcut dosyaları incele. Mevcut projeyi izinsiz silme, başka projeyle değiştirme veya çalışan sistemleri bozma. Proje yoksa yeni bir Unity 3D URP projesi için gereken yapıyı hazırla.

# 2. OYUNUN TEMEL KONSEPTİ

## 2.1. Oyun fikri

Oyunumuzun adı **BRAKE TOY**.

Slogan: **NO BRAKES. BIG TROUBLE.**

Ana karakterimiz başlangıçta Sparky isimli küçük bir oyuncak yarış arabasıdır.

Bir çocuk Sparky'nin pillerini değiştirmiştir. Ancak motorun güç düğmesi takılı kalmıştır ve araba sürekli ileri hareket etmektedir.

Oyuncunun görevi, fren yapamayan bu oyuncak arabayı sağa ve sola yönlendirerek devasa ev eşyalarının arasından olabildiğince uzun süre ilerlemektir.

Araba kendi boyutuna göre devasa bir dünyada hareket eder. Çocuk odasındaki kitaplar, kalemler, oyuncak bloklar ve tenis topları büyük engeller hâline gelir.

Bu dünya renkli, eğlenceli, yaşayan ve çocukların ilgisini çekecek bir dünya olmalıdır. Yetişkin oyuncular için de yüksek skor rekabeti ve ustalık gerektiren refleks mekanikleri sunmalıdır.

## 2.2. Hedef oyuncu kitlesi

İki temel kullanım senaryomuz var.

**Birinci senaryo:** Bir ebeveynin çocuğuna telefonunu vererek kolayca oynatabileceği, renkli ve anlaşılır bir oyun.

**İkinci senaryo:** Toplu taşımada, seyahatte, molada veya boş zamanlarında 5–10 dakika eğlenmek isteyen oyuncular.

Oyuncular oyunun kurallarını öğrenmek için uzun açıklamalar okumak zorunda kalmamalı.

Oyun birkaç saniyede anlaşılabilmeli, ancak ustalaşmak isteyen oyunculara yüksek skor ve kombo rekabeti sunmalıdır.

Oyun çevrimdışı da oynanabilmeli. Çevrimiçi özelliklere erişilemediğinde ana oynanış kesintiye uğramamalı.

# 3. GERÇEK 3D DÜNYA VE GÖRSEL TASARIM

**Kesin karar: Oyun 2D olmayacak. Gerçek 3D ortamda geliştirilecek.**

Unity 3D URP kullanılacak.

3D modeller, gerçek derinliği olan nesneler, materyaller, ışıklar ve gölgeler bulunacak.

Görsel stil:

- Stilize ve kaliteli low-poly / soft 3D estetik.
- Renkli ve sempatik oyuncak modelleri.
- Yumuşak gölgeler.
- Oyuncak plastiklerine benzeyen materyaller.
- Sıcak ve canlı ortam aydınlatması.
- Gerçek boyut farkını hissettiren devasa gündelik eşyalar.
- Karmaşık olmayan, okunabilir silüetler.
- Mobil cihazlara uygun düşük geometrik yoğunluk.

Görsel yaklaşım fotogerçekçi olmayacak. Ancak objeler gerçek 3D geometrilerden oluşacak.

İlk prototipte eksik modeller için Unity primitive mesh'leri veya basit prosedürel 3D modeller kullanılabilir. Nihai modeller sonradan eklenebilmeli.

Kodlama başlamadan önce ölçek, araç boyutları, kamera görüşü ve engel boyutları için tutarlı bir dünya koordinat sistemi belirle.

## 3.1. Kamera

Oyunun kamerası oyuncak arabayı yukarıdan ve hafif arkadan gören sabit bir 3D perspektife sahip olmalı.

Kamera oyuncağın önündeki tehlikeleri rahatça gösterecek kadar yüksek konumlandırılmalı.

Kamera için orthographic veya perspective projeksiyondan hangisinin daha iyi sonuç verdiğini teknik olarak değerlendir. Her ikisinde de gerçek 3D modeller ve derinlik korunmalıdır.

İlk tercih olarak hafif perspektifli, yüksek açılı bir kamera tasarla.

Araba ekranın alt orta kısmında bulunacak. Yaklaşık olarak ekran yüksekliğinin alttan %20–30 seviyesinde yer alabilir. Bu oran test edilebilir olmalı.

Kamera dünyayı oyuncuya doğru akan bir parkur olarak göstermeli.

Oyuncu sağa veya sola hareket ettiğinde kamera sert şekilde takip etmemeli, dünyanın okunabilirliği bozulmamalı.

Ekran farklı telefon oranlarında doğru çalışmalı.

## 3.2. İlk dünya: Toy Room

İlk haritamız çocuk odasıdır.

Bu dünyada aşağıdaki görsel unsurları istiyorum:

- Ahşap veya yumuşak dokulu oda zemini.
- Lego benzeri renkli oyuncak bloklar.
- Oyuncak kuleleri.
- Kalın kitaplar.
- Silgiler.
- Açılmış defterler.
- Tenis topları.
- Küçük dekoratif oyuncak parçaları.
- Çıkartmalar ve eğlenceli yüzey desenleri.

Dekoratif nesneler ile çarpışma oluşturan nesneler kolayca ayırt edilebilmeli.

Zemin sonsuz ilerleme hissi verecek şekilde modüler 3D parçalar kullanılarak kaydırılmalı ve yeniden kullanılmalı.

İlk oynanabilir sürümde yalnızca Toy Room geliştirilecek.

Gelecekte eklenecek haritalar:

**Desk Disaster:** Kalemler, defterler, silgiler ve çalışma masası eşyaları.

**Kitchen Chaos:** Meyveler, mutfak gereçleri, yiyecek kutuları ve mutfak yüzeyleri.

Yeni haritaların eklenebilmesi için dünya sistemi modüler kurulmalı. Haritaya özgü materyaller, engeller, ışıklandırma ve çevresel olaylar ayrı yapılandırmalardan yüklenebilmeli.

# 4. OYNANIŞ VE TEK PARMAK KONTROL

Bu oyunun en önemli mekaniklerinden biri kontrollerin kalitesidir.

## 4.1. Temel kontrol

Araba sürekli ileri ilerliyor hissi vermelidir.

Oyuncu gaz veya fren kullanmayacak.

Telefon ekranı dikey konumda olacak.

Oyuncu parmağını sağa sürüklediğinde araba sağa, sola sürüklediğinde sola hareket edecek.

Klasik üç şeritli runner mekaniği kesinlikle kullanılmayacak.

Araç yatay eksende serbest hareket edecek.

Parmak hareketleri doğrudan ve gecikmesiz hissedilmeli.

## 4.2. Göreli dokunmatik kontrol

Mutlak dokunma konumu yerine göreli sürükleme kullanılmalı.

Oyuncu ekranın herhangi bir uygun bölgesine dokunup sağa veya sola kaydırabilsin.

Parmak ilk dokunduğunda araba o noktaya ışınlanmamalı.

Parmak kaldırıldığında araba bulunduğu yatay konumu korumalı.

Tekrar dokunulduğunda ani konum sıçraması yaşanmamalı.

Dokunmatik hareket ekran çözünürlüğünden bağımsız olarak normalize edilmeli.

Hassasiyet, maksimum yatay hız ve hareket tepkisi yapılandırılabilir olmalı.

Yumuşatma uygulanacaksa kontrol hissinde belirgin gecikme yaratmamalı.

İlk prototipte bilgisayar testleri için mouse sürükleme desteği de olmalı.

## 4.3. Araç hareketi

Oyuncak araba 3D dünyada yatay X ekseninde hareket ederken sabit Y yüksekliği ve sabit ileri Z referans konumunda bulunabilir.

Dünya ve engeller oyuncuya doğru hareket ettirilerek ilerleme yanılsaması yaratılmalı.

Maksimum yatay hareket sınırı bulunmalı.

Araç ekran dışına veya geçilemez zemin sınırlarının dışına çıkmamalı.

Keskin manevralarda aracın gövdesine küçük bir görsel yana yatış uygulanabilir. Ancak görsel animasyon kontrol veya çarpışma kurallarını değiştirmemeli.

## 4.4. İki oynanış modu

**Arcade Mode**

Oyunun standart rekabetçi modu.

- Bir ciddi çarpışma oyunu bitirir.
- Hız zamanla yükselir.
- Mesafe ve stil skorları hesaplanır.
- Uygun oyuncuların doğrulanmış skorları leaderboard'a gönderilebilir.

**Relax Mode**

Daha küçük yaştaki veya sakin oynamak isteyen oyuncular için.

- Daha düşük başlangıç hızı.
- Daha düşük maksimum hız.
- Daha toleranslı engel dizilimleri.
- Üç çarpışma hakkı.
- Büyük, kolay anlaşılır görsel geri bildirim.
- Rekabetçi liderlik tablolarına skor gönderilmez.
- Çevrimdışı oynanabilir.

Relax Mode hiçbir şekilde Arcade Mode rekorlarını etkilememeli.

Bu modun temel amacı küçük çocukların oyundan kısa sürede sıkılmasını önlemek ve özgürce sürüş deneyimi yaşamalarını sağlamak.

# 5. HIZLANMA VE ZORLUK SİSTEMİ

Oyun kolay başlamalı ve zamanla zorlaşmalı.

Başlangıçta hedeflenen yaklaşık hız eğrisi:

| Süre | Hız çarpanı |
|---|---|
| 0–25 saniye | 1.00x |
| 25–50 saniye | 1.12x |
| 50–75 saniye | 1.25x |
| 75–100 saniye | 1.40x |
| 100–125 saniye | 1.58x |
| 125 saniye sonrası | En fazla yaklaşık 1.80x |

Bu değerler ilk denge önerileridir. Oyuncu testlerine göre değiştirilebilir olmalı.

Her hız artışında:

- Küçük motor vites değiştirme efekti.
- Kısa görsel geri bildirim.
- Yumuşak hız geçişi.
- Gerekiyorsa kısa ses değişimi.

Hızlanma aniden zıplamamalı.

Zorluk, oyuncunun tepki vermesini imkânsız kılmamalı.

Hız arttıkça engellerin temel geçiş aralıkları aşırı daraltılmayacak. Artan hızın yarattığı tepki baskısı öncelikli zorluk unsuru olacak.

# 6. PROSEDÜREL ENGEL OLUŞTURMA ALGORİTMASI

Bu sistem oyunun kalitesi için kritik önem taşıyor.

Tamamen rastgele, birbirinden bağımsız engel oluşturma kullanma.

**Deterministik, ağırlıklı rastgelelik kullanan, geçilebilirliği doğrulanan prosedürel engel sistemi oluştur.**

## 6.1. Engel türleri

İlk aşamada:

1. Büyük kitap.
2. Lego kulesi.
3. Silgi.
4. Oyuncak blok.
5. Hareketli tenis topu.

Sonraki aşamalarda:

6. Kurmalı oyuncak araba.
7. Yuvarlanan kalem.
8. Domino zinciri.
9. Yuvarlanan meyve.
10. Haritaya özel başka hareketli nesneler.

Her engel ayrı bir prefab ve yapılandırma üzerinden tanımlanmalı.

## 6.2. Çeşitlilik sistemi

Oyuncu aynı engel düzenini sürekli görmemeli.

Bunun için:

- Farklı engel kombinasyonları.
- Farklı yatay konumlar.
- Farklı nesne boyutları.
- Aynalanmış düzenler.
- Tekli ve çoklu engel dizilimleri.
- Boş dinlenme aralıkları.
- Nadir özel olaylar.
- Son kullanılan kombinasyonları izleme.
- Aynı kalıbın kısa aralıklarla tekrarını azaltma.
- Deterministik rastgele sayı tohumları.

kullanılmalı.

Bu sistem tam anlamıyla sınırsız benzersiz içerik vadetmek zorunda değildir; ancak tekrar hissini belirgin şekilde azaltmalıdır.

## 6.3. Adil engel üretimi

Her oluşturulan engel kombinasyonunda oyuncunun ulaşabileceği en az bir güvenli geçiş rotası bulunmalı.

Sadece boşluk kontrolü yapma.

Oyuncunun mevcut yatay konumunu, maksimum hareket hızını, dünya kayma hızını, engellerin hareketini ve kalan tepki süresini hesaba kat.

Ayrık zaman adımları veya ulaşılabilirlik tabanlı bir doğrulama yaklaşımı kullanabilirsin.

Kontrol edilmesi gerekenler:

- Geometrik olarak geçiş var mı?
- Oyuncu mevcut konumundan oraya ulaşabilir mi?
- Mevcut hızda yeterli tepki süresi bulunuyor mu?
- Hareketli engeller güvenli yolu kapatıyor mu?
- Birden fazla engel birleşip kaçınılmaz çarpışma yaratıyor mu?

Başlangıç için minimum yaklaşık 1.5 saniyelik tepki fırsatını hedefle. Bu değeri oynanış testlerinden sonra ayarlanabilir kıl.

Geçerli olmayan kombinasyonları yeniden üret. Belirli sayıda denemeden sonra güvenli bir yedek engel kalıbı kullan.

## 6.4. Nesne havuzu

Sürekli Instantiate/Destroy kullanımıyla performans kaybı oluşturma.

Object pooling kullan.

Görüş alanından çıkan engeller havuza döndürülsün.

Zemin parçaları da uygun şekilde tekrar kullanılsın.

## 6.5. Test edilebilirlik

Engel üreticisine otomatik testler ekle.

Sabit rastgele tohumlarla binlerce engel dizilimini test edebil.

Sistem geçersiz dizilimleri, minimum tepki süresi ihlallerini ve geçiş yollarını raporlayabilsin.

Otomatik doğrulamanın kapsamını belgele. Algoritmanın bütün insan reflekslerini kusursuz şekilde modellediğini varsayma.

# 7. 3D ÇARPIŞMA SİSTEMİ

Araç ve engeller için basit fakat doğru çarpışma algılaması kullanılmalı.

Kitap, silgi ve oyuncak blok gibi nesnelerde BoxCollider kullanılabilir.

Tenis topu ve benzeri yuvarlak nesnelerde SphereCollider tercih edilebilir.

Çarpışma şekilleri görsel modellere mümkün olduğunca uygun olmalı.

Basit arcade hareketi kullanılacağı için gerçekçi araç fiziğine ihtiyaç yok.

Kontrol ve dünya kaydırma sistemleri deterministik davranmalı.

Yüksek hızlarda engelin kareler arasında çarpışma alanını atlaması engellenmeli. Gerekliyse swept collision, cast veya zaman aralığına dayalı geçiş kontrolü uygulanmalı.

Çarpışma gerçekleştiğinde:

- Oyuncu kontrolü durdurulur.
- Kısa çarpışma animasyonu oynatılır.
- Hafif ekran sarsıntısı oluşur.
- Eğlenceli oyuncak sesleri duyulur.
- Çarpışma parçacıkları gösterilir.
- Oyun sonu ekranı açılır.

Çarpışmalar korkutucu, kanlı veya şiddetli görünmemeli.

# 8. NEAR MISS VE KOMBO SİSTEMİ

Oyunun rekabetçi eğlence sisteminin merkezinde Near Miss bulunacak.

Oyuncu bir engelin çok yakınından, ona çarpmadan geçerse stil puanı kazanmalı.

## 8.1. Yakın geçiş algılama

Her engel için:

- Fiziksel çarpışma alanı.
- Yakın geçiş değerlendirme alanı.

tanımlanmalı.

Yakın geçiş ödülü, araç engeli başarıyla geçtikten sonra verilmeli.

Aynı engel bir sürüşte aynı geçiş için yalnızca bir kez puan kazandırabilmeli.

Çarpışmayla sonuçlanan bir manevra yakın geçiş puanı vermemeli.

Yakınlık mesafesi bütün engellerde sabit olmak zorunda değil; nesnenin şekline ve boyutuna göre yapılandırılabilmeli.

## 8.2. Stil puanı

Başlangıç puanı örneği: 100 puan.

Kombo sistemi:

| Ardışık Near Miss | Çarpan |
|---|---|
| 1–2 | 1x |
| 3–4 | 2x |
| 5–7 | 3x |
| 8–11 | 4x |
| 12 ve üzeri | 5x |

Belirli süre yeni bir Near Miss yapılmazsa kombo sıfırlanır. İlk denemede yaklaşık 6 saniye kullanılabilir.

Çarpışma da aktif komboyu bitirir.

## 8.3. Combo Fever

Yüksek kombo elde edildiğinde özel görsel durum başlasın.

- Aracın arkasında renkli ışık izi.
- Küçük parçacıklar.
- Dinamik müzik katmanı.
- FEVER yazısı.
- Daha etkileyici yakın geçiş geri bildirimi.

Fever oyuncuya otomatik dokunulmazlık veya kontrol avantajı vermemeli.

Tamamen başarıyı ödüllendiren görsel-işitsel bir durum olarak tasarlanmalı.

# 9. İKİ AYRI SKOR VE ÇEVRİMİÇİ LEADERBOARD

Oyunda birbirinden ayrı iki rekabet ölçüsü istiyorum.

## 9.1. Mesafe sıralaması

Oyuncunun Arcade Mode'da tek sürüşte ulaştığı maksimum mesafe.

Metre cinsinden gösterilecek.

Her oyuncunun en iyi geçerli mesafe rekoru saklanacak.

## 9.2. Stil puanı sıralaması

Oyuncunun tek sürüşte Near Miss ve kombo mekanikleriyle kazandığı maksimum stil puanı.

Mesafe rekorundan bağımsız tutulacak.

## 9.3. Leaderboard ekranı

Ana menüde Leaderboard bölümü bulunacak.

İki sekme olacak:

**DISTANCE RANKING**

**STYLE RANKING**

Her tabloda:

- Sıralama.
- Oyuncunun güvenli görünen takma adı.
- Puan veya mesafe.
- İlk 100'e bakabilme.
- Oyuncunun kendi sırası.
- Oyuncunun yakınındaki sıralar.
- Genel tüm zamanlar tablosu.

İleride haftalık sıralama eklenebilecek altyapı hazırlansın.

Günlük meydan okumanın ayrı sıralama tablosu olabilir.

## 9.4. Altyapı

Unity Gaming Services Authentication, Leaderboards ve gerektiğinde Cloud Code kullanımını değerlendir.

İlgili resmî kaynaklar:

https://docs.unity.com/en-us/leaderboards/get-started

https://docs.unity.com/en-us/authentication

https://docs.unity.com/en-us/cloud-code/cheat-prevention

https://docs.unity.com/en-us/cloud-code/server-access-control

Çevrimiçi hizmet sağlayıcısının güncel özelliklerini ve fiyatlandırmasını uygulamadan önce kontrol et.

Leaderboard kodunu doğrudan oyun mantığına bağlamak yerine bir `ILeaderboardService` arayüzü oluştur.

Başlangıçta yerel bir mock servis ile test edebil.

Gerçek çevrimiçi entegrasyon sonraki geliştirme aşamasında yapılacak.

## 9.5. Hile engelleme

Oyuncunun telefonundan rastgele yüksek skor göndererek sıralamayı bozmasını mümkün olduğunca engelle.

Sadece istemciden gelen skora güvenme.

Sunucu tarafında koşu başlangıcı, süre, seed, hız sınırları ve olası maksimum skor doğrulamalarını planla.

Gerekli olursa yüksek skorlar için daha güçlü replay doğrulaması kullan.

Kritik yazma işlemlerini uygun bir sunucu katmanı üzerinden gerçekleştir.

Yerel skor doğrulamasının tek başına güvenli olmadığını kabul et ve gerçek koruma düzeyini açıkça belgele.

## 9.6. Çevrimdışı çalışma

İnternet bağlantısı yoksa oyun normal çalışmaya devam edecek.

Yerel rekorlar tutulacak.

Leaderboard ekranında bağlantı durumu açıklanacak.

Doğrulanamayan çevrimdışı skorlar otomatik olarak küresel sıralamaya güvenilir skor gibi gönderilmeyecek.

## 9.7. Çocuklara uygun çevrimiçi özellikler

Oyunumuzun küçük çocuklar tarafından da oynanması hedefleniyor.

Bu sebeple:

- Serbest metinle sohbet olmayacak.
- Kullanıcılar birbirlerine mesaj atamayacak.
- Fotoğraf yükleme olmayacak.
- Açık profil paylaşımı olmayacak.
- Başlangıçta kullanıcıya serbest isim yazdırmak yerine önceden onaylanmış güvenli takma adlar kullanılabilecek.
- Gereksiz kişisel veri toplanmayacak.
- Çevrimiçi kimlik ve sıralama hizmetleri, ilgili yaş kitlesi için hukuken ve mağaza kuralları açısından uygun olmadan etkinleştirilmeyecek.
- Gerekli olduğunda ebeveyn erişim mekanizması ve mevzuata uygun izin süreçleri kullanılacak.

Çocuğa yönelik çevrimdışı modun herhangi bir çevrimiçi hesap gerektirmeden çalışması temel koşuldur.

Bir ebeveyn kapısının tek başına hukuki ebeveyn izniyle eşdeğer olmadığını unutma.

# 10. ON FARKLI OYUNCAK ARABA

Oyuncular kazandıkları oyun içi puanlarla daha güzel arabalar açabilmeli.

Toplam 10 araba olacak.

Araçlar gittikçe daha etkileyici görünecek.

Ancak bütün araçların rekabetçi sürüş özellikleri eşit olacak.

Hiçbir araç diğerinden daha hızlı veya daha çevik olmayacak.

## 10.1. Araç listesi

| No | Araba | Görünüm | Önerilen açılma maliyeti |
|---|---|---|---:|
| 1 | Sparky | Klasik kırmızı oyuncak yarış arabası | Ücretsiz |
| 2 | Bubble Bug | Turkuaz, yuvarlak, eğlenceli mini araba | 200 TP |
| 3 | Mini Muscle | Turuncu kaslı spor araba | 500 TP |
| 4 | Candy Cruiser | Pembe, şeker renkli tasarım | 900 TP |
| 5 | Pixel Kart | Renkli, oyun makinesi tarzı araç | 1.500 TP |
| 6 | Rocket Pop | Roket esintili, sportif tasarım | 2.400 TP |
| 7 | Thunder Volt | Elektrik temalı, neon detaylı araç | 3.600 TP |
| 8 | Galaxy Glider | Uzay renkleri ve yıldız efektleri | 5.200 TP |
| 9 | Golden Comet | Altın renkli lüks yarış arabası | 7.200 TP |
| 10 | Crown Racer | Özel gövde, parlak detaylar, prestij aracı | 10.000 TP |

TP = Toy Points.

Bu fiyatlar örnek değerlerdir. Oyuncu testlerine göre dengelenmelidir.

## 10.2. Toy Points ekonomisi

Distance Score ve Style Score leaderboard rekorlarıdır.

Bunlardan bağımsız olarak oyun sonunda Toy Points kazanılır.

Toy Points, sürüş mesafesi ve stil başarısından türetilir.

Oyuncu bu puanları yeni arabalar ve kozmetik ürünler için harcayabilir.

Toy Points harcamak mesafe rekorunu veya stil puanı rekorunu düşürmez.

Para kazanma, ödüllendirme ve harcama işlemleri merkezi bir ekonomi sistemi üzerinden yürütülmeli.

Hileye açık istemci kayıtlarının küresel rekabete avantaj sağlamasına izin verilmemeli.

## 10.3. Garaj

Ana menüde GARAGE bölümü olacak.

Garajda:

- Seçili araba gerçek 3D model olarak gösterilecek.
- Araç yavaşça döndürülebilecek.
- Kilitli araçlar görülebilecek.
- Açılma fiyatları gösterilecek.
- Yeterli Toy Points varsa araç açılabilecek.
- Açılan araba seçilip oyuna girilebilecek.
- Satın alınan arabalar kalıcı olarak kayıtlı kalacak.

Her araç aynı standart gameplay collider ve hareket parametrelerini kullanmalı.

Görsel model farklılıkları adil rekabeti değiştirmemeli.

# 11. KOZMETİK ÖDÜLLER

Oyuncular araçları küçük görsel özelliklerle özelleştirebilsin.

Örnekler:

- Araba gövde boyaları.
- Lastik izi renkleri.
- Küçük çıkartmalar.
- Korna sesleri.
- Partikül ve sürüş izleri.
- Özel başarı rozetleri.

Kozmetik ürünler oyuncuya performans avantajı vermemeli.

İlk üretimde küçük bir kozmetik havuzu yeterli. Sistem ileride yeni ürünler eklenebilecek şekilde tasarlanmalı.

# 12. GÖREVLER VE BAŞARILAR

Oyunun yeniden açılmasını teşvik edecek basit görevler istiyorum.

Görevler uzun açıklamalar gerektirmemeli.

Örnekler:

- 300 metre ilerle.
- 5 Near Miss yap.
- 3x kombo yakala.
- Bir turda 1.000 stil puanı kazan.
- Top engelini başarıyla geç.
- Yeni bir araba aç.
- Tek turda 10 engelin yakınından geç.

Oyuncuya aynı anda üç kısa görev gösterilebilir.

Görev ödülleri Toy Points veya kozmetik öğeler olabilir.

Başlangıç içerik hedefi en az 15 farklı başarı.

Görevler veri odaklı tanımlanmalı. Yeni görev eklemek için ScoreManager gibi temel sistemlerin kodunu değiştirmek gerekmemeli.

Çocukları cezalandıran veya günlük giriş zorunluluğu hissettiren mekanikler kullanma.

# 13. TOY CHAOS EVENTS

Oyuncak dünyasını daha canlı hissettiren olaylar oluşturmak istiyorum.

Bu sistem oyunun önemli ayırt edici özelliklerinden biri olacak.

Toy Chaos Events, zaman zaman beliren kısa çevresel olaylardır.

Örnekler:

**Rolling Ball:** Büyük tenis topu ekranın yan tarafından yuvarlanır.

**Pencil Roll:** Uzun bir kalem oyun alanına doğru hareket eder.

**Wind-Up Racer:** Kurmalı küçük oyuncak araba önceden işaretlenmiş rotadan geçer.

**Domino Chain:** Domino taşları sırayla devrilir.

**Rolling Orange:** Mutfak haritasında büyük portakal hareket eder.

Bu olaylarda önemli kurallar:

1. Oyuncuya olay öncesinde anlaşılır uyarı ver.
2. Engellerin hareketi rastgele ve kaçınılmaz olmasın.
3. Olaylar engel üretim algoritmasının geçilebilirlik kontrolüne dâhil edilsin.
4. Hareketli engeller Near Miss puanı kazandırabilsin.
5. Aynı özel olaylar art arda aşırı sık tekrarlanmasın.
6. Olay yoğunluğu oyun hızı ve zorluğuna göre yönetilsin.

İlk Toy Room haritasında öncelikle Rolling Ball geliştirilecek.

Domino Chain daha sonra görsel çevre etkileşimi olarak eklenecek.

Diğer harita olayları genişleme aşamalarında uygulanacak.

# 14. ZİNCİRLEME ÇEVRE ETKİLEŞİMLERİ

Çevre zaman zaman oyuncunun hareketlerine eğlenceli görsel tepkiler vermeli.

Örneğin:

Araba domino taşlarının yakınından geçer ve taşlar ardışık biçimde devrilir.

Bu animasyonlar ana oynanışı karmaşıklaştırmamalı.

Özellikle ilk sürümde, yalnızca dekoratif olan zincirleme reaksiyonlar oyuncuya aniden yeni çarpışma tehlikesi oluşturmamalı.

Fizik hesaplarının çok fazla CPU tüketmesine izin verme.

Mümkünse önceden hazırlanmış animasyon, Tween veya basit kontrollü hareket kullan.

# 15. KOMİK ÇARPIŞMA ANİMASYONLARI

Oyun bittiğinde her seferinde aynı animasyonun görülmesini istemiyorum.

Farklı çarpışma sonuçları ekle.

Örnekler:

- Küçük oyuncak taklası.
- Tekerleğin komik şekilde yerinden çıkması.
- Arabanın kendi etrafında kısa dönüşü.
- Sıkışan oyuncak korna sesi.
- Küçük yıldız ve oyuncak parçacıkları.

Çarpışma animasyonları 1–2 saniyeyi aşmamalı.

Oyuncu beklemeden yeniden başlayabilmeli.

Görsel efektler performans dostu olmalı.

# 16. GÜNLÜK MEYDAN OKUMA

Oyuncular her gün aynı engel diziliminden oluşan özel bir parkurda yarışabilsin.

Bu sistem için deterministik seed kullanılmalı.

Aynı günün parkuru tüm uygun oyuncular için aynı olmalı.

Günlük görevler ile Daily Challenge birbirinden bağımsız sistemler olacak.

Daily Challenge:

- Aynı başlangıç koşulları.
- Aynı engel dizilimi.
- Aynı başlangıç hızı.
- Aynı oyun kuralları.
- Ayrı günlük sıralama.
- Gün değişiminde yeni seed.

Gün değişimini yalnızca cihazın değiştirilebilir yerel saatine bağlama. Mümkün olduğunda güvenilir sunucu zamanını kullan.

Bu özellik çevrimiçi uygunluk ve gizlilik gereksinimleri sağlandıktan sonra etkinleştirilecek.

# 17. GHOST CAR — KENDİ REKORUNLA YARIŞ

Oyuncu kendi önceki performansının yarı saydam bir tekrarını görebilsin.

Hayalet araba:

- Çarpışma yapmaz.
- Diğer araçların hareketini değiştirmez.
- Görsel olarak yarı saydamdır.
- Önceki koşunun konum ve zaman kayıtlarından yeniden oynatılır.

Önemli teknik ayrıntı:

Ghost sistemi aynı engel düzeninin tekrar oynandığı koşullarda anlamlı olacaktır.

Bu nedenle öncelikle sabit seed'li Daily Challenge veya aynı seed ile tekrar oynama modunda uygulanmalı.

Farklı rastgele parkurlarda eski rekor arabasını yanlış bağlamda göstermekten kaçın.

Replay kayıtları sıkıştırılmış ve performans dostu tutulmalı.

# 18. ARAYÜZ VE MENÜLER

Arayüz basit, temiz, büyük dokunmatik alanlara sahip ve anlaşılır olacak.

İlk hedef mobil portre ekran.

Gerekli ekranlar:

**Main Menu**

Play, Garage, Leaderboards, Daily Challenge, Missions, Settings.

**Mode Selection**

Arcade Mode ve Relax Mode.

**Gameplay HUD**

Mesafe, stil puanı, aktif kombo, vites seviyesi, pause.

**Pause Menu**

Resume, Restart, Main Menu.

**Game Over**

Son mesafe, yeni rekor bilgisi, stil puanı, kazanılan Toy Points, tekrar oyna, ana menü.

**Garage**

Araç seçimi, kilit açma, kozmetik seçenekleri.

**Leaderboards**

Distance Ranking ve Style Ranking sekmeleri.

**Missions**

Görevler ve kazanılabilecek ödüller.

**Settings**

Müzik, ses efektleri, titreşim, kontrol hassasiyeti, dil ve gerekli gizlilik seçenekleri.

Çocuklar için okunabilir semboller ve açık ifadeler kullanılmalı.

Oyun sırasında ekranda gereksiz sayıda buton bulunmamalı.

# 19. SES TASARIMI VE GAME FEEL

Oyunun oyuncuya verdiği his çok önemli.

Eklenmesini istediğim efektler:

- Oyuncak motor sesi.
- Vites atma efekti.
- Near Miss sesi.
- Kombo yükselme sesi.
- Fever müzik katmanı.
- Hafif lastik kayma sesi.
- Yüksek hızda lastik dumanı.
- Çarpışma sarsıntısı.
- Eğlenceli oyuncak parçacıkları.
- Oyuncak korna efektleri.
- Hız arttıkça güçlenen rüzgâr hissi.

Sesler rahatsız edici, çok yüksek veya sürekli tekrar eden türde olmamalı.

Müzik, hız artışlarına uyum sağlayan katmanlı bir yapıda olabilir.

Kullanıcı isterse tüm sesleri kapatabilmeli.

Titreşim isteğe bağlı olmalı.

Parçacık ve efekt yoğunluğu düşük performanslı cihazlarda azaltılabilmeli.

# 20. PARA KAZANMA VE REKLAM ALTYAPISI

Oyun ücretsiz olacak.

Öncelik oyuncunun eğlenceli deneyimidir.

Gelir modeli internet bağlantısı bulunan ve reklama uygun koşulları karşılayan kullanım senaryolarında devreye girebilir.

Planlanan reklam türleri:

**Rewarded Ads:** Oyuncu isterse ek Toy Points ödülü alabilir.

**Interstitial Ads:** Oyun bitişlerinde seyrek gösterilebilir.

Oyun sırasında ekranı kaplayan reklam veya sürekli banner kullanılmasın.

Reklam izlemek oyunun ilerlemesi için zorunlu olmayacak.

Çevrimdışı kullanımda oyun sorunsuz çalışacak.

Çocuklara yönelik veya karma yaş kitlesini kapsayan dağıtımlarda Google Play Families politikalarını ve diğer ilgili mevzuatı incele.

Çocuklara gösterilebilecek reklamlar için sadece izin verilen, uygun sertifikalı çözümleri ve kişiselleştirilmemiş reklam şartlarını değerlendir.

Uygunluk doğrulanana kadar reklam entegrasyonunu kapalı tut.

İleride reklam kaldırma satın alımı eklenebilecek mimari hazırlansın; ancak ilk prototipte ödeme veya reklam SDK'sı kurma.

# 21. GİZLİLİK, GÜVENLİK VE ÇOCUKLARA UYGUNLUK

Bu oyunun çocuklar tarafından oynanması bekleniyor.

Dolayısıyla veri güvenliği ve platform kuralları sonradan düşünülen bir ayrıntı olmayacak.

Temel kurallar:

- Oyun için konum izni isteme.
- Mikrofon ve kamera izni isteme.
- Gereksiz kişisel veri toplama.
- Zorunlu hesap oluşturma şartı getirme.
- Çevrimdışı oyunu sunucuya bağımlı hâle getirme.
- Çocuklar için açık sohbet veya kullanıcı tarafından oluşturulan kontrolsüz içerik sunma.
- Uygunluğu doğrulanmamış reklam veya analiz SDK'larını çocuklara açık modda başlatma.
- Gerekli yasal izinler sağlanmadan çocukların çevrimiçi kimlik ve veri özelliklerini aktifleştirme.

Oyunun hedef yaş grubu ve mağaza sınıflandırması yayın aşamasında doğru belirlenecek.

Google Play Families, Apple Kids Category, GDPR, COPPA ve uygulanabilir yerel veri koruma yükümlülükleri değerlendirilmelidir.

Teknik olarak mümkün olmak, bir hizmetin çocuklar için hukuken uygun olduğu anlamına gelmez.

Yayın öncesinde güncel resmî politikaların ayrıca doğrulanması gerekir.

# 22. PERFORMANS VE OPTİMİZASYON

Oyun orta segment Android cihazlarda akıcı çalışmalı.

Hedef performans:

- Uygun cihazlarda kararlı 60 FPS.
- Gerekirse 30 FPS alternatif kalite seviyesi.
- Düşük bellek tüketimi.
- Hızlı yeniden başlatma.
- Minimum gereksiz bellek tahsisi.
- Yeniden kullanılabilir 3D nesneler.
- Optimize edilmiş materyaller ve mesh'ler.
- Gereksiz gerçek zamanlı fizik simülasyonundan kaçınma.
- Mobil URP ayarları.
- Sınırlı dinamik gölge maliyeti.
- Gerektiğinde baked lighting.
- Profiling ve gerçek cihaz testi.

Performans iddialarını gerçek cihaz testi yapılmadan doğrulanmış olarak sunma.

Görüntü kalitesini korurken gereksiz post-processing, yüksek poligon sayısı ve büyük dokulardan kaçın.

# 23. YAZILIM MİMARİSİ

Önerilen modüler yapı:

**Core**
- GameManager
- GameStateMachine
- GameConfig
- SaveManager
- EventBus veya eşdeğer hafif olay sistemi

**Player**
- InputController
- PlayerController
- PlayerVisualController
- CarDefinition
- CarSelectionManager

**World**
- WorldScroller
- WorldChunkManager
- EnvironmentDefinition
- ObstacleSpawner
- ObstacleDefinition
- PatternGenerator
- PatternValidator
- ObjectPoolManager

**Gameplay**
- CollisionManager
- NearMissManager
- ComboManager
- DifficultyManager
- ChaosEventManager
- ScoreManager
- RewardManager

**Progression**
- CurrencyManager
- GarageManager
- CosmeticManager
- MissionManager
- AchievementManager

**Online**
- ILeaderboardService
- LocalLeaderboardService
- OnlineLeaderboardService
- AuthenticationManager
- ScoreValidationService
- DailyChallengeManager

**Replay**
- ReplayRecorder
- ReplayPlayer
- GhostCarController

**Presentation**
- UIManager
- HUDController
- AudioManager
- VFXManager
- ScreenShakeController

Bu sınıf isimleri bir mimari öneridir; gereksiz soyutlama veya şişkinlik yaratıyorsa daha temiz bir yapı kurabilirsin.

ScriptableObject tabanlı veri yapıları kullanarak araç, engel, harita ve görev tanımlarını mümkün olduğunca koddan ayır.

Servislerin birbirine gereksiz bağımlı olmasını önle.

Oyun mantığının önemli kısımlarını Unity sahnesine bağımlı olmadan test edilebilir tasarla.

# 24. GELİŞTİRME AŞAMALARI

Bu projeyi aşağıdaki sırayla geliştireceğiz.

## FAZ 1 — Çalışan 3D prototip

- Unity 3D URP proje iskeleti.
- Portrait kamera.
- Basit 3D Sparky modeli.
- Basit 3D zemin.
- Tek parmak yatay kontrol.
- Mouse ile editör kontrolü.
- Ekran sınırları.
- Kaydırılabilir dünya.
- Basit statik engeller.
- Çarpışınca oyun bitmesi.
- Yeniden başlatma.

**Kabul kriteri:** Telefonda veya Unity editöründe temel sürüş deneyimi düzgün çalışmalı.

## FAZ 2 — Oynanış sistemleri

- Prosedürel engel üretimi.
- Adil rota kontrolü.
- Object pooling.
- Hızlanma.
- Mesafe.
- Near Miss.
- Stil puanı.
- Kombo.
- Combo Fever.
- Oyun sonu ekranı.

**Kabul kriteri:** Oyunun ana döngüsü teknik olarak tamamlanmış olmalı.

## FAZ 3 — Toy Room ve Game Feel

- Gerçek 3D çocuk odası görselleri.
- İlk engel modelleri.
- Gölgeler ve materyaller.
- Oyuncak araba modeli iyileştirmeleri.
- Hareketli tenis topu.
- İlk Toy Chaos Event.
- Ses efektleri.
- Parçacıklar.
- Komik çarpışmalar.
- Domino zincirleme çevre reaksiyonları.

**Kabul kriteri:** Oyun basit bir teknik demo gibi değil, oyuncak dünyasına sahip bir arcade oyunu gibi hissettirmeli.

## FAZ 4 — Oyuncu ilerlemesi

- 10 araba.
- Garage ekranı.
- Toy Points ekonomisi.
- Araba açma ve seçme.
- Kozmetik sistemleri.
- Görevler.
- Başarılar.
- Yerel kayıtlar.
- Relax Mode.

**Kabul kriteri:** Oyuncu yeni hedefler için oyuna tekrar dönmek isteyebilmeli.

## FAZ 5 — Çevrimiçi özellikler

- Uygunluk ve gizlilik değerlendirmesi.
- Güvenli kullanıcı kimliği.
- Mesafe leaderboard.
- Stil leaderboard.
- Oyuncunun kendi sırası.
- Sunucu skor doğrulaması.
- Günlük meydan okuma.
- Günlük sıralama.
- Ghost replay.
- Ağ hatalarına dayanıklı çalışma.

**Kabul kriteri:** Uygun oyuncuların sıralamaları güvenilir biçimde gösterilmeli. Çevrimdışı oynanış bozulmamalı.

## FAZ 6 — Yayın hazırlığı

- Farklı Android cihazlarda test.
- Dokunmatik hassasiyet düzenlemeleri.
- Performans optimizasyonu.
- Erişilebilirlik.
- Gerekli gizlilik ve mağaza dokümanları.
- Uygun reklam entegrasyonu.
- Uygulama ikonu.
- Açılış ekranı.
- Mağaza ekran görüntüleri.
- Test sürümü.
- Hata düzeltmeleri.

**Kabul kriteri:** Gerçek oyuncu testlerine ve mağaza incelemesine sunulabilecek bir Android sürümü hazırlanmalı.

## FAZ 7 — Harita genişlemesi

İlk Toy Room sürümü tamamlanıp oyuncu testlerinden geçtikten sonra:

- Desk Disaster.
- Kitchen Chaos.
- Yeni engel çeşitleri.
- Yeni Toy Chaos Events.
- Yeni kozmetikler.
- Yeni görevler.

eklenecek.

Bu faz ilk yayın için zorunlu değil.

# 25. TEST STRATEJİSİ

Her ana mekanizma için uygun test yöntemi oluştur.

Özellikle:

1. Kontrol sistemi farklı ekran çözünürlüklerinde çalışıyor mu?
2. Parmağı kaldırıp tekrar dokunmak pozisyon sıçramasına neden oluyor mu?
3. Engeller geçilebilir rota bırakıyor mu?
4. Yüksek hızda çarpışmalar atlanıyor mu?
5. Near Miss aynı engelde yanlışlıkla birden fazla puan veriyor mu?
6. Kombo doğru şekilde artıyor ve sıfırlanıyor mu?
7. Para harcamak leaderboard rekorlarını etkiliyor mu?
8. Araç değiştirmek oynanış avantajı yaratıyor mu?
9. Çevrimdışı oynanış sorunsuz mu?
10. Kayıt verileri oyunun yeniden başlatılmasından sonra korunuyor mu?
11. Online hataları ana oyun döngüsünü durduruyor mu?
12. Relax Mode yanlışlıkla küresel sıralamaya skor gönderiyor mu?
13. Günlük meydan okuma aynı seed ile aynı parkuru üretiyor mu?
14. Ghost sistemi doğru parkur ve zaman çizelgesinde çalışıyor mu?
15. Mobil cihazda uzun süre oynarken performans ve bellek tüketimi kararlı mı?

Mümkün olduğunda Unity Test Framework kullan.

Fiziksel cihaz gerektiren testleri ayrı belirt.

Her faz tamamlandığında test sonuçlarını raporla.

# 26. DOSYA DÜZENİ VE DOKÜMANTASYON

Projede düzenli bir klasör yapısı istiyorum.

Örnek:

Assets/
- BrakeToy/
  - Art/
    - Models/
    - Materials/
    - Textures/
    - Animations/
  - Audio/
  - Prefabs/
    - Cars/
    - Obstacles/
    - Environments/
    - UI/
  - Scenes/
  - Scripts/
    - Core/
    - Player/
    - World/
    - Gameplay/
    - Progression/
    - Online/
    - Replay/
    - Presentation/
  - ScriptableObjects/
  - Tests/

Repository içinde ayrıca şu dokümantasyon dosyalarını oluştur:

- `README.md` — Kurulum ve proje özeti.
- `Docs/Archive/pre-v09/GAME_DESIGN.md` — Oyunun temel ve nihai tasarım kararları.
- `Docs/Archive/pre-v09/ARCHITECTURE.md` — Sistem mimarisi.
- `Docs/Archive/pre-v09/ROADMAP.md` — Fazlar ve görevler.
- `CHANGELOG.md` — Yapılan değişiklikler.
- `TEST_PLAN.md` — Test stratejisi.

Gerekli editör kurulum adımlarını da README içinde belirt.

Unity sahne ve prefab bağlantılarını mümkün olduğunca tekrarlanabilir biçimde oluştur.

Gerekli bir nesneyi Unity editöründe elle eklemem gerekiyorsa tam olarak hangi menüden, hangi seçenekle ekleyeceğimi açıkla.

# 27. GELİŞTİRME SIRASINDA UYMAN GEREKEN KURALLAR

- Önce çalışan sürüm, sonra yeni özellik.
- Önce mekanik, sonra görsel cilalama.
- Önce doğrulama, sonra optimizasyon.
- Gereksiz paket bağımlılıklarından kaçın.
- Harici servis için gizli anahtarları istemci koduna gömme.
- Üçüncü taraf modeller ve sesler için kullanım haklarını dikkate al.
- İnternetten indirilen rastgele kodları güvenlik kontrolü olmadan projeye ekleme.
- Nihai kapsamın hiçbir önemli özelliğini sessizce unutma.
- Tamamlanmamış işleri açıkça belirt.
- Var olmayan sistemleri varmış gibi gösterme.
- Gerçek test yapılmadıysa "test edildi" deme.
- Her fazın sonunda çalışan sürümü Git üzerinden kaydetmeye uygun şekilde düzenle.
- Bir özelliğin daha iyi teknik çözümü varsa nedenini açıklayarak öner, ancak oyunun temel tasarım kararlarını kendiliğinden değiştirme.

# 28. ŞİMDİ SENDEN İSTEDİĞİM İLK İŞ

Bu dokümanı Brake Toy projesinin ana gereksinim kaynağı kabul et.

Fakat bütün oyunu tek seferde üretmeye çalışma.

İlk olarak şu işleri yap:

**A.** Mevcut geliştirme ortamını ve varsa Unity proje dosyalarını incele.

**B.** Unity 6.3 LTS, C# ve gerçek 3D URP yapısıyla uyumlu bir mimari hazırla.

**C.** Nihai oyun kapsamını `Docs/Archive/pre-v09/GAME_DESIGN.md` ve aşamalı geliştirme planını `Docs/Archive/pre-v09/ROADMAP.md` içinde düzenle.

**D.** FAZ 1 için gereken en basit çalışan 3D prototipi oluşturmaya başla.

**E.** İlk aşamada yalnızca şu özellikleri uygula:
- Dikey ekran.
- Doğru 3D kamera.
- Geçici ama üç boyutlu Sparky modeli.
- Tek parmakla sağ-sol hareket.
- Mouse ile kontrol testi.
- Basit 3D zemin.
- Sürekli ileri ilerleme hissi.
- Basit engeller.
- Çarpışma ve yeniden başlama.

**F.** Sistemin doğru çalıştığını mümkün olan testlerle doğrula.

**G.** Sonunda bana Türkçe bir rapor ver:
- Oluşturduğun dosyalar.
- Yazdığın sistemler.
- Çalıştırdığın testler.
- Doğrulayamadığın noktalar.
- Unity editöründe benim yapmam gereken işlemler.
- Bir sonraki geliştirme adımı.

Proje dosyalarını doğrudan düzenleme imkânın yoksa bunu açıkça söyle; o durumda gerekli dosyaları, kodları ve Unity kurulumu için eksiksiz adım adım talimatları üret.

**Son hedefimiz:** Çocukların tekrar tekrar oynamak isteyeceği kadar renkli ve eğlenceli; yetişkinlerin yüksek skor kovalayacağı kadar ustalık gerektiren; gerçek 3D oyuncak dünyasına sahip, akıcı ve profesyonel bir mobil oyun geliştirmek.

Oyunun adı BRAKE TOY.

Artık Faz 1 ile başla.