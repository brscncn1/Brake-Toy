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
