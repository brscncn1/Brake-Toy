# K1.5 — 25 Ekim 2026 (yeni APK yayımlanmadı)

K1.5 tamamlandı: jetpack alçalmasının son 0.45 saniyesi korumalı; yakın iniş alanı alçalma başında temizleniyor, güç sonundaki 0.85 saniye koruma korunuyor ve bitiyor. Cetvel destek yüksekliğinin bir simülasyon adımı geriden gelmesi giderildi. Kitap/cetvel sağ-sol temas sınırları 24 koşuda, 30/60/120 Hz adımla denendi; cihaz FPS ölçümü değildir. Yan çıkış, güç bitişi sonrası normal çarpışma ve aynı rotada retry stokları kontrol edildi. Önce 3/5, düzeltmeden sonra hedefli 5/5; nihai 53 EditMode +71 PlayMode =124/124. Gerçek kariyer/ghost dosyaları değişmedi. APK ve tablet testi yapılmadı. Kaynak dal fix/landing-ramp-boundaries. Sonraki küçük paket K1.6/K1.7 okunabilirlik ve açık build hedefi değerlendirmesidir. Kanıt Reports/LANDING_VALIDATION.json, ayrıntı Docs/LANDING-2026-10-25.md. Notion tekrar denenmedi; notlar masaüstü yedeğine eklendi.

# Sağlamlaştırma teslimi — 25 Ekim 2026

25 Ekim sağlamlaştırma: Relax aynı oyuncaktan tekrar hak kaybetmeyi önlemek için yakın yeni oyuncak/prop/rampaları temizler. Adventure ayrı career-adventure-v1.json kullanır; eski dosyadan TP, araçlar, görev/başarımlar ve harcanmış güç stokları korunarak taşınır, eski kurallı rekorlar aktarılmaz. Stil gerçek ArcadeDirector anahtarından yüklenir. İlk iki test düzeltme öncesi başarısız, sonrası 2/2 geçti; nihai 53 EditMode +66 PlayMode =119/119. Gerçek kariyer/ghost dosyaları değişmedi. APK derlenmedi. Notion yeni sayfa denemesi blok kotasından reddedildi, sayfa oluşmadı. Masaüstü BrakeToy_Notion_Yedek.txt ek yedek olarak tutulur. Ayrıntı Docs/HARDENING-2026-10-25.md ve Reports/HARDENING_VALIDATION.json. Sonraki paket K1.5 güç bitişi/iniş ve rampa-cetvel kenarlarıdır.

# 25 Ekim 2026 — proje düzeni eşitlemesi bekliyor

Proje merkezi güncellemesi update_page/block_creation için entitlement_required döndürdü; ücretsiz blok sınırı dolu. Hiçbir içerik oluşturulmadı, otomatik tekrar yapılmadı. 181 dosyalık rapor/arşiv taşıması, Editor çıktı yolları, Unity derleme çıkış 0 ve doğrulama sonuçları eşitlenecek. Kaynak: chore/project-organization, Reports/ORGANIZATION_VALIDATION.json ve DEV_LOG.md. Mevcut 24 Ekim Notion kayıtları bu yeni paketi kapsamıyor.

# Notion eşitleme durumu — 24 Ekim 2026

v0.8 başlangıç kaydı mevcut geliştirme günlüğüne eklendi ve fetch ile okundu. Sonraki 34 numaralı plan sayfası oluşturma işlemi Notion `entitlement_required / block_creation` ile reddedildi: ücretsiz blok sınırı doldu. Yeni sayfa oluşmadı; otomatik tekrar yapılmıyor. Kullanıcıya bildirildi. Yerel belgeler ve GitHub ana kayıt olarak sürdürülür; bağlantı/plan sınırı çözülünce güncel v0.8 plan/test/teslim belgeleri Notion'a aktarılmalıdır. Önceki 33 alt sayfa korunur. Kullanıcı adına ücretli plana geçilmedi.


v0.8.1 yerleşim düzeltmesi de eşitleme bekler: PLAN-v0.8.1.md, MILESTONE-v081.md ve 104 test/Android teslim raporları. Yeni blok oluşturma aynı engel nedeniyle otomatik denenmedi.


## v0.9 — 2026-10-24 / devam ediyor
Kullanıcı sekiz parkur mekaniğinin tamamını onayladı. PLAN-v0.9.md: karton tünel, kurmalı oyuncak, vinç, tren, misket oluğu, bant/kart yüzeyleri, cetvel tahterevalli, garaj araçları. Yeni Adventure sahnesi Tidy yerleşimini ve kariyeri korur; domino yok. Sekizli karıştırılmış torba ve son üç aileyi tekrarlamama. Havuz, analitik görsel/çarpışma eşliği, açık yeşil yol, yerde yüzey etkisi, güç koruması. İlk kural testi 50/50. İlk sahne testinde hızlı misketlerin gelecek karşılaşmaya erken ulaştığı görüldü; başlangıç mesafesi hız faktörüne göre düzeltildi. Son test/Android/teslim kanıtları daha sonra eklenecek. Notion ücretsiz blok kotası için otomatik yeniden deneme yapılmadı; bu kayıt henüz Notion'a gönderilmedi.

### v0.9 doğrulama checkpoint
112/112 test, 13 gerçek Unity görüntüsü ve model denetimi tamamlandı. Eksik mesh/malzeme/shader hatası yok. Sekiz aile + kurbağa/ördek ve bant/kart varyantları, tünel uçuş açıklığı, misket başlangıç mesafesi düzeltmesi. APK/GitHub teslimi sırada. Bu kayıt Notion'a henüz eşitlenmedi.

### v0.9 Android doğrulandı
Kaynak b656017cb48c7b10b62891647135d0bc2371cfde; ARM64 IL2CPP 0.9.0/kod10, SHA256 4cb72fc735be4f5dc6300eece1d4cac34f99da589ec7d4e05c97e11a883726ba. V2 imza/aynı sertifika, ZIP/ELF16KB, yalnız INTERNET geçti. Fiziksel tablet kabulü bekler. GitHub Release bağlantısı https://github.com/brscncn1/Brake-Toy/releases/tag/v0.9.0 . Yeni Notion blok isteği yapılmadı.

### v0.9 teslim kaydı — 24 Ekim 2026
PR https://github.com/brscncn1/Brake-Toy/pull/9 ; Release https://github.com/brscncn1/Brake-Toy/releases/tag/v0.9.0 ; main README güncel. APK asset digest/byte eşleşmesi API üzerinden doğrulandı. 112/112 test, 13 gerçek render, 24 dakika güvenli yol simülasyonu. PC Adventure Play menüsü hazır; tablette yeni fiziksel kabul yapılmadı. Ayrıntılar DELIVERY-v09.json / MILESTONE-v09.md. Bu yeni kayıtlar Notion'a gönderilmedi; bilinen ücretsiz blok kotası devam ediyor.

## 24 Ekim — agent rehberleri / kısmi eşitleme doğrulandı
AGENTS/YOL_HARITASI/MIMARI/MEKANIKLER/CLAUDE/KARARLAR/DOGRULAMA GitHub'da tam metin olarak hazır. Mevcut Notion merkezi, dört belge özeti ve günlük güncellendi ve geri okundu; eski33 alt sayfa korundu. Yedi yeni tam belge sayfası oluşturma isteği entitlement_required/block_creation nedeniyle reddedildi; yan etki yok, sıfır yeni sayfa, otomatik tekrar yok. Tam kopyalar kota çözülene kadar bekler; mevcut Notion sayfaları güncel kaynak dosyalarına yönlendirir. Docs/NOTION_SYNC-agent-handbook.json gerçek kapsamı kaydeder. Önceki v0.8–v0.9 tam rapor eşitlemeleri de tümü tamamlandı sayılmaz. Kullanıcı adına ücretli plan alınmadı.
