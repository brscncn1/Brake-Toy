# v0.8 — güncel kullanım

Unity: Tools → Brake Toy → Open Playground v0.8 → Play. Sürüşte mouse/parmak sürüklenir. Yeşil rota güvenli geçit, altın rota isteğe bağlı risk/ödül; kitabın kendi yeşil yaklaşma oku rampayı gösterir. Rampa sol/orta/sağ konumda gelebilir. Kenardan teker teması da yükseltir; yandan çıkınca kısa düşüş uygulanır. Havada kazanılan TP bölümü 2x.

- YAVAŞLAT: 3 aktif oyun saniyesi; dünya yaklaşık %32 hızda, direksiyon normal.
- ROKET: 3 saniye; 1.85x'e çıkan yumuşak hızlanma ve engel temasına koruma.
- JETPACK: 5 saniye; yumuşak kalkış/iniş. İniş/roket bitişinde kısa güvenlik payı.
- İlk v0.8 kariyerine her birinden üç hak. Tüketim kalıcı, retry yenilemez. Aynı anda bir güç, paused/menüde kullanılamaz. Güç kullanımı aktif sürüklemeyi kesmez.
- ANA MENÜ → GÜÇ HAKLARI: bir kullanım 350/500/650 TP. Gerçek para, coin veya mağaza ödeme akışı yok. Yetersiz TP'de ücret alınmaz. Kayıt hatasında işlem geri alınır.
- Güç kullanılan tur etiketlenir; aynı parkur ghost'u gizlenir ve yeni ghost yazılmaz. Yerel puan/TP kazanımı sürer; online kapalıdır.
- AYARLAR → MASA: KLASİK ile yeni bölge dekorları kapanır. DOMİNO: KAPALI ayrı çalışır ve mevcut domino bariyerini kaldırır. İkisini bağımsız deneyebilirsin. Müzik değişmedi.
- GÖREVLER: görsel kartlar ilerleme/TP gösterir; GÖREV / ROZET ile 15 başarım üçlü sayfalanır.

APK'yı önceki oyunu kaldırmadan güncelle; paket com.braketoy.prototype, sürüm0.8.0/kod8. TP ve açık araçlar taşınır; yeni rekor/kariyer/ghost ayrıdır. Eski v0.7 sahnesi korunur. Aşağıdaki önceki kullanım notları tarihseldir; v0.8 farkları yukarıdaki gibidir.

# v0.7 güncel kullanım

Tools → Brake Toy → Open Joyful Room v0.7 → Play. Arcade 14→30, Relax 9→14. Yeşil ok rampadır; üzerinden geçince 1.65 sn uçuş ve havada kazanılan TP bölümüne 2x. İnişte çarpışma devam eder. Motor uğultu/lastik, Efektler çarpma/korna/yıldız, Müzik menü ve yarış kanalını yönetir. Yeni sürüm eski TP/açılmış araçları taşır; yeni rekor ve ghost ayrı tutulur. Aşağıdaki önceki sürüm belgesi tarihsel ayrıntıdır; v0.7 farklılıkları bu paragraf ve MILESTONE-v07.md ile üstüne yazılır.

# Brake Toy — revizyon adayı oyuncu el kitabı

Unity: Tools → Brake Toy → Open Review Candidate v0.6; Play. Android: önceki oyunu kaldırmadan yeni APK ile güncelle. Kaldırma yerel ilerlemeyi silebilir.

- ARCADE: tek çarpışma turu bitirir. Parmağını ekranda sürükle; bıraktığında yatay konum korunur. Yakın geçiş ve risk yıldızları stil kazandırır.
- RELAX: 3 hak, 7→10 hız, daha seyrek engel. Arcade rekoruna ve küresel sıralamaya yazmaz.
- GÜNLÜK ANTRENMAN: aynı UTC günü aynı başlangıç seed'i. Bu pakette cihaz tarihi kullanılır; doğrulanmış çevrimiçi günlük yarış değildir.
- GARAJ: sağ/sol ile 10 aracı incele, döndür, Toy Points ile aç ve seç. Bütün araçların hızı/kontrolü/collider'ı aynıdır. İz rengi ve korna da TP ile alınır; zaten sahip olduğun seçimden tekrar ücret alınmaz.
- GÖREVLER: tur başına üç kısa hedef; bitişte TP. 15 başarım bir kez ödül verir. Günlük giriş zorunluluğu yoktur.
- AYNI PARKUR + GHOST: son tamamlanan aynı mod/seed kaydı varsa kişisel yarı saydam hayalet. Yeni rastgele parkurun hayaleti yoktur; ghost çarpışmaz ve puan vermez. Her modda son parkur için en iyi kayıt tutulur, en fazla 10 dakika.
- AYARLAR: ghost, daha büyük yazı, motor/efekt/genel ses, yüksek/ekonomik grafik profili. Ölçüm düğmesi gerçek sürüşte 60 saniye FPS/p95 toplar ve yalnızca cihazda kaydeder. APK'nın sürümüne göre dosya adı değişir.
- Mola: duraklat, devam et, ayarlara git veya turu bitir. Uygulama odağı kaybedildiğinde sürüş duraklar; dönüşte DEVAM ET gerekir.
- SIRALAMA: yerel rekorlar her zaman kullanılabilir. Online düğmeleri bu pakette kapalı bağlantı durumunu bildirir. Üretim servisi etkinleştirilmeden küresel skor gönderilmez.

TP harcamak rekorları düşürmez. Kariyer checksum/yedekli yerel dosyadadır; bu koruma küresel hile önleme veya bulut yedekleme değildir. Save hatasında ekranda uyarı görünür. Oyun silinirse veya başka cihaza geçilirse kayıt aktarımı garanti edilmez.
