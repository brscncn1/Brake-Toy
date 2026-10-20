# Test planı

## v0.2 güncel sonuç ve manuel kontrol

19 EditMode + 8 PlayMode = 27/27 geçti. v0.2 kanıtları Tests altındaki sürümlü XML dosyalarıdır. Oda sahnesi/dekor collider ayrımı, kamera tepki görüşü, retry ve dekor kayma/reset testleri eklendi.

Manuel: yeni sahneyi 9:16'da aç; odanın ve masa yüksekliğinin okunurluğunu, kalemlik/defterin akışını, parmağı bırakıp yeniden tutmayı, pause/kesinti/tekrar oynamayı ve gerçek telefonda uzun sürüşü kontrol et. 20° kamera yeni kullanıcı geri bildirimine açıktır.


## Faz 1 kabul sırası

| Alan | Yöntem | Mevcut durum |
|---|---|---|
| Göreli sürükleme/sınır/hız/reset | Üretim C# + .NET runner | 19 ortak kontrolün ilgili bölümü geçti |
| Çarpışma/tünelleme | Üretim C# + bağımsız geometri oracle | Sabit örnekler ve 10.000 seed örneği geçti |
| Unity derlemesi/paket çözümü | Unity 6000.3.26f1 batch import | Geçti |
| Sahne/3D geometri/URP | Üretici + Play Mode 3D render | Üretildi ve önizleme incelendi |
| UI ve giriş entegrasyonu | Input System sanal mouse/touch | 2 PlayMode testi geçti; fiziksel kullanım bekliyor |
| Oyun döngüsü ve kamera | Unity PlayMode testleri | 2 PlayMode testi geçti |
| Android build | Unity ARM64 IL2CPP Development | Geçti; APK imza/arşiv kontrolü geçti |
| Android dokunma/performance | Fiziksel cihaz | Bekliyor |

`TEST_RESULTS.json` yalnızca gerçekten çalıştırılan bağımsız C# kontrollerinin makine çıktısıdır. Ayrı Unity koşularının kanıtı `Tests/EditMode-results.xml` ve `Tests/PlayMode-results.xml` dosyalarıdır. Fiziksel cihaz sonuçları bu koşulara dahil değildir.

## Manuel Faz 1 senaryoları

1. **İlk import:** Unity 6.3 LTS ile açın; paketler çözülsün. Console'da compile/error olmamalı. Otomatik üretim veya Tools menüsü sahneyi oluşturmalı. Sahne ve varlıkları kaydedip Editor'ü yeniden açın; üzerine yazılmamalı.
2. **3D kontrol:** Scene görünümünde modelin etrafında dönün; araç/engel/zemin gerçekten hacimli olmalı. Perspektif kamera, plastik materyal ve gölgeleri gözle kontrol edin. Bu aşama için nihai art kalitesi beklenmez.
3. **Yatay sürüş:** Ekranın farklı boş noktalarına ilk dokunmada konum değişmemeli. Aynı genişlik oranında sürükleme 360/720/1080/1440 çözünürlüklerde aynı mesafeye gitmeli. Hız sınırı hissedilir gecikme yaratıyor mu kaydedin.
4. **Bırakma/retouch:** Hızlı sürüklerken parmağı bırakın; konum korunmalı. Başka yere dokunun, sıçramamalı. Sınırda ters hareket hemen başlamalı. İkinci parmak arabayı devralmamalı.
5. **UI:** OYNA/TEKRAR OYNA/II/DEVAM ET dokunuşu direksiyon sayılmamalı. Düğmeler portrede rahat hedeflenmeli; safe area çentiğin altında kalmamalı.
6. **Çarpışma:** Düz gidip ilk ortadaki kitaba çarpın. En erken temasta dünya ve giriş durmalı. Tam sınır teması çarpışma kabul edilir. Yanından açık mesafe ile geçiş oyunu bitirmemeli. Yüksek hız/30 FPS'te atlama olmamalı.
7. **Retry:** En az 20 kez tekrar başlatın. Oyuncu X=0, ilk engel Z=22, başlangıç durumu aynı olmalı. Nesne sayısı büyümemeli; yeni sahne yüklenmemeli.
8. **Kaydırma:** Engellerden kaçarak en az 5 dakika sürün. Zemin boşluğu, atlama, üst üste spawn veya bellek artışı olmamalı. Sabit dizilimin tekrar etmesi Faz 1'de beklenir.
9. **Kesinti:** Pause, başka pencereye geçme, telefonun kilitlenmesi ve uygulamanın arka plana alınması sürüşü durdurmalı. Geri dönüşte DEVAM ET istenmeli; önceki dokunma farkı birikmemeli.
10. **Oranlar:** 9:16, 9:19.5, 9:21 ve tablet 3:4; araç merkezi alt %25 civarında, sürüş sınırları görünür. Kamera yatay hareket etmemeli.
11. **Offline:** İlk paket importundan ve build'den sonra internet kapalıyken oyun açılmalı/oynanmalı. İlk Editor paket indirmesinin internet gerektirmesi gameplay offline koşulundan ayrıdır.
12. **Cihaz:** Gerçek Android'de build/install, dokunma hassasiyeti, 10–20 dakika sürüş, CPU/GPU frame time, GC tahsisi, bellek ve ısı ölçün. 60 FPS yalnızca hedef; başarım ölçülerek raporlanmalı.

## Sonraki fazların zorunlu regresyonları

- **Faz 2 üretim:** binlerce sabit seed; aynı seed/kural sürümü aynı parkur; birleşik ve hareketli tehlikeler; oyuncunun farklı başlangıç X'leri; en yüksek hız/tepki süresi; başarısız aday/fallback. Güvenli rota ve ihlal çıktısı saklanır. İnsan refleksi/hatalı oynama otomatik doğrulama garantisine dahil değildir.
- **Near Miss/kombo:** geçiş bitmeden ödül yok; çarpışmada yok; aynı obstacle instance yeniden kullanıldığında yeni geçiş kimliği; aynı geçişte tek ödül; 1–2/3–4/5–7/8–11/12+ eşikleri ve 6 s reset; Fever avantaj vermez.
- **Ekonomi/araç:** TP harcama iki rekoru azaltmaz; bütün 10 araç aynı gameplay profile; kozmetik collider/hızı değiştirmez. Yetersiz bakiye/çift tıklama ve sürümlü kalıcı kayıt/bozuk kayıt sınanır.
- **Relax:** 3 hak, hız/düzen farkı, Arcade rekorundan ayrı kayıt; hiçbir global tabloya gönderim yok.
- **Online:** ağ zaman aşımı, auth hatası, offline devam, doğrulanmamış skor reddi, güvensiz istemci yazmalarının kapalı olması, yaş/izin kapıları ve güvenli takma adlar.
- **Daily/ghost:** sunucu günü/seed, kural versiyonu, zaman ekseni, seed eşleşmesi; farklı parkurda ghost göstermeme; sıkıştırma/okuma doğruluğu; çarpışma etkisi olmaması.
- **Yayın:** farklı Android sınıfları, uzun seans, kayıt, yaşam döngüsü, erişilebilirlik, izinler ve güncel mağaza/politika kontrolü. iOS ayrı macOS/Xcode cihaz matrisi gerektirir.

## v0.3 ek kapsam

Hız integrali/ilk karşılaşma, seed tekrarları, 128.000 satır koridor/geçiş denetimi, 10.000 top oracle karşılaştırması; iki dakikalık motor içi tanık rota, hareketli top süpürmesi, bir kez Near Miss, combo reset/timeout/Fever, pause/retry/havuz, model gameplay sınırı/mesh geçerliliği ve döner top gerçek vertexleri, maksimum hızda dört portre oranı. Gerçek test sonuçları TEST_REPORT.md ve Tests/*v0.3-results.xml içinde. Fiziksel Android FPS/ısı/dokunma ve kullanıcı tempo/hissetme kabulü ayrı manuel adımdır.

## v0.4 ek kabul

Yeni katalog/prefab havuzu, varied seed koridorları, aynı parkur/yeni tur, menüde tek panel, sürüşte ayar gizleme, kaynak pipeline izolasyonu, ses dalga sınırları ve impact kök sabitliği doğrulanır. 28 EditMode/25 PlayMode sonuçları XML'de. DEVICE_TEST-v0.4.md Huawei MatePad 11 için fiziksel dokunma/ısı/kesinti ve iki profilde 60 sn yerel ölçüm planıdır; cihaz onayı henüz açık.
